using System.Collections.Generic;
using System.Drawing;

namespace kulerIslands
{
    // each tri is made of 3 points with 3 indices
    class Tri
    {
        // UV coordinates
        public PointF posA { get; set; }
        public PointF posB { get; set; }
        public PointF posC { get; set; }

        // indices
        public int indexA { get; set; }
        public int indexB { get; set; }
        public int indexC { get; set; }

        // Pixel coordinates
        //public PointF pixelA { get; set; }
        //public PointF pixelB { get; set; }
        //public PointF pixelC { get; set; }
    }

    class Island
    {
        // a list containing all the tri's which make a complete UV island
        public List<Tri> TriList { get; set; }

        public Island()
        {
            TriList = new List<Tri>();
        }
    }

    // contains data from file
    class ModelAsset
    {
        // stores filepath from which the data came from
        public string SourceFile { get; set; }

        // list of tris in file
        public List<Tri> TriList { get; set; }

        // list containing sublist of islands
        public List<Island> Islands { get; set; }

        // the number of vertices in the object
        public int numberOfVerts { get; set; }

        // init
        public ModelAsset()
        {
            TriList = new List<Tri>();
            Islands = new List<Island>();
        }
    }
}