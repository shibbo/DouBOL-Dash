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

        private const float k_FOV = (float)((70f * Math.PI) / 180f);
        private const float k_zNear = 0.01f;
        private const float k_zFar = 1000f;

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

        private bool Selected = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Objects list
        int checkpointList = 0;
        int objectList = 0;

        // BMD pre-render list
        int courseList = 0;
        Bmd course;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.envDir == "")
                doFolderChoose();
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
                string dirName = Path.GetDirectoryName(openFileDialog1.FileName);
                string fileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                Properties.Settings.Default.curDir = dirName;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.curFile = fileName;
                Properties.Settings.Default.Save();
                if (loaded)
                {
                    // if these aren't cleared, then we get an out of bounds
                    GL.DeleteLists(courseList, 1);
                    GL.DeleteLists(checkpointList, 1);
                    GL.DeleteLists(objectList, 1);
                }
                using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open)))
                {
                    BOL bol = new BOL();
                    bol.Parse(reader);

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
                    List<CheckpointObject> chkobj = chckPt.returnList();

                    RouteGroup routeGrp = new RouteGroup();
                    routeGrp.Parse(reader, sec3Count);
                    Dictionary<uint, GroupStruct> dictionary2 = routeGrp.returnDictionary();

                    RoutePoint routePt = new RoutePoint();
                    routePt.Parse(reader, dictionary2);

                    Object obj = new Object();
                    obj.Parse(reader, count2);
                    List<LevelObject> lvlobj = obj.returnList();

                    KartPoint kart = new KartPoint();
                    kart.Parse(reader);

                    Area area = new Area();
                    area.Parse(reader, count3);

                    Camera camera = new Camera();
                    camera.Parse(reader, count4);

                    Respawn respawn = new Respawn();
                    respawn.Parse(reader, count6);

                    reader.Close();

                    courseList = GL.GenLists(1);


                    if (Properties.Settings.Default.bmdEnabled != false)
                    {
                        FileBase fb = new FileBase();
                        if (File.Exists(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile + ".bmd"))
                        {
                            Console.WriteLine(Properties.Settings.Default.curDir + "\\" + Properties.Settings.Default.curFile+".bmd");
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

                    objectList = GL.GenLists(1);
                    GL.NewList(objectList, ListMode.Compile);
                    foreach (LevelObject objEntry in lvlobj)
                    {
                        loadingLabel.Text = "Loading object " + objEntry.objID;
                        GL.PushMatrix();
                        GL.Translate(objEntry.xPos, objEntry.yPos, objEntry.zPos);
                        GL.Rotate(objEntry.rotation, 0f, 1f, 0f);
                        GL.Scale(1f, 1f, 1f);
                        DrawCube(0f, 1f, 0f);
                        GL.PopMatrix();
                    }
                    GL.EndList();

                    checkpointList = GL.GenLists(1);
                    GL.NewList(checkpointList, ListMode.Compile);
                    foreach (CheckpointObject objEntry in chkobj)
                    {
                        loadingLabel.Text = "Loading checkpoint...";
                        GL.PushMatrix();
                        GL.Translate(objEntry.xPosStart, objEntry.yPosStart, objEntry.zPosStart);
                        GL.Scale(1f, 1f, 1f);
                        DrawCube(0.5f, 0.25f, 0f);
                        GL.PopMatrix();

                        GL.PushMatrix();
                        GL.Translate(objEntry.xPosEnd, objEntry.yPosEnd, objEntry.zPosEnd);
                        GL.Scale(1f, 1f, 1f);
                        DrawCube(0.5f, 0.25f, 0f);
                        GL.PopMatrix();

                        GL.PushMatrix();
                        GL.Begin(BeginMode.Lines);
                        GL.Color4(0.5f, 0.25f, 0f, 1f);
                        GL.Vertex3(objEntry.xPosStart, objEntry.yPosStart, objEntry.zPosStart);
                        GL.Vertex3(objEntry.xPosEnd, objEntry.yPosEnd, objEntry.zPosEnd);
                        GL.End();
                        GL.PopMatrix();
                        
                    }
                    GL.EndList();

                    loadingLabel.Text = "Loading course model...";
                    GL.NewList(courseList, ListMode.Compile);
                    if (course != null)
                        DrawBMD(course);
                    GL.EndList();

                    loadingLabel.Text = "Ready!";
                }
            }
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

            Control control = sender as Control;
            Point pt = control.PointToClient(Control.MousePosition);
            mousePosLabel.Text = "X: " + pt.X.ToString() + ", Y: " + pt.Y.ToString();

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
            if (e.KeyCode == Keys.A)
            {
                m_CamPosition.X += 10f;
                m_CamPosition.Y += 10f;
                m_CamPosition.Z += 10f;
                Console.WriteLine("Camera position is at " + m_CamPosition.X);
            }
            if (e.KeyCode == Keys.S)
            {
                
            }
            if (e.KeyCode == Keys.D)
            {
                
            }
            if (e.KeyCode == Keys.W)
            {
                
            }

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

            GL.CallList(checkpointList);
            GL.CallList(objectList);

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

        public static void DrawBMD(Bmd model, RenderMode rnd = RenderMode.Opaque)
        {
            RenderInfo ri = new RenderInfo();
            ri.Mode = rnd;

            BmdRenderer br = new BmdRenderer(model);
            br.Render(ri);
        }


        public static void DrawCube(float color1, float color2, float color3, RenderMode rnd = RenderMode.Opaque)
        {
            RenderInfo ri = new RenderInfo();
            ri.Mode = rnd;

            RendererBase cubeRender = new ColorCubeRenderer(250f, new Vector4(1f, 1f, 1f, 1f), new Vector4(color1, color2, color3, 1f), true);
            cubeRender.Render(ri);
        }
    }
}
