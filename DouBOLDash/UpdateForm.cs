using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Linq;

namespace DouBOLDash
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public string NEXT_VER = "v0.1";

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
