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
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace DouBOLDash
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public string NEXT_VER = "v0.2";

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            doUpdateCheck(false);
        }

        string lines = "";
        public void doUpdateCheck(bool showBox)
        {
            try
            {
                if (Properties.Settings.Default.knowsNewUpdate == false)
                {
                    HttpWebRequest request = WebRequest.Create("https://github.com/Reanyboi/DouBOL-Dash/releases/tag/" + NEXT_VER) as HttpWebRequest;
                    request.Method = "HEAD";
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    response.Close();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        button3.Enabled = false;

                        label1.Text = "An update (" + NEXT_VER + ") is available!\nWould you like to download it?";

                        using (var reader = new StreamReader("changelog.txt"))
                        {
                            lines = reader.ReadToEnd();
                        }

                        richTextBox1.Text = lines;
                    }

                }
            }
            catch
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;

                if (showBox)
                {
                    MessageBox.Show("There is no update available.", "No Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    label1.Text = "There is no update available.";

                    richTextBox1.Text = "No update information.";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Reanyboi/DouBOL-Dash/releases/tag/" + NEXT_VER);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // if the user wants to check for updates on startup
            // this will always be false, so the update dialog will always show
            if (Properties.Settings.Default.checkForUpdate == false)
            {
                Properties.Settings.Default.knowsNewUpdate = true;
                Properties.Settings.Default.Save();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
