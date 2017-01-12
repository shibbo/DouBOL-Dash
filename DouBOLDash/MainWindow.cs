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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.ComponentModel;

namespace DouBOLDash
{
    public partial class MainWindow : Form
    {

        public string PROGRAM_NAME = "DouBOL Dash ";
        public string VERSION = "v0.1 Beta ";

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

        // dictionary that contains all of the music ids that i know so far
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
            {0x33, "Sherbet Land"},
            {0x34, "Luigi's Mansion"},
            {0x35, "Nintendo Gamecube"},
            {0x36, "Block City"},
            {0x37, "Unused"},
            {0x38, "Tilt-A-Kart"},
            {0x39, "Unused"},
            {0x3A, "Cookie Land"},
            {0x3B, "Pipe Plaza"},
            {0x3C, "Unused"},
            {0x3D, "Unused"},
            {0x3E, "Unused"},
            {0x3F, "Unused"},
            {0x40, "Unused"},
            {0x41, "Unused"},
            {0x42, "Unused"},
            {0x43, "Unused"},
            {0x44, "Unused"},
            {0x45, "Ending Credits"}
        };

        // model storage (cache)
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
        int respawnList = 0;

        Image minimap;

        // selected object render box
        int selectedList = 0;

        // BMD pre-render list
        int courseList = 0;
        Bmd course;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.checkForUpdate == true)
            {
                UpdateForm update = new UpdateForm();
                update.doUpdateCheck(false);
                update.Show();
            }

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
                musicSelect.Text = "Unused";
        }

        private void doFolderChoose()
        {
            MessageBox.Show("Please choose the root directory of the filesystem.\nIf one is not chosen, some rendering may not be supported.", "Choose Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(fbDialog.SelectedPath + "/Course"))
                {
                    MessageBox.Show("Path successfully changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.envDir = fbDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.bmdEnabled = true;
                }
                else
                    MessageBox.Show("/Course folder not found. Settings were not changed.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Folder path not changed.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BOL files (*.bol)|*.bol|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                parseBOL(openFileDialog1.FileName);
            }
        }

        public void parseBOL(string file)
        {
            this.Text = PROGRAM_NAME + VERSION + file;
            this.Update();

            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;

            lapCounter.Enabled = true;
            musicInput.Enabled = true;
            musicSelect.Enabled = true;
            unknown3.Enabled = true;
            unknown4.Enabled = true;
            unknown5.Enabled = true;
            unknown7.Enabled = true;
            unknown8.Enabled = true;

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

            string dirName = Path.GetDirectoryName(file);
            string fileName = Path.GetFileNameWithoutExtension(file);

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
                GL.DeleteLists(respawnList, 1);
                GL.DeleteLists(camList, 1);
                GL.DeleteLists(areaObjList, 1);
            }

            using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(file, FileMode.Open)))
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
                    FileInfo f = new FileInfo(file);
                    bolInfo.fileSize = (int)f.Length;
                }

                // here we parse each section in order, which is crucial
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

                // minimap BTI to load
                // loads the BTI, converts to bitmap, and loads it into picture box
                string bti_path = Properties.Settings.Default.curFile.Replace("course", "map");

                if (File.Exists(Properties.Settings.Default.curDir + "/" + bti_path + ".bti"))
                {
                    FileBase fileb = new FileBase();
                    fileb.Stream = new FileStream(Properties.Settings.Default.curDir + "/" + bti_path + ".bti", FileMode.Open);
                    minimap = BTIFile.ReadBTIToBitmap(fileb);
                    pictureBox1.Image = minimap;
                    fileb.Close();
                }
                else
                    MessageBox.Show("The minimap file (" + bti_path + ".bti) doesn't exist.");

                // generate the list that the course model will be rendered in
                // try to find the course model in the same folder the BLO was opened in
                // if it's not there, show an error andset it to null to avoid crashing
                // if you think we're loading the course model every build you're wrong
                // BuildScene will also handle changed objects in the scene, so we don't
                // want to load the same course model again. waste of time.
                courseList = GL.GenLists(1);

                if (Properties.Settings.Default.bmdEnabled != false)
                {
                    FileBase fb = new FileBase();
                    if (File.Exists(Properties.Settings.Default.curDir + "/" + Properties.Settings.Default.curFile + ".bmd"))
                    {
                        Console.WriteLine(Properties.Settings.Default.curDir + "/" + Properties.Settings.Default.curFile + ".bmd");
                        fb.Stream = new FileStream(Properties.Settings.Default.curDir + "/" + Properties.Settings.Default.curFile + ".bmd", FileMode.Open);
                        course = new Bmd(fb);
                        fb.Close();
                    }
                    else
                    {
                        MessageBox.Show(Properties.Settings.Default.curDir + "/" + Properties.Settings.Default.curFile + ".bmd doesn't exist.");
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

        public void BuildScene()
        {
            foreach (BOLInformation bolEntry in bolInf)
            {
                // set the BOL information tab data
                musicID = bolEntry.musicID;
                lapCount = bolEntry.numLaps;

                unknown3.Value = (decimal)bolEntry.unk3;
                unknown4.Value = (decimal)bolEntry.unk4;
                unknown5.Value = (decimal)bolEntry.unk5;
                unknown7.Value = (decimal)bolEntry.unk7;
                unknown8.Value = (decimal)bolEntry.unk8;

                setMusic(musicID);
                lapCounter.Value = lapCount;
                musicInput.Value = musicID;
            }
            
            // after filling the lists with data we call functions that update the scene
            UpdateEnemyPoints(false);
            UpdateRouteGroups(false);
            UpdateRoutePoints(false);
            UpdateCheckpointGroups(false);
            UpdateCheckpoints(false);
            UpdateObjects(false);
            UpdateKartPoints(false);
            UpdateAreas(false);
            UpdateCameras(false);
            UpdateRespawns(false);

            ChangeText("Ready!");
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
            // updates the scene upon window resize (which also resizes the GL)
            glControl1.MakeCurrent();

            UpdateViewport();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            // moves the camera around the scene
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
            // all of the keyboard shortcuts
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
            // changes the GL render to wireframe or fill
            if (isWireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            MainWindow main = new MainWindow();
            main.glControl1.Refresh();
        }

        public static void refreshGL()
        {
            // refreshes the GL scene
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

            UpdateCamera();
        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doFolderChoose();
        }

        private void UpdateViewport()
        {
            if (glControl1.Height == 0 || glControl1.Width == 0)
                Console.WriteLine("we no update view");
            else
            {
                GL.Viewport(glControl1.ClientRectangle);

                m_AspectRatio = (float)glControl1.Width / (float)glControl1.Height;
                GL.MatrixMode(MatrixMode.Projection);
                Matrix4 projmtx = Matrix4.CreatePerspectiveFieldOfView(k_FOV, m_AspectRatio, k_zNear, k_zFar);
                GL.LoadMatrix(ref projmtx);

                m_PixelFactorX = ((2f * (float)Math.Tan(k_FOV / 2f) * m_AspectRatio) / (float)(glControl1.Width));
                m_PixelFactorY = ((2f * (float)Math.Tan(k_FOV / 2f)) / (float)(glControl1.Height));
            }
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

        Bmd obj;
        Vector3 min = Vector3.Zero;
        Vector3 max = Vector3.Zero;
        float width, height, length;

        private void objList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LevelObject level = new LevelObject();
            level = (LevelObject)objList.SelectedItem;

            propertyGrid3.SelectedObject = objList.SelectedItem;

            if (objList.SelectedIndex != -1)
            {
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

                foreach (RoutePointObject objEntry in rpobj)
                {
                    if (objEntry.groupID == level.routeID && level.routeID != -1)
                    {
                        GL.PushMatrix();
                        GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                        GL.Scale(2f, 2f, 2f);
                        DrawCube(0.941f, 0.071f, 0.745f, false, false, false);
                        GL.PopMatrix();
                    }
                }
                GL.EndList();

                selectionInfo.Text = "Currently selected: Track object at (" + level.xPos + ", " + level.yPos + ", " + level.zPos + ") (" + level.friendlyName + ") (ID: " + level.objID.ToString("X") + ")";
                glControl1.Refresh();
            }
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

        private void respList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            propertyGrid4.SelectedObject = respList.SelectedItem;

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
                // something will happen here
            }
        }

        private void respawnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (chckList.SelectedIndex != -1)
            {
                chckList.Items.Remove(chckList.SelectedItem);

                chckList.ClearSelected();

                UpdateRespawns(true);

                glControl1.Refresh();
            }
        }

        private void chckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            propertyGrid5.SelectedObject = chckList.SelectedItem;

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

            propertyGrid1.SelectedObject = routeList.SelectedItem;

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

            propertyGrid8.SelectedObject = kartPointList.SelectedItem;

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

                selectionInfo.Text = "Currently selected: Starting point at (" + kartObj.xPos + ", " + kartObj.yPos + ", " + kartObj.zPos + ") with player id " + kartObj.playerID + ".";
                glControl1.Refresh();
            }
        }

        private void cameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            propertyGrid2.SelectedObject = cameraList.SelectedItem;

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

                foreach (RoutePointObject objEntry in rpobj)
                {
                    if (objEntry.groupID == camObj.routeID && camObj.routeID != -1)
                    {
                        GL.PushMatrix();
                        GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                        GL.Scale(2f, 2f, 2f);
                        DrawCube(0.941f, 0.071f, 0.745f, false, false, false);
                        GL.PopMatrix();
                    }
                }
                GL.EndList();

                selectionInfo.Text = "Currently selected: Camera at (" + camObj.xView1 + ", " + camObj.yView1 + ", " + camObj.zView1 + ") (Route ID: " + camObj.routeID + ") with name " + camObj.name + ".";
                glControl1.Refresh();
            }
        }
        
        // function that gets all of the data needed for a new track
        private void initNewTrack()
        {
            string[] courseModels = getCourseModels();
            string collisionModel = getCollisionModel();
            string[] miscFiles = getMiscFiles();

            if (courseModels == null || collisionModel == "")
            {
                MessageBox.Show("New course creation failed.");
                return;
            }

            MessageBox.Show("You will now be asked to enter an internal track name to reference your track with. The program will then make a folder with this name in your game directory. The program will also add the files you chose earlier.");

            TrackNameBox trackName = new TrackNameBox();
            trackName.ShowDialog();

            string name = trackName.textBox1.Text;

            if (!Directory.Exists(Properties.Settings.Default.envDir + "/Course/" + name))
            {
                Directory.CreateDirectory(Properties.Settings.Default.envDir + "/Course/" + name);
                Directory.CreateDirectory(Properties.Settings.Default.envDir + "/Course/" + name + "/objects");
            }
            else
            {
                MessageBox.Show("Folder " + Properties.Settings.Default.envDir + "/Course/" + name + " already exists!");
                return;
            }

            foreach (string s in courseModels)
            {
                string fileName = Path.GetFileName(s);
                File.Copy(s, Properties.Settings.Default.envDir + "/Course/" + name + "/" + fileName, true);
            }

            string colfileName = Path.GetFileName(collisionModel);
            File.Copy(collisionModel, Properties.Settings.Default.envDir + "/Course/" + colfileName, true);

            foreach (string s in miscFiles)
            {
                string fileName = Path.GetFileName(s);
                File.Copy(s, Properties.Settings.Default.envDir + "/Course/" + name + "/objects/" + fileName, true);
            }


        }

        private string[] getCourseModels()
        {
            DialogResult result = MessageBox.Show("Please select the track model and the sky model you will be using.", "Select Track Model", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                OpenFileDialog courseModelDialog = new OpenFileDialog();
                courseModelDialog.Multiselect = true;
                courseModelDialog.Filter = "Model files (*.bmd)|*.bmd|All files (*.*)|*.*";

                if (courseModelDialog.ShowDialog() == DialogResult.OK && courseModelDialog.FileNames.Length == 2)
                {
                    return courseModelDialog.FileNames;
                }
                else
                    return null;
            }
            else
                return null;
        }

        private string getCollisionModel()
        {
            DialogResult result = MessageBox.Show("Please select the collision model you will be using.", "Select Collision Model", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                OpenFileDialog courseCollisionDialog = new OpenFileDialog();
                courseCollisionDialog.Filter = "Collision files (*.bco)|*.bco|All files (*.*)|*.*";

                if (courseCollisionDialog.ShowDialog() == DialogResult.OK)
                {
                    return courseCollisionDialog.FileName;
                }
                else
                    return "";
            }
            else
                return "";
        }

        private string[] getMiscFiles()
        {
            DialogResult result = MessageBox.Show("If you would like to go ahead and add the needed files to the track in /objects, select 'Yes'. Otherwise, select 'No'.", "Select Misc Files", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                OpenFileDialog miscFilesDialog = new OpenFileDialog();
                miscFilesDialog.Multiselect = true;
                miscFilesDialog.Filter = "All files (*.*)|*.*";

                if (miscFilesDialog.ShowDialog() == DialogResult.OK)
                {
                    return miscFilesDialog.FileNames;
                }
                else
                    return null;
            }
            else
                return null;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // saves to the file already open
            saveCourse(Properties.Settings.Default.curDir + "/" + Properties.Settings.Default.curFile + ".bol");
        }


        private void saveCourse(string file)
        {
            enmRoute.Clear(); // enemy route list
            chkGRP.Clear(); // checkpoint group list
            chkobj.Clear(); // checkpoint list
            grpSetup.Clear(); // route group list
            rpobj.Clear(); // route point list
            lvlobj.Clear(); // object list
            kartobj.Clear(); // kart list
            areaobj.Clear(); // area list
            camobj.Clear(); // camera list
            resObj.Clear(); // respawn list

            MiscHacks misc = new MiscHacks();

            // here we set the information that goes into the reader (mostly caluclated crap though)
            foreach (BOLInformation bolInfo in bolInf)
            {
                // why would you even need negative entry counts
                bolInfo.sec1Count = (ushort)enemyRouteList.Items.Count;
                bolInfo.sec2Count = (ushort)chckGroup.Items.Count;
                bolInfo.sec5Count = (ushort)objList.Items.Count;
                bolInfo.sec7Count = (ushort)areaList.Items.Count;
                bolInfo.sec8Count = (ushort)cameraList.Items.Count;
                bolInfo.sec3Count = (ushort)routeGroupList.Items.Count;
                bolInfo.sec9Count = (ushort)respList.Items.Count;

                uint offset = 0x7C;
                bolInfo.sec1Offs = offset; // first entry ALWAYS has to be 0x7C
                offset += (uint)bolInfo.sec1Count * 0x20;

                if (enemyRouteList.Items.Count == 0)
                    bolInfo.sec2Offs = 0x7C;
                else
                    bolInfo.sec2Offs = offset;

                uint sec2GroupSize = (uint)chckGroup.Items.Count * 0x14;
                uint sec2EntrySize = (uint)chckList.Items.Count * 0x1C;
                offset += sec2GroupSize + sec2EntrySize;

                if (chckGroup.Items.Count == 0)
                    bolInfo.sec3Offs = 0x7C;
                else
                    bolInfo.sec3Offs = offset;

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

            foreach (CheckpointGroupObject chkGrpObj in chckGroup.Items)
            {
                chkGRP.Add(chkGrpObj);
            }

            foreach (CheckpointObject chkObj in chckList.Items)
            {
                chkobj.Add(chkObj);
            }

            foreach (RouteGroupSetup routeGroupObj in routeGroupList.Items)
            {
                grpSetup.Add(routeGroupObj);
            }

            foreach (RoutePointObject routeObj in routeList.Items)
            {
                rpobj.Add(routeObj);
            }

            foreach (LevelObject lvlObj in objList.Items)
            {
                lvlobj.Add(lvlObj);
            }

            bool routeNotGood = misc.checkForRoute(lvlobj);

            if (routeNotGood)
                return;

            foreach (KartPointObject kpObj in kartPointList.Items)
            {
                kartobj.Add(kpObj);
            }

            if (kartobj.Count < 1)
            {
                MessageBox.Show("You need at least one starting point!");
                return;
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
                musicInput.Value = id;
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

        private void objList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && objList.SelectedIndex != -1)
            {
                objectContext.Show(objList, e.Location);
            }
        }

        private void contextItemEdit_Click(object sender, EventArgs e)
        {
            if (objList.SelectedIndex != -1)
                MessageBox.Show("Select an item.");
        }

        private void musicInput_ValueChanged(object sender, EventArgs e)
        {
            foreach(BOLInformation bolInfo in bolInf)
            {
                bolInfo.musicID = (byte)musicInput.Value;
                musicSelect.SelectedIndex = (int)musicInput.Value - 0x21;
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateRoutePoints(true);
        }

        private void areaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            propertyGrid7.SelectedObject = areaList.SelectedItem;

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

        private void propertyGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateCameras(true);
        }

        private void enemyRouteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            propertyGrid6.SelectedObject = enemyRouteList.SelectedItem;

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

        private void propertyGrid3_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateObjects(true);
        }

        public void ChangeText(string text)
        {
            loadingLabel.Text = text;
        }

        private void bTIViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BTIReader btireader = new BTIReader();
            btireader.Show();
        }

        private void insertCourseModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Select a course model to render. Be careful, however. This will overwrite the current course rendered! If you choose not to import one, hit 'Cancel'.", "Important", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                OpenFileDialog openBMD = new OpenFileDialog();
                openBMD.Filter = "Model files (*.bmd)|*.bmd|All files (*.*)|*.*";

                if (openBMD.ShowDialog() == DialogResult.OK)
                {
                    // delete the course already rendered
                    GL.DeleteLists(courseList, 1);

                    courseList = GL.GenLists(1);

                    FileBase fb = new FileBase();
                    fb.Stream = new FileStream(openBMD.FileName, FileMode.Open);
                    course = new Bmd(fb);
                    fb.Close();

                    GL.NewList(courseList, ListMode.Compile);
                    if (course != null)
                        DrawBMD(course);
                    GL.EndList();
                }
            }
        }

        private void propertyGrid8_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateKartPoints(true);
        }

        private void propertyGrid7_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateAreas(true);
        }

        private void propertyGrid5_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateCheckpoints(true);
        }

        private void propertyGrid6_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateEnemyPoints(true);
        }

        private void selectAllEnemy_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void selectAllInGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoutePointObject rtPt = (RoutePointObject)routeList.SelectedItem;
            uint groupID = rtPt.groupID;
            uint nodeCount = 0;

            selectedList = GL.GenLists(1);
            GL.NewList(selectedList, ListMode.Compile);
            foreach (RoutePointObject routePoint in routeList.Items)
            {
                if (routePoint.groupID == groupID)
                {
                    GL.PushMatrix();
                    GL.Translate(routePoint.xPos, routePoint.yPos, routePoint.zPos);
                    GL.Scale(2f, 2f, 2f);
                    DrawCube(1f, 1f, 1f, false, false, false);
                    GL.PopMatrix();
                    nodeCount += 1;
                }
            }
            GL.EndList();

            selectionInfo.Text = "Currently selected: " + nodeCount + " node(s) in route group " + groupID + ".";
        }

        private void routeList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && routeList.SelectedIndex != -1)
            {
                routeContext.Show(routeList, e.Location);
            }
        }

        private void addObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectAddForm objAdd = new ObjectAddForm();
            objAdd.getList(objList);
            objAdd.Show();
        }

        private void chckGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid9.SelectedObject = chckGroup.SelectedItem;
        }

        private void routeGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid10.SelectedObject = routeGroupList.SelectedItem;
        }

        private void insertRouteHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnemyRoute enemyRoute = new EnemyRoute();
            enemyRoute.link = -1;
            enemyRouteList.Items.Insert(enemyRouteList.SelectedIndex, enemyRoute);

            UpdateEnemyPoints(true);
        }

        private void routeGroupList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            propertyGrid10.SelectedObject = routeGroupList.SelectedItem;
        }

        private void enemyRouteList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && enemyRouteList.SelectedIndex != -1)
            {
                enemyRouteContext.Show(enemyRouteList, e.Location);
            }
        }

        private void deleteEnemPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enemyRouteList.SelectedIndex != -1)
            {
                EnemyRoute enmRoute = (EnemyRoute)enemyRouteList.SelectedItem;
                if (enmRoute.link != -1)
                {
                    DialogResult result = MessageBox.Show("You are deleting a point that creates a new group, or ends one. Are you sure you want to delete it?", "Deleting Important Point", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        enemyRouteList.Items.Remove(enemyRouteList.SelectedItem);

                        UpdateEnemyPoints(true);
                    }
                    else
                        return;
                }
            }
        }

        private void insertRouteAtBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnemyRoute enemyRoute = new EnemyRoute();
            enemyRoute.link = -1;
            enemyRouteList.Items.Add(enemyRoute);

            UpdateEnemyPoints(true);
        }

        private void duplicatePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enemyRouteList.SelectedIndex != -1)
            {
                EnemyRoute enemyRoute = (EnemyRoute)enemyRouteList.SelectedItem;
                enemyRouteList.Items.Add(enemyRoute);

                UpdateEnemyPoints(true);
            }
        }

        private void bCOToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BCOEditor bco = new BCOEditor();
            bco.Show();
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objList.SelectedIndex != -1)
            {
                objList.Items.Remove(objList.SelectedItem);
                UpdateObjects(true);
            }
        }

        private void duplicateObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objList.SelectedIndex != -1)
            {
                LevelObject levelObject = (LevelObject)objList.SelectedItem;
                objList.Items.Add(levelObject);
                UpdateObjects(true);
            }
        }

        private void addCheckpointGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckpointGroupObject checkpointGroup = new CheckpointGroupObject();
            int count = chckGroup.Items.Count;

            checkpointGroup.groupLink = (ushort)count;
            chckGroup.Items.Add(checkpointGroup);
            UpdateCheckpointGroups(true);
        }

        private void deleteCheckpointGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chckGroup.SelectedIndex != -1)
            {
                CheckpointGroupObject checkpointGrp = (CheckpointGroupObject)chckGroup.SelectedItem;

                ushort id = checkpointGrp.index;

                foreach (CheckpointObject chckObj in chckList.Items)
                {
                    if (chckObj.groupID == id)
                    {
                        DialogResult result = MessageBox.Show("This checkpoint group you want to delete has checkpoints still assigned to it. Deleting this will be dangerous. Are you sure you want to delete this?", "Dangerous Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (result == DialogResult.Yes)
                        {
                            chckGroup.Items.Remove(chckGroup.SelectedItem);
                            UpdateCheckpointGroups(true);
                            return;
                        }
                        else
                            return;
                    }
                    else
                    {
                        chckGroup.Items.Remove(chckGroup.SelectedItem);
                        UpdateCheckpointGroups(true);
                        return;
                    }
                }
            }
        }

        private void duplicateCheckpointGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chckGroup.SelectedIndex != -1)
            {
                CheckpointGroupObject checkpointGrp = (CheckpointGroupObject)chckGroup.SelectedItem;
                chckGroup.Items.Add(checkpointGrp);
                UpdateCheckpointGroups(true);
            }
        }

        private void addCheckpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckpointObject checkpoint = new CheckpointObject();
            chckList.Items.Add(checkpoint);
            UpdateCheckpoints(true);
        }

        private void insertCheckpointHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chckList.SelectedIndex != -1)
            {
                CheckpointObject checkpoint = new CheckpointObject();
                chckList.Items.Insert(chckList.SelectedIndex, checkpoint);
                UpdateCheckpoints(true);
            }
        }

        private void deleteCheckpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chckList.SelectedIndex != -1)
            {
                chckList.Items.Remove(chckList.SelectedItem);
                UpdateCheckpoints(true);
            }
        }

        private void duplicateCheckpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chckList.SelectedIndex != -1)
            {
                CheckpointObject checkpoint = (CheckpointObject)chckList.SelectedItem;
                chckList.Items.Add(checkpoint);
                UpdateCheckpoints(true);
            }
        }

        private void chckList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chckList.SelectedIndex != -1)
            {
                checkpointContext.Show(chckList, e.Location);
            }
        }

        private void chckGroup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chckGroup.SelectedIndex != -1)
            {
                checkpointGroupContext.Show(chckGroup, e.Location);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private void selectAllInGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnemyRoute curRoute = (EnemyRoute)enemyRouteList.SelectedItem;
            byte groupID = curRoute.group;
            uint enemyCount = 0;

            selectedList = GL.GenLists(1);
            GL.NewList(selectedList, ListMode.Compile);
            foreach (EnemyRoute enemyRoute in enemyRouteList.Items)
            {
                if (enemyRoute.group == groupID)
                {
                    GL.PushMatrix();
                    GL.Translate(enemyRoute.xPos, enemyRoute.yPos, enemyRoute.zPos);
                    GL.Scale(2f, 2f, 2f);
                    DrawCube(1f, 1f, 1f, false, false, false);
                    GL.PopMatrix();
                    enemyCount += 1;
                }
            }
            GL.EndList();

            selectionInfo.Text = "Currently selected: " + enemyCount + " node(s) in enemy group " + groupID + ".";
        }

        private void unknown7_ValueChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                bol.unk7 = (float)unknown7.Value;
            }
        }

        private void unknown8_ValueChanged(object sender, EventArgs e)
        {
            foreach (BOLInformation bol in bolInf)
            {
                bol.unk8 = (float)unknown8.Value;
            }
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.knowsNewUpdate = false; // has to be set in order for the dialog to even show

            UpdateForm update = new UpdateForm();
            update.doUpdateCheck(true);
        }

        private void addRespawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RespawnObject respawnObj = new RespawnObject();
            respawnObj.respawnID = (ushort)respList.Items.Count; // mind as well, since the IDs are relative to 0

            respList.Items.Add(respawnObj);
            UpdateRespawns(true);
        }

        private void deleteRespawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (respList.SelectedIndex != -1)
            {
                respList.Items.Remove(respList.SelectedItem);
            }
        }

        private void duplicateRespawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (respList.SelectedIndex != -1)
            {
                RespawnObject respObj = (RespawnObject)respList.SelectedItem;
                respList.Items.Add(respObj);
            }
        }

        private void addCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraObject camObj = new CameraObject();
            camObj.name = "null";
            camObj.unk1 = 1; // 1 is the most common so shrug
            camObj.type = 1;
            camObj.routeID = -1;
            camObj.startZoom = 15;
            camObj.routeSpeed = 25;
            cameraList.Items.Add(camObj);

            UpdateCameras(true);
        }

        private void deleteCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraList.SelectedIndex != -1)
            {
                cameraList.Items.Remove(cameraList.SelectedItem);
                UpdateCameras(true);
            }
        }

        private void duplicateCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraList.SelectedIndex != -1)
            {
                CameraObject camObj = (CameraObject)cameraList.SelectedItem;
                cameraList.Items.Add(camObj);
                UpdateCameras(true);
            }
        }

        private void addAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaObject areaObj = new AreaObject();

            areaList.Items.Add(areaObj);

            UpdateAreas(true);
        }

        private void deleteAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (areaList.SelectedIndex != -1)
            {
                areaList.Items.Remove(areaList.SelectedItem);
                UpdateAreas(true);
            }
        }

        private void duplicateAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (areaList.SelectedIndex != -1)
            {
                AreaObject areaObj = (AreaObject)areaList.SelectedItem;
                areaList.Items.Add(areaObj);
                UpdateAreas(true);
            }
        }

        private void addStartingPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KartPointObject kartObj = new KartPointObject();
            kartObj.playerID = 0xFF;
            kartObj.polePos = 0;

            kartPointList.Items.Add(kartObj);
            UpdateKartPoints(true);
        }

        private void deleteStartingPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kartPointList.SelectedIndex != -1)
            {
                kartPointList.Items.Remove(kartPointList.SelectedItem);
                UpdateKartPoints(true);
            }
        }

        private void duplicateStartingPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kartPointList.SelectedIndex != -1)
            {
                KartPointObject areaObj = (KartPointObject)kartPointList.SelectedItem;
                kartPointList.Items.Add(areaObj);
                UpdateKartPoints(true);
            }
        }

        private void addRouteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RouteGroupSetup routeGroup = new RouteGroupSetup();
            routeGroupList.Items.Add(routeGroup);
            UpdateRouteGroups(true);
        }

        private void deleteRouteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (routeGroupList.SelectedIndex != -1)
            {
                routeGroupList.Items.Remove(routeGroupList.SelectedItem);
                UpdateRouteGroups(true);
            }
        }

        private void selectAllInGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (routeGroupList.SelectedIndex != -1)
            {
                RouteGroupSetup routeGroup = (RouteGroupSetup)routeGroupList.SelectedItem;
                uint id = routeGroup.groupID;
                uint count = 0;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                foreach (RoutePointObject routePoint in routeList.Items)
                {
                    if (routePoint.groupID == id)
                    {
                        GL.PushMatrix();
                        GL.Translate(routePoint.xPos, routePoint.yPos, routePoint.zPos);
                        GL.Scale(2f, 2f, 2f);
                        DrawCube(1f, 1f, 1f, false, false, false);
                        GL.PopMatrix();
                        count += 1;
                    }
                }
                GL.EndList();
                selectionInfo.Text = "Currently selected: " + count + " points in group " + id + "."; 
                
            }
        }

        private void addRoutePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO group id check
            RoutePointObject routePoint = new RoutePointObject();
            routeList.Items.Add(routePoint);
            UpdateRoutePoints(true);
        }

        private void insertRoutePointHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // store the index that we currently have (pretty much where we want to insert our entry)
            int index = routeList.SelectedIndex;
            uint id;

            // go to the entry before it to get our group ID
            routeList.SelectedIndex = routeList.SelectedIndex - 1;
            RoutePointObject routeObjBefore = (RoutePointObject)routeList.SelectedItem;
            id = routeObjBefore.groupID;

            // now we go back to our original index
            routeList.SelectedIndex = index;
            RoutePointObject routeObj = new RoutePointObject();
            routeObj.groupID = id;

            routeList.Items.Insert(index, routeObj);
            UpdateRoutePoints(true);
        }

        private void deleteRoutePointToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (routeList.SelectedIndex != -1)
            {
                routeList.Items.Remove(routeList.SelectedItem);
                UpdateRoutePoints(true);
            }
        }

        private void duplicateRoutePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoutePointObject routeObj = (RoutePointObject)routeList.SelectedItem;
            routeList.Items.Add(routeObj);
            UpdateRoutePoints(true);
        }

        private void routeGroupList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && routeGroupList.SelectedIndex != -1)
            {
                routeGroupContext.Show(routeGroupList, e.Location);
            }
        }

        private void kartPointList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && kartPointList.SelectedIndex != -1)
            {
                kartContext.Show(kartPointList, e.Location);
            }
        }

        private void areaList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && areaList.SelectedIndex != -1)
            {
                areaContext.Show(areaList, e.Location);
            }
        }

        private void cameraList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && cameraList.SelectedIndex != -1)
            {
                cameraContext.Show(cameraList, e.Location);
            }
        }

        private void respList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && respList.SelectedIndex != -1)
            {
                respawnContext.Show(respList, e.Location);
            }
        }

        private void propertyGrid4_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateRespawns(true);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initNewTrack();
        }

        public void UpdateEnemyPoints(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);

            if (isUpdate)
            {
                GL.DeleteLists(enemyPointList, 1);
                
                enmRoute.Clear();
                enemyRouteList.Refresh();

                foreach (EnemyRoute enemyObj in enemyRouteList.Items)
                {
                    enmRoute.Add(enemyObj);
                }

                enemyRouteList.Items.Clear();
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
        }

        public void UpdateRouteGroups(bool isUpdate)
        {
            if (isUpdate)
            {
                routeGroupList.Items.Clear();
                grpSetup.Clear();

                foreach (RouteGroupSetup routeSetup in routeGroupList.Items)
                {
                    grpSetup.Add(routeSetup);
                }
            }

            foreach (RouteGroupSetup routeSetup in grpSetup)
            {
                routeGroupList.Items.Add(routeSetup);
            }
        }

        public void UpdateRoutePoints(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);

            if (isUpdate)
            {
                GL.DeleteLists(routePointList, 1);
                
                rpobj.Clear();
                routeList.Refresh();

                foreach (RoutePointObject routeObj in routeList.Items)
                {
                    rpobj.Add(routeObj);
                }
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
                    GL.PushMatrix();
                    GL.Translate(curX, curY, curZ);
                    GL.Scale(1f, 1f, 1f);
                    DrawCube(0f, 0f, 1f, true, true, false);
                    GL.PopMatrix();

                    currentID += 1;
                    firstEntry = true;
                }
            }
            GL.EndList();

            glControl1.Invalidate();
        }

        public void UpdateCheckpointGroups(bool isUpdate)
        {
            if (isUpdate)
            {
                chkGRP.Clear();

                foreach (CheckpointGroupObject chckPtGrp in chckGroup.Items)
                {
                    chkGRP.Add(chckPtGrp);
                }

                chckGroup.Items.Clear();
            }

            foreach (CheckpointGroupObject objEntry in chkGRP)
            {
                chckGroup.Items.Add(objEntry);
            }
        }

        /// <summary>
        /// Updates checkpoints
        /// </summary>
        public void UpdateCheckpoints(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);

            if (isUpdate)
            {
                GL.DeleteLists(checkpointList, 1);
                
                chkobj.Clear();
                chckList.Refresh();

                foreach (CheckpointObject chckPt in chckList.Items)
                {
                    chkobj.Add(chckPt);
                }

                chckList.Items.Clear();
            }

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
        }

        Bmd objModel;
        public void UpdateObjects(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);
            if (isUpdate)
            {
                GL.DeleteLists(objectList, 1);
                
                lvlobj.Clear();
                objList.Refresh();

                foreach (LevelObject levelObj in objList.Items)
                {
                    lvlobj.Add(levelObj);
                }

                objList.Items.Clear();
            }

            objectList = GL.GenLists(1);
            GL.NewList(objectList, ListMode.Compile);

            foreach (LevelObject objEntry in lvlobj)
            {
                objList.Items.Add(objEntry);

                this.Invoke((Action)delegate()
                {
                    ChangeText("Loading object " + objEntry.objID);
                });

                // get the new model name
                MiscHacks misc = new MiscHacks();
                objEntry.modelName = misc.returnModel(objEntry.objID);

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
                        if (File.Exists(Properties.Settings.Default.curDir + "/objects/" + objEntry.modelName + ".bmd"))
                        {
                            objFB.Stream = new FileStream(Properties.Settings.Default.curDir + "/objects/" + objEntry.modelName + ".bmd", FileMode.Open);
                            Bmd obj = new Bmd(objFB);
                            GL.Scale(objEntry.xScale, objEntry.yScale, objEntry.zScale);
                            DrawBMD(obj);
                            objModelList.Add(objEntry.modelName, obj);
                        }

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
        }

        public void UpdateKartPoints(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);

            if (isUpdate)
            {
                GL.DeleteLists(kartList, 1);
                kartobj.Clear();
                kartPointList.Refresh();

                foreach (KartPointObject kartObj in kartPointList.Items)
                {
                    kartobj.Add(kartObj);
                }
                kartPointList.Items.Clear();
            }

            int rotation;
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
        }

        public void UpdateAreas(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);

            if (isUpdate)
            {
                GL.DeleteLists(areaObjList, 1);

                areaobj.Clear();
                areaList.Refresh();

                foreach (AreaObject areaObj in areaList.Items)
                {
                    areaobj.Add(areaObj);
                }

                areaList.Items.Clear();
            }

            int rotation;
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
        }

        public void UpdateCameras(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);
            if (isUpdate)
            {
                GL.DeleteLists(camList, 1);
                
                camobj.Clear();
                cameraList.Refresh();

                foreach (CameraObject cameraObj in cameraList.Items)
                {
                    camobj.Add(cameraObj);
                }

                cameraList.Items.Clear();
            }

            int rotation;
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
        }

        public void UpdateRespawns(bool isUpdate)
        {
            GL.DeleteLists(selectedList, 1);
            if (isUpdate)
            {
                GL.DeleteLists(respawnList, 1);
                resObj.Clear();
                respList.Refresh();

                foreach (RespawnObject respObj in respList.Items)
                {
                    resObj.Add(respObj);
                }

                respList.Items.Clear();
            }

            int rotation;
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
        }
    }
}