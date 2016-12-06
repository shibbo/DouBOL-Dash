using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DouBOLDash
{
    public partial class MainWindow : Form
    {
        /* Here, variables that will be used throughout the file are here*/
        public string FORM_NAME = "DouBOL Dash";
        public string VERSION = " v0.1 ";
        public string CURRENT_FILE = "";

        private const float k_FOV = (float)((70f * Math.PI) / 180f);
        private const float k_zNear = 0.01f;
        private const float k_zFar = 1000f;

        public bool isSelectionMode = false;

        private float m_AspectRatio;
        private Vector2 m_CamRotation;
        private Vector3 m_CamPosition;
        private Vector3 m_CamTarget;
        private float m_CamDistance;
        private bool m_UpsideDown;
        private Matrix4 m_CamMatrix, m_SkyboxMatrix;
        private RenderInfo m_RenderInfo;

        private MouseButtons m_MouseDown;
        private Point m_LastMouseMove, m_LastMouseClick;
        private float m_PixelFactorX, m_PixelFactorY;

        private const float CUBESIZE = 50f;
        bool loaded = false;

        List<BOLInformation> bolInf;

        List<EnemyRoute> enmRoute;

        List<RoutePointObject> rpobj;
        List<RouteGroupSetup> grpSetup;

        List<CheckpointObject> chkobj;
        List<CheckpointGroupObject> chkGRP;

        List<LevelObject> lvlobj;
        List<KartPointObject> kartobj;
        List<AreaObject> areaobj;
        List<CameraObject> camobj;
        List<RespawnObject> resObj;

        // vars for TrackInfoEditor
        public float unk1;
        public byte musicID, lapCount;

        Dictionary<int, string> idToMusic = new Dictionary<int, string>()
        {
            {0x21, "Baby Park"},
            {0x22, "Peach Beach"},
            {0x23, "Daisy Cruiser"},
            {0x24, "Luigi Circuit"},
            {0x25, "Mario Circuit"},
            {0x26, "Yoshi Circuit"},
            {0x27, "Unknown"},
            {0x28, "Mushroom Bridge"},
            {0x29, "Mushroom City"},
            {0x2A, "Waluigi Stadium"},
            {0x2B, "Wario Colosseum"},
            {0x2C, "Dino Dino Jungle"},
            {0x2D, "DK Mountain"},
            {0x2E, "Unknown"},
            {0x2F, "Bowser's Castle"},
            {0x30, "Unknown"},
            {0x31, "Rainbow Road"},
            {0x32, "Dry Dry Desert"},
            {0x33, "Sherbet Land"}
        };

        Dictionary<string, Bmd> objModelList = new Dictionary<string, Bmd>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Objects list
        int enemyPointList = 0;
        int routePointList = 0;
        int checkpointList = 0;
        int objectList = 0;
        int kartList = 0;
        int areaObjList = 0;
        int camList = 0;
        public int respawnList = 0;

        Image minimap;

        // selected object render box
        int selectedList = 0;

        // BMD pre-render list
        int courseList = 0;
        Bmd course;

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.SizeMode = TabSizeMode.Normal;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ShowToolTips = true;

            foreach(KeyValuePair<int, string> entry in idToMusic)
            {
                musicSelect.Items.Add(entry.Value);
            }

            if (Properties.Settings.Default.envDir == "")
                doFolderChoose();
        }

        private void setMusic(int id)
        {
            if (idToMusic.ContainsKey(id))
                musicSelect.Text = idToMusic[id];
            else
                musicSelect.Text = "wat";
        }

        private void doFolderChoose()
        {
            MessageBox.Show("Please choose the root directory of the filesystem.\nIf one is not chosen, some rendering may not be supported.", "Choose Folder");

            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(fbDialog.SelectedPath + "/Course"))
                {
                    MessageBox.Show("Path successfully changed.", "Success");
                    Properties.Settings.Default.envDir = fbDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.bmdEnabled = true;
                }
                else
                    MessageBox.Show("/Course folder not found. Settings were not changed.", "Invalid Folder");
            }
            else
                MessageBox.Show("Folder path not changed.", "Invalid Folder");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BOL files (*.bol)|*.bol|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                addToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;

                // clear all listboxes
                enemyRouteList.Items.Clear();
                chckGroup.Items.Clear();
                chckList.Items.Clear();
                routeGroupList.Items.Clear();
                routeList.Items.Clear();
                objList.Items.Clear();
                kartPointList.Items.Clear();
                areaList.Items.Clear();
                cameraList.Items.Clear();
                respList.Items.Clear();

                string dirName = Path.GetDirectoryName(openFileDialog1.FileName);
                string fileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                Properties.Settings.Default.curDir = dirName;
                Properties.Settings.Default.curFile = fileName;
                Properties.Settings.Default.Save();

                if (loaded)
                {
                    // if these aren't cleared, then we get an out of bounds
                    GL.DeleteLists(selectedList, 1);
                    GL.DeleteLists(enemyPointList, 1);
                    GL.DeleteLists(routePointList, 1);
                    GL.DeleteLists(courseList, 1);
                    GL.DeleteLists(checkpointList, 1);
                    GL.DeleteLists(objectList, 1);
                }
                using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open)))
                {
                    BOL bol = new BOL();
                    bol.Parse(reader);
                    bolInf = bol.returnList();

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

                    // get the filesize for future offset reference
                    foreach (BOLInformation bolInfo in bolInf)
                    {
                        FileInfo f = new FileInfo(openFileDialog1.FileName);
                        bolInfo.fileSize = (int)f.Length;
                    }

                    EnemyRoutes enmRoutes = new EnemyRoutes();
                    enmRoutes.Parse(reader, sec1Count);
                    enmRoute = enmRoutes.returnList();

                    CheckpointGroup chckGroups = new CheckpointGroup();
                    chckGroups.Parse(reader, count1);
                    Dictionary<uint, uint> dictionary1 = chckGroups.returnDictionary();
                    chkGRP = chckGroups.returnList();

                    Checkpoint chckPt = new Checkpoint();
                    chckPt.Parse(reader, dictionary1, count1);
                    chkobj = chckPt.returnList();

                    RouteGroup routeGrp = new RouteGroup();
                    routeGrp.Parse(reader, sec3Count);
                    Dictionary<uint, RouteGroupSetup> dictionary2 = routeGrp.returnDictionary();
                    grpSetup = routeGrp.returnList();

                    RoutePoint routePt = new RoutePoint();
                    routePt.Parse(reader, dictionary2);
                    rpobj = routePt.returnList();

                    TrackObject obj = new TrackObject();
                    obj.Parse(reader, count2);
                    lvlobj = obj.returnList();

                    KartPoint kart = new KartPoint();
                    kart.Parse(reader);
                    kartobj = kart.returnList();

                    Area area = new Area();
                    area.Parse(reader, count3);
                    areaobj = area.returnList();

                    Camera camera = new Camera();
                    camera.Parse(reader, count4);
                    camobj = camera.returnList();

                    Respawn respawn = new Respawn();
                    respawn.Parse(reader, count6);
                    resObj = respawn.returnList();

                    reader.Close();

                    List<PositionObject> posObjs = new List<PositionObject>();
                    posObjs.Add(kart);

                    // if you think we're loading the course model every build you're wrong
                    // BuildScene will also handle changed objects in the scene, so we don't
                    // want to load the same course model again. waste of time.

                    string bti_path = Properties.Settings.Default.curFile.Replace("course", "map");
                    int mapWidth, mapHeight;

                    FileBase fileb = new FileBase();
                    fileb.Stream = new FileStream(Properties.Settings.Default.curDir  + "\\" + bti_path + ".bti", FileMode.Open);
                    minimap = BTIFile.ReadBTIToBitmap(fileb);
                    pictureBox1.Image = minimap;
                    fileb.Close();

                    courseList = GL.GenLists(1);

                    if (Properties.Settings.Default.bmdEnabled != false)
                    {
                        FileBase fb = new FileBase();
                        if (File.Exists(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile + ".bmd"))
                        {
                            Console.WriteLine(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile + ".bmd");
                            fb.Stream = new FileStream(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile + ".bmd", FileMode.Open);
                            course = new Bmd(fb);
                            fb.Close();
                        }
                        else
                        {
                            Console.WriteLine(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile + ".bmd doesn't exist.");
                            course = null;
                        }
                    }

                    loadingLabel.Text = "Loading course model...";
                    GL.NewList(courseList, ListMode.Compile);
                    if (course != null)
                        DrawBMD(course);
                    GL.EndList();

                    BuildScene();
                }
            }
        }

        public void BuildScene()
        {
            foreach (BOLInformation bolEntry in bolInf)
            {
                musicID = bolEntry.musicID;
                lapCount = bolEntry.numLaps;

                unknown3.Value = (decimal)bolEntry.unk3;
                unknown4.Value = (decimal)bolEntry.unk4;
                unknown5.Value = (decimal)bolEntry.unk5;

                setMusic(musicID);
                lapCounter.Value = lapCount;
            }

            uint count = 0;
            float posX1, posY1, posZ1;
            float posX2 = 0, posY2 = 0, posZ2 = 0;
            enemyPointList = GL.GenLists(1);
            GL.NewList(enemyPointList, ListMode.Compile);
            foreach (EnemyRoute objEntry in enmRoute)
            {
                enemyRouteList.Items.Add(objEntry);
                posX1 = objEntry.xPos;
                posY1 = objEntry.yPos;
                posZ1 = objEntry.zPos;

                if (objEntry.link != -1 && count % 2 == 0)
                {
                    GL.PushMatrix();
                    GL.Translate(posX1, posY1, posZ1);
                    GL.Scale(1f, 1f, 1f);
                    DrawCube(1f, 0f, 1f, true, true, false);
                    GL.PopMatrix();

                    posX2 = objEntry.xPos;
                    posY2 = objEntry.yPos;
                    posZ2 = objEntry.zPos;

                    count += 1;
                }
                else if (objEntry.link != -1)
                {
                    GL.PushMatrix();
                    GL.Translate(posX1, posY1, posZ1);
                    GL.Scale(1f, 1f, 1f);
                    DrawCube(1f, 0f, 1f, true, true, false);
                    GL.PopMatrix();

                    GL.PushMatrix();
                    GL.Begin(BeginMode.Lines);
                    GL.Color4(1f, 0f, 1f, 1f);
                    GL.Vertex3(posX1, posY1, posZ1);
                    GL.Vertex3(posX2, posY2, posZ2);
                    GL.End();
                    GL.PopMatrix();

                    count += 1;
                }
                else
                {
                    GL.PushMatrix();
                    GL.Translate(posX1, posY1, posZ1);
                    GL.Scale(1f, 1f, 1f);
                    DrawCube(1f, 0f, 1f, true, true, false);
                    GL.PopMatrix();

                    GL.PushMatrix();
                    GL.Begin(BeginMode.Lines);
                    GL.Color4(1f, 0f, 1f, 1f);
                    GL.Vertex3(posX1, posY1, posZ1);
                    GL.Vertex3(posX2, posY2, posZ2);
                    GL.End();
                    GL.PopMatrix();

                    posX2 = objEntry.xPos;
                    posY2 = objEntry.yPos;
                    posZ2 = objEntry.zPos;
                }
            }
            GL.EndList();

            foreach(CheckpointGroupObject objEntry in chkGRP)
            {
                chckGroup.Items.Add(objEntry);
            }

            foreach(RouteGroupSetup routeSetup in grpSetup)
            {
                routeGroupList.Items.Add(routeSetup);
            }

            uint groupID;
            uint currentID = 0;
            float curX, curY, curZ;
            float prevX = 0, prevY = 0, prevZ = 0;
            bool firstEntry = true;
            int seccount = 0;
            routePointList = GL.GenLists(1);
            GL.NewList(routePointList, ListMode.Compile);
            foreach (RoutePointObject objEntry in rpobj)
            {
                routeList.Items.Add(objEntry);
                loadingLabel.Text = "Loading route point...";

                /* 
                 * if the group id is the same as the current id, we join it together with a line
                 * if it isn't we just place a new block and start over
                 */
                groupID = objEntry.groupID;
                curX = objEntry.xPos;
                curY = objEntry.yPos;
                curZ = objEntry.zPos;

                if (groupID == currentID)
                {
                    // if it's the first entry, we set this to false so it doesn't run through again
                    if (firstEntry)
                    {
                        seccount += 1;
                        GL.PushMatrix();
                        GL.Translate(curX, curY, curZ);
                        GL.Scale(1f, 1f, 1f);
                        DrawCube(0f, 0f, 1f, true, true, false);
                        GL.PopMatrix();

                        prevX = objEntry.xPos;
                        prevY = objEntry.yPos;
                        prevZ = objEntry.zPos;

                        firstEntry = false;
                    }
                    // if it isn't, we draw a line to the next entry
                    else
                    {
                        GL.PushMatrix();
                        GL.Translate(curX, curY, curZ);
                        GL.Scale(1f, 1f, 1f);
                        DrawCube(0f, 0f, 1f, true, true, false);
                        GL.PopMatrix();

                        GL.PushMatrix();
                        GL.Begin(BeginMode.Lines);
                        GL.Color4(0f, 0f, 1f, 1f);
                        GL.Vertex3(curX, curY, curZ);
                        GL.Vertex3(prevX, prevY, prevZ);
                        GL.End();
                        GL.PopMatrix();

                        prevX = objEntry.xPos;
                        prevY = objEntry.yPos;
                        prevZ = objEntry.zPos;

                        firstEntry = false;
                    }
                }
                else
                {
                    currentID += 1;
                    firstEntry = true;
                }
            }
            GL.EndList();

            float xprev1 = 0, yprev1 = 0, zprev1 = 0;
            float xprev2 = 0, yprev2 = 0, zprev2 = 0;
            bool isFirst = true;
            checkpointList = GL.GenLists(1);
            GL.NewList(checkpointList, ListMode.Compile);
            foreach (CheckpointObject objEntry in chkobj)
            {
                chckList.Items.Add(objEntry);
                loadingLabel.Text = "Loading checkpoint...";
                GL.PushMatrix();
                GL.Translate(objEntry.xPosStart, objEntry.yPosStart, objEntry.zPosStart);
                GL.Scale(1f, 1f, 1f);
                DrawCube(0.5f, 0.25f, 0f, true, true, false);
                GL.PopMatrix();

                GL.PushMatrix();
                GL.Translate(objEntry.xPosEnd, objEntry.yPosEnd, objEntry.zPosEnd);
                GL.Scale(1f, 1f, 1f);
                DrawCube(0.5f, 0.25f, 0f, true, true, false);
                GL.PopMatrix();

                GL.PushMatrix();
                GL.Begin(BeginMode.Lines);
                GL.Color4(0.5f, 0.25f, 0f, 1f);
                GL.Vertex3(objEntry.xPosStart, objEntry.yPosStart, objEntry.zPosStart);
                GL.Vertex3(objEntry.xPosEnd, objEntry.yPosEnd, objEntry.zPosEnd);
                GL.End();
                GL.PopMatrix();

                if (!isFirst)
                {
                    GL.PushMatrix();
                    GL.Begin(BeginMode.Lines);
                    GL.Color4(0.5f, 0.25f, 0f, 1f);
                    GL.Vertex3(objEntry.xPosStart, objEntry.yPosStart, objEntry.zPosStart);
                    GL.Vertex3(xprev1, yprev1, zprev1);
                    GL.Vertex3(objEntry.xPosEnd, objEntry.yPosEnd, objEntry.zPosEnd);
                    GL.Vertex3(xprev2, yprev2, zprev2);
                    GL.End();
                    GL.PopMatrix();
                }

                xprev1 = objEntry.xPosStart;
                yprev1 = objEntry.yPosStart;
                zprev1 = objEntry.zPosStart;

                xprev2 = objEntry.xPosEnd;
                yprev2 = objEntry.yPosEnd;
                zprev2 = objEntry.zPosEnd;

                isFirst = false;

            }
            GL.EndList();

            Bmd objModel;
            objectList = GL.GenLists(1);
            GL.NewList(objectList, ListMode.Compile);
            int rotation;
            foreach (LevelObject objEntry in lvlobj)
            {
                objList.Items.Add(objEntry);
                loadingLabel.Text = "Loading object " + objEntry.objID;

                GL.PushMatrix();
                GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                GL.Rotate(objEntry.rotation, 0f, 1f, 0f);
                if (objEntry.modelName != "null")
                {
                    if (objModelList.ContainsKey(objEntry.modelName))
                    {
                        objModelList.TryGetValue(objEntry.modelName, out objModel);
                        GL.Scale(objEntry.xScale, objEntry.yScale, objEntry.zScale);
                        DrawBMD(objModel);
                    }
                    else
                    {
                        FileBase objFB = new FileBase();
                        objFB.Stream = new FileStream(Properties.Settings.Default.curDir + "\\objects\\" + objEntry.modelName + ".bmd", FileMode.Open);
                        Bmd obj = new Bmd(objFB);
                        GL.Scale(objEntry.xScale, objEntry.yScale, objEntry.zScale);
                        DrawBMD(obj);
                        objModelList.Add(objEntry.modelName, obj);
                        objFB.Close();
                    }
                }
                else
                {
                    GL.Scale(1f, 1f, 1f);
                    DrawCube(0f, 1f, 0f, true, true, false);
                }
                GL.PopMatrix();
            }
            GL.EndList();

            kartList = GL.GenLists(1);
            GL.NewList(kartList, ListMode.Compile);
            foreach (KartPointObject objEntry in kartobj)
            {
                kartPointList.Items.Add(objEntry);
                loadingLabel.Text = "Loading kart...";
                GL.PushMatrix();
                GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                rotation = Convert.ToInt16(objEntry.rotation);
                GL.Rotate(rotation, 0f, 1f, 0f);
                GL.Scale(1f, 1f, 1f);
                DrawCube(1f, 0f, 0f, true, true, false);
                GL.PopMatrix();
            }
            GL.EndList();

            areaObjList = GL.GenLists(1);
            GL.NewList(areaObjList, ListMode.Compile);
            foreach (AreaObject objEntry in areaobj)
            {
                areaList.Items.Add(objEntry);
                loadingLabel.Text = "Loading area...";
                GL.PushMatrix();
                GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                rotation = Convert.ToInt16(objEntry.rotation);
                GL.Rotate(rotation, 0f, 1f, 0f);
                GL.Scale(objEntry.xScale, objEntry.yScale, objEntry.zScale);
                DrawCube(0.867f, 0.867f, 0.867f, false, false, true);
                GL.PopMatrix();
            }
            GL.EndList();

            camList = GL.GenLists(1);
            GL.NewList(camList, ListMode.Compile);
            foreach (CameraObject objEntry in camobj)
            {
                cameraList.Items.Add(objEntry);
                loadingLabel.Text = "Loading camera...";
                GL.PushMatrix();
                GL.Translate(objEntry.xView1, objEntry.yView1, objEntry.zView1);
                rotation = Convert.ToInt16(objEntry.rotation);
                GL.Rotate(rotation, 0f, 1f, 0f);
                GL.Scale(1f, 1f, 1f);
                DrawCube(0.498f, 0.859f, 1f, true, true, false);
                GL.PopMatrix();
            }
            GL.EndList();

            respawnList = GL.GenLists(1);
            GL.NewList(respawnList, ListMode.Compile);
            foreach (RespawnObject objEntry in resObj)
            {
                loadingLabel.Text = "Loading respawn...";
                GL.PushMatrix();
                GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                rotation = Convert.ToInt16(objEntry.rotation);
                GL.Rotate(rotation, 0f, 1f, 0f);
                GL.Scale(1f, 1f, 1f);
                DrawCube(1f, 0.863f, 0f, true, true, false);
                GL.PopMatrix();
                respList.Items.Add(objEntry);
            }
            GL.EndList();

            loadingLabel.Text = "Ready!";
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();

            GL.Enable(EnableCap.DepthTest);
            GL.ClearDepth(1f);

            GL.FrontFace(FrontFaceDirection.Cw);

            m_CamPosition = new Vector3(0f, 0f, 0f);
            m_CamRotation = new Vector2(0.0f, 0.0f);
            m_CamDistance = 1f;

            m_RenderInfo = new RenderInfo();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            loaded = true;
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();

            UpdateViewport();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float xdelta = (float)(e.X - m_LastMouseMove.X);
            float ydelta = (float)(e.Y - m_LastMouseMove.Y);

            m_LastMouseMove = e.Location;

            if (m_MouseDown != MouseButtons.None)
            {
                if (m_MouseDown == MouseButtons.Right)
                {
                    if (m_UpsideDown)
                        xdelta = -xdelta;

                    m_CamRotation.X -= xdelta * 0.002f;
                    m_CamRotation.Y -= ydelta * 0.002f;
                }
                else if (m_MouseDown == MouseButtons.Left)
                {
                    xdelta *= 0.005f;
                    ydelta *= 0.005f;

                    m_CamTarget.X -= xdelta * (float)Math.Sin(m_CamRotation.X);
                    m_CamTarget.X -= ydelta * (float)Math.Cos(m_CamRotation.X) * (float)Math.Sin(m_CamRotation.Y);
                    m_CamTarget.Y += ydelta * (float)Math.Cos(m_CamRotation.Y);
                    m_CamTarget.Z += xdelta * (float)Math.Cos(m_CamRotation.X);
                    m_CamTarget.Z -= ydelta * (float)Math.Sin(m_CamRotation.X) * (float)Math.Sin(m_CamRotation.Y);
                }

                UpdateCamera();
            }

            glControl1.Refresh();
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != m_MouseDown) return;

            m_MouseDown = MouseButtons.None;
            m_LastMouseMove = e.Location;
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_MouseDown != MouseButtons.None) return;

            m_MouseDown = e.Button;
            m_LastMouseMove = m_LastMouseClick = e.Location;
        }

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                if (Properties.Settings.Default.isWireframe == true)
                {
                    changeGLRender(false);
                    Properties.Settings.Default.isWireframe = false;
                }
                else
                {
                    changeGLRender(true);
                    Properties.Settings.Default.isWireframe = true;
                }
            }

            if (e.KeyCode == Keys.D2)
            {
                if (Properties.Settings.Default.showAxis == true)
                    Properties.Settings.Default.showAxis = false;
                else
                    Properties.Settings.Default.showAxis = true;
            }

            if (e.KeyCode == Keys.D3)
            {
                if (Properties.Settings.Default.showEnemyRoutes == true)
                    Properties.Settings.Default.showEnemyRoutes = false;
                else
                    Properties.Settings.Default.showEnemyRoutes = true;
            }

            if (e.KeyCode == Keys.D4)
            {
                if (Properties.Settings.Default.showRoutes == true)
                    Properties.Settings.Default.showRoutes = false;
                else
                    Properties.Settings.Default.showRoutes = true;
            }

            if (e.KeyCode == Keys.D5)
            {
                if (Properties.Settings.Default.showCheckpoints == true)
                    Properties.Settings.Default.showCheckpoints = false;
                else
                    Properties.Settings.Default.showCheckpoints = true;
            }

            if (e.KeyCode == Keys.D6)
            {
                if (Properties.Settings.Default.showItems == true)
                    Properties.Settings.Default.showItems = false;
                else
                    Properties.Settings.Default.showItems = true;
            }

            if (e.KeyCode == Keys.D7)
            {
                if (Properties.Settings.Default.showStarting == true)
                    Properties.Settings.Default.showStarting = false;
                else
                    Properties.Settings.Default.showStarting = true;
            }

            if (e.KeyCode == Keys.D8)
            { 
                if (Properties.Settings.Default.showAreas == true)
                    Properties.Settings.Default.showAreas = false;
                else
                    Properties.Settings.Default.showAreas = true;
            }

            if (e.KeyCode == Keys.D9)
            {
                if (Properties.Settings.Default.showCameras == true)
                    Properties.Settings.Default.showCameras = false;
                else
                    Properties.Settings.Default.showCameras = true;
            }

            if (e.KeyCode == Keys.D0)
            {
                if (Properties.Settings.Default.showRespawns == true)
                    Properties.Settings.Default.showRespawns = false;
                else
                    Properties.Settings.Default.showRespawns = true;
            }

            Properties.Settings.Default.Save();
            glControl1.Refresh();
        }

        public static void changeGLRender(bool isWireframe)
        {
            if (isWireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            MainWindow main = new MainWindow();
            main.glControl1.Refresh();
        }

        public static void refreshGL()
        {
            MainWindow main = new MainWindow();
            main.glControl1.Refresh();
        }


        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;

            glControl1.MakeCurrent();

            GL.DepthMask(true);

            // bg color
            GL.ClearColor(0f, 0f, 0.125f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // set our matrix modes and load it
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref m_CamMatrix);

            GL.Disable(EnableCap.Texture2D);

            GL.CallList(courseList);
            if (Properties.Settings.Default.showEnemyRoutes)
                GL.CallList(enemyPointList);
            if (Properties.Settings.Default.showCheckpoints)
                GL.CallList(checkpointList);
            if (Properties.Settings.Default.showRoutes)
                GL.CallList(routePointList);
            if (Properties.Settings.Default.showItems)
                GL.CallList(objectList);
            if (Properties.Settings.Default.showStarting)
                GL.CallList(kartList);
            if (Properties.Settings.Default.showAreas)
                GL.CallList(areaObjList);
            if (Properties.Settings.Default.showCameras)
                GL.CallList(camList);
            if (Properties.Settings.Default.showRespawns)
                GL.CallList(respawnList);

            GL.CallList(selectedList);

            if (Properties.Settings.Default.showAxis)
            {
                GL.Begin(BeginMode.Lines);
                GL.Color4(1f, 0f, 0f, 1f);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(100000f, 0f, 0f);
                GL.Color4(0f, 1f, 0f, 1f);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(0, 100000f, 0f);
                GL.Color4(0f, 0f, 1f, 1f);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(0f, 0f, 100000f);
                GL.End();
            }

            GL.Color4(1f, 1f, 1f, 1f);

            glControl1.SwapBuffers();
        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doFolderChoose();
        }

        private void UpdateViewport()
        {
            GL.Viewport(glControl1.ClientRectangle);

            m_AspectRatio = (float)glControl1.Width / (float)glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projmtx = Matrix4.CreatePerspectiveFieldOfView(k_FOV, m_AspectRatio, k_zNear, k_zFar);
            GL.LoadMatrix(ref projmtx);

            m_PixelFactorX = ((2f * (float)Math.Tan(k_FOV / 2f) * m_AspectRatio) / (float)(glControl1.Width));
            m_PixelFactorY = ((2f * (float)Math.Tan(k_FOV / 2f)) / (float)(glControl1.Height));
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
        }

        private void BMDViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BMDViewer bmdviewer = new BMDViewer();
            bmdviewer.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        Bmd obj;
        Vector3 min = Vector3.Zero;
        Vector3 max = Vector3.Zero;
        float width, height, length;

        private void objList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LevelObject level = new LevelObject();
            level = (LevelObject)objList.SelectedItem;

            selectedList = GL.GenLists(1);
            GL.NewList(selectedList, ListMode.Compile);
            GL.PushMatrix();
            GL.Translate(level.xPos, level.yPos, level.zPos);
            GL.Rotate(level.rotation, 0f, 1f, 0f);

            if (level.modelName != "null")
            {
                if (objModelList.ContainsKey(level.modelName))
                {
                    objModelList.TryGetValue(level.modelName, out obj);
                    GetRealBounds(out min, out max, obj);
                    width = max.X - min.X;
                    height = max.Y - min.Y;
                    length = max.Z - min.Z;
                    GL.Scale(width / (100 * level.xScale), height / (100 * level.yScale), length / (100 * level.zScale));
                }
            }
            else
                GL.Scale(2f, 2f, 2f);

            DrawCube(1f, 1f, 1f, false, false, false);
            GL.PopMatrix();
            GL.EndList();

            selectionInfo.Text = "Currently selected: Track object at (" + level.xPos + ", " + level.yPos + ", " + level.zPos + ") with ID " + level.objID.ToString("X") + ".";
            glControl1.Refresh();
        }

        private void UpdateCamera()
        {
            Vector3 up;

            if (Math.Cos(m_CamRotation.Y) < 0)
            {
                m_UpsideDown = true;
                up = new Vector3(0.0f, -1.0f, 0.0f);
            }
            else
            {
                m_UpsideDown = false;
                up = new Vector3(0.0f, 1.0f, 0.0f);
            }

            m_CamPosition.X = m_CamDistance * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            m_CamPosition.Y = m_CamDistance * (float)Math.Sin(m_CamRotation.Y);
            m_CamPosition.Z = m_CamDistance * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            Vector3 skybox_target;
            skybox_target.X = -(float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            skybox_target.Y = -(float)Math.Sin(m_CamRotation.Y);
            skybox_target.Z = -(float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            Vector3.Add(ref m_CamPosition, ref m_CamTarget, out m_CamPosition);

            m_CamMatrix = Matrix4.LookAt(m_CamPosition, m_CamTarget, up);
            m_SkyboxMatrix = Matrix4.LookAt(Vector3.Zero, skybox_target, up);

            m_CamMatrix = Matrix4.Mult(Matrix4.Scale(0.0001f), m_CamMatrix);
        }

        private void respawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RespawnAdd resAdd = new RespawnAdd();
            resAdd.Show();
            resAdd.getLists(respList, glControl1, respawnList);
        }

        private void respList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (respList.SelectedIndex != -1)
            {
                string curItem = respList.SelectedItem.ToString();

                RespawnObject respawn = new RespawnObject();
                respawn = (RespawnObject)respList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(respawn.xPos, respawn.yPos, respawn.zPos);
                GL.Rotate(respawn.rotation, 0f, 1f, 0f);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Respawn object at (" + respawn.xPos + ", " + respawn.yPos + ", " + respawn.zPos + ") with ID " + respawn.respawnID + ".";
                glControl1.Refresh();
            }
        }

        private void respList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (respList.SelectedIndex != -1)
            {
                RespawnEditor res = new RespawnEditor();
                res.getLists(respList, glControl1, respawnList);
                res.loadData((RespawnObject)respList.SelectedItem);
                res.Show();
            }
        }

        private void respawnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (chckList.SelectedIndex != -1)
            {
                chckList.Items.Remove(chckList.SelectedItem);

                chckList.ClearSelected();

                UpdateRespawns(chckList);

                glControl1.Refresh();
            }
        }

        private void chckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (chckList.SelectedIndex != -1)
            {
                string curItem = chckList.SelectedItem.ToString();

                CheckpointObject checkpObj = new CheckpointObject();
                checkpObj = (CheckpointObject)chckList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(checkpObj.xPosStart, checkpObj.yPosStart, checkpObj.zPosStart);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();

                GL.PushMatrix();
                GL.Translate(checkpObj.xPosEnd, checkpObj.yPosEnd, checkpObj.zPosEnd);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Checkpoint object at (" + checkpObj.xPosStart + ", " + checkpObj.yPosStart + ", " + checkpObj.zPosStart + ") connected to (" + checkpObj.xPosEnd + ", " + checkpObj.yPosEnd + ", " + checkpObj.zPosEnd + ") with group ID " + checkpObj.groupID + ".";
                glControl1.Refresh();
            }
        }

        private void routeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (routeList.SelectedIndex != -1)
            {
                string curItem = routeList.SelectedItem.ToString();

                RoutePointObject routeObj = new RoutePointObject();
                routeObj = (RoutePointObject)routeList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(routeObj.xPos, routeObj.yPos, routeObj.zPos);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Route point at (" + routeObj.xPos + ", " + routeObj.yPos + ", " + routeObj.zPos + ") in group id " + routeObj.groupID + ".";
                glControl1.Refresh();
            }
        }

        private void kartPointList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (kartPointList.SelectedIndex != -1)
            {
                string curItem = kartPointList.SelectedItem.ToString();

                KartPointObject kartObj = new KartPointObject();
                kartObj = (KartPointObject)kartPointList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(kartObj.xPos, kartObj.yPos, kartObj.zPos);
                GL.Rotate(kartObj.rotation, 0f, 1f, 0f);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Route point at (" + kartObj.xPos + ", " + kartObj.yPos + ", " + kartObj.zPos + ") with player id " + kartObj.playerID + ".";
                glControl1.Refresh();
            }
        }

        private void cameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (cameraList.SelectedIndex != -1)
            {
                string curItem = cameraList.SelectedItem.ToString();

                CameraObject camObj = new CameraObject();
                camObj = (CameraObject)cameraList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(camObj.xView1, camObj.yView1, camObj.zView1);
                GL.Rotate(camObj.rotation, 0f, 1f, 0f);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Route point at (" + camObj.xView1 + ", " + camObj.yView1 + ", " + camObj.zView1 + ") with name " + camObj.name + ".";
                glControl1.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // saves to the file already open
            saveCourse(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile + ".bol");
        }


        private void saveCourse(string file)
        {
            enmRoute.Clear(); // enemy route list
            chkobj.Clear(); // checkpoint list
            rpobj.Clear(); // route point list
            lvlobj.Clear(); // object list
            kartobj.Clear(); // kart list
            areaobj.Clear(); // area list
            camobj.Clear(); // camera list
            resObj.Clear(); // respawn list

            // here we set the information that goes into the reader (mostly caluclated crap though)
            foreach (BOLInformation bolInfo in bolInf)
            {
                // why would you even need negative entry counts
                bolInfo.sec1Count = (ushort)enemyRouteList.Items.Count;
                //bolInfo.sec2Count this doesn't have to be calculated until we make a list for it
                bolInfo.sec5Count = (ushort)objList.Items.Count;
                bolInfo.sec7Count = (ushort)areaList.Items.Count;
                bolInfo.sec8Count = (ushort)cameraList.Items.Count;
                //bolInfo.sec3Count this doesn't have to be calculated until we make a list for it
                bolInfo.sec9Count = (ushort)respList.Items.Count;

                uint offset = 0x7C;
                bolInfo.sec1Offs = offset; // first entry ALWAYS has to be 0x7C
                offset += (uint)bolInfo.sec1Count * 0x20;

                if (enemyRouteList.Items.Count == 0)
                    bolInfo.sec1Offs = 0x7C;
                else
                    bolInfo.sec1Offs = offset;

                uint sec2GroupSize = (uint)chckGroup.Items.Count * 0x14;
                uint sec2EntrySize = (uint)chckList.Items.Count * 0x1C;
                offset += sec2GroupSize + sec2EntrySize;

                if (chckGroup.Items.Count == 0)
                    bolInfo.sec2Offs = 0x7C;
                else
                    bolInfo.sec2Offs = offset;

                // after this is a breeze

                uint sec3Size = (uint)routeGroupList.Items.Count * 0x10;
                offset += sec3Size;

                bolInfo.sec4Offs = offset;

                uint sec4Size = (uint)routeList.Items.Count * 0x20;
                offset += sec4Size;

                bolInfo.sec5Offs = offset;

                uint sec5Size = (uint)objList.Items.Count * 0x40;
                offset += sec5Size;

                bolInfo.sec6Offs = offset;

                uint sec6Size = (uint)kartPointList.Items.Count * 0x28;
                offset += sec6Size;

                bolInfo.sec7Offs = offset;

                uint sec7Size = (uint)areaList.Items.Count * 0x38;
                offset += sec7Size;

                bolInfo.sec8Offs = offset;

                uint sec8Size = (uint)cameraList.Items.Count * 0x48;
                offset += sec8Size;

                if (respList.Items.Count == 0)
                    bolInfo.sec9Offs = (uint)bolInfo.fileSize;
                else
                    bolInfo.sec9Offs = offset;

                uint sec9Size = (uint)respList.Items.Count * 0x20;
                offset += sec9Size;

                // insert section 10 offset here
            }

            foreach (EnemyRoute enmObj in enemyRouteList.Items)
            {
                enmRoute.Add(enmObj);
            }

            foreach (CheckpointObject chkObj in chckList.Items)
            {
                chkobj.Add(chkObj);
            }

            foreach (RoutePointObject routeObj in routeList.Items)
            {
                rpobj.Add(routeObj);
            }

            foreach (LevelObject lvlObj in objList.Items)
            {
                lvlobj.Add(lvlObj);
            }

            foreach (KartPointObject kpObj in kartPointList.Items)
            {
                kartobj.Add(kpObj);
            }

            foreach (AreaObject areaObj in areaList.Items)
            {
                areaobj.Add(areaObj);
            }

            foreach (CameraObject camObj in cameraList.Items)
            {
                camobj.Add(camObj);
            }

            foreach (RespawnObject respObj in respList.Items)
            {
                resObj.Add(respObj);
            }

            /* calling these methods in order is CRUCIAL! */
            using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(file, FileMode.Create)))
            {
                BOL bol = new BOL();
                bol.Write(writer, bolInf);

                EnemyRoutes routes = new EnemyRoutes();
                routes.Write(writer, enmRoute);

                CheckpointGroup chckGrp = new CheckpointGroup();
                chckGrp.Write(writer, chkGRP);

                Checkpoint checkpoint = new Checkpoint();
                checkpoint.Write(writer, chkobj);

                RouteGroup routeGrp = new RouteGroup();
                routeGrp.Write(writer, grpSetup);

                RoutePoint routePt = new RoutePoint();
                routePt.Write(writer, rpobj);

                TrackObject levelObj = new TrackObject();
                levelObj.Write(writer, lvlobj);

                KartPoint kartpoint = new KartPoint();
                kartpoint.Write(writer, kartobj);

                Area area = new Area();
                area.Write(writer, areaobj);

                Camera cameraObj = new Camera();
                cameraObj.Write(writer, camobj);

                Respawn respObj = new Respawn();
                respObj.Write(writer, resObj);

                writer.Close();
            }
        }

        private void unknown1_ValueChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                bol.unk3 = (float)unknown3.Value;
            }
        }

        private void lapCounter_ValueChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                bol.numLaps = (byte)lapCounter.Value;
            }
        }

        private void unknown4_ValueChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                bol.unk4 = (float)unknown4.Value;
            }
        }

        private void unknown5_ValueChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                bol.unk5 = (float)unknown5.Value;
            }
        }

        private void musicSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                int id = musicSelect.SelectedIndex + 0x21;
                bol.musicID = (byte)id;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BOL files (*.bol)|*.bol|All files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                saveCourse(sfd.FileName);
            }
        }

        private void areaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (areaList.SelectedIndex != -1)
            {
                string curItem = areaList.SelectedItem.ToString();

                AreaObject areaObj = new AreaObject();
                areaObj = (AreaObject)areaList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(areaObj.xPos, areaObj.yPos, areaObj.zPos);
                GL.Rotate(areaObj.rotation, 0f, 1f, 0f);
                GL.Scale(areaObj.xScale + 0.1f, areaObj.yScale + 0.1f, areaObj.zScale + 0.1f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Area object at (" + areaObj.xPos + ", " + areaObj.yPos + ", " + areaObj.zPos + ")";
                glControl1.Refresh();
            }
        }

        public void UpdateRespawns(ListBox list)
        {
            List<RespawnObject> shitballs = new List<RespawnObject>();

            foreach (RespawnObject respobj in list.Items)
            {
                shitballs.Add(respobj);
            }

            GL.DeleteLists(respawnList, 1);
            list.Items.Clear();

            respawnList = GL.GenLists(1);
            GL.NewList(respawnList, ListMode.Compile);
            foreach (RespawnObject resp in shitballs)
            {
                GL.PushMatrix();
                GL.Translate(resp.xPos, resp.yPos, resp.zPos);
                GL.Scale(1f, 1f, 1f);
                DrawCube(1f, 0.863f, 0f, true, true, false);
                GL.PopMatrix();

                list.Items.Add(resp);
            }
            GL.EndList();

            glControl1.Refresh();
        }

        private void enemyRouteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            if (enemyRouteList.SelectedIndex != -1)
            {
                string curItem = enemyRouteList.SelectedItem.ToString();

                EnemyRoute route = new EnemyRoute();
                route = (EnemyRoute)enemyRouteList.SelectedItem;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                GL.PushMatrix();
                GL.Translate(route.xPos, route.yPos, route.zPos);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false, false);
                GL.PopMatrix();
                GL.EndList();

                selectionInfo.Text = "Currently selected: Enemy route at (" + route.xPos + ", " + route.yPos + ", " + route.zPos + ") with link " + route.link + " tied to group " + route.group + ".";
                glControl1.Refresh();
            }
        }

        public static void DrawBMD(Bmd model, RenderMode rnd = RenderMode.Opaque)
        {
            RenderInfo ri = new RenderInfo();
            ri.Mode = rnd;

            BmdRenderer br = new BmdRenderer(model);
            br.Render(ri);
        }

        public static void DrawCube(float color1, float color2, float color3, bool showAxis, bool useFill, bool isArea, RenderMode rnd = RenderMode.Opaque)
        {
            RenderInfo ri = new RenderInfo();
            ri.Mode = rnd;
            RendererBase cubeRender;

            if (!useFill)
            {
                if (isArea)
                    cubeRender = new ColorCubeRenderer(250f, new Vector4(0.867f, 0.867f, 0.867f, 1f), new Vector4(color1, color2, color3, 1f), showAxis, useFill);
                else
                    cubeRender = new ColorCubeRenderer(250f, new Vector4(1f, 0f, 0f, 1f), new Vector4(color1, color2, color3, 1f), showAxis, useFill);
                cubeRender.Render(ri);
            }
            else
            {
                cubeRender = new ColorCubeRenderer(250f, new Vector4(1f, 1f, 1f, 1f), new Vector4(color1, color2, color3, 1f), showAxis, useFill);
                cubeRender.Render(ri);
            }
        }

        public void GetRealBounds(out Vector3 minBox, out Vector3 maxBox, Bmd model)
        {
            minBox.X = float.MaxValue;
            minBox.Y = float.MaxValue;
            minBox.Z = float.MaxValue;

            maxBox.X = float.MinValue;
            maxBox.Y = float.MinValue;
            maxBox.Z = float.MinValue;

            foreach (Bmd.SceneGraphNode node in model.SceneGraph)
            {
                if (node.NodeType != 0)
                    continue;
                Bmd.Batch batch = model.Batches[node.NodeID];
                foreach (Bmd.Batch.Packet packet in batch.Packets)
                {
                    foreach (Bmd.Batch.Packet.Primitive primitive in packet.Primitives)
                    {
                        foreach (int nd in primitive.PositionIndices)
                        {
                            Vector3 pos = model.PositionArray[nd];
                            if (pos.X < minBox.X)
                                minBox.X = pos.X;
                            if (pos.Y < minBox.Y)
                                minBox.Y = pos.Y;
                            if (pos.Z < minBox.Z)
                                minBox.Z = pos.Z;

                            if (pos.X > maxBox.X)
                                maxBox.X = pos.X;
                            if (pos.Y > maxBox.Y)
                                maxBox.Y = pos.Y;
                            if (pos.Z > maxBox.Z)
                                maxBox.Z = pos.Z;
                        }
                    }
                }
            }
        }
    }
}
