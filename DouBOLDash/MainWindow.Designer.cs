/*
    Copyright 2016-2017 shibboleet
    This file is part of DouBOL Dash.
    DouBOL Dash is free software: you can redistribute it and/or modify it under
    the terms of the GNU General Public License as published by the Free
    Software Foundation, either version 3 of the License, or (at your option)
    any later version.
    DouBOL Dash is distributed in the hope that it will be useful, but WITHOUT ANY
    WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
    FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
    You should have received a copy of the GNU General Public License along
    with DouBOL Dash. If not, see http://www.gnu.org/licenses/.
*/
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
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BMDViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bTIViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertCourseModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bCOToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.loadingLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectionInfo = new System.Windows.Forms.ToolStripLabel();
            this.glControl1 = new OpenTK.GLControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.bolTab = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.musicInput = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.unknown5 = new System.Windows.Forms.NumericUpDown();
            this.unknown4 = new System.Windows.Forms.NumericUpDown();
            this.unknown3 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.musicSelect = new System.Windows.Forms.ComboBox();
            this.lapCounter = new System.Windows.Forms.NumericUpDown();
            this.enemyPointsTab = new System.Windows.Forms.TabPage();
            this.propertyGrid6 = new System.Windows.Forms.PropertyGrid();
            this.enemyRouteList = new System.Windows.Forms.ListBox();
            this.checkpointGroupTab = new System.Windows.Forms.TabPage();
            this.propertyGrid9 = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chckGroup = new System.Windows.Forms.ListBox();
            this.checkpointTab = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyGrid5 = new System.Windows.Forms.PropertyGrid();
            this.chckList = new System.Windows.Forms.ListBox();
            this.routeGroupTab = new System.Windows.Forms.TabPage();
            this.propertyGrid10 = new System.Windows.Forms.PropertyGrid();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.routeGroupList = new System.Windows.Forms.ListBox();
            this.routeTab = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.routeList = new System.Windows.Forms.ListBox();
            this.itemsTab = new System.Windows.Forms.TabPage();
            this.propertyGrid3 = new System.Windows.Forms.PropertyGrid();
            this.objList = new System.Windows.Forms.ListBox();
            this.startingPointTab = new System.Windows.Forms.TabPage();
            this.propertyGrid8 = new System.Windows.Forms.PropertyGrid();
            this.kartPointList = new System.Windows.Forms.ListBox();
            this.areaTab = new System.Windows.Forms.TabPage();
            this.propertyGrid7 = new System.Windows.Forms.PropertyGrid();
            this.areaList = new System.Windows.Forms.ListBox();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.cameraList = new System.Windows.Forms.ListBox();
            this.respawnTab = new System.Windows.Forms.TabPage();
            this.propertyGrid4 = new System.Windows.Forms.PropertyGrid();
            this.respList = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.routeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllInGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyRouteContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertRouteHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRouteAtBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicatePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRoutePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkpointGroupContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCheckpointGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCheckpointGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateCheckpointGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkpointContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCheckpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertCheckpointHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCheckpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateCheckpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllInGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unknown7 = new System.Windows.Forms.NumericUpDown();
            this.unknown8 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.bolTab.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lapCounter)).BeginInit();
            this.enemyPointsTab.SuspendLayout();
            this.checkpointGroupTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.checkpointTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.routeGroupTab.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.routeTab.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.itemsTab.SuspendLayout();
            this.startingPointTab.SuspendLayout();
            this.areaTab.SuspendLayout();
            this.cameraTab.SuspendLayout();
            this.respawnTab.SuspendLayout();
            this.routeContext.SuspendLayout();
            this.objectContext.SuspendLayout();
            this.enemyRouteContext.SuspendLayout();
            this.checkpointGroupContext.SuspendLayout();
            this.checkpointContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unknown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown8)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1310, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripMenuItem.Image")));
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BMDViewerToolStripMenuItem,
            this.bTIViewerToolStripMenuItem,
            this.insertCourseModelToolStripMenuItem,
            this.bCOToolToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 19);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // BMDViewerToolStripMenuItem
            // 
            this.BMDViewerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("BMDViewerToolStripMenuItem.Image")));
            this.BMDViewerToolStripMenuItem.Name = "BMDViewerToolStripMenuItem";
            this.BMDViewerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BMDViewerToolStripMenuItem.Text = "BMD Viewer";
            this.BMDViewerToolStripMenuItem.Click += new System.EventHandler(this.BMDViewerToolStripMenuItem_Click);
            // 
            // bTIViewerToolStripMenuItem
            // 
            this.bTIViewerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bTIViewerToolStripMenuItem.Image")));
            this.bTIViewerToolStripMenuItem.Name = "bTIViewerToolStripMenuItem";
            this.bTIViewerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bTIViewerToolStripMenuItem.Text = "BTI Viewer";
            this.bTIViewerToolStripMenuItem.Click += new System.EventHandler(this.bTIViewerToolStripMenuItem_Click);
            // 
            // insertCourseModelToolStripMenuItem
            // 
            this.insertCourseModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insertCourseModelToolStripMenuItem.Image")));
            this.insertCourseModelToolStripMenuItem.Name = "insertCourseModelToolStripMenuItem";
            this.insertCourseModelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.insertCourseModelToolStripMenuItem.Text = "Insert Course Model";
            this.insertCourseModelToolStripMenuItem.Click += new System.EventHandler(this.insertCourseModelToolStripMenuItem_Click);
            // 
            // bCOToolToolStripMenuItem
            // 
            this.bCOToolToolStripMenuItem.Name = "bCOToolToolStripMenuItem";
            this.bCOToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bCOToolToolStripMenuItem.Text = "BCO Tool";
            this.bCOToolToolStripMenuItem.Click += new System.EventHandler(this.bCOToolToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.chooseFolderToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripMenuItem.Image")));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // chooseFolderToolStripMenuItem
            // 
            this.chooseFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("chooseFolderToolStripMenuItem.Image")));
            this.chooseFolderToolStripMenuItem.Name = "chooseFolderToolStripMenuItem";
            this.chooseFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chooseFolderToolStripMenuItem.Text = "Choose folder";
            this.chooseFolderToolStripMenuItem.Click += new System.EventHandler(this.chooseFolderToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadingLabel,
            this.toolStripSeparator1,
            this.selectionInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 716);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1310, 25);
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
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(867, 670);
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
            this.panel1.Location = new System.Drawing.Point(429, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 670);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 640);
            this.panel2.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.bolTab);
            this.tabControl1.Controls.Add(this.enemyPointsTab);
            this.tabControl1.Controls.Add(this.checkpointGroupTab);
            this.tabControl1.Controls.Add(this.checkpointTab);
            this.tabControl1.Controls.Add(this.routeGroupTab);
            this.tabControl1.Controls.Add(this.routeTab);
            this.tabControl1.Controls.Add(this.itemsTab);
            this.tabControl1.Controls.Add(this.startingPointTab);
            this.tabControl1.Controls.Add(this.areaTab);
            this.tabControl1.Controls.Add(this.cameraTab);
            this.tabControl1.Controls.Add(this.respawnTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(59, 19);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(423, 640);
            this.tabControl1.TabIndex = 0;
            // 
            // bolTab
            // 
            this.bolTab.Controls.Add(this.panel3);
            this.bolTab.ImageKey = "blo informations.png";
            this.bolTab.Location = new System.Drawing.Point(4, 23);
            this.bolTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bolTab.Name = "bolTab";
            this.bolTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bolTab.Size = new System.Drawing.Size(415, 613);
            this.bolTab.TabIndex = 8;
            this.bolTab.ToolTipText = "Track Information";
            this.bolTab.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Location = new System.Drawing.Point(3, 8);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(418, 643);
            this.panel3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(0, 305);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(421, 327);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Track Minimap";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(415, 301);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.unknown8);
            this.groupBox1.Controls.Add(this.unknown7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.musicInput);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.unknown5);
            this.groupBox1.Controls.Add(this.unknown4);
            this.groupBox1.Controls.Add(this.unknown3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.musicSelect);
            this.groupBox1.Controls.Add(this.lapCounter);
            this.groupBox1.Location = new System.Drawing.Point(-3, -4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(424, 309);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Track Information";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Music";
            // 
            // musicInput
            // 
            this.musicInput.Enabled = false;
            this.musicInput.Location = new System.Drawing.Point(117, 75);
            this.musicInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.musicInput.Maximum = new decimal(new int[] {
            69,
            0,
            0,
            0});
            this.musicInput.Minimum = new decimal(new int[] {
            33,
            0,
            0,
            0});
            this.musicInput.Name = "musicInput";
            this.musicInput.Size = new System.Drawing.Size(140, 25);
            this.musicInput.TabIndex = 11;
            this.musicInput.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.musicInput.ValueChanged += new System.EventHandler(this.musicInput_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Unknown Float 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Unknown Float 2";
            // 
            // unknown5
            // 
            this.unknown5.DecimalPlaces = 2;
            this.unknown5.Enabled = false;
            this.unknown5.Location = new System.Drawing.Point(118, 212);
            this.unknown5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.unknown5.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.unknown5.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.unknown5.Name = "unknown5";
            this.unknown5.Size = new System.Drawing.Size(140, 25);
            this.unknown5.TabIndex = 8;
            this.unknown5.ValueChanged += new System.EventHandler(this.unknown5_ValueChanged);
            // 
            // unknown4
            // 
            this.unknown4.DecimalPlaces = 2;
            this.unknown4.Enabled = false;
            this.unknown4.Location = new System.Drawing.Point(118, 178);
            this.unknown4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.unknown4.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.unknown4.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.unknown4.Name = "unknown4";
            this.unknown4.Size = new System.Drawing.Size(140, 25);
            this.unknown4.TabIndex = 7;
            this.unknown4.ValueChanged += new System.EventHandler(this.unknown4_ValueChanged);
            // 
            // unknown3
            // 
            this.unknown3.DecimalPlaces = 2;
            this.unknown3.Enabled = false;
            this.unknown3.Location = new System.Drawing.Point(117, 144);
            this.unknown3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.unknown3.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.unknown3.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.unknown3.Name = "unknown3";
            this.unknown3.Size = new System.Drawing.Size(140, 25);
            this.unknown3.TabIndex = 6;
            this.unknown3.ValueChanged += new System.EventHandler(this.unknown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Music ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of laps";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Unknown Float 1";
            // 
            // musicSelect
            // 
            this.musicSelect.Enabled = false;
            this.musicSelect.FormattingEnabled = true;
            this.musicSelect.Location = new System.Drawing.Point(117, 109);
            this.musicSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.musicSelect.Name = "musicSelect";
            this.musicSelect.Size = new System.Drawing.Size(140, 25);
            this.musicSelect.TabIndex = 2;
            this.musicSelect.SelectedIndexChanged += new System.EventHandler(this.musicSelect_SelectedIndexChanged);
            // 
            // lapCounter
            // 
            this.lapCounter.Enabled = false;
            this.lapCounter.Location = new System.Drawing.Point(118, 35);
            this.lapCounter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lapCounter.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lapCounter.Name = "lapCounter";
            this.lapCounter.Size = new System.Drawing.Size(140, 25);
            this.lapCounter.TabIndex = 0;
            this.lapCounter.ValueChanged += new System.EventHandler(this.lapCounter_ValueChanged);
            // 
            // enemyPointsTab
            // 
            this.enemyPointsTab.Controls.Add(this.propertyGrid6);
            this.enemyPointsTab.Controls.Add(this.enemyRouteList);
            this.enemyPointsTab.ImageKey = "enemy routes.png";
            this.enemyPointsTab.Location = new System.Drawing.Point(4, 23);
            this.enemyPointsTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.enemyPointsTab.Name = "enemyPointsTab";
            this.enemyPointsTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.enemyPointsTab.Size = new System.Drawing.Size(415, 613);
            this.enemyPointsTab.TabIndex = 0;
            this.enemyPointsTab.ToolTipText = "Enemy / Item Routes";
            this.enemyPointsTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid6
            // 
            this.propertyGrid6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid6.Location = new System.Drawing.Point(3, 306);
            this.propertyGrid6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid6.Name = "propertyGrid6";
            this.propertyGrid6.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid6.Size = new System.Drawing.Size(415, 303);
            this.propertyGrid6.TabIndex = 1;
            this.propertyGrid6.ToolbarVisible = false;
            this.propertyGrid6.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid6_PropertyValueChanged);
            // 
            // enemyRouteList
            // 
            this.enemyRouteList.FormattingEnabled = true;
            this.enemyRouteList.ItemHeight = 17;
            this.enemyRouteList.Location = new System.Drawing.Point(3, 4);
            this.enemyRouteList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.enemyRouteList.Name = "enemyRouteList";
            this.enemyRouteList.Size = new System.Drawing.Size(415, 293);
            this.enemyRouteList.TabIndex = 0;
            this.enemyRouteList.SelectedIndexChanged += new System.EventHandler(this.enemyRouteList_SelectedIndexChanged);
            this.enemyRouteList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.enemyRouteList_MouseDown);
            // 
            // checkpointGroupTab
            // 
            this.checkpointGroupTab.Controls.Add(this.propertyGrid9);
            this.checkpointGroupTab.Controls.Add(this.groupBox3);
            this.checkpointGroupTab.ImageKey = "checkpoints groups.png";
            this.checkpointGroupTab.Location = new System.Drawing.Point(4, 23);
            this.checkpointGroupTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkpointGroupTab.Name = "checkpointGroupTab";
            this.checkpointGroupTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkpointGroupTab.Size = new System.Drawing.Size(415, 613);
            this.checkpointGroupTab.TabIndex = 10;
            this.checkpointGroupTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid9
            // 
            this.propertyGrid9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid9.Location = new System.Drawing.Point(2, 197);
            this.propertyGrid9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid9.Name = "propertyGrid9";
            this.propertyGrid9.Size = new System.Drawing.Size(420, 400);
            this.propertyGrid9.TabIndex = 3;
            this.propertyGrid9.ToolbarVisible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chckGroup);
            this.groupBox3.Location = new System.Drawing.Point(2, 8);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(415, 180);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Checkpoint Groups";
            // 
            // chckGroup
            // 
            this.chckGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chckGroup.FormattingEnabled = true;
            this.chckGroup.ItemHeight = 17;
            this.chckGroup.Location = new System.Drawing.Point(3, 22);
            this.chckGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chckGroup.Name = "chckGroup";
            this.chckGroup.Size = new System.Drawing.Size(409, 154);
            this.chckGroup.TabIndex = 0;
            this.chckGroup.SelectedIndexChanged += new System.EventHandler(this.chckGroup_SelectedIndexChanged);
            this.chckGroup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chckGroup_MouseDown);
            // 
            // checkpointTab
            // 
            this.checkpointTab.Controls.Add(this.groupBox4);
            this.checkpointTab.ImageKey = "checkpoints.png";
            this.checkpointTab.Location = new System.Drawing.Point(4, 23);
            this.checkpointTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkpointTab.Name = "checkpointTab";
            this.checkpointTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkpointTab.Size = new System.Drawing.Size(415, 613);
            this.checkpointTab.TabIndex = 1;
            this.checkpointTab.ToolTipText = "Checkpoints";
            this.checkpointTab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.propertyGrid5);
            this.groupBox4.Controls.Add(this.chckList);
            this.groupBox4.Location = new System.Drawing.Point(0, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(413, 597);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Checkpoints";
            // 
            // propertyGrid5
            // 
            this.propertyGrid5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid5.Location = new System.Drawing.Point(3, 238);
            this.propertyGrid5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid5.Name = "propertyGrid5";
            this.propertyGrid5.Size = new System.Drawing.Size(406, 351);
            this.propertyGrid5.TabIndex = 1;
            this.propertyGrid5.ToolbarVisible = false;
            this.propertyGrid5.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid5_PropertyValueChanged);
            // 
            // chckList
            // 
            this.chckList.FormattingEnabled = true;
            this.chckList.ItemHeight = 17;
            this.chckList.Location = new System.Drawing.Point(3, 21);
            this.chckList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chckList.Name = "chckList";
            this.chckList.Size = new System.Drawing.Size(405, 208);
            this.chckList.TabIndex = 0;
            this.chckList.SelectedIndexChanged += new System.EventHandler(this.chckList_SelectedIndexChanged);
            this.chckList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chckList_MouseDown);
            // 
            // routeGroupTab
            // 
            this.routeGroupTab.Controls.Add(this.propertyGrid10);
            this.routeGroupTab.Controls.Add(this.groupBox5);
            this.routeGroupTab.ImageKey = "route groups.png";
            this.routeGroupTab.Location = new System.Drawing.Point(4, 23);
            this.routeGroupTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.routeGroupTab.Name = "routeGroupTab";
            this.routeGroupTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.routeGroupTab.Size = new System.Drawing.Size(415, 613);
            this.routeGroupTab.TabIndex = 9;
            this.routeGroupTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid10
            // 
            this.propertyGrid10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid10.Location = new System.Drawing.Point(3, 409);
            this.propertyGrid10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid10.Name = "propertyGrid10";
            this.propertyGrid10.Size = new System.Drawing.Size(415, 196);
            this.propertyGrid10.TabIndex = 3;
            this.propertyGrid10.ToolbarVisible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.routeGroupList);
            this.groupBox5.Location = new System.Drawing.Point(3, 8);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(415, 394);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Route Groups";
            // 
            // routeGroupList
            // 
            this.routeGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routeGroupList.FormattingEnabled = true;
            this.routeGroupList.ItemHeight = 17;
            this.routeGroupList.Location = new System.Drawing.Point(3, 22);
            this.routeGroupList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.routeGroupList.Name = "routeGroupList";
            this.routeGroupList.Size = new System.Drawing.Size(409, 368);
            this.routeGroupList.TabIndex = 0;
            this.routeGroupList.SelectedIndexChanged += new System.EventHandler(this.routeGroupList_SelectedIndexChanged_1);
            // 
            // routeTab
            // 
            this.routeTab.Controls.Add(this.groupBox6);
            this.routeTab.ImageKey = "route points.png";
            this.routeTab.Location = new System.Drawing.Point(4, 23);
            this.routeTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.routeTab.Name = "routeTab";
            this.routeTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.routeTab.Size = new System.Drawing.Size(415, 613);
            this.routeTab.TabIndex = 2;
            this.routeTab.ToolTipText = "Routes";
            this.routeTab.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.propertyGrid1);
            this.groupBox6.Controls.Add(this.routeList);
            this.groupBox6.Location = new System.Drawing.Point(3, 4);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Size = new System.Drawing.Size(422, 609);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Routes";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(3, 373);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(408, 228);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.ToolbarVisible = false;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // routeList
            // 
            this.routeList.FormattingEnabled = true;
            this.routeList.ItemHeight = 17;
            this.routeList.Location = new System.Drawing.Point(3, 21);
            this.routeList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.routeList.Name = "routeList";
            this.routeList.Size = new System.Drawing.Size(408, 344);
            this.routeList.TabIndex = 0;
            this.routeList.SelectedIndexChanged += new System.EventHandler(this.routeList_SelectedIndexChanged);
            this.routeList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.routeList_MouseDown);
            // 
            // itemsTab
            // 
            this.itemsTab.Controls.Add(this.propertyGrid3);
            this.itemsTab.Controls.Add(this.objList);
            this.itemsTab.ImageKey = "objects.png";
            this.itemsTab.Location = new System.Drawing.Point(4, 23);
            this.itemsTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.itemsTab.Name = "itemsTab";
            this.itemsTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.itemsTab.Size = new System.Drawing.Size(415, 613);
            this.itemsTab.TabIndex = 3;
            this.itemsTab.ToolTipText = "Objects";
            this.itemsTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid3
            // 
            this.propertyGrid3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid3.Location = new System.Drawing.Point(3, 373);
            this.propertyGrid3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid3.Name = "propertyGrid3";
            this.propertyGrid3.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid3.Size = new System.Drawing.Size(414, 224);
            this.propertyGrid3.TabIndex = 1;
            this.propertyGrid3.ToolbarVisible = false;
            this.propertyGrid3.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid3_PropertyValueChanged);
            // 
            // objList
            // 
            this.objList.FormattingEnabled = true;
            this.objList.ItemHeight = 17;
            this.objList.Location = new System.Drawing.Point(3, 4);
            this.objList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.objList.Name = "objList";
            this.objList.Size = new System.Drawing.Size(415, 361);
            this.objList.TabIndex = 0;
            this.objList.SelectedIndexChanged += new System.EventHandler(this.objList_SelectedIndexChanged);
            this.objList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.objList_MouseDown);
            // 
            // startingPointTab
            // 
            this.startingPointTab.Controls.Add(this.propertyGrid8);
            this.startingPointTab.Controls.Add(this.kartPointList);
            this.startingPointTab.ImageKey = "starting points.png";
            this.startingPointTab.Location = new System.Drawing.Point(4, 23);
            this.startingPointTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startingPointTab.Name = "startingPointTab";
            this.startingPointTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startingPointTab.Size = new System.Drawing.Size(415, 613);
            this.startingPointTab.TabIndex = 4;
            this.startingPointTab.ToolTipText = "Starting Points";
            this.startingPointTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid8
            // 
            this.propertyGrid8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid8.Location = new System.Drawing.Point(3, 204);
            this.propertyGrid8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid8.Name = "propertyGrid8";
            this.propertyGrid8.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid8.Size = new System.Drawing.Size(415, 393);
            this.propertyGrid8.TabIndex = 1;
            this.propertyGrid8.ToolbarVisible = false;
            this.propertyGrid8.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid8_PropertyValueChanged);
            // 
            // kartPointList
            // 
            this.kartPointList.FormattingEnabled = true;
            this.kartPointList.ItemHeight = 17;
            this.kartPointList.Location = new System.Drawing.Point(3, 4);
            this.kartPointList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.kartPointList.Name = "kartPointList";
            this.kartPointList.Size = new System.Drawing.Size(415, 191);
            this.kartPointList.TabIndex = 0;
            this.kartPointList.SelectedIndexChanged += new System.EventHandler(this.kartPointList_SelectedIndexChanged);
            // 
            // areaTab
            // 
            this.areaTab.Controls.Add(this.propertyGrid7);
            this.areaTab.Controls.Add(this.areaList);
            this.areaTab.ImageKey = "areas.png";
            this.areaTab.Location = new System.Drawing.Point(4, 23);
            this.areaTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.areaTab.Name = "areaTab";
            this.areaTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.areaTab.Size = new System.Drawing.Size(424, 764);
            this.areaTab.TabIndex = 5;
            this.areaTab.ToolTipText = "Areas";
            this.areaTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid7
            // 
            this.propertyGrid7.Location = new System.Drawing.Point(3, 289);
            this.propertyGrid7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid7.Name = "propertyGrid7";
            this.propertyGrid7.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid7.Size = new System.Drawing.Size(415, 463);
            this.propertyGrid7.TabIndex = 1;
            this.propertyGrid7.ToolbarVisible = false;
            this.propertyGrid7.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid7_PropertyValueChanged);
            // 
            // areaList
            // 
            this.areaList.FormattingEnabled = true;
            this.areaList.ItemHeight = 17;
            this.areaList.Location = new System.Drawing.Point(3, 4);
            this.areaList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.areaList.Name = "areaList";
            this.areaList.Size = new System.Drawing.Size(415, 276);
            this.areaList.TabIndex = 0;
            this.areaList.SelectedIndexChanged += new System.EventHandler(this.areaList_SelectedIndexChanged);
            // 
            // cameraTab
            // 
            this.cameraTab.Controls.Add(this.propertyGrid2);
            this.cameraTab.Controls.Add(this.cameraList);
            this.cameraTab.ImageKey = "cameras.png";
            this.cameraTab.Location = new System.Drawing.Point(4, 23);
            this.cameraTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cameraTab.Size = new System.Drawing.Size(424, 764);
            this.cameraTab.TabIndex = 6;
            this.cameraTab.ToolTipText = "Cameras";
            this.cameraTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid2.Location = new System.Drawing.Point(3, 340);
            this.propertyGrid2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid2.Size = new System.Drawing.Size(415, 417);
            this.propertyGrid2.TabIndex = 1;
            this.propertyGrid2.ToolbarVisible = false;
            this.propertyGrid2.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid2_PropertyValueChanged);
            // 
            // cameraList
            // 
            this.cameraList.FormattingEnabled = true;
            this.cameraList.ItemHeight = 17;
            this.cameraList.Location = new System.Drawing.Point(3, 4);
            this.cameraList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cameraList.Name = "cameraList";
            this.cameraList.Size = new System.Drawing.Size(415, 327);
            this.cameraList.TabIndex = 0;
            this.cameraList.SelectedIndexChanged += new System.EventHandler(this.cameraList_SelectedIndexChanged);
            // 
            // respawnTab
            // 
            this.respawnTab.Controls.Add(this.propertyGrid4);
            this.respawnTab.Controls.Add(this.respList);
            this.respawnTab.ImageKey = "respawn points.png";
            this.respawnTab.Location = new System.Drawing.Point(4, 23);
            this.respawnTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.respawnTab.Name = "respawnTab";
            this.respawnTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.respawnTab.Size = new System.Drawing.Size(424, 764);
            this.respawnTab.TabIndex = 7;
            this.respawnTab.ToolTipText = "Respawns";
            this.respawnTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid4
            // 
            this.propertyGrid4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid4.Location = new System.Drawing.Point(3, 221);
            this.propertyGrid4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGrid4.Name = "propertyGrid4";
            this.propertyGrid4.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid4.Size = new System.Drawing.Size(415, 531);
            this.propertyGrid4.TabIndex = 1;
            this.propertyGrid4.ToolbarVisible = false;
            // 
            // respList
            // 
            this.respList.FormattingEnabled = true;
            this.respList.ItemHeight = 17;
            this.respList.Location = new System.Drawing.Point(3, 4);
            this.respList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.respList.Name = "respList";
            this.respList.Size = new System.Drawing.Size(415, 208);
            this.respList.TabIndex = 0;
            this.respList.SelectedIndexChanged += new System.EventHandler(this.respList_SelectedIndexChanged);
            this.respList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.respList_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Tag = "";
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "areas.png");
            this.imageList1.Images.SetKeyName(1, "blo informations.png");
            this.imageList1.Images.SetKeyName(2, "cameras.png");
            this.imageList1.Images.SetKeyName(3, "checkpoints groups.png");
            this.imageList1.Images.SetKeyName(4, "checkpoints.png");
            this.imageList1.Images.SetKeyName(5, "enemy routes.png");
            this.imageList1.Images.SetKeyName(6, "objects.png");
            this.imageList1.Images.SetKeyName(7, "respawn points.png");
            this.imageList1.Images.SetKeyName(8, "route groups.png");
            this.imageList1.Images.SetKeyName(9, "route points.png");
            this.imageList1.Images.SetKeyName(10, "starting points.png");
            this.imageList1.Images.SetKeyName(11, "bmd viewer.png");
            this.imageList1.Images.SetKeyName(12, "bti viewer.png");
            this.imageList1.Images.SetKeyName(13, "close.png");
            this.imageList1.Images.SetKeyName(14, "insertion of a course model.png");
            this.imageList1.Images.SetKeyName(15, "open file.png");
            this.imageList1.Images.SetKeyName(16, "reset.png");
            this.imageList1.Images.SetKeyName(17, "save as.png");
            this.imageList1.Images.SetKeyName(18, "save.png");
            this.imageList1.Images.SetKeyName(19, "settings.png");
            // 
            // routeContext
            // 
            this.routeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllInGroupsToolStripMenuItem});
            this.routeContext.Name = "routeContext";
            this.routeContext.Size = new System.Drawing.Size(172, 26);
            // 
            // selectAllInGroupsToolStripMenuItem
            // 
            this.selectAllInGroupsToolStripMenuItem.Name = "selectAllInGroupsToolStripMenuItem";
            this.selectAllInGroupsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.selectAllInGroupsToolStripMenuItem.Text = "Select All In Group";
            this.selectAllInGroupsToolStripMenuItem.Click += new System.EventHandler(this.selectAllInGroupsToolStripMenuItem_Click);
            // 
            // objectContext
            // 
            this.objectContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addObjectToolStripMenuItem,
            this.deleteObjectToolStripMenuItem,
            this.duplicateObjectToolStripMenuItem});
            this.objectContext.Name = "contextMenuStrip1";
            this.objectContext.Size = new System.Drawing.Size(163, 70);
            // 
            // addObjectToolStripMenuItem
            // 
            this.addObjectToolStripMenuItem.Name = "addObjectToolStripMenuItem";
            this.addObjectToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.addObjectToolStripMenuItem.Text = "Add Object";
            this.addObjectToolStripMenuItem.Click += new System.EventHandler(this.addObjectToolStripMenuItem_Click);
            // 
            // enemyRouteContext
            // 
            this.enemyRouteContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertRouteHereToolStripMenuItem,
            this.insertRouteAtBottomToolStripMenuItem,
            this.duplicatePointToolStripMenuItem,
            this.deleteRoutePointToolStripMenuItem,
            this.selectAllInGroupToolStripMenuItem});
            this.enemyRouteContext.Name = "enemyRouteContext";
            this.enemyRouteContext.Size = new System.Drawing.Size(232, 114);
            // 
            // insertRouteHereToolStripMenuItem
            // 
            this.insertRouteHereToolStripMenuItem.Name = "insertRouteHereToolStripMenuItem";
            this.insertRouteHereToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.insertRouteHereToolStripMenuItem.Text = "Insert Enemy Point Here";
            this.insertRouteHereToolStripMenuItem.ToolTipText = "Inserts a enemy point before the current selected point.";
            this.insertRouteHereToolStripMenuItem.Click += new System.EventHandler(this.insertRouteHereToolStripMenuItem_Click);
            // 
            // insertRouteAtBottomToolStripMenuItem
            // 
            this.insertRouteAtBottomToolStripMenuItem.Name = "insertRouteAtBottomToolStripMenuItem";
            this.insertRouteAtBottomToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.insertRouteAtBottomToolStripMenuItem.Text = "Insert Enemy Point At Bottom";
            this.insertRouteAtBottomToolStripMenuItem.ToolTipText = "Inserts a enemy route point at the bottom of a list.";
            this.insertRouteAtBottomToolStripMenuItem.Click += new System.EventHandler(this.insertRouteAtBottomToolStripMenuItem_Click);
            // 
            // duplicatePointToolStripMenuItem
            // 
            this.duplicatePointToolStripMenuItem.Name = "duplicatePointToolStripMenuItem";
            this.duplicatePointToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.duplicatePointToolStripMenuItem.Text = "Duplicate Enemy Point";
            this.duplicatePointToolStripMenuItem.ToolTipText = "Duplicates the currently selected point.";
            this.duplicatePointToolStripMenuItem.Click += new System.EventHandler(this.duplicatePointToolStripMenuItem_Click);
            // 
            // deleteRoutePointToolStripMenuItem
            // 
            this.deleteRoutePointToolStripMenuItem.Name = "deleteRoutePointToolStripMenuItem";
            this.deleteRoutePointToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.deleteRoutePointToolStripMenuItem.Text = "Delete Enemy Point";
            this.deleteRoutePointToolStripMenuItem.ToolTipText = "Removes the currently selected point.";
            this.deleteRoutePointToolStripMenuItem.Click += new System.EventHandler(this.deleteEnemPointToolStripMenuItem_Click);
            // 
            // deleteObjectToolStripMenuItem
            // 
            this.deleteObjectToolStripMenuItem.Name = "deleteObjectToolStripMenuItem";
            this.deleteObjectToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteObjectToolStripMenuItem.Text = "Delete Object";
            this.deleteObjectToolStripMenuItem.Click += new System.EventHandler(this.deleteObjectToolStripMenuItem_Click);
            // 
            // duplicateObjectToolStripMenuItem
            // 
            this.duplicateObjectToolStripMenuItem.Name = "duplicateObjectToolStripMenuItem";
            this.duplicateObjectToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.duplicateObjectToolStripMenuItem.Text = "Duplicate Object";
            this.duplicateObjectToolStripMenuItem.Click += new System.EventHandler(this.duplicateObjectToolStripMenuItem_Click);
            // 
            // checkpointGroupContext
            // 
            this.checkpointGroupContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCheckpointGroupToolStripMenuItem,
            this.deleteCheckpointGroupToolStripMenuItem,
            this.duplicateCheckpointGroupToolStripMenuItem});
            this.checkpointGroupContext.Name = "checkpointGroupContext";
            this.checkpointGroupContext.Size = new System.Drawing.Size(225, 70);
            // 
            // addCheckpointGroupToolStripMenuItem
            // 
            this.addCheckpointGroupToolStripMenuItem.Name = "addCheckpointGroupToolStripMenuItem";
            this.addCheckpointGroupToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addCheckpointGroupToolStripMenuItem.Text = "Add Checkpoint Group";
            this.addCheckpointGroupToolStripMenuItem.Click += new System.EventHandler(this.addCheckpointGroupToolStripMenuItem_Click);
            // 
            // deleteCheckpointGroupToolStripMenuItem
            // 
            this.deleteCheckpointGroupToolStripMenuItem.Name = "deleteCheckpointGroupToolStripMenuItem";
            this.deleteCheckpointGroupToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.deleteCheckpointGroupToolStripMenuItem.Text = "Delete Checkpoint Group";
            this.deleteCheckpointGroupToolStripMenuItem.Click += new System.EventHandler(this.deleteCheckpointGroupToolStripMenuItem_Click);
            // 
            // duplicateCheckpointGroupToolStripMenuItem
            // 
            this.duplicateCheckpointGroupToolStripMenuItem.Name = "duplicateCheckpointGroupToolStripMenuItem";
            this.duplicateCheckpointGroupToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.duplicateCheckpointGroupToolStripMenuItem.Text = "Duplicate Checkpoint Group";
            this.duplicateCheckpointGroupToolStripMenuItem.Click += new System.EventHandler(this.duplicateCheckpointGroupToolStripMenuItem_Click);
            // 
            // checkpointContext
            // 
            this.checkpointContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCheckpointToolStripMenuItem,
            this.insertCheckpointHereToolStripMenuItem,
            this.deleteCheckpointToolStripMenuItem,
            this.duplicateCheckpointToolStripMenuItem});
            this.checkpointContext.Name = "checkpointContext";
            this.checkpointContext.Size = new System.Drawing.Size(196, 92);
            // 
            // addCheckpointToolStripMenuItem
            // 
            this.addCheckpointToolStripMenuItem.Name = "addCheckpointToolStripMenuItem";
            this.addCheckpointToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.addCheckpointToolStripMenuItem.Text = "Add Checkpoint";
            this.addCheckpointToolStripMenuItem.Click += new System.EventHandler(this.addCheckpointToolStripMenuItem_Click);
            // 
            // insertCheckpointHereToolStripMenuItem
            // 
            this.insertCheckpointHereToolStripMenuItem.Name = "insertCheckpointHereToolStripMenuItem";
            this.insertCheckpointHereToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.insertCheckpointHereToolStripMenuItem.Text = "Insert Checkpoint Here";
            this.insertCheckpointHereToolStripMenuItem.Click += new System.EventHandler(this.insertCheckpointHereToolStripMenuItem_Click);
            // 
            // deleteCheckpointToolStripMenuItem
            // 
            this.deleteCheckpointToolStripMenuItem.Name = "deleteCheckpointToolStripMenuItem";
            this.deleteCheckpointToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.deleteCheckpointToolStripMenuItem.Text = "Delete Checkpoint";
            this.deleteCheckpointToolStripMenuItem.Click += new System.EventHandler(this.deleteCheckpointToolStripMenuItem_Click);
            // 
            // duplicateCheckpointToolStripMenuItem
            // 
            this.duplicateCheckpointToolStripMenuItem.Name = "duplicateCheckpointToolStripMenuItem";
            this.duplicateCheckpointToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.duplicateCheckpointToolStripMenuItem.Text = "Duplicate Checkpoint";
            this.duplicateCheckpointToolStripMenuItem.Click += new System.EventHandler(this.duplicateCheckpointToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(49, 19);
            this.toolStripMenuItem1.Text = "Other";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // selectAllInGroupToolStripMenuItem
            // 
            this.selectAllInGroupToolStripMenuItem.Name = "selectAllInGroupToolStripMenuItem";
            this.selectAllInGroupToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.selectAllInGroupToolStripMenuItem.Text = "Select All In Group";
            this.selectAllInGroupToolStripMenuItem.Click += new System.EventHandler(this.selectAllInGroupToolStripMenuItem_Click);
            // 
            // unknown7
            // 
            this.unknown7.DecimalPlaces = 2;
            this.unknown7.Enabled = false;
            this.unknown7.Location = new System.Drawing.Point(118, 245);
            this.unknown7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.unknown7.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.unknown7.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.unknown7.Name = "unknown7";
            this.unknown7.Size = new System.Drawing.Size(140, 25);
            this.unknown7.TabIndex = 13;
            this.unknown7.ValueChanged += new System.EventHandler(this.unknown7_ValueChanged);
            // 
            // unknown8
            // 
            this.unknown8.DecimalPlaces = 2;
            this.unknown8.Enabled = false;
            this.unknown8.Location = new System.Drawing.Point(118, 278);
            this.unknown8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.unknown8.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.unknown8.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.unknown8.Name = "unknown8";
            this.unknown8.Size = new System.Drawing.Size(140, 25);
            this.unknown8.TabIndex = 14;
            this.unknown8.ValueChanged += new System.EventHandler(this.unknown8_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Unknown Float 4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Unknown Float 5";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check For Update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 741);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainWindow";
            this.Text = "DouBOL Dash v0.1 Beta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.bolTab.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lapCounter)).EndInit();
            this.enemyPointsTab.ResumeLayout(false);
            this.checkpointGroupTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.checkpointTab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.routeGroupTab.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.routeTab.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.itemsTab.ResumeLayout(false);
            this.startingPointTab.ResumeLayout(false);
            this.areaTab.ResumeLayout(false);
            this.cameraTab.ResumeLayout(false);
            this.respawnTab.ResumeLayout(false);
            this.routeContext.ResumeLayout(false);
            this.objectContext.ResumeLayout(false);
            this.enemyRouteContext.ResumeLayout(false);
            this.checkpointGroupContext.ResumeLayout(false);
            this.checkpointContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.unknown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown8)).EndInit();
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
        private System.Windows.Forms.ToolStripLabel selectionInfo;
        public OpenTK.GLControl glControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage enemyPointsTab;
        private System.Windows.Forms.TabPage checkpointTab;
        private System.Windows.Forms.ListBox chckList;
        private System.Windows.Forms.TabPage routeTab;
        private System.Windows.Forms.ListBox routeList;
        private System.Windows.Forms.TabPage itemsTab;
        private System.Windows.Forms.ListBox objList;
        private System.Windows.Forms.TabPage startingPointTab;
        private System.Windows.Forms.ListBox kartPointList;
        private System.Windows.Forms.TabPage areaTab;
        private System.Windows.Forms.ListBox areaList;
        private System.Windows.Forms.TabPage cameraTab;
        private System.Windows.Forms.ListBox cameraList;
        private System.Windows.Forms.TabPage respawnTab;
        private System.Windows.Forms.ListBox enemyRouteList;
        public System.Windows.Forms.ListBox respList;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BMDViewerToolStripMenuItem;
        private System.Windows.Forms.TabPage bolTab;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ComboBox musicSelect;
        public System.Windows.Forms.NumericUpDown lapCounter;
        public System.Windows.Forms.NumericUpDown unknown3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown unknown5;
        public System.Windows.Forms.NumericUpDown unknown4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown musicInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.PropertyGrid propertyGrid3;
        private System.Windows.Forms.PropertyGrid propertyGrid4;
        private System.Windows.Forms.PropertyGrid propertyGrid5;
        private System.Windows.Forms.PropertyGrid propertyGrid6;
        private System.Windows.Forms.PropertyGrid propertyGrid7;
        private System.Windows.Forms.PropertyGrid propertyGrid8;
        private System.Windows.Forms.ToolStripMenuItem bTIViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertCourseModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip routeContext;
        private System.Windows.Forms.ToolStripMenuItem selectAllInGroupsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip objectContext;
        private System.Windows.Forms.ToolStripMenuItem addObjectToolStripMenuItem;
        private System.Windows.Forms.TabPage routeGroupTab;
        private System.Windows.Forms.TabPage checkpointGroupTab;
        private System.Windows.Forms.PropertyGrid propertyGrid9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox chckGroup;
        private System.Windows.Forms.PropertyGrid propertyGrid10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox routeGroupList;
        private System.Windows.Forms.ContextMenuStrip enemyRouteContext;
        private System.Windows.Forms.ToolStripMenuItem insertRouteHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRouteAtBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicatePointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRoutePointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bCOToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateObjectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip checkpointGroupContext;
        private System.Windows.Forms.ToolStripMenuItem addCheckpointGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCheckpointGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateCheckpointGroupToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip checkpointContext;
        private System.Windows.Forms.ToolStripMenuItem addCheckpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertCheckpointHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCheckpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateCheckpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllInGroupToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown unknown8;
        public System.Windows.Forms.NumericUpDown unknown7;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
    }
}

