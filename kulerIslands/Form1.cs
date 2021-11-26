using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace kulerIslands
{
    public partial class Form1 : Form
    {
        // Public Variables
        // contains a list of the files placed into the UI
        static List<string> files = new List<string>();

        public Form1()
        {
            InitializeComponent();

            // set defaults at startup
            uiResolution.SelectedIndex = 3;
            uiColoring.SelectedIndex = 0;
            uiWireframe.SelectedIndex = 0;

            //TestSetup(); // omit on release - dev only
        }

        void TestSetup()
        {
            //files.Clear();

            // Testing files / Omit on release
            //string fileA = @"..\..\..\testing\plane_4Polys.obj";
            //string fileB = @"..\..\..\testing\plane_4PolysSubdivs1.obj";
            //string fileC = @"..\..\..\testing\plane_4PolysSubdivs2.obj";
            //string fileD = @"..\..\..\testing\teapot_regular.obj";
            //string fileE = @"..\..\..\testing\teapot_subdivs1.obj";
            //string fileF = @"..\..\..\testing\teapot_subdivs2.obj";
            //string fileG = @"..\..\..\testing\plane_TriPolys.obj";

            //files.Add(fileA);
            //files.Add(fileB);
            //files.Add(fileC);
            //files.Add(fileD);
            //files.Add(fileE);
            //files.Add(fileF);
            //files.Add(fileG);

            //UpdateUI();
        }

        void UpdateUI()
        {
            // sort alphabetical for cleaner GUI display
            files.Sort((x, y) => string.Compare(x, y));
            uiFileList.Items.Clear();
            uiFileList.Items.AddRange(files.ToArray());
        }

        public void removeSelectedFiles()
        {
            // removes selected files from list
            if (uiFileList.SelectedIndices.Count > 0)
            {
                var selectedItems = uiFileList.SelectedIndices;

                if (selectedItems.Count >= 1)
                {
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        int index = selectedItems[i];
                        files.RemoveAt(index);
                        Console.WriteLine(index);
                    }
                }
                UpdateUI();
            }
        }

        public void clearFiles()
        {
            // clears files from list
            files.Clear();
            UpdateUI();
        }

        public void uiGenerate_Click(object sender, EventArgs e)
        {
            if (files.Count == 0)
            {
                string warning = "There are no files for processing.\nPlease Drag-N-Drop files into list before continuing.";
                MessageBox.Show(warning, "Kuler Islands", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            //omit time process
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int res = Convert.ToInt32(uiResolution.Text.ToString());
            int pad = Convert.ToInt32(uiPadding.Value);
            string color = uiColoring.Text.ToString();
            string wireframe = uiWireframe.Text.ToString();
            float thickness = Convert.ToSingle(uiWireframeThickness.Value);

            Processing.GenerateKulerIslandMap(files: files, resolution: res, padding: pad, colorize: color, wireframe: wireframe, wirethickness: thickness);
            string msgComplete = "Process finished!\n" + files.Count.ToString() + " files created successfully.";
            MessageBox.Show(msgComplete, "Kuler Islands");

            // omit time process
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            Console.WriteLine(ts);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeSelectedFiles();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearFiles();
        }

        private void uiFileList_DragDrop(object sender, DragEventArgs e)
        {
            string[] filesDropped = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filesDropped.Length >= 1)
            {
                foreach (var f in filesDropped)
                {
                    if (Processing.IsFormatOBJ(f))
                    {
                        files.Add(f);
                    }
                }
            }
            UpdateUI();
        }

        private void uiFileList_DragEnter(object sender, DragEventArgs e)
        {
            // check files being added to make sure they are supported image formats
            DragDropEffects effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (File.Exists(path))
                {
                    effects = DragDropEffects.Copy;
                }
            }
            e.Effect = effects;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kuler Islands was developed by: \nJohn Martini - https://www.JokerMartini.com/ \nMathew Kaustinen - https://www.boomerlabs.com/.\n\nSource Code: https://github.com/JokerMartini/KulerIslands",
            "About");
        }

        private void uiClearButton_Click(object sender, EventArgs e)
        {
            clearFiles();
        }

        private void uiRemoveButton_Click(object sender, EventArgs e)
        {
            removeSelectedFiles();
        }

        private void uiAppendButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse Files";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.DefaultExt = "obj";
            openFileDialog1.Filter = "3D Model Files (*.obj)|*.obj|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (String f in openFileDialog1.FileNames)
                {
                    files.Add(f);
                }
                UpdateUI();
            }
        }
    }
}
