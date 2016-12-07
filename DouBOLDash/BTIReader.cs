using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DouBOLDash
{
    public partial class BTIReader : Form
    {
        public BTIReader()
        {
            InitializeComponent();
        }

        Image bti;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BTI files (*.bti)|*.bti|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileBase fileb = new FileBase();
                fileb.Stream = new FileStream(openFileDialog1.FileName, FileMode.Open);
                bti = BTIFile.ReadBTIToBitmap(fileb);
                pictureBox1.Image = bti;
                fileb.Close();
            }
        }
    }
}
