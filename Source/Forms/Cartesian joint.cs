using System;
using System.Windows.Forms;


namespace Truss_2D
{
    public partial class Cartesian_Form : Form
    {
        private Joint joint;
        private DashBoard dashboard;

        private double OUT;
        public Cartesian_Form(Joint Passjoint, DashBoard PassDashBoard)
        {
            InitializeComponent();
            joint = Passjoint;
            dashboard = PassDashBoard;
            CreateNewLink_CheckBox.CheckState = CheckState.Checked;
        }

        private void Add_cartesian_Joint_Click(object sender, EventArgs e)
        {
            if(textBox_X.Text!=string.Empty && textBox_Y.Text != string.Empty)
            {
                if (double.TryParse(textBox_X.Text, out OUT) && double.TryParse(textBox_Y.Text, out OUT))
                {
                    ErrorLabel.Text = string.Empty;

                    Joint relative_joint = dashboard.CreateJoint
                        (
                        joint.XL + double.Parse(textBox_X.Text),
                        joint.YL + double.Parse(textBox_Y.Text),
                        joint
                        );

                    if (CreateNewLink_CheckBox.CheckState == CheckState.Checked)
                    {
                        dashboard.CreateLink(joint, relative_joint);
                    }
                    Close();
                }
                else
                {
                    ErrorLabel.Text = "Please enter number only!";
                }
            }
            else
            {
                ErrorLabel.Text = "Please enter cartesian coordinates!";
            }
        }
    }
}
