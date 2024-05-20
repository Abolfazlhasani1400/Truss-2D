using System;
using System.Windows.Forms;

namespace Truss_2D
{
    public partial class JointProperties : UserControl
    {
        double Out; //for TryParse 

        Joint joint;
        DashBoard dashBoard;

        public void JointUpdate()
        {
            //X location
            if (double.TryParse(Location_X.Text, out Out))
            {
                joint.XL = double.Parse(Location_X.Text);
            }
            else
            {
                MessageBox.Show("Invalid X!");
            }
            //Y location
            if (double.TryParse(Location_Y.Text, out Out))
            {
                joint.YL = double.Parse(Location_Y.Text);
            }
            else
            {
                MessageBox.Show("Invalid Y!");
            }
            //Fx location
            if (double.TryParse(Fx.Text, out Out))
            {
                joint.XF = double.Parse(Fx.Text);
            }
            else
            {
                MessageBox.Show("Invalid Fx!");
            }
            //Fy location
            if (double.TryParse(Fy.Text, out Out))
            {
                joint.YF = double.Parse(Fy.Text);
            }
            else
            {
                MessageBox.Show("Invalid Fy!");
            }
            //
            dashBoard.SketchPanel.Invalidate();
            joint.Node.Text = joint.InFo;
        }

        public JointProperties(Joint PassJoint, DashBoard PassDashBoard)
        {
            InitializeComponent();
            joint = PassJoint;
            dashBoard = PassDashBoard;

            Dock = DockStyle.Fill;
            JointType_label.Text = joint.JT;

            Location_X.Text = Math.Round(joint.XL,4).ToString();
            Location_Y.Text = Math.Round(joint.YL,4).ToString();
            Fx.Text =         Math.Round(joint.XF,4).ToString();
            Fy.Text =         Math.Round(joint.YF,4).ToString();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Enter)://pressing ENTER btn//
                    JointUpdate();
                    break;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            JointUpdate();
        }
    }
}
