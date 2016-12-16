namespace DouBOLDash
{
    partial class SettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.buttonOK = new System.Windows.Forms.Button();
            this.enableWireframe = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.showEnemyRoutes = new System.Windows.Forms.CheckBox();
            this.showPaths = new System.Windows.Forms.CheckBox();
            this.showCheckpoints = new System.Windows.Forms.CheckBox();
            this.showObjects = new System.Windows.Forms.CheckBox();
            this.showKartPos = new System.Windows.Forms.CheckBox();
            this.showRespawns = new System.Windows.Forms.CheckBox();
            this.showAreas = new System.Windows.Forms.CheckBox();
            this.showCameras = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(142, 218);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // enableWireframe
            // 
            this.enableWireframe.AutoSize = true;
            this.enableWireframe.Location = new System.Drawing.Point(8, 12);
            this.enableWireframe.Name = "enableWireframe";
            this.enableWireframe.Size = new System.Drawing.Size(110, 17);
            this.enableWireframe.TabIndex = 1;
            this.enableWireframe.Text = "Enable Wireframe";
            this.enableWireframe.UseVisualStyleBackColor = true;
            this.enableWireframe.CheckedChanged += new System.EventHandler(this.enableWireframe_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(8, 35);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Show Axis";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // showEnemyRoutes
            // 
            this.showEnemyRoutes.AutoSize = true;
            this.showEnemyRoutes.Location = new System.Drawing.Point(8, 57);
            this.showEnemyRoutes.Name = "showEnemyRoutes";
            this.showEnemyRoutes.Size = new System.Drawing.Size(125, 17);
            this.showEnemyRoutes.TabIndex = 3;
            this.showEnemyRoutes.Text = "Show Enemy Routes";
            this.showEnemyRoutes.UseVisualStyleBackColor = true;
            this.showEnemyRoutes.CheckedChanged += new System.EventHandler(this.showEnemyRoutes_CheckedChanged);
            // 
            // showPaths
            // 
            this.showPaths.AutoSize = true;
            this.showPaths.Location = new System.Drawing.Point(8, 80);
            this.showPaths.Name = "showPaths";
            this.showPaths.Size = new System.Drawing.Size(83, 17);
            this.showPaths.TabIndex = 4;
            this.showPaths.Text = "Show Paths";
            this.showPaths.UseVisualStyleBackColor = true;
            this.showPaths.CheckedChanged += new System.EventHandler(this.showPaths_CheckedChanged);
            // 
            // showCheckpoints
            // 
            this.showCheckpoints.AutoSize = true;
            this.showCheckpoints.Location = new System.Drawing.Point(8, 103);
            this.showCheckpoints.Name = "showCheckpoints";
            this.showCheckpoints.Size = new System.Drawing.Size(115, 17);
            this.showCheckpoints.TabIndex = 5;
            this.showCheckpoints.Text = "Show Checkpoints";
            this.showCheckpoints.UseVisualStyleBackColor = true;
            this.showCheckpoints.CheckedChanged += new System.EventHandler(this.showCheckpoints_CheckedChanged);
            // 
            // showObjects
            // 
            this.showObjects.AutoSize = true;
            this.showObjects.Location = new System.Drawing.Point(8, 126);
            this.showObjects.Name = "showObjects";
            this.showObjects.Size = new System.Drawing.Size(92, 17);
            this.showObjects.TabIndex = 6;
            this.showObjects.Text = "Show Objects";
            this.showObjects.UseVisualStyleBackColor = true;
            this.showObjects.CheckedChanged += new System.EventHandler(this.showObjects_CheckedChanged);
            // 
            // showKartPos
            // 
            this.showKartPos.AutoSize = true;
            this.showKartPos.Location = new System.Drawing.Point(8, 149);
            this.showKartPos.Name = "showKartPos";
            this.showKartPos.Size = new System.Drawing.Size(137, 17);
            this.showKartPos.TabIndex = 7;
            this.showKartPos.Text = "Show Starting Positions";
            this.showKartPos.UseVisualStyleBackColor = true;
            this.showKartPos.CheckedChanged += new System.EventHandler(this.showKartPos_CheckedChanged);
            // 
            // showRespawns
            // 
            this.showRespawns.AutoSize = true;
            this.showRespawns.Location = new System.Drawing.Point(8, 172);
            this.showRespawns.Name = "showRespawns";
            this.showRespawns.Size = new System.Drawing.Size(106, 17);
            this.showRespawns.TabIndex = 8;
            this.showRespawns.Text = "Show Respawns";
            this.showRespawns.UseVisualStyleBackColor = true;
            this.showRespawns.CheckedChanged += new System.EventHandler(this.showRespawns_CheckedChanged);
            // 
            // showAreas
            // 
            this.showAreas.AutoSize = true;
            this.showAreas.Location = new System.Drawing.Point(8, 195);
            this.showAreas.Name = "showAreas";
            this.showAreas.Size = new System.Drawing.Size(83, 17);
            this.showAreas.TabIndex = 9;
            this.showAreas.Text = "Show Areas";
            this.showAreas.UseVisualStyleBackColor = true;
            this.showAreas.CheckedChanged += new System.EventHandler(this.showAreas_CheckedChanged);
            // 
            // showCameras
            // 
            this.showCameras.AutoSize = true;
            this.showCameras.Location = new System.Drawing.Point(8, 218);
            this.showCameras.Name = "showCameras";
            this.showCameras.Size = new System.Drawing.Size(97, 17);
            this.showCameras.TabIndex = 10;
            this.showCameras.Text = "Show Cameras";
            this.showCameras.UseVisualStyleBackColor = true;
            this.showCameras.CheckedChanged += new System.EventHandler(this.showCameras_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 254);
            this.Controls.Add(this.showCameras);
            this.Controls.Add(this.showAreas);
            this.Controls.Add(this.showRespawns);
            this.Controls.Add(this.showKartPos);
            this.Controls.Add(this.showObjects);
            this.Controls.Add(this.showCheckpoints);
            this.Controls.Add(this.showPaths);
            this.Controls.Add(this.showEnemyRoutes);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.enableWireframe);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.Text = "DouBOL Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox enableWireframe;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox showEnemyRoutes;
        private System.Windows.Forms.CheckBox showPaths;
        private System.Windows.Forms.CheckBox showCheckpoints;
        private System.Windows.Forms.CheckBox showObjects;
        private System.Windows.Forms.CheckBox showKartPos;
        private System.Windows.Forms.CheckBox showRespawns;
        private System.Windows.Forms.CheckBox showAreas;
        private System.Windows.Forms.CheckBox showCameras;
    }
}