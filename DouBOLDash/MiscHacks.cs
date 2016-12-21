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
using System.Windows.Forms;
using System.Drawing;

namespace DouBOLDash
{
    class MiscHacks
    {
        // the algorithm for calculating the rotations
        // Z rotation is always a constant
        public static double returnRotations(int xrot, int yrot)
        {
            double angle1;
            double radToDeg = 57.2957795;
            angle1 = radToDeg * Math.Atan2((double)yrot, (double)xrot);
            angle1 = angle1 - 90;

            return Math.Round(angle1);
        }

        public static void inverseRotations(int angle, out double x, out double y)
        {
            const int length = 655360000;
            const double radToDeg = 57.2957795;

            x = length * Math.Cos((double)angle / radToDeg);
            y = length * Math.Sin((double)angle / radToDeg);
        }

        // dictionary that gets a model name based on the object id
        Dictionary<string, string> objDict = new Dictionary<string, string>()
        {
            {"D4A", "poihana1"},
            {"D4D", "peachtree1"},
            //{"DAF", "pool"},
            {"E75", "mariotree1"},
            {"E77", "marioflower1"},
            {"E78", "wanwan1"},
            {"E7E", "skyship1"},
            {"E7F", "kuribo1"},
            {"E80", "pakkun"},
            {"E82", "mash_balloon"},
            {"106D", "wl_screen1"},
            {"106F", "wlarrow1"},
            {"1195", "cannon1"},
            {"11A1", "nossie"},
            {"11A4", "dinotree1"},
            {"11A5", "swimnossie"},
            {"11A6", "ptera"},
            {"125D", "dossun1"},
            {"1389", "sanbo1"},
            {"1392", "deserttree1"},
            {"138F", "antlion"},
            {"13ED", "snowrock1"}
        };

        Dictionary<string, string> objNames = new Dictionary<string, string>()
        {
            {"1", "Item Box"},
            {"A", "Route Controlled Item Box"},
            {"D4A", "Cataquack"},
            {"D4B", "Seagull Flock"},
            {"D4D", "Peach Beach Tree"},
            {"D4F", "Noki Spectator A"},
            {"D52", "Pianta Spectator A"},
            {"D53", "Pianta Spectator B"},
            {"D54", "Pianta Spectator C"},
            {"DAF", "Daisy Cruiser Pool"},
            {"DAE", "Daisy Cruiser Table"},
            {"E75", "Mario Circuit Tree"},
            {"E77", "Mario Circuit Flower"},
            {"E78", "Chain Chomp"},
            {"E7E", "Luigi Circuit Blimp"},
            {"E7F", "Goomba"},
            {"E80", "Piranha Plant (in pipe)"},
            {"E82", "Mario Circuit Air Balloon"},
            {"ED9", "Yoshi Circuit Helicopter"},
            {"FA4", "Mushroom City Traffic Light"},
            {"106B", "Waluigi Stadium Fire Ring"},
            {"106D", "Waluigi Stadium Screen"},
            {"106E", "Waluigi Stadium Piranha Plant"},
            {"106F", "Waluigi Stadium Arrow Sign"},
            {"1195", "DK Mountain Cannon"},
            {"1196", "DK Mountain Rock"},
            {"1198", "DK Mountain Tree"},
            {"119E", "Water Geyser"},
            {"11A1", "Dinosaur (Land)"},
            {"11A4", "Dino Dino Jungle Tree"},
            {"11A5", "Dinosaur (Water)"},
            {"11A6", "Dinosaur (Flying)"},
            {"125D", "Thwomp"},
            {"125E", "Lavaball"},
            {"1262", "Fire Ring"},
            {"1389", "Pokey"},
            {"138B", "Dry Dry Desert Tornado"},
            {"138F", "Antlion"},
            {"1392", "Dry Dry Desert Tree"},
            {"13ED", "Iceberg"},
            {"13EE", "Skating Shy Guys"},
            {"13F0", "Sherbet Land Snowman"},
            {"13F3", "Sherbet Land Tree Light"},
            {"13F4", "Sherbet Land Snow Drift"}
        };

        // a list of items that need routes to function (or whatnot)
        // when this is gone through it will find ones that have -1 as the route when it should be > -1
        List<uint> needRoute = new List<uint>
        {
            {10},
            {3403},
            {3502},
            {3599},
            {3710},
            {3801},
            {4001},
            {4002},
            {4003},
            {4005},
            {4006},
            {4008},
            {4009},
            {4206},
            {4502},
            {4517},
            {4518},
            {5003},
            {5102}
        };

        List<uint> hasNoRoute = new List<uint>();

        // true == has routes that need fixing
        // false == doesn't have any flaws in routes
        public bool checkForRoute(List<LevelObject> levelObj)
        {
            // go through each object in the current list upon saving
            foreach (LevelObject listObj in levelObj)
            {
                // store the current ID in objectID
                uint objectID = listObj.objID;
                // if the route is -1 (because we can suspect that there needs to be a route)
                if (listObj.routeID == -1)
                {
                    // we go through each id in the above list (objects that need a route)
                    foreach (uint currentID in needRoute)
                    {
                        // if the ID on the list is the object id, it needs a route
                        if (currentID == objectID)
                        {
                            // add it to the list of items that need a route
                            // we will yell at the user later
                            hasNoRoute.Add(objectID);
                        }
                    }
                }
            }

            // this means that there are objects that need a route.
            if (hasNoRoute.Count > 0)
            {
                // empty message
                string message = "";
                // go through each route
                foreach (uint route in hasNoRoute)
                {
                    // add the route name to a string to be able to insert into message
                    string objName = returnName(route);
                    message += route.ToString() + " (" + objName + ")\n";
                }
                // display message
                MessageBox.Show("There are routes missing! Objects that are missing routes: \n" + message + "These need to be corrected in order for the file to save.");
                // houston we have a problem
                return true;
            }
            else
            {
                // no problem
                return false;
            }
        }

        // file names, seperated by colons
        // this will help finding files if they don't exist for an object
        Dictionary<string, string> fileNames = new Dictionary<string, string>()
        {
            {"106F", "wlarrow1.bck:wlarrow1.bmd:wlarrow1.btk"}
        };

        string name, obj;
        public string returnModel(uint objectID)
        {
            obj = objectID.ToString("X");
            if (objDict.ContainsKey(obj))
            {
                name = objDict[obj];
            }
            else
                name = "null";

            return name;
        }

        // returns a user friendly name
        public string returnName(uint objectID)
        {
            obj = objectID.ToString("X");
            if (objNames.ContainsKey(obj))
            {
                name = objNames[obj];
            }
            else
                name = "Unknown";

            return name;
        }

        // this is for BCO
        Dictionary<int, Color> colToColor = new Dictionary<int, Color>
        {
            {0x0000, Color.Brown}, // Sand
            {0x0001, Color.RosyBrown}, // Offroad mud
            {0x0100, Color.DarkRed}, // Road
            {0x0101, Color.SaddleBrown}, // Bridge / Wood
            {0x0102, Color.Silver}, // Cage Road
            {0x0103, Color.Brown}, // Dirt Road
            {0x0104, Color.PaleVioletRed}, // Carpet
            {0x0300, Color.DarkGreen}, // Grass
            {0x0400, Color.LightSkyBlue}, // Slippery Ice
            {0x0504, Color.MediumPurple}, // Out of bounds 1
            {0x0505, Color.Purple}, // Out of bounds 2
            {0x0800, Color.LightGreen}, // Speed Boost
            {0x0C00, Color.SandyBrown}, // Sand (Offroad)
            {0x0F01, Color.Orange}, // Lava (might just be like water?)
            {0x1000, Color.DarkOrange}, // Quicksand Sinkhole
            {0x1200, Color.Brown}, // Wall
            {0x1300, Color.DarkKhaki} // Sand out of bounds
        };

        // returns a color based on the collision id
        // uses the colToColor dictionary to get the color value
        public Color returnColor(int collisionFlag)
        {
            Color colColor;

            if (colToColor.ContainsKey(collisionFlag))
            {
                colColor = colToColor[collisionFlag];
            }
            else
                colColor = Color.DeepSkyBlue;

            return colColor;
        }
    }
}
