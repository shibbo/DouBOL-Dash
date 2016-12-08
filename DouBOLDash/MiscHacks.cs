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
            {"1195", "cannon1"},
            {"11A1", "nossie"},
            {"125D", "dossun1"},
            {"1389", "sanbo1"},
            {"1392", "deserttree1"},
            {"138F", "antlion"},
            {"13ED", "snowrock1"}
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
    }
}
