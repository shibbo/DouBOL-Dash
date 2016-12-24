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
            setupObjects();
            label2.AutoSize = true;
            label2.MaximumSize = new Size(272, 0);
        }

        ListBox rofl;
        public void getList(ListBox list)
        {
            rofl = list;
        }

        Dictionary<string, string> objNames = new Dictionary<string, string>()
        {
            {"1", "Item Box"},
            {"3", "Unknown Object"},
            {"7", "Unknown Object"},
            {"9", "Unknown Object"},
            {"A", "Route Controlled Item Box"},
            {"E", "Unknown Object"},
            {"10", "Unknown Object"},
            {"11", "Unknown Object"},
            {"13", "Unknown Object"},
            {"14", "Unknown Object"},
            {"CE5", "Unknown Object"},
            {"CE6", "Unknown Object"},
            {"CE8", "Unknown Object"},
            {"D49", "Unknown Object"},
            {"D4A", "Cataquack"},
            {"D4B", "Seagull Flock"},
            {"D4D", "Peach Beach Tree"},
            {"D4E", "Unknown Object"},
            {"D4F", "Noki Spectator A"},
            {"D52", "Pianta Spectator A"},
            {"D53", "Pianta Spectator B"},
            {"D54", "Pianta Spectator C"},
            {"D7A", "Unknown Object"},
            {"D7E", "Unknown Object"},
            {"DAF", "Daisy Cruiser Pool"},
            {"DAE", "Daisy Cruiser Table"},
            {"DB1", "Unknown Object"},
            {"E0E", "Unknown Object"},
            {"E0F", "Unknown Object"},
            {"E75", "Mario Circuit Tree"},
            {"E77", "Mario Circuit Flower"},
            {"E78", "Chain Chomp"},
            {"E7E", "Luigi Circuit Blimp"},
            {"E7F", "Goomba"},
            {"E80", "Piranha Plant (in pipe)"},
            {"E82", "Mario Circuit Air Balloon"},
            {"ED9", "Yoshi Circuit Helicopter"},
            {"FA1", "Unknown Object"},
            {"FA2", "Unknown Object"},
            {"FA3", "Unknown Object"},
            {"FA4", "Mushroom City Traffic Light"},
            {"FA5", "Unknown Object"},
            {"FA6", "Unknown Object"},
            {"FA8", "Unknown Object"},
            {"FA9", "Unknown Object"},
            {"1069", "Unknown Object"},
            {"106B", "Waluigi Stadium Fire Ring"},
            {"106D", "Waluigi Stadium Screen"},
            {"106E", "Waluigi Stadium Piranha Plant"},
            {"106F", "Waluigi Stadium Arrow Sign"},
            {"1070", "Unknown Object"},
            {"1071", "Unknown Object"},
            {"1072", "Unknown Object"},
            {"1195", "DK Mountain Cannon"},
            {"1196", "DK Mountain Rock"},
            {"1198", "DK Mountain Tree"},
            {"1199", "Unknown Object"},
            {"119A", "Butterfly Group"},
            {"119C", "Bird Flock"},
            {"119E", "Water Geyser"},
            {"11A1", "Dinosaur (Land)"},
            {"11A4", "Dino Dino Jungle Tree"},
            {"11A5", "Dinosaur (Water)"},
            {"11A6", "Dinosaur (Flying)"},
            {"125D", "Thwomp"},
            {"125E", "Lavaball"},
            {"125F", "Metal Bowser Statue"},
            {"1261", "Unknown Object"},
            {"1262", "Fire Ring"},
            {"1327", "Unknown Object"},
            {"1389", "Pokey"},
            {"138B", "Dry Dry Desert Tornado"},
            {"138C", "Unknown Object"},
            {"138F", "Antlion"},
            {"1390", "Unknown Object"},
            {"1391", "Unknown Object"},
            {"1392", "Dry Dry Desert Tree"},
            {"13ED", "Iceberg"},
            {"13EE", "Skating Shy Guys"},
            {"13F0", "Sherbet Land Snowman"},
            {"13F3", "Sherbet Land Tree Light"},
            {"13F4", "Sherbet Land Snow Drift"},
            {"26B2", "Unknown Object"},
            {"26B3", "Unknown Object"}
        };

        public void setupObjects()
        {
            foreach (KeyValuePair<string, string> obj in objNames)
            {
                listBox1.Items.Add(obj.Value);
            }
        }

        Dictionary<int, string> descList = new Dictionary<int, string>() {
            {0, "A regular item box. Item boxes generate an item for the player, and can be singular, double, or quadtruple."},
            {4, "A route controlled item box. It functions the same as item boxes, except that it needs a route. Item boxes generate an item for the player, and can be singular, double, or quadtruple. \n\nNote: This needs a path to function properly!"},
            {14, "An enemy that will spring the player up into the air if you run into it! Beware!\n\nNote: Needs a route to function!"},
            {15, "A nice seagull flock for decoration.\n\nNote: Needs a route to function!"},
            {16, "A tree from Peach Beach."},
            {18, "A Noki from Super Mario Sunshine. Is placed out of bounds of the player for functionality."},
            {19, "A pianta from Super Mario Sunshine. Is placed out of bounds of the player for functionality." },
            {20, "A pianta from Super Mario Sunshine. Is placed out of bounds of the player for functionality." },
            {21, "A pianta from Super Mario Sunshine. Is placed out of bounds of the player for functionality." },
            {31, "A chain chomp that hops around stuck to a chain. Every now and then, it will extend and attack players nearby."},
            {32, "A blimp with a logo of Luigi on it.\n\nNote: Needs a route to function!"},
            {33, "One of the oldest enemies that Mario knows. Waddles back and forth, and will spin the player out upon contact.\n\nNote: Needs a route to function!"},
            {34, "A piranha plant in a pipe. When the player approaches, the piranha plant will attempt to chomp on the player, knocking them over."},
            {57, "A group of butterflies that fly around.\n\nNote: Needs a route to function!"},
            {58, "A flock of birds.\n\nNote: Needs a route to function!"},
            {59, "A geyser of water that will send the player flying in the air if the player collides with it."},
            {60, "A dinosaur that stomps the player with 4 feet. If the player is directly under the foot stomped, they will be flattened. If they collide with the leg or face, they will spin out."},
            {61, "A tree from the track Dino Dino Jungle."},
            {62, "A dinosaur that swims in the water.\n\nNote: Needs a route to function!"},
            {63, "A dinosaur that flies through the air.\n\nNote: Needs a route to function!"},
            {64, "An enemy that can crush the player. If hit from the side, it will spin the player out."},
            {65, "A ball of lava that rises from lava and can burn the player upon contact."},
            {66, "A statue of Bowser that moves back and forth and shoots fireballs.\n\nNote: Needs a route to function!"},
            {68, "A ring of fireballs that burn the player upon contact."},
            {70, "A spikey desert enemy that bends back and forth."},
        };

        Dictionary<int, string> indexToObject = new Dictionary<int, string>()
        {
            {0, "1"},
            {1, "3"},
            {2, "7"},
            {3, "9"},
            {4, "A"},
            {5, "E"},
            {6, "10"},
            {7, "11"},
            {8, "13"},
            {9, "14"},
            {10, "CE5"},
            {11, "CE6"},
            {12, "CE8"},
            {13, "D49"},
            {14, "D4A"},
            {15, "D4B"},
            {16, "D4D"},
            {17, "D4E"},
            {18, "D4F"},
            {19, "D52"},
            {20, "D53"},
            {21, "D54"},
            {22, "D7A"},
            {23, "D7E"},
            {24, "DAF"},
            {25, "DAE"},
            {26, "DB1"},
            {27, "E0E"},
            {28, "E0F"},
            {29, "E75"},
            {30, "E77"},
            {31, "E78"},
            {32, "E7E"},
            {33, "E7F"},
            {34, "E80"},
            {35, "E82"},
            {36, "ED9"},
            {37, "FA1"},
            {38, "FA2"},
            {39, "FA3"},
            {40, "FA4"},
            {41, "FA5"},
            {42, "FA6"},
            {43, "FA8"},
            {44, "FA9"},
            {45, "1069"},
            {46, "106B"},
            {47, "106D"},
            {48, "106E"},
            {49, "106F"},
            {50, "1070"},
            {51, "1071"},
            {52, "1072"},
            {53, "1195"},
            {54, "1196"},
            {55, "1198"},
            {56, "1199"},
            {57, "119A"},
            {58, "119C"},
            {59, "119E"},
            {60, "11A1"},
            {61, "11A4"},
            {62, "11A5"},
            {63, "11A6"},
            {64, "125D"},
            {65, "125E"},
            {66, "125F"},
            {67, "1261"},
            {68, "1262"},
            {69, "1327"},
            {70, "1389"},
            {71, "138B"},
            {72, "138C"},
            {73, "138F"},
            {74, "1390"},
            {75, "1391"},
            {76, "1392"},
            {77, "13ED"},
            {78, "13EE"},
            {79, "13F0"},
            {80, "13F3"},
            {81, "13F4"},
            {82, "26B2"},
            {83, "26B3"}
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

        
        public void setObjectID(int index)
        {
            int count = 0;
            foreach(KeyValuePair<string, string> obj in objNames)
            {
                if (index == count)
                {
                    idLabel.Text = "Object ID: " + obj.Key;
                    break;
                }
                else
                    count += 1;
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // update 2 UI things
            setDesc(listBox1.SelectedIndex);
            setObjectID(listBox1.SelectedIndex);
            label1.Text = listBox1.Text;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string objIDStr;
            ushort objID;

            if (indexToObject.ContainsKey(index))
            {
                objIDStr = indexToObject[index];
            }
            else
                objIDStr = "1"; // item box

            objID = Convert.ToUInt16(objIDStr, 16);

            LevelObject trackObj = new LevelObject();
            trackObj.objID = objID;
            trackObj.routeID = -1;

            MiscHacks misc = new MiscHacks();
            trackObj.modelName = misc.returnModel(objID);
            trackObj.friendlyName = misc.returnName(objID);

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
            trackObj.routeID = -1;
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

