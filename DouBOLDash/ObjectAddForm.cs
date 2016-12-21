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

