using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

/* 
    :to do:
*/

namespace kulerIslands
{
    class Processing
    {
        // Used to generate random values
        private static readonly Random seed = new Random();

        // Used to validate supported file formats
        public static bool IsFormatOBJ(string filepath)
        {
            return (filepath.ToLower().EndsWith(".obj"));
        }

        // returns asset with tri list contain vert positions and corresponding indices
        public static ModelAsset ReadFileOBJ(string filepath = "")
        {
            // complete asset
            ModelAsset newAsset = new ModelAsset();
            // list used to store position data from OBJ file
            List<PointF> positionList = new List<PointF>();
            // list used to store position indices from OBJ file
            List<List<int>> indicesList = new List<List<int>>();

            try
            {
                // Read each line of the file into a string array. Each element of the array is one line of the file. 
                string[] lines = System.IO.File.ReadAllLines(filepath);

                // OBJ format: only collect valid UV Verts from lines which start with vt
                foreach (string line in lines)
                {
                    // TVerts - positions
                    if (line.StartsWith("vt"))
                    {
                        /*  Example Lines
                            3dsMax: vt 0.0854 0.0854 0.0000
                            Maya: vt 0.0854 0.0854 0.0000
                        */

                        // remove leading 'vt' text from each line of the file
                        char[] invalidChars = { 'v', 't', ' ' };
                        string pointString = line.TrimStart(invalidChars);

                        // remove any leading or trailing spaces
                        pointString = pointString.Trim();

                        // split each pt into it's respected parts X,Y,Z
                        string[] parts = pointString.Split(' ');

                        if (parts.Length >= 2)
                        {
                            float X = Convert.ToSingle(parts[0]);
                            float Y = Convert.ToSingle(parts[1]);
                            PointF pt = new PointF(X, Y);
                            positionList.Add(pt);
                        }
                    }
                    // TVert Indices
                    if (line.StartsWith("f "))
                    {
                        /*  Example Lines
                            3dsMax: f 1/1 2/2 3/3 4/4 
                            Maya: f 1/1/1 2/2/2 3/3/3 4/4/4
                        */

                        // remove leading 'f' text from each line of the file
                        char[] invalidChars = { 'f', ' ' };
                        string pointString = line.TrimStart(invalidChars);

                        // remove any leading or trailing spaces
                        pointString = pointString.Trim();

                        // split each pt into it's respected parts A,B,C or A,B,C,D
                        string[] parts = pointString.Split(' ');

                        if (parts.Length >= 2)
                        {
                            // list containing indices collect from line of file
                            List<int> indices = new List<int>();

                            foreach (string p in parts)
                            {
                                string[] subParts = p.Split('/');
                                // the second value in the 1/1/1 is the vertex id
                                int id = Convert.ToInt32(subParts[1]);
                                indices.Add(id);
                            }

                            // generate Tri object for each tri in faces
                            // Important: offset the indices by '-1' since arrays start at 0 but indices in the OBJ files start at 1
                            // Matt@boomerlabs:: you should really do a -1 here and not bother later
                            switch (indices.Count)
                            {
                                case 3: // single tri face
                                    Tri triA = new Tri();
                                    triA.indexA = indices[0] - 1;
                                    triA.indexB = indices[1] - 1;
                                    triA.indexC = indices[2] - 1;
                                    newAsset.TriList.Add(triA);
                                    break;
                                case 4: // quad face made of two tris
                                    Tri triB = new Tri();
                                    triB.indexA = indices[0] - 1;
                                    triB.indexB = indices[1] - 1;
                                    triB.indexC = indices[2] - 1;
                                    newAsset.TriList.Add(triB);
                                    Tri triC = new Tri();
                                    triC.indexA = indices[2] - 1;
                                    triC.indexB = indices[3] - 1;
                                    triC.indexC = indices[0] - 1;
                                    newAsset.TriList.Add(triC);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException) { }

            // if UV coordinate data exists go through and assign positions to the Tri's
            if (positionList.Count >= 3)
            {
                foreach (var t in newAsset.TriList)
                {
                    t.posA = positionList[t.indexA];
                    t.posB = positionList[t.indexB];
                    t.posC = positionList[t.indexC];
                }
            }

            // Store the number of verts
            newAsset.numberOfVerts = positionList.Count();

            return newAsset;
        }

        public static PointF ConvertUVSpaceToPixels(PointF pt, int resolution, int padding)
        {
            //Calculate the X,Y positiosn by converting UV space 0-1 to pixel resolution
            float xPixel = pt.X * resolution;
            float yPixel = pt.Y * resolution;
            return (new PointF(xPixel, yPixel));
        }

        public static Color GetRandomColor(string method = "Random")
        {
            switch (method)
            {
                case "Random":
                    {
                        return (Color.FromArgb(seed.Next(0, 255), seed.Next(0, 255), seed.Next(0, 255)));
                    }
                case "RGB":
                    {
                        int id = seed.Next(0, 3);
                        return (new List<Color> { Color.Red, Color.Green, Color.Blue })[id];
                    }
                default:
                    {
                        return (Color.White);
                    }
            }
        }

        // ********
        //
        // Routine:     void FindIslands(List<ModelAssets> Assets)
        // Author:      Mathew Kaustinen, Boomer Labs LLC (matt@boomerlabs.com)
        // Purpose:     Take the list of tris (faces) containing vertices. Create a unique "island" for each group of non-contiguous vertices
        // 
        // ********
        public static void FindIslands(List<ModelAsset> Assets)
        {

            int[] foundIsland = new int[3];              // where we store shared island values (up to 3) for a given tri

            foreach (ModelAsset asset in Assets)
            {
                // create an array for all of the verts, each entry indicates which island the vert is part of
                int vert_array_size = asset.numberOfVerts; // fix this later, should store verts in the face array 0 indexed, see comment in ReadObjFile
                int[] vert_array = new int[vert_array_size];

                // Set to -1 to indicate that each vertex is not yet part of any island
                for (int i = 0; i < vert_array_size; i++)
                {
                    vert_array[i] = -1;
                }

                // Take a look at each tri (face) and see if any of its vertices are part of an existing island
                // Note that each vertex could be part of a different island, hence all of the extra work to reconcile tris
                // that are part of mulitple islands (that will need collapsing)
                foreach (Tri tri in asset.TriList)
                {
                    // store the 3 indexes in question for tri (ie. face)
                    int vert1 = tri.indexA;
                    int vert2 = tri.indexB;
                    int vert3 = tri.indexC;

                    int foundCount = 0;     // how many island matches have we found (0 to 3)
                    int vertIsland1 = -1;   // The associated island for each vertex
                    int vertIsland2 = -1;
                    int vertIsland3 = -1;

                    // Check to see if the vertex is already part of an island, do this 3 times (once for each vert)
                    // We know if a vert is part of an island if the vert_array value is not -1
                    if (vert_array[vert1] >= 0)
                    {
                        vertIsland1 = vert_array[vert1];
                        foundCount = 1;
                        foundIsland[0] = vert_array[vert1];
                    }

                    if (vert_array[vert2] >= 0)
                    {
                        vertIsland2 = vert_array[vert2];
                        if (vertIsland2 != vertIsland1)
                        {
                            foundIsland[foundCount] = vert_array[vert2];
                            foundCount++;
                        }
                    }

                    if (vert_array[vert3] >= 0)
                    {
                        vertIsland3 = vert_array[vert3];
                        if ((vertIsland3 != vertIsland1) && (vertIsland3 != vertIsland2))
                        {
                            foundIsland[foundCount] = vert_array[vert3];
                            foundCount++;
                        }
                    }

                    // If we didn't find any islands, then create a new one at the end of the Island List
                    if (foundCount == 0)
                    {
                        int islandCount = asset.Islands.Count();
                        vert_array[vert1] = islandCount;
                        vert_array[vert2] = islandCount;
                        vert_array[vert3] = islandCount;

                        Island newIsland = new Island();
                        newIsland.TriList.Add(tri);
                        // add new island to asset
                        asset.Islands.Add(newIsland);
                    }

                    // Otherwise we need to use (and possibly collapse) islands
                    else
                    {
                        // Easy case, only 1 island is found, so use that for all 3 vertex
                        if (foundCount == 1)
                        {
                            // Set all 3 verts to the found island value
                            vert_array[vert1] = foundIsland[0];
                            vert_array[vert2] = foundIsland[0];
                            vert_array[vert3] = foundIsland[0];

                            // Add the tri to the Island Array
                            asset.Islands[foundIsland[0]].TriList.Add(tri);
                        }
                        else if (foundCount == 2)
                        {
                            // find the lowest value, use that island, prepare the other one for removal
                            int indexToUse = foundIsland[0];
                            int indexToRemove = foundIsland[1];
                            if (indexToRemove < indexToUse)
                            {
                                int temp = indexToUse;
                                indexToUse = indexToRemove;
                                indexToRemove = temp;
                            }
                            // move all of tris that are part of the higher numbered island to the lower
                            foreach (Tri removetri in asset.Islands[indexToRemove].TriList)
                            {
                                asset.Islands[indexToUse].TriList.Add(removetri);
                            }
                            // since we've moved all of these tris, zero the list for the old island
                            // Eventually we'll probably want to remove the island all together
                            asset.Islands[indexToRemove].TriList.Clear();

                            // replace all references to the remove values
                            for (int i = 0; i < vert_array_size; i++)
                            {
                                if (vert_array[i] == indexToRemove)
                                    vert_array[i] = indexToUse;
                            }

                            // OK, now write the tri that we're working on originally
                            vert_array[vert1] = indexToUse;
                            vert_array[vert2] = indexToUse;
                            vert_array[vert3] = indexToUse;

                            asset.Islands[indexToUse].TriList.Add(tri);
                        }
                        else // rare case where all 3 sides are from different islands
                        {
                            int indexToUse;
                            int indexToRemove1;
                            int indexToRemove2;

                            // Find the lowest value
                            if ((foundIsland[0] < foundIsland[1]) && (foundIsland[0] < foundIsland[2]))
                            {
                                indexToUse = foundIsland[0];
                                indexToRemove1 = foundIsland[1];
                                indexToRemove2 = foundIsland[2];
                            }
                            else if ((foundIsland[1] < foundIsland[0]) && (foundIsland[1] < foundIsland[2]))
                            {
                                indexToUse = foundIsland[1];
                                indexToRemove1 = foundIsland[0];
                                indexToRemove2 = foundIsland[2];
                            }
                            else
                            {
                                indexToUse = foundIsland[2];
                                indexToRemove1 = foundIsland[0];
                                indexToRemove2 = foundIsland[1];

                            }

                            // move all of tris that are part of the higher numbered island to the lower
                            foreach (Tri removetri in asset.Islands[indexToRemove1].TriList)
                            {
                                asset.Islands[indexToUse].TriList.Add(removetri);
                            }

                            // and again for the third island
                            foreach (Tri removetri in asset.Islands[indexToRemove2].TriList)
                            {
                                asset.Islands[indexToUse].TriList.Add(removetri);
                            }

                            // since we've moved all of these tris, zero the list for the old island
                            // Eventually we'll probably want to remove the island all together
                            asset.Islands[indexToRemove1].TriList.Clear();
                            asset.Islands[indexToRemove2].TriList.Clear();
                            //asset.Islands.RemoveAt(indextoRemove);    // don't do this now, will mess up island numbering

                            // replace all references to the remove values
                            for (int i = 0; i < vert_array_size; i++)
                            {
                                if ((vert_array[i] == indexToRemove1) || (vert_array[i] == indexToRemove2))
                                    vert_array[i] = indexToUse;
                            }

                            // OK, now write the tri that we're working on originally
                            vert_array[vert1] = indexToUse;
                            vert_array[vert2] = indexToUse;
                            vert_array[vert3] = indexToUse;

                            asset.Islands[indexToUse].TriList.Add(tri);
                        }
                    }
                }
                // Clean up by removing Islands that have no tris, but do this in reverse order (for both speed and stability)
                for (int i = asset.Islands.Count() - 1; i >= 0; i--)
                {
                    if (asset.Islands[i].TriList.Count() == 0)
                    {
                        asset.Islands.RemoveAt(i);
                    }
                }
            }
        }

        public static void GenerateKulerIslandMap(List<String> files = null, int resolution = 1024, int padding = 0, string colorize = "Random", string wireframe = "None", float wirethickness = 2F)
        {
            /*  Process Walkthrough
                01: - collect all vertex data from each OBJ file
                02: - sort tris into islands for colorizing
                03: - create bitmaps
            */

            if (files == null || files.Count() == 0) return;

            // list containg all the file assets collected
            List<ModelAsset> Assets = new List<ModelAsset>();

            /* 01 */
            // loop through each file and collect the vertex data
            foreach (string filepath in files)
            {
                // get data from file
                if (filepath.ToLower().EndsWith(".obj"))
                {
                    ModelAsset newAsset = ReadFileOBJ(filepath: filepath);
                    newAsset.SourceFile = filepath;
                    Assets.Add(newAsset);
                    Console.WriteLine("Completed Reading OBJ File - {0}", filepath);
                }
            }

            /* 02 */
            // loop through each assets collected data and sort into UV lands
            // Matt's Algorithm 
            FindIslands(Assets);

            /* 03 */
            // eventually condense into foreach loop above - john
            // Next Generate the actual bitmaps
            foreach (ModelAsset asset in Assets)
            {
                //Create bitmap with padding and crop afterwards to avoid pixels being placed out of place
                Bitmap bitmap = new Bitmap(Convert.ToInt32(resolution), Convert.ToInt32(resolution), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bitmap);
                // make optional to use Alias or not Aliased
                // Set the SmoothingMode property to smooth the line.
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // flip image on the y. UV space in 3d apps [0,0] starts in the bottom/left, images start start top/left
                g.ScaleTransform(1.0F, -1.0F);
                // Translate the drawing area accordingly
                g.TranslateTransform(0.0F, -(float)resolution);
                // clear canvas background to be black
                g.Clear(Color.Black);

                // color each island based on user settings
                for (int i = 0; i < asset.Islands.Count; i++)
                {
                    var island = asset.Islands[i];

                    // Create solid brush color based on user settings
                    Color col = GetRandomColor(colorize);
                    SolidBrush brush = new SolidBrush(col);

                    // draw the original fill color
                    foreach (Tri tri in island.TriList)
                    {
                        PointF A = ConvertUVSpaceToPixels(tri.posA, resolution, padding);
                        PointF B = ConvertUVSpaceToPixels(tri.posB, resolution, padding);
                        PointF C = ConvertUVSpaceToPixels(tri.posC, resolution, padding);
                        PointF[] Triangle = { A, B, C };
                        g.FillPolygon(brush, Triangle);

                        if (padding != 0)
                        {
                            Pen penPadding = new Pen(brush);
                            penPadding.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            penPadding.Width = padding;
                            g.DrawPolygon(penPadding, Triangle);
                            penPadding.Dispose();
                        }
                    }
                    // if we don't draw wire after the fill color it will appear broken
                    if (wireframe != "None")
                    {
                        foreach (Tri tri in island.TriList)
                        {
                            PointF A = ConvertUVSpaceToPixels(tri.posA, resolution, padding);
                            PointF B = ConvertUVSpaceToPixels(tri.posB, resolution, padding);
                            PointF C = ConvertUVSpaceToPixels(tri.posC, resolution, padding);
                            PointF[] Triangle = { A, B, C };

                            Pen penWire = wireframe == "White" ? new Pen(Brushes.White) : new Pen(Brushes.Black);
                            penWire.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            penWire.Width = wirethickness;
                            g.DrawPolygon(penWire, Triangle);
                            penWire.Dispose();
                        }
                    }
                }
                // save bitmap
                var bmpFilepath = Path.ChangeExtension(asset.SourceFile, "_UVMap_KI.png");
                string filepath = bmpFilepath;
                bitmap.Save(filepath, ImageFormat.Png);
                Console.WriteLine("Completed Image Creation - {0}", filepath);
            }
        }
    }
}
