using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
