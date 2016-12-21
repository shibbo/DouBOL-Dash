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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.showVerts = new System.Windows.Forms.CheckBox();
            this.showTris = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(273, 332);
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
            this.enableWireframe.Location = new System.Drawing.Point(6, 19);
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
            this.checkBox1.Location = new System.Drawing.Point(6, 42);
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
            this.showEnemyRoutes.Location = new System.Drawing.Point(6, 64);
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
            this.showPaths.Location = new System.Drawing.Point(6, 87);
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
            this.showCheckpoints.Location = new System.Drawing.Point(6, 110);
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
            this.showObjects.Location = new System.Drawing.Point(6, 133);
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
            this.showKartPos.Location = new System.Drawing.Point(6, 156);
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
            this.showRespawns.Location = new System.Drawing.Point(6, 179);
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
            this.showAreas.Location = new System.Drawing.Point(6, 202);
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
            this.showCameras.Location = new System.Drawing.Point(6, 225);
            this.showCameras.Name = "showCameras";
            this.showCameras.Size = new System.Drawing.Size(97, 17);
            this.showCameras.TabIndex = 10;
            this.showCameras.Text = "Show Cameras";
            this.showCameras.UseVisualStyleBackColor = true;
            this.showCameras.CheckedChanged += new System.EventHandler(this.showCameras_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.showCameras);
            this.groupBox1.Controls.Add(this.showAreas);
            this.groupBox1.Controls.Add(this.enableWireframe);
            this.groupBox1.Controls.Add(this.showRespawns);
            this.groupBox1.Controls.Add(this.showEnemyRoutes);
            this.groupBox1.Controls.Add(this.showKartPos);
            this.groupBox1.Controls.Add(this.showPaths);
            this.groupBox1.Controls.Add(this.showObjects);
            this.groupBox1.Controls.Add(this.showCheckpoints);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 321);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Course Rendering";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.showTris);
            this.groupBox2.Controls.Add(this.showVerts);
            this.groupBox2.Location = new System.Drawing.Point(162, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 321);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Collision Rendering";
            // 
            // showVerts
            // 
            this.showVerts.AutoSize = true;
            this.showVerts.Location = new System.Drawing.Point(7, 20);
            this.showVerts.Name = "showVerts";
            this.showVerts.Size = new System.Drawing.Size(96, 17);
            this.showVerts.TabIndex = 0;
            this.showVerts.Text = "Show Verticies";
            this.showVerts.UseVisualStyleBackColor = true;
            this.showVerts.CheckedChanged += new System.EventHandler(this.showVerts_CheckedChanged);
            // 
            // showTris
            // 
            this.showTris.AutoSize = true;
            this.showTris.Location = new System.Drawing.Point(6, 42);
            this.showTris.Name = "showTris";
            this.showTris.Size = new System.Drawing.Size(99, 17);
            this.showTris.TabIndex = 1;
            this.showTris.Text = "Show Triangles";
            this.showTris.UseVisualStyleBackColor = true;
            this.showTris.CheckedChanged += new System.EventHandler(this.showTris_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 357);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.Text = "DouBOL Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox showTris;
        private System.Windows.Forms.CheckBox showVerts;
    }
}