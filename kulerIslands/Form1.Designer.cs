namespace kulerIslands
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.uiResolution = new System.Windows.Forms.ComboBox();
            this.uiPadding = new System.Windows.Forms.NumericUpDown();
            this.uiGenerate = new System.Windows.Forms.Button();
            this.uiColoring = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uiFileList = new System.Windows.Forms.ListBox();
            this.rcMenuFileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.uiWireframe = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.uiWireframeThickness = new System.Windows.Forms.NumericUpDown();
            this.ttGenerate = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.uiPadding)).BeginInit();
            this.rcMenuFileList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiWireframeThickness)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiResolution
            // 
            this.uiResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiResolution.FormattingEnabled = true;
            this.uiResolution.Items.AddRange(new object[] {
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096"});
            this.uiResolution.Location = new System.Drawing.Point(95, 31);
            this.uiResolution.Name = "uiResolution";
            this.uiResolution.Size = new System.Drawing.Size(181, 21);
            this.uiResolution.TabIndex = 0;
            // 
            // uiPadding
            // 
            this.uiPadding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiPadding.Location = new System.Drawing.Point(95, 85);
            this.uiPadding.Name = "uiPadding";
            this.uiPadding.Size = new System.Drawing.Size(181, 20);
            this.uiPadding.TabIndex = 1;
            this.uiPadding.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // uiGenerate
            // 
            this.uiGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGenerate.Location = new System.Drawing.Point(10, 361);
            this.uiGenerate.Name = "uiGenerate";
            this.uiGenerate.Size = new System.Drawing.Size(265, 35);
            this.uiGenerate.TabIndex = 2;
            this.uiGenerate.Text = "Generate";
            this.ttGenerate.SetToolTip(this.uiGenerate, "- [Clicked] will generate all UVMaps for each OBJ file in the list.\r\n- UVMaps are" +
        " saved in the same location as the source OBJ.\r\n- All maps are saved as PNG form" +
        "at.\r\n");
            this.uiGenerate.UseVisualStyleBackColor = true;
            this.uiGenerate.Click += new System.EventHandler(this.uiGenerate_Click);
            // 
            // uiColoring
            // 
            this.uiColoring.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiColoring.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiColoring.FormattingEnabled = true;
            this.uiColoring.Items.AddRange(new object[] {
            "Random",
            "RGB",
            "White"});
            this.uiColoring.Location = new System.Drawing.Point(95, 58);
            this.uiColoring.Name = "uiColoring";
            this.uiColoring.Size = new System.Drawing.Size(181, 21);
            this.uiColoring.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Resolution:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Island Colors:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Padding:";
            // 
            // uiFileList
            // 
            this.uiFileList.AllowDrop = true;
            this.uiFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiFileList.ContextMenuStrip = this.rcMenuFileList;
            this.uiFileList.FormattingEnabled = true;
            this.uiFileList.Location = new System.Drawing.Point(10, 170);
            this.uiFileList.Name = "uiFileList";
            this.uiFileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.uiFileList.Size = new System.Drawing.Size(265, 186);
            this.uiFileList.TabIndex = 4;
            this.uiFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.uiFileList_DragDrop);
            this.uiFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.uiFileList_DragEnter);
            // 
            // rcMenuFileList
            // 
            this.rcMenuFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.rcMenuFileList.Name = "rcMenuFileList";
            this.rcMenuFileList.Size = new System.Drawing.Size(108, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Wireframe:";
            // 
            // uiWireframe
            // 
            this.uiWireframe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiWireframe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiWireframe.FormattingEnabled = true;
            this.uiWireframe.Items.AddRange(new object[] {
            "None",
            "White",
            "Black"});
            this.uiWireframe.Location = new System.Drawing.Point(95, 111);
            this.uiWireframe.Name = "uiWireframe";
            this.uiWireframe.Size = new System.Drawing.Size(181, 21);
            this.uiWireframe.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Thickness:";
            // 
            // uiWireframeThickness
            // 
            this.uiWireframeThickness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiWireframeThickness.DecimalPlaces = 1;
            this.uiWireframeThickness.Location = new System.Drawing.Point(95, 138);
            this.uiWireframeThickness.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.uiWireframeThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uiWireframeThickness.Name = "uiWireframeThickness";
            this.uiWireframeThickness.Size = new System.Drawing.Size(181, 20);
            this.uiWireframeThickness.TabIndex = 1;
            this.uiWireframeThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(284, 403);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.uiFileList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiGenerate);
            this.Controls.Add(this.uiWireframeThickness);
            this.Controls.Add(this.uiPadding);
            this.Controls.Add(this.uiWireframe);
            this.Controls.Add(this.uiColoring);
            this.Controls.Add(this.uiResolution);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kuler Islands";
            ((System.ComponentModel.ISupportInitialize)(this.uiPadding)).EndInit();
            this.rcMenuFileList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiWireframeThickness)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox uiResolution;
        private System.Windows.Forms.NumericUpDown uiPadding;
        private System.Windows.Forms.Button uiGenerate;
        private System.Windows.Forms.ComboBox uiColoring;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox uiFileList;
        private System.Windows.Forms.ContextMenuStrip rcMenuFileList;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox uiWireframe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown uiWireframeThickness;
        private System.Windows.Forms.ToolTip ttGenerate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

