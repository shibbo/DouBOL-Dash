/*
    Copyright 2016-2017 shibboleet, StapleButter
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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DouBOLDash
{
    public class ColorCubeRenderer : RendererBase
    {
        public ColorCubeRenderer(float size, Vector4 border, Vector4 fill, bool axes, bool useFill)
        {
            m_Size = size;
            m_BorderColor = border;
            m_FillColor = fill;
            m_ShowAxes = axes;
            m_useFill = useFill;
        }

        public override bool GottaRender(RenderInfo info)
        {
            return info.Mode != RenderMode.Translucent;
        }

        public override void Render(RenderInfo info)
        {
            if (info.Mode == RenderMode.Translucent) return;

            float s = m_Size / 2f;

            if (info.Mode != RenderMode.Picking)
            {
                for (int i = 0; i < 8; i++)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + i);
                    GL.Disable(EnableCap.Texture2D);
                }

                // if useFill is set to true, we fill in the cube
                if (m_useFill)
                {
                    GL.DepthFunc(DepthFunction.Lequal);
                    GL.DepthMask(true);
                    GL.Color4(m_FillColor);
                    GL.Disable(EnableCap.Lighting);
                    GL.Disable(EnableCap.Blend);
                    GL.Disable(EnableCap.ColorLogicOp);
                    GL.Disable(EnableCap.AlphaTest);
                    GL.CullFace(CullFaceMode.Front);
                    try { GL.UseProgram(0); } catch { }
                }
            }


            if (m_useFill)
            {
                GL.Begin(BeginMode.TriangleStrip);
                GL.Vertex3(-s, -s, -s);
                GL.Vertex3(-s, s, -s);
                GL.Vertex3(s, -s, -s);
                GL.Vertex3(s, s, -s);
                GL.Vertex3(s, -s, s);
                GL.Vertex3(s, s, s);
                GL.Vertex3(-s, -s, s);
                GL.Vertex3(-s, s, s);
                GL.Vertex3(-s, -s, -s);
                GL.Vertex3(-s, s, -s);
                GL.End();

                GL.Begin(BeginMode.TriangleStrip);
                GL.Vertex3(-s, s, -s);
                GL.Vertex3(-s, s, s);
                GL.Vertex3(s, s, -s);
                GL.Vertex3(s, s, s);
                GL.End();

                GL.Begin(BeginMode.TriangleStrip);
                GL.Vertex3(-s, -s, -s);
                GL.Vertex3(s, -s, -s);
                GL.Vertex3(-s, -s, s);
                GL.Vertex3(s, -s, s);
                GL.End();
            }

            if (info.Mode != RenderMode.Picking)
            {
                GL.LineWidth(1.5f);
                GL.Color4(m_BorderColor);

                GL.Begin(BeginMode.LineStrip);
                GL.Vertex3(s, s, s);
                GL.Vertex3(-s, s, s);
                GL.Vertex3(-s, s, -s);
                GL.Vertex3(s, s, -s);
                GL.Vertex3(s, s, s);
                GL.Vertex3(s, -s, s);
                GL.Vertex3(-s, -s, s);
                GL.Vertex3(-s, -s, -s);
                GL.Vertex3(s, -s, -s);
                GL.Vertex3(s, -s, s);
                GL.End();

                GL.Begin(BeginMode.Lines);
                GL.Vertex3(-s, s, s);
                GL.Vertex3(-s, -s, s);
                GL.Vertex3(-s, s, -s);
                GL.Vertex3(-s, -s, -s);
                GL.Vertex3(s, s, -s);
                GL.Vertex3(s, -s, -s);
                GL.End();

                if (m_ShowAxes)
                {
                    GL.Begin(BeginMode.Lines);
                    GL.Color3(1.0f, 0.0f, 0.0f);
                    GL.Vertex3(0.0f, 0.0f, 0.0f);
                    GL.Color3(1.0f, 0.0f, 0.0f);
                    GL.Vertex3(s * 2.0f, 0.0f, 0.0f);
                    GL.Color3(0.0f, 1.0f, 0.0f);
                    GL.Vertex3(0.0f, 0.0f, 0.0f);
                    GL.Color3(0.0f, 1.0f, 0.0f);
                    GL.Vertex3(0.0f, s * 2.0f, 0.0f);
                    GL.Color3(0.0f, 0.0f, 1.0f);
                    GL.Vertex3(0.0f, 0.0f, 0.0f);
                    GL.Color3(0.0f, 0.0f, 1.0f);
                    GL.Vertex3(0.0f, 0.0f, s * 2.0f);
                    GL.End();
                }
            }
        }


        private float m_Size;
        private Vector4 m_BorderColor, m_FillColor;
        private bool m_ShowAxes, m_useFill;
    }
}