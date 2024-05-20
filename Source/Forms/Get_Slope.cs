using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Truss_2D
{
    public partial class ChooseDiretion : Form
    {
        Joint Joint;
        public ChooseDiretion(Joint joint)
        {
            InitializeComponent();
            Joint = joint;

            Slope0_Check.Checked = true;
        }
        private void DrawItem()
        {
          
        }

        private void ChooseDirection_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case (Keys.Enter):
                    if (Slope0_Check.Checked)
                    {
                        Joint.XT = 1;
                        Joint.YT = 0;
                        Joint.Slope = 0;
                        Close();
                    }
                    else if(Slope90_Check.Checked)
                    {
                        Joint.XT = 0;
                        Joint.YT = 1;
                        Joint.Slope = 90;
                        Close();

                    }
                    else if (Slope180_Check.Checked)
                    {
                        Joint.XT = 1;
                        Joint.YT = 0;
                        Joint.Slope = 180;
                        Close();
                    }
                    else if (Slope270_Check.Checked)
                    {
                        Joint.XT = 0;
                        Joint.YT = 1;
                        Joint.Slope = 270;
                        Close();
                    }
                    break;
            }
        }

        private void Slope0_Check_CheckedChanged(object sender, EventArgs e)
        {
            if (Slope0_Check.Checked)
            {
                foreach (CheckBox item in OptionPanel.Controls)
                {
                    if(item == Slope0_Check)
                    {
                        continue;
                    }
                    item.Checked = false;
                }
            }
        }

        private void Slope90_Check_CheckedChanged(object sender, EventArgs e)
        {
            if (Slope90_Check.Checked)
            {
                foreach (CheckBox item in OptionPanel.Controls)
                {
                    if (item == Slope90_Check)
                    {
                        continue;
                    }
                    item.Checked = false;
                }
            }
        }
        private void Slope180_Check_CheckedChanged(object sender, EventArgs e)
        {
            if (Slope180_Check.Checked)
            {
                foreach (CheckBox item in OptionPanel.Controls)
                {
                    if (item == Slope180_Check)
                    {
                        continue;
                    }
                    item.Checked = false;
                }
            }
        }

        private void Slope270_Check_CheckedChanged(object sender, EventArgs e)
        {
            if (Slope270_Check.Checked)
            {
                foreach (CheckBox item in OptionPanel.Controls)
                {
                    if (item == Slope270_Check)
                    {
                        continue;
                    }
                    item.Checked = false;
                }
            }
        }
        private void OptionPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics JointGraphics = OptionPanel.CreateGraphics();
            JointGraphics.TranslateTransform(ClientRectangle.Width/2, ClientRectangle.Height/2);
            JointGraphics.ScaleTransform(1, -1);
            JointGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            List<Joint> Options = new List<Joint>();
            Joint joint1 = new Joint()
            {
                XG = -90,
                YG = +65,
                Slope = 0
            };
            Joint joint2 = new Joint()
            {
                XG = +60,
                YG = +55,
                Slope = 180
            };
            Joint joint3 = new Joint()
            {
                XG = -90,
                YG = -15,
                Slope = 90
            };
            Joint joint4 = new Joint()
            {
                XG = +60,
                YG = -15,
                Slope = 270
            };
            Options.Add(joint1);
            Options.Add(joint2);
            Options.Add(joint3);
            Options.Add(joint4);

            foreach(Joint joint in Options)
            {
                PointF[] Corners = new PointF[3];
                Corners[0] = new PointF(joint.XG, joint.YG);
                Corners[1] = new PointF(
                    joint.XG - (float)(10 * Math.Cos((60 + joint.Slope) * Math.PI / 180)),
                    joint.YG - (float)(10 * Math.Sin((60 + joint.Slope) * Math.PI / 180)));

                Corners[2] = new PointF(
                    joint.XG - (float)(10 * Math.Cos((120 + joint.Slope) * Math.PI / 180)),
                    joint.YG - (float)(10 * Math.Sin((120 + joint.Slope) * Math.PI / 180)));

                JointGraphics.FillPolygon(new SolidBrush(Color.Gray), Corners);
                JointGraphics.DrawPolygon(new Pen(Color.Orange, 1), Corners);

                JointGraphics.DrawLine(
                    new Pen(Color.Black, 1),
                    joint.XG - (float)(13 * Math.Cos((60 + joint.Slope) * Math.PI / 180)),
                    joint.YG - (float)(13 * Math.Sin((60 + joint.Slope) * Math.PI / 180)),
                    joint.XG - (float)(13 * Math.Cos((120 + joint.Slope) * Math.PI / 180)),
                    joint.YG - (float)(13 * Math.Sin((120 + joint.Slope) * Math.PI / 180)));
            }
        }

        
    }
}
