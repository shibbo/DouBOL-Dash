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

                    uint offs0 = bol.returnOffset(0); // enemy/item point offset
                    uint offs1 = bol.returnOffset(1); // checkpoint groups
                    uint offs2 = bol.returnOffset(2); // route group offset
                    uint offs3 = bol.returnOffset(3); // route point offset
                    uint offs4 = bol.returnOffset(4); // object offset
                    uint offs5 = bol.returnOffset(5); // kart point offset 
                    uint offs6 = bol.returnOffset(6); // area offset

                    uint count0 = bol.returnCount(0); // enemy/item route count
                    uint count1 = bol.returnCount(1); // group count
                    uint count2 = bol.returnCount(2); // object count
                    uint count3 = bol.returnCount(3); // area count
                    uint count4 = bol.returnCount(4); // camera count
                    uint count5 = bol.returnCount(5); // route count (unused)
                    uint count6 = bol.returnCount(6); // respawn point count

                    uint sec1Count = 0;
                    if (count0 > 0)
                    {
                        // this is a failsafe that the old BOL editor doesn't do
                        sec1Count = offs1 - offs0;
                        sec1Count = sec1Count / 0x20;
                    }
                    else
                        sec1Count = 0;

                    uint sec3Count = offs3 - offs2;
                    sec3Count = sec3Count / 0x10;

                    EnemyRoutes enmRoutes = new EnemyRoutes();
                    enmRoutes.Parse(reader, sec1Count);

                    CheckpointGroup chckGroups = new CheckpointGroup();
                    chckGroups.Parse(reader, count1);
                    Dictionary<uint, uint> dictionary1 = chckGroups.returnDictionary();

                    Checkpoint chckPt = new Checkpoint();
                    chckPt.Parse(reader, dictionary1, count1);

                    RouteGroup routeGrp = new RouteGroup();
                    routeGrp.Parse(reader, sec3Count);
                    Dictionary<uint, GroupStruct> dictionary2 = routeGrp.returnDictionary();

                    RoutePoint routePt = new RoutePoint();
                    routePt.Parse(reader, dictionary2);

                    Object obj = new Object();
                    obj.Parse(reader, count2);

                    KartPoint kart = new KartPoint();
                    kart.Parse(reader);

                    Area area = new Area();
                    area.Parse(reader, count3);

                    Camera camera = new Camera();
                    camera.Parse(reader, count4);

                    Respawn respawn = new Respawn();
                    respawn.Parse(reader, count6);
                }

                foreach (LevelObj obj in objects)
                {
                    // parsing code here
                }
            }
        }
    }
}
