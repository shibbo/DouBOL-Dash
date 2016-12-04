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
            MainWindow main = new MainWindow();

            foreach (RespawnObject respobj in listbox.Items)
            {
                Console.WriteLine("boop");
                shitballs.Add(respobj);
            }

            GL.DeleteLists(shit2, 1);
            listbox.Items.Clear();

            RespawnObject res = new RespawnObject();

            double xRot = 0;
            double yRot = 0;

            int rotation = Convert.ToInt32(Math.Round(rotInput.Value, 0));
            uint resID = Convert.ToUInt32(Math.Round(groupInput.Value, 0));
            uint unk1 = Convert.ToUInt32(Math.Round(unknown1.Value, 0));
            uint unk2 = Convert.ToUInt32(Math.Round(unknown2.Value, 0));
            uint unk3 = Convert.ToUInt32(Math.Round(unknown3.Value, 0));

            res.xPos = (float)xInput.Value;
            res.yPos = (float)yInput.Value;
            res.zPos = (float)zInput.Value;

            MiscHacks.inverseRotations(rotation, out xRot, out yRot);

            res.xRot = (int)xRot;
            res.yRot = (int)yRot;
            res.zRot = 655360000;

            res.respawnID = resID;
            res.unk1 = unk1;
            res.unk2 = unk2;
            res.unk3 = unk3;

            shitballs.Add(res);

            shit2 = GL.GenLists(1);
            GL.NewList(shit2, ListMode.Compile);
            foreach (RespawnObject resp in shitballs)
            {
                GL.PushMatrix();
                GL.Translate(resp.xPos, resp.yPos, resp.zPos);
                GL.Scale(1f, 1f, 1f);
                MainWindow.DrawCube(1f, 0.863f, 0f, true, true, false);
                GL.PopMatrix();

                listbox.Items.Add(resp);
            }
            GL.EndList();

           shit1.Refresh();

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
