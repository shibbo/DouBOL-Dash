using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DouBOLDash
{
    public partial class RespawnEditor : Form
    {
        public RespawnEditor()
        {
            InitializeComponent();
        }

        public void loadData(RespawnObject obj)
        {
            xInput.Value = (decimal)obj.xPos;
            yInput.Value = (decimal)obj.yPos;
            zInput.Value = (decimal)obj.zPos;

            rotInput.Value = (int)obj.rotation;

            groupInput.Value = (int)obj.respawnID;
            unk1.Value = (int)obj.unk1;
            unk2.Value = (int)obj.unk2;
            unk3.Value = (int)obj.unk3;
        }
    }
}
