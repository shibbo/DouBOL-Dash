using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DouBOLDash
{
    public partial class Form1 : Form
    {

        List<LevelObj> objects = new List<LevelObj>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BOL files (*.bol)|*.bol|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open)))
                {
                    BOL bol = new BOL();
                    bol.Parse(reader);
                    objects.Add(bol); // add the BOL section to the LevelObj list

                    uint offs1 = bol.returnOffset(0);
                    uint offs2 = bol.returnOffset(1);
                    uint offs3 = bol.returnOffset(2);

                    // despite us having the numbers for this, we cannot use them
                    // not every single section has a count, so let's keep the methods the same
                    uint sec1Count = offs2 - offs1;
                    sec1Count = sec1Count / 0x20;

                    EnemyRoutes enmRoutes = new EnemyRoutes();

                    reader.Close(); // close the reader at the end
                }

                foreach (LevelObj obj in objects)
                {
                    Console.WriteLine(obj.ToString());
                }
            }
        }
    }
}
