using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DouBOLDash
{
    public partial class RespawnAdd : Form
    {
        public RespawnAdd()
        {
            InitializeComponent();
        }

        ListBox listbox;
        GLControl shit1;
        int shit2;

        public void getLists(ListBox list, GLControl gl, int glList)
        {
            shit1 = gl;
            shit2 = glList;
            listbox = list;
        }

        List<RespawnObject> shitballs = new List<RespawnObject>();

        private void button1_Click(object sender, EventArgs e)
        {
            RespawnObject derp = new RespawnObject();

            double xRot = 0;
            double yRot = 0;

            int rotation = Convert.ToInt32(Math.Round(rotInput.Value, 0));
            ushort resID = Convert.ToUInt16(Math.Round(groupInput.Value, 0));
            ushort unk1 = Convert.ToUInt16(Math.Round(unknown1.Value, 0));
            ushort unk2 = Convert.ToUInt16(Math.Round(unknown2.Value, 0));
            ushort unk3 = Convert.ToUInt16(Math.Round(unknown3.Value, 0));

            derp.xPos = (float)xInput.Value;
            derp.yPos = (float)yInput.Value;
            derp.zPos = (float)zInput.Value;

            MiscHacks.inverseRotations(rotation, out xRot, out yRot);

            derp.xRot = (int)xRot;
            derp.yRot = (int)yRot;
            derp.zRot = 655360000;

            derp.respawnID = resID;
            derp.unk1 = unk1;
            derp.unk2 = unk2;
            derp.unk3 = unk3;

            foreach (RespawnObject respObj in listbox.Items)
            {
                shitballs.Add(respObj);
            }

            shitballs.Add(derp);
            listbox.Items.Add(derp);
            listbox.Refresh();

            GL.DeleteLists(shit2, 1);

            shit2 = GL.GenLists(1);
            GL.NewList(shit2, ListMode.Compile);
            foreach (RespawnObject resp in shitballs)
            {
                GL.PushMatrix();
                GL.Translate(resp.xPos, resp.yPos, resp.zPos);
                GL.Scale(1f, 1f, 1f);
                GL.Rotate(resp.rotation, 0f, 1f, 0f);
                MainWindow.DrawCube(1f, 0.863f, 0f, true, true, false);
                GL.PopMatrix();
            }
            GL.EndList();

            shit1.Invalidate();

            Close();
        }

        private void isMulti_CheckedChanged(object sender, EventArgs e)
        {
            if (isMulti.Checked)
                groupInput.Enabled = true;
            else
            {
                groupInput.Value = 0xFF;
                groupInput.Enabled = false;
            }
        }
    }
}
