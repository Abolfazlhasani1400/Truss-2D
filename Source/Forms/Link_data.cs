using System;
using System.Windows.Forms;

namespace Truss_2D
{
    public partial class LinkData : Form
    {
        private Link Link;
        private double _;
        public LinkData(Link link)
        {
            InitializeComponent();
            Link = link;
        }

        private void link_data_button_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(data_E_textbox.Text) && !string.IsNullOrEmpty(data_A_textbox.Text))
            {
                if(double.TryParse(data_A_textbox.Text,out _) && double.TryParse(data_E_textbox.Text,out _))
                {
                    Link.Area = double.Parse(data_A_textbox.Text);
                    Link.Elasticity = double.Parse(data_E_textbox.Text) * 1e9;
                    this.Close();
                }
                else
                {
                    data_error_label.Text = "Please enter numbers Only! ";
                }
            }
            else
            {
                data_error_label.Text = "Please enter value";
            }
        }

        private void LinkData_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Escape):
                    Link.Area = 0;
                    Link.Elasticity = 0;
                    this.Close();
                    break;
            }
        }
    }
}
