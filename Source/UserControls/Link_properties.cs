using System;
using System.Windows.Forms;

namespace Truss_2D
{
    public partial class LinkProperties : UserControl
    {
        double Out;//for tryParse

        Link link;
        DashBoard dashboard;

        public void LinkUpdate()
        {
            //section area
            if (double.TryParse(A_textbox.Text, out Out))
            {
                link.Area = double.Parse(A_textbox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Section area!");
            }

            //Elasticity
            if (double.TryParse(E_textbox.Text, out Out))
            {
                link.Elasticity = double.Parse(E_textbox.Text) * 1e9;
            }
            else
            {
                MessageBox.Show("Invalid Elasticity modulus!");
            }
            dashboard.SketchPanel.Invalidate();
            link.Node.Text = link.Info;

        }
        public LinkProperties(Link PassLink,DashBoard PassDashboerd)
        {
            InitializeComponent();
            link = PassLink;
            dashboard = PassDashboerd;

            Dock = DockStyle.Fill;

            JI_TextBox.Text = link.JI.InFo;
            JF_TextBox.Text = link.JF.InFo;

            //start_joint_textbox.Text = $"(X={Math.Round(link.JI.XL,4)},Y={Math.Round(link.JI.YL,4)}";
           // end_joint_textbox.Text = $"(X={Math.Round(link.JF.XL, 4)},Y={Math.Round(link.JF.YL, 4)}";
            E_textbox.Text = $"{link.Elasticity / 1e9}";
            A_textbox.Text = $"{link.Area}";

            InternalForce.Text = $"{Math.Round(link.InternalForce / 1e3, 4)} kN";
            Stress.Text = $"{Math.Round(link.Stress / 1e6, 4)} Mpa";
            Strain.Text = $"{link.Strain} --";
            Length.Text = $"{link.FinalLength} m";
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Enter)://pressing ENTER btn//
                    LinkUpdate();
                    break;
                case (Keys.Escape):
                    E_textbox.Text = link.Elasticity.ToString();
                    A_textbox.Text = link.Area.ToString();
                    break;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            LinkUpdate();
        }

        private void Change_start_point_btn_Click(object sender, EventArgs e)
        {
            dashboard.ChangeJoint = true;
            dashboard.ChangingStartJoint = true;
        }

        private void Change_end_point_btn_Click(object sender, EventArgs e)
        {
            dashboard.ChangeJoint = true;
            dashboard.ChangingEndJoint = true;
        }
    }
}
