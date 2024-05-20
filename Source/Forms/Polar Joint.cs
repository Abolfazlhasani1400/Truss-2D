using System;
using System.Windows.Forms;

namespace Truss_2D
{
    public partial class Polar_Form : Form
    {
        Joint joint;
        DashBoard dashboard;
        double OUT; //for tryout
        public Polar_Form(Joint Passjoint, DashBoard PassDashBoard)
        {
            InitializeComponent();
            joint = Passjoint;
            dashboard = PassDashBoard;
        }

        private void Add_Polar_Joint_Click(object sender, EventArgs e)
        {
            if (textBox_R.Text != string.Empty && textBox_Theta.Text != string.Empty)
            {
                if (double.TryParse(textBox_R.Text, out OUT) && double.TryParse(textBox_Theta.Text, out OUT))
                {
                    error_label.Text = string.Empty;

                    Joint relative_joint = dashboard.CreateJoint
                        (
                        joint.XL + double.Parse(textBox_R.Text) * Math.Cos(double.Parse(textBox_Theta.Text) * (Math.PI) / 180),
                        joint.YL + double.Parse(textBox_R.Text) * Math.Sin(double.Parse(textBox_Theta.Text) * (Math.PI) / 180),
                        joint
                        );

                    if (CreateNewLink_CheckBox.CheckState == CheckState.Checked)
                    {
                        dashboard.CreateLink(joint, relative_joint);
                    }
                    this.Close();
                }
                else
                {
                    error_label.Text = "Please enter number only!";
                }
            }
            else
            {
                error_label.Text = "Please enter Polar coordinates!";
            }
        }
    }
}
