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
    public partial class ObjectAddForm : Form
    {
        public ObjectAddForm()
        {
            InitializeComponent();
            label2.AutoSize = true;
            label2.MaximumSize = new Size(272, 0);
        }

        ListBox rofl;
        public void getList(ListBox list)
        {
            rofl = list;
        }

        Dictionary<int, string> descList = new Dictionary<int, string>() {
            {0, "A regular item box. Item boxes generate an item for the player, and can be singular, double, or quadtriple."},
            {1, "A route controlled item box. It functions the same as item boxes, except that it needs a route. Item boxes generate an item for the player, and can be singular, double, or quadtriple. Note: This needs a path to function properly!" }
        };

        Dictionary<int, string> indexToObject = new Dictionary<int, string>()
        {

        };

        public void setDesc(int id)
        {
            if (descList.ContainsKey(id))
            {
                label2.Text = descList[id];
            }
            else
                label2.Text = "No description.";
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            setDesc(listBox1.SelectedIndex);
            label1.Text = listBox1.Text;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
