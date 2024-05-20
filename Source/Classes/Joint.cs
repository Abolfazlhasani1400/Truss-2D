using System;
using System.Drawing;
using System.Windows.Forms;

namespace Truss_2D
{
    public class Joint
    {
        public Joint()
        {
            Node = new TreeNode();
        }

        //Information
        string Joint_ID;
        public string ID
        {
            get
            {
                return Joint_ID;
            }
            set
            {
                Joint_ID = value;

                u = "u" + Joint_ID;
                v = "v" + Joint_ID;
            }
        }
        public string InFo
        {
            get
            {
                return $"{JT}: ({Math.Round(XL, 4)}, {Math.Round(YL, 4)})";
            }
        }

        //cartesian Real location
        public double XL { get; set; }
        public double YL { get; set; }

        //cartesian Graphical location
        public float XG { get; set; }
        public float YG { get; set; }

        //external Forces (cartesian directions)
        public double XF { get; set; }
        public double YF { get; set; }

        //tree node
        public TreeNode Node { get; set; }

        //Select
        public bool IsSelected { get; set; }

        //DOF
        public double XT { get; set; }
        public double YT { get; set; }
        public double Rot { get; set; }

        //Suport Forces
        public double XS { get; set; }
        public double YS { get; set; }
        
        //global translation
        public string u { get; set; } //Label
        public string v { get; set; } //Label
        public double UT { get; set; } //value
        public double VT { get; set; } //value

        //Translation slope (Roller supprort ONLY!)
        public double Slope { get; set; }
        
        //Type
        public string JT { get; set; }
       

    }
}

