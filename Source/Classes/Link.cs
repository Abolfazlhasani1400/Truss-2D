using System;
using System.Windows.Forms;

namespace Truss_2D
{
    public class Link
    {
        public Link()
        {
            this.Neutral = true;
            this.Stress = 0;
            this.Strain = 0;
            this.InternalForce = 0;

            Node = new TreeNode
            {
                Text = Info
            };
        }
        //
        //InternalForce
        //
        public double InternalForce { get; set; }

        //
        //Stress
        //
        public double Stress { get; set; }

        //
        //Strain
        //
        public double Strain { get; set; }

        //
        //Elasticity
        //
        public double Elasticity { get; set; }

        //
        //Section Area
        //
        public double Area { get; set; }

        //
        //Initial joint
        //
        public Joint JI { get; set; }

        //
        //Final joint
        //
        public Joint JF { get; set; }

        //
        //Length
        //
        public double FinalLength
        {
            get
            {
                return (InitialLength * (1-Strain) );
            }
        }
        public double InitialLength 
        {
            get
            {
                if (JF != null && JI != null)
                {
                    return  Math.Pow(Math.Pow(JF.XL - JI.XL, 2) + Math.Pow(JF.YL - JI.YL, 2), 0.5);
                }
                else
                {
                    return 0;
                }
            }
        }

        //
        //Link Validity
        //
        public bool IsValid()
        {
            if(JF != null && JI != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //
        //Creating local matrix
        //
        public string[,] LocalMartix = new string[5, 5];
        public void CreatLocalMatrix()
        { 
            if(int.Parse(JI.ID) > int.Parse(JF.ID))//sorting joints by ID number
            {
                Joint temp = JF;
                JF = JI;
                JI = temp;
            }

            double L = (JF.XL - JI.XL) / this.InitialLength; //Cos phi
            double M = (JF.YL - JI.YL) / this.InitialLength; //Sin phi
            if (JF != null && JI != null)//filling local matrix
            {

                double A = this.Area; // Square meter!
                double E = this.Elasticity; //Pa!
                double Length = this.InitialLength; // meter
                /*
                 * :: Local matrix form ::
                 * 
                 * @    u1  v1  u2  v2
                 * 
                 * u1   *   *   *   *
                 * 
                 * v1   *   *   *   *
                 * 
                 * u2   *   *   *   *
                 * 
                 * v2   *   *   *   *
                 */


                LocalMartix[0, 0] = string.Empty;
                //
                LocalMartix[0, 1] = JI.u;
                LocalMartix[0, 2] = JI.v;
                LocalMartix[0, 3] = JF.u;
                LocalMartix[0, 4] = JF.v;

                LocalMartix[1, 0] = JI.u;
                LocalMartix[2, 0] = JI.v;
                LocalMartix[3, 0] = JF.u;
                LocalMartix[4, 0] = JF.v;
                //
                LocalMartix[1, 1] = (L * L * A * E / Length).ToString();
                LocalMartix[1, 2] = (L * M * A * E / Length).ToString();
                LocalMartix[1, 3] = (-1 * L * L * A * E / Length).ToString();
                LocalMartix[1, 4] = (-1 * L * M * A * E / Length).ToString();

                LocalMartix[2, 1] = (L * M * A * E / Length).ToString();
                LocalMartix[2, 2] = (M * M * A * E / Length).ToString();
                LocalMartix[2, 3] = (-1 * L * M * A * E / Length).ToString();
                LocalMartix[2, 4] = (-1 * M * M * A * E / Length).ToString();

                LocalMartix[3, 1] = (-1 * L * L * A * E / Length).ToString();
                LocalMartix[3, 2] = (-1 * L * M * A * E / Length).ToString();
                LocalMartix[3, 3] = (L * L * A * E / Length).ToString();
                LocalMartix[3, 4] = (L * M * A * E / Length).ToString();

                LocalMartix[4, 1] = (-1 * L * M * A * E / Length).ToString();
                LocalMartix[4, 2] = (-1 * M * M * A * E / Length).ToString();
                LocalMartix[4, 3] = (L * M * A * E / Length).ToString();
                LocalMartix[4, 4] = (M * M * A * E / Length).ToString();
            } 
        }
        //
        //Creating T matrix
        //
        public double[,] T_matrix = new double[2, 4];
        public void Creat_T_Matrix()
        {
            double L = (JF.XL - JI.XL) / this.InitialLength;
            double M = (JF.YL - JI.YL) / this.InitialLength;

            T_matrix[0, 0] = L;
            T_matrix[0, 1] = M;
            T_matrix[0, 2] = 0;
            T_matrix[0, 3] = 0;

            T_matrix[1, 0] = 0;
            T_matrix[1, 1] = 0;
            T_matrix[1, 2] = L;
            T_matrix[1, 3] = M;
        }
        //
        //Creating B matrix
        //
        public double[] B_Matrix = new double[2];
        public void Create_B_matrix()
        {
            double Length = this.InitialLength ; // meter
            B_Matrix[0] = -(1 / Length);
            B_Matrix[1] =  (1 / Length);
        }

        //
        //Creating U matrix
        //
        public double[] U_Matrix = new double[4];
        public void Create_u_Matrix()
        {
            U_Matrix[0] = JI.UT;
            U_Matrix[1] = JI.VT;
            U_Matrix[2] = JF.UT;
            U_Matrix[3] = JF.VT;
        }
        //
        //Select
        //
        public bool IsSelected { get; set; }

        //
        //tension or compression/
        //
        public bool Tension { get; set; }
        public bool Compression { get; set; }
        public bool Neutral { get; set; } // no tension or compression//

        //TreeViewNode
        public TreeNode Node { get; set; }
        public string Info 
        {
            get
            {
                if (JI!=null && JF != null)
                {
                    return $"E:{Elasticity / 1e9} Gpa, A:{Area} m^2, i:{JI.InFo}, f:{JF.InFo}";
                }
                return string.Empty;
            }
        }
    }
}
