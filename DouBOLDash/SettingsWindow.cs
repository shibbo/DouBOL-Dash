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
using System;
using System.Windows.Forms;

namespace DouBOLDash
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.isWireframe == true)
                enableWireframe.Checked = true;
            else
                enableWireframe.Checked = false;
            if (Properties.Settings.Default.showAxis == true)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
            if (Properties.Settings.Default.showEnemyRoutes == true)
                showEnemyRoutes.Checked = true;
            else
                showEnemyRoutes.Checked = false;
            if (Properties.Settings.Default.showRoutes == true)
                showPaths.Checked = true;
            else
                showPaths.Checked = false;
            if (Properties.Settings.Default.showCheckpoints == true)
                showCheckpoints.Checked = true;
            else
                showCheckpoints.Checked = false;
            if (Properties.Settings.Default.showItems == true)
                showObjects.Checked = true;
            else
                showObjects.Checked = false;
            if (Properties.Settings.Default.showStarting == true)
                showKartPos.Checked = true;
            else
                showKartPos.Checked = false;
            if (Properties.Settings.Default.showAreas == true)
                showAreas.Checked = true;
            else
                showAreas.Checked = false;
            if (Properties.Settings.Default.showCameras == true)
                showCameras.Checked = true;
            else
                showCameras.Checked = false;
            if (Properties.Settings.Default.showRespawns == true)
                showRespawns.Checked = true;
            else
                showRespawns.Checked = false;
        }

        private void enableWireframe_CheckedChanged(object sender, EventArgs e)
        {
            if (enableWireframe.Checked)
            {
                Properties.Settings.Default.isWireframe = true;
                Properties.Settings.Default.Save();
                MainWindow.changeGLRender(true);
            }
            else
            {
                Properties.Settings.Default.isWireframe = false;
                Properties.Settings.Default.Save();
                MainWindow.changeGLRender(false);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.showAxis = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showAxis = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void showEnemyRoutes_CheckedChanged(object sender, EventArgs e)
        {
            if (showEnemyRoutes.Checked)
            {
                Properties.Settings.Default.showEnemyRoutes = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showEnemyRoutes = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showPaths_CheckedChanged(object sender, EventArgs e)
        {
            if (showPaths.Checked)
            {
                Properties.Settings.Default.showRoutes = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showRoutes = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showCheckpoints_CheckedChanged(object sender, EventArgs e)
        {
            if (showCheckpoints.Checked)
            {
                Properties.Settings.Default.showCheckpoints = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showCheckpoints = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showObjects_CheckedChanged(object sender, EventArgs e)
        {
            if (showObjects.Checked)
            {
                Properties.Settings.Default.showItems = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showItems = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showKartPos_CheckedChanged(object sender, EventArgs e)
        {
            if (showKartPos.Checked)
            {
                Properties.Settings.Default.showStarting = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showStarting = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showRespawns_CheckedChanged(object sender, EventArgs e)
        {
            if (showRespawns.Checked)
            {
                Properties.Settings.Default.showRespawns = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showRespawns = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (showAreas.Checked)
            {
                Properties.Settings.Default.showAreas = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showAreas = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }

        private void showCameras_CheckedChanged(object sender, EventArgs e)
        {
            if (showCameras.Checked)
            {
                Properties.Settings.Default.showCameras = true;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
            else
            {
                Properties.Settings.Default.showCameras = false;
                Properties.Settings.Default.Save();
                MainWindow.refreshGL();
            }
        }
    }
}
