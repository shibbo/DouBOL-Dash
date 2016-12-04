namespace DouBOLDash
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BMDViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respawnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respawnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.loadingLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectionInfo = new System.Windows.Forms.ToolStripLabel();
            this.glControl1 = new OpenTK.GLControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.enemyRouteList = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chckList = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.routeList = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.objList = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.kartPointList = new System.Windows.Forms.ListBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.areaList = new System.Windows.Forms.ListBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.cameraList = new System.Windows.Forms.ListBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.respList = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1057, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFolderToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.BMDViewerToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // chooseFolderToolStripMenuItem
            // 
            this.chooseFolderToolStripMenuItem.Name = "chooseFolderToolStripMenuItem";
            this.chooseFolderToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.chooseFolderToolStripMenuItem.Text = "Choose folder";
            this.chooseFolderToolStripMenuItem.Click += new System.EventHandler(this.chooseFolderToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // BMDViewerToolStripMenuItem
            // 
            this.BMDViewerToolStripMenuItem.Name = "BMDViewerToolStripMenuItem";
            this.BMDViewerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.BMDViewerToolStripMenuItem.Text = "BMD Viewer";
            this.BMDViewerToolStripMenuItem.Click += new System.EventHandler(this.BMDViewerToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.respawnToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // respawnToolStripMenuItem
            // 
            this.respawnToolStripMenuItem.Name = "respawnToolStripMenuItem";
            this.respawnToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.respawnToolStripMenuItem.Text = "Respawn";
            this.respawnToolStripMenuItem.Click += new System.EventHandler(this.respawnToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.respawnToolStripMenuItem1});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // respawnToolStripMenuItem1
            // 
            this.respawnToolStripMenuItem1.Name = "respawnToolStripMenuItem1";
            this.respawnToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.respawnToolStripMenuItem1.Text = "Respawn";
            this.respawnToolStripMenuItem1.Click += new System.EventHandler(this.respawnToolStripMenuItem1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadingLabel,
            this.toolStripSeparator1,
            this.selectionInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 513);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1057, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // loadingLabel
            // 
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(42, 22);
            this.loadingLabel.Text = "Ready!";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // selectionInfo
            // 
            this.selectionInfo.Name = "selectionInfo";
            this.selectionInfo.Size = new System.Drawing.Size(100, 22);
            this.selectionInfo.Text = "Nothing selected.";
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(0, 16);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(677, 469);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.glControl1);
            this.panel1.Location = new System.Drawing.Point(368, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 489);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 483);
            this.panel2.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(59, 19);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 483);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.enemyRouteList);
            this.tabPage1.ImageKey = "enemyitemp.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.ToolTipText = "Enemy / Item Routes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // enemyRouteList
            // 
            this.enemyRouteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enemyRouteList.FormattingEnabled = true;
            this.enemyRouteList.Location = new System.Drawing.Point(3, 3);
            this.enemyRouteList.Name = "enemyRouteList";
            this.enemyRouteList.Size = new System.Drawing.Size(356, 450);
            this.enemyRouteList.TabIndex = 0;
            this.enemyRouteList.SelectedIndexChanged += new System.EventHandler(this.enemyRouteList_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chckList);
            this.tabPage2.ImageKey = "cp.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.ToolTipText = "Checkpoints";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chckList
            // 
            this.chckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chckList.FormattingEnabled = true;
            this.chckList.Location = new System.Drawing.Point(3, 3);
            this.chckList.Name = "chckList";
            this.chckList.Size = new System.Drawing.Size(356, 450);
            this.chckList.TabIndex = 0;
            this.chckList.SelectedIndexChanged += new System.EventHandler(this.chckList_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.routeList);
            this.tabPage3.ImageKey = "paths.png";
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(362, 456);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.ToolTipText = "Routes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // routeList
            // 
            this.routeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routeList.FormattingEnabled = true;
            this.routeList.Location = new System.Drawing.Point(3, 3);
            this.routeList.Name = "routeList";
            this.routeList.Size = new System.Drawing.Size(356, 450);
            this.routeList.TabIndex = 0;
            this.routeList.SelectedIndexChanged += new System.EventHandler(this.routeList_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.objList);
            this.tabPage4.ImageKey = "objects.png";
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(362, 456);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.ToolTipText = "Objects";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // objList
            // 
            this.objList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objList.FormattingEnabled = true;
            this.objList.Location = new System.Drawing.Point(3, 3);
            this.objList.Name = "objList";
            this.objList.Size = new System.Drawing.Size(356, 450);
            this.objList.TabIndex = 0;
            this.objList.SelectedIndexChanged += new System.EventHandler(this.objList_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.kartPointList);
            this.tabPage5.ImageKey = "startungpos.png";
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(362, 456);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.ToolTipText = "Starting Points";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // kartPointList
            // 
            this.kartPointList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kartPointList.FormattingEnabled = true;
            this.kartPointList.Location = new System.Drawing.Point(3, 3);
            this.kartPointList.Name = "kartPointList";
            this.kartPointList.Size = new System.Drawing.Size(356, 450);
            this.kartPointList.TabIndex = 0;
            this.kartPointList.SelectedIndexChanged += new System.EventHandler(this.kartPointList_SelectedIndexChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.areaList);
            this.tabPage6.ImageKey = "arearererrere.png";
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(362, 456);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.ToolTipText = "Areas";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // areaList
            // 
            this.areaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaList.FormattingEnabled = true;
            this.areaList.Location = new System.Drawing.Point(3, 3);
            this.areaList.Name = "areaList";
            this.areaList.Size = new System.Drawing.Size(356, 450);
            this.areaList.TabIndex = 0;
            this.areaList.SelectedIndexChanged += new System.EventHandler(this.areaList_SelectedIndexChanged);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.cameraList);
            this.tabPage7.ImageKey = "cam.png";
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(362, 456);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.ToolTipText = "Cameras";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // cameraList
            // 
            this.cameraList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraList.FormattingEnabled = true;
            this.cameraList.Location = new System.Drawing.Point(3, 3);
            this.cameraList.Name = "cameraList";
            this.cameraList.Size = new System.Drawing.Size(356, 450);
            this.cameraList.TabIndex = 0;
            this.cameraList.SelectedIndexChanged += new System.EventHandler(this.cameraList_SelectedIndexChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.respList);
            this.tabPage8.ImageKey = "respawn2.png";
            this.tabPage8.Location = new System.Drawing.Point(4, 23);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(362, 456);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.ToolTipText = "Respawns";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // respList
            // 
            this.respList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.respList.FormattingEnabled = true;
            this.respList.Location = new System.Drawing.Point(3, 3);
            this.respList.Name = "respList";
            this.respList.Size = new System.Drawing.Size(356, 450);
            this.respList.TabIndex = 0;
            this.respList.SelectedIndexChanged += new System.EventHandler(this.respList_SelectedIndexChanged);
            this.respList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.respList_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Tag = "";
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cam.png");
            this.imageList1.Images.SetKeyName(1, "cp.png");
            this.imageList1.Images.SetKeyName(2, "enemyitemp.png");
            this.imageList1.Images.SetKeyName(3, "respawn2.png");
            this.imageList1.Images.SetKeyName(4, "paths.png");
            this.imageList1.Images.SetKeyName(5, "arearererrere.png");
            this.imageList1.Images.SetKeyName(6, "objects.png");
            this.imageList1.Images.SetKeyName(7, "startungpos.png");
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 538);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "DouBOL Dash v0.1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel loadingLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BMDViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel selectionInfo;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem respawnToolStripMenuItem;
        public OpenTK.GLControl glControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem respawnToolStripMenuItem1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox chckList;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox routeList;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox objList;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListBox kartPointList;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListBox areaList;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.ListBox cameraList;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.ListBox enemyRouteList;
        public System.Windows.Forms.ListBox respList;
    }
}

