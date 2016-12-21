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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DouBOLDash
{
    public partial class BCOEditor : Form
    {
        public BCOEditor()
        {
            InitializeComponent();
        }

        List<BCOInformation> bcoInfo;
        List<BCOTri> bcoTriangles;
        List<BCOVert> bcoListVerts;
        List<Vector3> vecList;

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

        // GL rendering vert list
        public int vertList = 0;
        // GL rendering tri list
        public int triList = 0;
        // GL selected face list
        public int selectedList = 0;

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

            GL.CallList(vertList);
            GL.CallList(triList);
            GL.CallList(selectedList);

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

        bool loaded = false;

        private void openBCOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BCO files (*.bco)|*.bco|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                trisListBox.Items.Clear();
                vertsListBox.Items.Clear();

                using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open)))
                {
                    BCO bco = new BCO();
                    bco.Parse(reader);
                    bcoInfo = bco.returnBCOInf();

                    foreach (BCOInformation bcoInformation in bcoInfo)
                    {
                        uint count = bcoInformation.unkOffs3 - bcoInformation.vertOffs;
                        count = count / 36;
                        uint count2 = bcoInformation.vertOffs - bcoInformation.triOffs;
                        count2 = count2 / 36;

                        reader.BaseStream.Seek(bcoInformation.triOffs, 0);
                        BCOTris bcoTris = new BCOTris();
                        bcoTris.Parse(reader, count2);
                        bcoTriangles = bcoTris.returnTris();

                        reader.BaseStream.Seek(bcoInformation.vertOffs, 0);
                        BCOVerts bcoVerts = new BCOVerts();
                        bcoVerts.Parse(reader, count);
                        bcoListVerts = bcoVerts.returnBCOVerts();
                        vecList = bcoVerts.returnVectors();
                    }

                    // first we draw verts
                    vertList = GL.GenLists(1);
                    GL.NewList(vertList, ListMode.Compile);
                    GL.Begin(BeginMode.Points);
                    foreach (BCOVert bcoVertic in bcoListVerts)
                    {
                        GL.Color3(Color.DeepSkyBlue);
                        GL.PointSize(2f);
                        GL.Vertex3(bcoVertic.xPos1, bcoVertic.yPos1, bcoVertic.zPos1);
                        GL.Vertex3(bcoVertic.xPos2, bcoVertic.yPos2, bcoVertic.zPos2);
                        GL.Vertex3(bcoVertic.xPos3, bcoVertic.yPos3, bcoVertic.zPos3);
                    }
                    GL.End();
                    GL.EndList();

                    // now we draw tris

                    int index1, index2, index3;
                    Vector3 set1, set2, set3;

                    Console.WriteLine(vecList.Count);

                    triList = GL.GenLists(1);
                    GL.NewList(triList, ListMode.Compile);
                    GL.Begin(BeginMode.Triangles);
                    foreach (BCOTri bcTri in bcoTriangles)
                    {
                        trisListBox.Items.Add(bcTri);

                        index1 = bcTri.index1;
                        index2 = bcTri.index2;
                        index3 = bcTri.index3;

                        set1 = vecList[index1];
                        set2 = vecList[index2];
                        set3 = vecList[index3];

                        MiscHacks misc = new MiscHacks();
                        Color faceColor = misc.returnColor(bcTri.collisionFlag);

                        GL.Color4(faceColor);
                        GL.Vertex3(set1.X, set1.Y, set1.Z);
                        GL.Vertex3(set2.X, set2.Y, set2.Z);
                        GL.Vertex3(set3.X, set3.Y, set3.Z);
                    }
                    GL.End();
                    GL.EndList();

                    reader.Close();
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

        private void trisListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedList != 0)
                GL.DeleteLists(selectedList, 1);

            propertyGrid1.SelectedObject = trisListBox.SelectedItem;

            if (trisListBox.SelectedIndex != -1)
            {
                Vector3 set1, set2, set3;
                BCOTri bcoTri = (BCOTri)trisListBox.SelectedItem;

                set1 = vecList[bcoTri.index1];
                set2 = vecList[bcoTri.index2];
                set3 = vecList[bcoTri.index3];

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);

                GL.PushMatrix();
                GL.Translate(set1.X, set1.Y, set1.Z);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false);
                GL.PopMatrix();

                GL.PushMatrix();
                GL.Translate(set2.X, set2.Y, set2.Z);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false);
                GL.PopMatrix();

                GL.PushMatrix();
                GL.Translate(set3.X, set3.Y, set3.Z);
                GL.Scale(2f, 2f, 2f);
                DrawCube(1f, 1f, 1f, false, false);
                GL.PopMatrix();

                GL.EndList();

                glControl1.Update();
            }
        }

        private void selectFacesWithCollisionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (trisListBox.SelectedIndex != -1)
            {
                BCOTri selectedTri = (BCOTri)trisListBox.SelectedItem;
                short colFlag = selectedTri.collisionFlag;

                selectedList = GL.GenLists(1);
                GL.NewList(selectedList, ListMode.Compile);
                foreach (BCOTri tri in trisListBox.Items)
                {
                    if (tri.collisionFlag == colFlag)
                    {
                        Vector3 set1, set2, set3;

                        set1 = vecList[tri.index1];
                        set2 = vecList[tri.index2];
                        set3 = vecList[tri.index3];

                        GL.PushMatrix();
                        GL.Translate(set1.X, set1.Y, set1.Z);
                        GL.Scale(2f, 2f, 2f);
                        DrawCube(1f, 1f, 1f, false, false);
                        GL.PopMatrix();

                        GL.PushMatrix();
                        GL.Translate(set2.X, set2.Y, set2.Z);
                        GL.Scale(2f, 2f, 2f);
                        DrawCube(1f, 1f, 1f, false, false);
                        GL.PopMatrix();

                        GL.Begin(BeginMode.Lines);
                        GL.Color4(1.000f, 0.255f, 0.212f, 1f);
                        GL.LineWidth(2f);
                        GL.Vertex3(set1.X, set1.Y, set1.Z);
                        GL.Vertex3(set2.X, set2.Y, set2.Z);
                        GL.End();

                        GL.PushMatrix();
                        GL.Translate(set3.X, set3.Y, set3.Z);
                        GL.Scale(2f, 2f, 2f);
                        DrawCube(1f, 1f, 1f, false, false);
                        GL.PopMatrix();

                        GL.Begin(BeginMode.Lines);
                        GL.Color4(1.000f, 0.255f, 0.212f, 1f);
                        GL.LineWidth(2f);
                        GL.Vertex3(set2.X, set2.Y, set2.Z);
                        GL.Vertex3(set3.X, set3.Y, set3.Z);
                        GL.End();

                        GL.Begin(BeginMode.Lines);
                        GL.Color4(1.000f, 0.255f, 0.212f, 1f);
                        GL.LineWidth(2f);
                        GL.Vertex3(set3.X, set3.Y, set3.Z);
                        GL.Vertex3(set1.X, set1.Y, set1.Z);
                        GL.End();
                    }
                }
                GL.EndList();

                glControl1.Update();
            }
        }

        public static void DrawCube(float color1, float color2, float color3, bool showAxis, bool useFill, RenderMode rnd = RenderMode.Opaque)
        {
            RenderInfo ri = new RenderInfo();
            ri.Mode = rnd;
            RendererBase cubeRender;

            if (!useFill)
            {
                cubeRender = new ColorCubeRenderer(250f, new Vector4(1f, 0f, 0f, 1f), new Vector4(color1, color2, color3, 1f), showAxis, useFill);
                cubeRender.Render(ri);
            }
            else
            {
                cubeRender = new ColorCubeRenderer(250f, new Vector4(1f, 1f, 1f, 1f), new Vector4(color1, color2, color3, 1f), showAxis, useFill);
                cubeRender.Render(ri);
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
    }
}
