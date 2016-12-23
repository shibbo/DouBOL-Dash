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
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImage = new SaveFileDialog();
            saveImage.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";

            if (saveImage.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveImage.FileName, ImageFormat.Png);
            }
        }
    }
}
