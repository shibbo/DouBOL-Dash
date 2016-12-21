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
    public partial class ObjectAddForm : Form
    {
        public ObjectAddForm()
        {
            InitializeComponent();
            label2.AutoSize = true;
            label2.MaximumSize = new Size(272, 0);
        }

        ListBox rofl;
        public void getList(ListBox list)
        {
            rofl = list;
        }

        Dictionary<int, string> descList = new Dictionary<int, string>() {
            {0, "A regular item box. Item boxes generate an item for the player, and can be singular, double, or quadtriple."},
            {1, "A route controlled item box. It functions the same as item boxes, except that it needs a route. Item boxes generate an item for the player, and can be singular, double, or quadtriple. Note: This needs a path to function properly!" }
        };

        Dictionary<int, string> indexToObject = new Dictionary<int, string>()
        {

        };

        public void setDesc(int id)
        {
            if (descList.ContainsKey(id))
            {
                label2.Text = descList[id];
            }
            else
                label2.Text = "No description.";
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            setDesc(listBox1.SelectedIndex);
            label1.Text = listBox1.Text;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // here we have to get the object's id from the textbox as a hex string
            // then we convert it to ushort (uint16) and then use that

            string value = textBox1.Text;
            ushort objectID = Convert.ToUInt16(value, 16);

            LevelObject trackObj = new LevelObject();
            trackObj.objID = objectID;
            MiscHacks misc = new MiscHacks();
            trackObj.modelName = misc.returnModel(objectID);
            trackObj.friendlyName = misc.returnName(objectID);

            if (trackObj.modelName != "null")
            {
                FileBase objFB = new FileBase();
                if (File.Exists(Properties.Settings.Default.curDir + "\\objects\\" + trackObj.modelName + ".bmd"))
                {
                    objFB.Stream = new FileStream(Properties.Settings.Default.curDir + "\\objects\\" + trackObj.modelName + ".bmd", FileMode.Open);
                    rofl.Items.Add(trackObj);
                    rofl.Refresh();
                    Close();
                }
                else
                {
                    MessageBox.Show("File \\objects\\" + trackObj.modelName + ".bmd does not exist. Please add this file and it's depencies before you can add this object.");
                    Close();
                    return;
                }
            }
            else
            {
                rofl.Items.Add(trackObj);
                rofl.Refresh();
                Close();
            }
        }
    }
}

