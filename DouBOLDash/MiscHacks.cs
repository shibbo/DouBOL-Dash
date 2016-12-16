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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

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
            {"106E", "Waluigi Stadium"},
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
            {"13F3", "Sherbet Land Tree Light"}
        };

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
    }
}
