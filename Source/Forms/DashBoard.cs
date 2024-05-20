using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Truss_2D
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            DoubleBuffered = true;
            TrussRootNode.Text = "Truss2D";
            Treeview.Nodes.Add(TrussRootNode);

            this.SketchPanel.MouseWheel += Sketch_panel_MouseWheel;
            this.JointCollection.CollectionChanged += JointsList_CollectionChanged;
        }

        public Origin O = new Origin();
        public float CellSize = new float();
        public float GridViewSize = new float();

        public List<Joint> JointList = new List<Joint>();
        public List<Link> LinkList = new List<Link>();

        public ObservableCollection<Joint> JointCollection = new ObservableCollection<Joint>();
        public ObservableCollection<Link> LinkCollection = new ObservableCollection<Link>();

        public PointF Mouse, TheMousePosition;

        public Joint ClickedJoint, linkEndJoint;
        public Link ClickedLink;

        public double Out; // double.TryParse out countainer
        //Conditions
        public bool GridView = false;
        public bool SelectJoint_i = false;
        public bool SelectJoint_f = false;
        public bool JointCreatingProcess = false;
        public bool ChangeJoint = false;
        public bool ChangingEndJoint = false;
        public bool ChangingStartJoint = false;
        public bool IsComfirmed = false;
        public bool PublicCharReq = false;
        //Pan
        public bool Pan = false;
        public PointF PanPointF_i, PanPointF_f;
        //
        public string JType;
        public TreeNode TrussRootNode = new TreeNode();
        public new float Scale = 0.5f;

        #region Functions
        public Joint CreateJoint(double X, double Y, Joint RelativeJoint)
        {
            Joint NewJoint = new Joint
            {
                XL = X,
                YL = Y
            };
            Type(NewJoint, JType);
            JointCollection.Add(NewJoint);
            SynchListCollection(JointCollection, JointList);
            UnSelectAll();

            return NewJoint;
        }
        public void CreateJoint(double X, double Y)
        {
            Joint NewJoint = new Joint
            {
                XL = X,
                YL = Y
            };
            Type(NewJoint, JType);
            JointCollection.Add(NewJoint);
            SynchListCollection(JointCollection, JointList);
            UnSelectAll();
        }
        public void CreateLink(Joint InitialJoint, Joint FinalJoint)
        {
            Link link = new Link
            {
                JI = InitialJoint,
                JF = FinalJoint
            };
            LinkCollection.Add(link);
            SynchListCollection(LinkCollection, LinkList);
            LinkData linkData = new LinkData(link);
            linkData.ShowDialog();
            UnSelectAll();
        }

        //List-Collection synchronization
        public void SynchListCollection(ObservableCollection<Joint> Collection, List<Joint> List)
        {
            List.Clear();
            foreach (Joint joint in Collection)
            {
                List.Add(joint);
            }
        }
        public void SynchListCollection(ObservableCollection<Link> Collection, List<Link> List)
        {
            List.Clear();
            foreach (Link link in Collection)
            {
                List.Add(link);
            }
        }

        // Changing Real world location to pixel location from origin!!
        public void SetGraphicalLocations()
        {
            foreach (Joint joint in JointList)
            {
                joint.XG = (float)(10 * CellSize * Scale * joint.XL);
                joint.YG = (float)(10 * CellSize * Scale * joint.YL);
            }
        }

        public void UnSelectAll()
        {
            Treeview.SelectedNode = null;
            TrussRootNode.Nodes.Clear();

            foreach (Joint joint in JointList.ToArray())
            {
                joint.IsSelected = false;

                joint.Node.Text = joint.InFo;
                TrussRootNode.Nodes.Add(joint.Node);
            }
            foreach (Link link in LinkList.ToArray())
            {
                link.IsSelected = false;

                if (!link.IsValid())//removing invalid links
                {
                    LinkList.Remove(link);
                }
                else
                {
                    link.Node.Text = link.Info;
                    TrussRootNode.Nodes.Add(link.Node);
                }
            }

            PropertiesPanel.Controls.Clear();
            SketchPanel.Invalidate();
        }

        public void Move_Origin()
        {
            if (PanPointF_f != null && PanPointF_i != null)
            {
                O.X += (PanPointF_f.X - PanPointF_i.X);
                O.Y += (PanPointF_f.Y - PanPointF_i.Y);
            }
            SketchPanel.Invalidate();
        }

        public Joint CursorOnJoint(PointF Mouseposition)
        {
            //check if cursor is on a joint /
            //if the cursor is on joint return the joint//
            foreach (Joint joint in JointList.ToArray())
            {
                if ((Math.Pow(Math.Abs(joint.XL - Mouseposition.X), 2) + Math.Pow(Math.Abs(joint.YL - Mouseposition.Y), 2)) <= Math.Pow((5.0 / Scale / CellSize / 10), 2)) //Joint radius = 5 (px)
                {
                    return joint;
                }
            }
            return null;
        }
        public Link CursorOnLink(PointF Mouseposition)
        {
            foreach (Link link in LinkList.ToArray())
            {
                //check MousePosition distance from link.MidPoint ::
                PointF MP = new PointF((float)((link.JI.XL + link.JF.XL) / 2), (float)((link.JI.YL + link.JF.YL) / 2));
                if ((Math.Pow(Math.Abs(MP.X - Mouseposition.X), 2) + Math.Pow(Math.Abs(MP.Y - Mouseposition.Y), 2)) <= Math.Pow((5.0 / Scale / CellSize / 10), 2))
                {
                    return link;
                }
            }
            return null;
        }

        public void Type(Joint joint, string Type)
        {
            if (Type == "j")
            {
                joint.Rot = 1;
                joint.XT = 1;
                joint.YT = 1;
                joint.JT = "Joint";
                joint.Slope = 0;
            }
            else if (Type == "r")
            {
                ChooseDiretion getSlope = new ChooseDiretion(joint);
                getSlope.ShowDialog();
                joint.Rot = 1;
                joint.JT = "Roller";
            }
            else if (Type == "h")
            {
                //Get_Slope get_Slope = new Get_Slope(joint);
                //get_Slope.ShowDialog();
                joint.Rot = 1;
                joint.XT = 0;
                joint.YT = 0;
                joint.JT = "Hinge";
            }
            else if (Type == "f")
            {
                joint.Rot = 0;
                joint.XT = 0;
                joint.YT = 0;
                joint.JT = "Fixed";
                joint.Slope = 0;
            }
        }

        //return mouse Graphical position relative to coordinate origin 
        public PointF MouseGPosition(PointF mouse_location)
        {
            // Cellsize is 1cm in Graphics @ scale=1 :: 10cm in logic
            // CellSize = SketchPanel.CreateGraphics().DpiX / (float)2.54 :: 1 Inch => 2.54 cm ::

            PointF pointF = new PointF((-(int)O.X + mouse_location.X) / Scale / CellSize / 10, ((int)O.Y - mouse_location.Y) / Scale / CellSize / 10);
            return pointF;
        }
        #endregion

        #region UserInterface
        private void DashBoard_Load(object sender, EventArgs e)
        {
            O.X = SketchPanel.ClientSize.Width / 2;
            O.Y = SketchPanel.ClientSize.Height / 2;

            // Cellsize is 1cm in "Graphics @ scale=1" :: 10cm in "logic"
            // CellSize value = pixels per centimeter length
            // 1 Inch => 2.54 cm :: DpiX = DpiY
            CellSize = SketchPanel.CreateGraphics().DpiX / (float)2.54;

            //Initial value for scale label
            ScaleLabel.Text = $"Scale: {Math.Round(Scale, 2)}x";
        }

        private void Sketch_panel_MouseClick(object sender, MouseEventArgs e)
        {
            MouseGPosition(e.Location);
            if (e.Button == MouseButtons.Left)
            {
                //Change Joint
                if (ChangeJoint)
                {
                    foreach (Link link in LinkList.ToArray())
                    {
                        if (link.IsSelected)
                        {
                            Joint joint = CursorOnJoint(MouseGPosition(e.Location));
                            if (ChangingEndJoint && joint != null)
                            {
                                link.JF = joint;
                                ChangingEndJoint = false;
                                ChangeJoint = false;
                                UnSelectAll();
                                return;
                            }
                            else if (ChangingStartJoint && joint != null)
                            {
                                link.JI = joint;
                                ChangingStartJoint = false;
                                ChangeJoint = false;
                                UnSelectAll();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Changing joint operation FAILED!");
                                ChangingStartJoint = false;
                                ChangingEndJoint = false;
                                ChangeJoint = false;
                                UnSelectAll();
                                return;
                            }
                        }
                    }
                }

                //Link Creating mode 
                if (SelectJoint_f)
                {
                    Joint endPoint = CursorOnJoint(MouseGPosition(e.Location));

                    //JointMenu :: "Set link to other joint"
                    if (endPoint != null && ClickedJoint != null)
                    {
                        CreateLink(ClickedJoint, endPoint);
                        SelectJoint_f = false;
                        endPoint = null;
                        return;
                    }

                    ////Link Button :: selecting second Joint
                    //if (endPoint != null)
                    //{
                    //    if (LinkList.Count > 0)
                    //    {
                    //        Link link = LinkList[LinkList.Count - 1]; //link was creating while selecting first joint
                    //        link.FinalJoint = endPoint;
                    //        SelectJoint_f = false;
                    //        endPoint = null;
                    //        UnselectAll();

                    //        LinkData link_data = new LinkData(link);
                    //        link_data.ShowDialog();
                    //        return;
                    //    }
                    //}
                    else
                    {
                        MessageBox.Show("Link creation operation canceled!");
                        SelectJoint_f = false;
                        endPoint = null;
                        UnSelectAll();
                        return;
                    }
                }

                //Joint Creating mode
                if (JointCreatingProcess)
                {
                    //Creating a simple joint
                    CreateJoint(MouseGPosition(e.Location).X, MouseGPosition(e.Location).Y);
                    JointCreatingProcess = false;
                    return;
                }

                //selecting mode!!!!
                else
                {
                    // Selecting Joint by direct mouseClick//
                    // Only One Joint Can be selected at once ==> No multi select ::

                    //clicked on a Joint
                    ClickedJoint = CursorOnJoint(MouseGPosition(e.Location));
                    if (ClickedJoint != null)
                    {
                        UnSelectAll();

                        if (ClickedJoint.IsSelected)//unselecting...//
                        {
                            ClickedJoint = null;
                        }
                        else//select//
                        {
                            //No multi select!!!
                            ClickedJoint.IsSelected = true;
                            Treeview.SelectedNode = ClickedJoint.Node;
                            SketchPanel.Invalidate();

                            JointProperties jointproperties = new JointProperties(ClickedJoint, this);
                            this.PropertiesPanel.Controls.Add(jointproperties);
                        }
                        return;
                    }

                    //clicked on a Link
                    ClickedLink = CursorOnLink(MouseGPosition(e.Location));
                    if (ClickedLink != null)
                    {
                        UnSelectAll();
                        ClickedLink.IsSelected = true;
                        SketchPanel.Invalidate();

                        //creating properties panel 
                        LinkProperties linkproperties = new LinkProperties(ClickedLink, this);
                        this.PropertiesPanel.Controls.Add(linkproperties);
                        return;
                    }

                    //click on sketchpanel 
                    if (ClickedJoint == null && ClickedLink == null)//Clicking on Sketch Panel will Unselect all//
                    {
                        UnSelectAll();
                        SketchPanel.Cursor = Cursors.Default;
                        return;
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                //Show JointMenu
                ClickedJoint = CursorOnJoint(MouseGPosition(e.Location));
                if (ClickedJoint != null)
                {
                    UnSelectAll();
                    ClickedJoint.IsSelected = true;
                    JointMenu.Show(SketchPanel, e.Location, ToolStripDropDownDirection.BelowRight);
                }
            }

            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                Scale = 1;
                SketchPanel.Invalidate();
            }
        }

        private void sketch_panel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                O.X = SketchPanel.ClientSize.Width / 2;
                O.Y = SketchPanel.ClientSize.Height / 2;
                Scale = 0.5f;
                SketchPanel.Invalidate();
            }
        }

        private void Sketch_panel_MouseMove(object sender, MouseEventArgs e)
        {
            coordinate_label.Text = MouseGPosition(e.Location).ToString();

            var joint = CursorOnJoint(MouseGPosition(e.Location));//Check Cursur position
            var link = CursorOnLink(MouseGPosition(e.Location));// Check Cursor position

            if (JointCreatingProcess)
            {
                SketchPanel.Cursor = Cursors.Cross;
                return;
            }
            else if (joint != null || link != null || Pan)//on member
            {
                SketchPanel.Cursor = Cursors.Hand;
            }
            else//Off member
            {
                SketchPanel.Cursor = Cursors.Default;
            }
        }

        private void Sketch_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Pan = true;
                PanPointF_i = e.Location;
                SketchPanel.Cursor = Cursors.Hand;
            }
        }

        private void Sketch_panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (Pan)
            {
                PanPointF_f = e.Location;
                Move_Origin();
                Pan = false;
                Cursor = Cursors.Default;
            }
        }

        private void Sketch_panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 120)
            {
                if (Scale <= 50.0f)
                {
                    Scale *= 1.2f;
                    SketchPanel.Invalidate();
                }
            }
            else
            {
                if (Scale >= 0.0001f)
                {
                    Scale /= 1.2f;
                    SketchPanel.Invalidate();
                }
            }
            ScaleLabel.Text = $"Scale: {Math.Round(Scale, 2)}x";
        }

        private void JointsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //synchronizing GridView with new joint's location::
            GridViewSize = 10;
            if (JointCollection.Count > 0)
            {
                double Gridtemp = GridViewSize / 10; ;
                foreach (Joint joint in JointCollection)
                {
                    if (Math.Abs(joint.XL) > Gridtemp)
                    {
                        Gridtemp = (float)Math.Abs(joint.XL);
                    }
                    if (Math.Abs(joint.YL) > Gridtemp)
                    {
                        Gridtemp = (float)Math.Abs(joint.YL);
                    }
                }
                Gridtemp = (float)Math.Ceiling((Gridtemp) * 10);
                if (Gridtemp > GridViewSize)
                {
                    GridViewSize = (float)Gridtemp;
                }
            }
        }

        private void Treeview_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                UnSelectAll();
                foreach (Joint joint in JointCollection)
                {
                    if (e.Node == joint.Node)
                    {
                        joint.IsSelected = true;
                        Treeview.SelectedNode = e.Node;
                        SketchPanel.Invalidate();
                        JointProperties jointproperties = new JointProperties(joint, this);
                        this.PropertiesPanel.Controls.Add(jointproperties);
                        return;
                    }
                }
                foreach (Link link in LinkCollection)
                {
                    if (e.Node == link.Node)
                    {
                        link.IsSelected = true;
                        Treeview.SelectedNode = e.Node;
                        SketchPanel.Invalidate();
                        LinkProperties linkproperties = new LinkProperties(link, this);
                        this.PropertiesPanel.Controls.Add(linkproperties);
                        return;
                    }
                }
            }
        }

        private void AddCartesianJoint(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "j";
                    Cartesian_Form cartesianform = new Cartesian_Form(ClickedJoint, this);
                    cartesianform.ShowDialog();
                }
            }

        }

        private void AddCartesianHinge(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "h";
                    Cartesian_Form cartesianform = new Cartesian_Form(ClickedJoint, this);
                    cartesianform.ShowDialog();
                }
            }
        }

        private void AddCartesianRoller(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "r";
                    Cartesian_Form cartesianform = new Cartesian_Form(ClickedJoint, this);
                    cartesianform.ShowDialog();
                }
            }
        }

        private void AddCartesianFixed(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "f";
                    Cartesian_Form cartesianform = new Cartesian_Form(ClickedJoint, this);
                    cartesianform.ShowDialog();
                }
            }
        }

        private void AddPolarJoint(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "j";
                    Polar_Form polarform = new Polar_Form(ClickedJoint, this);
                    polarform.ShowDialog();
                }
            }
        }

        private void AddPolarHinge(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "h";
                    Polar_Form polarform = new Polar_Form(ClickedJoint, this);
                    polarform.ShowDialog();
                }
            }
        }

        private void AddPolarRoller(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "r";
                    Polar_Form polarform = new Polar_Form(ClickedJoint, this);
                    polarform.ShowDialog();
                }
            }
        }

        private void AddPolarFixed(object sender, EventArgs e)
        {
            //Creating a relative joint
            if (ClickedJoint != null)
            {
                if (ClickedJoint.IsSelected)
                {
                    JType = "f";
                    Polar_Form polarform = new Polar_Form(ClickedJoint, this);
                    polarform.ShowDialog();
                }
            }
        }

        private void Link_Creator_Click(object sender, EventArgs e)
        {
            SelectJoint_f = true;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClickedJoint != null && ClickedJoint.IsSelected)
            {
                foreach (Link link in LinkList.ToArray())
                {
                    if (link.JI == ClickedJoint || link.JF == ClickedJoint)
                    {
                        LinkCollection.Remove(link);
                        SynchListCollection(LinkCollection, LinkList);
                    }
                }
                JointCollection.Remove(ClickedJoint);
                SynchListCollection(JointCollection, JointList);
            }
            UnSelectAll();
        }

        private void Sketch_panel_SizeChanged(object sender, EventArgs e)
        {
            SketchPanel.Invalidate();
        }

        private void Grid_View_Button_MouseHover(object sender, EventArgs e)
        {
            Grid_View_Button.Cursor = Cursors.Hand;
        }

        private void Grid_View_Button_Click(object sender, EventArgs e)
        {
            if (GridView)
            {
                GridView = false;
                SketchPanel.Invalidate();
                return;
            }
            if (!GridView)
            {
                GridView = true;
                SketchPanel.Invalidate();
                return;
            }
        }

        private void DashBoard_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Escape):
                    SelectJoint_i = false;
                    JointCreatingProcess = false;
                    SketchPanel.Cursor = Cursors.Default;
                    UnSelectAll();
                    break;
                case (Keys.Delete):
                    if (ClickedJoint != null && ClickedJoint.IsSelected)
                    {
                        JointCollection.Remove(ClickedJoint);
                        SynchListCollection(JointCollection, JointList);
                        UnSelectAll();
                    }
                    foreach (Link link in LinkList.ToArray())
                    {
                        if(link.IsSelected)
                        {
                            LinkCollection.Remove(link);
                            SynchListCollection(LinkCollection, LinkList);
                            SketchPanel.Invalidate();
                        }
                    }
                    break;
            }
        }

        private void JointType_Click(object sender, EventArgs e)
        {
            if (ClickedJoint != null)
            {
                Type(ClickedJoint, "j");
                UnSelectAll();
            }
        }

        private void RollerType_Click(object sender, EventArgs e)
        {
            if (ClickedJoint != null)
            {
                Type(ClickedJoint, "r");
                UnSelectAll();
            }
        }

        private void HingeType_Click(object sender, EventArgs e)
        {
            if (ClickedJoint != null)
            {
                Type(ClickedJoint, "h");
                UnSelectAll();
            }
        }

        private void FixedType_Click(object sender, EventArgs e)
        {
            if (ClickedJoint != null)
            {
                Type(ClickedJoint, "f");
                UnSelectAll();
            }
        }

        private void Simple_button_Click(object sender, EventArgs e)
        {
            JointCreatingProcess = true;
            JType = "j";
        }

        private void Hinge_button_Click(object sender, EventArgs e)
        {
            JointCreatingProcess = true;
            JType = "h";
        }

        private void Roller_button_Click(object sender, EventArgs e)
        {
            JointCreatingProcess = true;
            JType = "r";
        }

        private void Fixed_button_Click(object sender, EventArgs e)
        {
            JointCreatingProcess = true;
            JType = "f";
        }

        private void Sketch_panel_Paint(object sender, PaintEventArgs e)
        {
            //!!Using Graphical Locations Only!!\\

            //Grid View
            Graphics GridViewGraphics = SketchPanel.CreateGraphics();
            GridViewGraphics.SmoothingMode = SmoothingMode.HighQuality;
            GridViewGraphics.TranslateTransform((int)O.X, (int)O.Y);
            GridViewGraphics.ScaleTransform(1.0f, -1.0f);
            if (GridView)
            {
                if (GridViewSize <= 10)
                {
                    GridViewSize = 10;
                    for (int i = (int)-GridViewSize; i <= GridViewSize; i++)
                    {
                        //Horizontal Grid
                        GridViewGraphics.DrawLine(new Pen(Color.LightGray, 1), (-GridViewSize * CellSize) * Scale, (i * CellSize) * Scale, (GridViewSize * CellSize) * Scale, (i * CellSize) * Scale);
                        //Vertical Grid
                        GridViewGraphics.DrawLine(new Pen(Color.LightGray, 1), (i * CellSize) * Scale, (-GridViewSize * CellSize) * Scale, (i * CellSize) * Scale, (GridViewSize * CellSize) * Scale);
                    }
                }
                else
                {
                    for (int i = (int)-GridViewSize; i <= GridViewSize; i++)
                    {
                        //Horizontal Grid
                        GridViewGraphics.DrawLine(new Pen(Color.LightGray, 1), (-GridViewSize * CellSize) * Scale, (i * CellSize) * Scale, (GridViewSize * CellSize) * Scale, (i * CellSize) * Scale);
                        //Vertical Grid
                        GridViewGraphics.DrawLine(new Pen(Color.LightGray, 1), (i * CellSize) * Scale, (-GridViewSize * CellSize) * Scale, (i * CellSize) * Scale, (GridViewSize * CellSize) * Scale);
                    }
                }
            }

            //Origin
            Graphics OriginGraphics = SketchPanel.CreateGraphics();
            OriginGraphics.TranslateTransform((int)O.X, (int)O.Y);
            OriginGraphics.ScaleTransform(1.0f, +1.0f);
            OriginGraphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen originpen = new Pen(Color.FromArgb(100, 100, 100), 3)
            {
                EndCap = LineCap.ArrowAnchor
            };
            OriginGraphics.DrawLine(originpen, 0, 0, 0, -40);
            OriginGraphics.DrawString($"y", new Font("Arial", 11), new SolidBrush(Color.Red), -13, -40);

            OriginGraphics.DrawLine(originpen, 0, 0, 40, 0);
            OriginGraphics.DrawString($"x", new Font("Arial", 11), new SolidBrush(Color.Red), 23, -17);
            OriginGraphics.FillEllipse(new SolidBrush(Color.Red), -3, -3, 6, 6);

            //
            // Changing Real world location to pixel location from origin!!!
            //
            SetGraphicalLocations();


            //
            //LINK
            //

            //links
            Graphics LinksGraphics = SketchPanel.CreateGraphics();
            LinksGraphics.TranslateTransform((float)O.X, (float)O.Y);
            LinksGraphics.ScaleTransform(1, -1);
            LinksGraphics.SmoothingMode = SmoothingMode.HighQuality;
            //LinkInternalForces (MidPoint)
            Graphics LinkLabelGraphics = SketchPanel.CreateGraphics();
            LinkLabelGraphics.TranslateTransform((int)O.X, (int)O.Y);
            LinkLabelGraphics.ScaleTransform(1.0f, +1.0f);
            LinkLabelGraphics.SmoothingMode = SmoothingMode.HighQuality;
            SolidBrush LinkLabelPen = new SolidBrush(Color.Black);

            foreach (Link link in LinkList.ToArray())
            {
                //Links
                if (link.IsValid())
                {
                    if (link.IsSelected)
                    {
                        LinksGraphics.DrawLine(new Pen(Color.Orange, 3),
                            link.JI.XG,
                            link.JI.YG,
                            link.JF.XG,
                            link.JF.YG);
                    }
                    else if (link.Neutral)
                    {
                        LinksGraphics.DrawLine(new Pen(Color.DarkGray, 3),
                             link.JI.XG,
                             link.JI.YG,
                             link.JF.XG,
                             link.JF.YG);
                    }
                    else if (link.Tension)
                    {
                        LinksGraphics.DrawLine(new Pen(Color.LightGreen, 3),
                            link.JI.XG,
                            link.JI.YG,
                            link.JF.XG,
                            link.JF.YG);
                    }
                    else if (link.Compression)
                    {
                        LinksGraphics.DrawLine(new Pen(Color.Red, 3),
                            link.JI.XG,
                            link.JI.YG,
                            link.JF.XG,
                            link.JF.YG);
                    }
                }

                //link internal forces (MidPoint)
                PointF GMidPoint = new PointF((float)((link.JI.XG + link.JF.XG) / 2), (float)((link.JI.YG + link.JF.YG) / 2));
                LinkLabelGraphics.FillEllipse(new SolidBrush(Color.Black), GMidPoint.X - 2, -GMidPoint.Y - 2, 4, 4);
                LinkLabelGraphics.DrawString($"{Math.Round(link.InternalForce / 1e3, 4)} kN", new Font("Arial", 8, FontStyle.Italic), LinkLabelPen, GMidPoint.X, -GMidPoint.Y);

            }

            //
            //Joint
            //

            //External force vectors and labels
            Graphics ExternalForceVectorGraphics = SketchPanel.CreateGraphics();
            ExternalForceVectorGraphics.TranslateTransform((int)O.X, (int)O.Y);
            ExternalForceVectorGraphics.ScaleTransform(1.0f, -1.0f);
            ExternalForceVectorGraphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen ExternalForceVectorPen = new Pen(Color.Blue, 2)
            {
                EndCap = LineCap.ArrowAnchor
            };
            Graphics ExternalForceLabelGraphics = SketchPanel.CreateGraphics();
            ExternalForceLabelGraphics.TranslateTransform((int)O.X, (int)O.Y);
            ExternalForceLabelGraphics.ScaleTransform(1.0f, +1.0f);
            ExternalForceLabelGraphics.SmoothingMode = SmoothingMode.HighQuality;
            SolidBrush ExternalForceLabelPen = new SolidBrush(Color.Black);
            //Support force vectors and labels
            Graphics SupportForceVectorGraphics = SketchPanel.CreateGraphics();
            SupportForceVectorGraphics.TranslateTransform((int)O.X, (int)O.Y);
            SupportForceVectorGraphics.ScaleTransform(1.0f, -1.0f);
            SupportForceVectorGraphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen SupportForceVectorPen = new Pen(Color.Black, 2)
            {
                EndCap = LineCap.ArrowAnchor
            };
            Graphics SupportForceLabelGraphics = SketchPanel.CreateGraphics();
            SupportForceLabelGraphics.TranslateTransform((int)O.X, (int)O.Y);
            SupportForceLabelGraphics.ScaleTransform(1.0f, +1.0f);
            SupportForceLabelGraphics.SmoothingMode = SmoothingMode.HighQuality;
            SolidBrush SupportForceLabelPen = new SolidBrush(Color.Black);
            //JOINTS
            Graphics JointGraphics = SketchPanel.CreateGraphics();
            JointGraphics.TranslateTransform((float)O.X, (float)O.Y);
            JointGraphics.ScaleTransform(1, -1);
            JointGraphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (Joint joint in JointList.ToArray())
            {
                //External Forces vectors and labels
                if (joint.XF != 0 || joint.YF != 0)
                {
                    int Dx = Math.Sign(joint.XF);
                    int Dy = Math.Sign(joint.YF);

                    if (Dy > 0)
                    {
                        ExternalForceVectorGraphics.DrawLine(ExternalForceVectorPen, joint.XG, joint.YG, joint.XG, joint.YG + 20);
                        ExternalForceLabelGraphics.DrawString($"{Math.Round(joint.YF / 1e3, 4)} kN", new Font("Arial", 11), ExternalForceLabelPen, joint.XG, -(joint.YG + 20) - 10);
                    }
                    else if (Dy < 0)
                    {
                        ExternalForceVectorGraphics.DrawLine(ExternalForceVectorPen, joint.XG, joint.YG, joint.XG, joint.YG - 20);
                        ExternalForceLabelGraphics.DrawString($"{Math.Round(joint.YF / 1e3, 4)} kN", new Font("Arial", 11), ExternalForceLabelPen, joint.XG, -(joint.YG) + 10);
                    }

                    if (Dx > 0)
                    {
                        ExternalForceVectorGraphics.DrawLine(ExternalForceVectorPen, joint.XG, joint.YG, joint.XG + 20, joint.YG);
                        ExternalForceLabelGraphics.DrawString($"{Math.Round(joint.XF / 1e3, 4)} kN", new Font("Arial", 11), ExternalForceLabelPen, joint.XG + 20, -joint.YG);
                    }
                    else if (Dx < 0)
                    {
                        ExternalForceVectorGraphics.DrawLine(ExternalForceVectorPen, joint.XG, joint.YG, joint.XG - 20, joint.YG);
                        ExternalForceLabelGraphics.DrawString($"{Math.Round(joint.XF / 1e3, 4)} kN", new Font("Arial", 11), ExternalForceLabelPen, joint.XG - 60, -joint.YG);
                    }
                }
                //Support force vectors and labels
                if (!double.IsNaN(joint.YS) && !double.IsNaN(joint.XS))
                {
                    if (joint.XS != 0 || joint.YS != 0)
                    {
                        int Dx = Math.Sign(joint.XS);
                        int Dy = Math.Sign(joint.YS);

                        if (Dy > 0)
                        {
                            SupportForceVectorGraphics.DrawLine(SupportForceVectorPen, joint.XG, joint.YG, joint.XG, joint.YG + 20);
                            SupportForceLabelGraphics.DrawString($"{Math.Abs(Math.Round(joint.YS / 1e3, 4))} kN", new Font("Arial", 11), SupportForceLabelPen, joint.XG, -(joint.YG + 20) - 10);
                        }
                        else if (Dy < 0)
                        {
                            SupportForceVectorGraphics.DrawLine(SupportForceVectorPen, joint.XG, joint.YG, joint.XG, joint.YG - 20);
                            SupportForceLabelGraphics.DrawString($"{Math.Abs(Math.Round(joint.YS / 1e3, 4))} kN", new Font("Arial", 11), SupportForceLabelPen, joint.XG, -(joint.YG) + 10);
                        }

                        if (Dx > 0)
                        {
                            SupportForceVectorGraphics.DrawLine(SupportForceVectorPen, joint.XG, joint.YG, joint.XG + 20, joint.YG);
                            SupportForceLabelGraphics.DrawString($"{Math.Abs(Math.Round(joint.XS / 1e3, 4))} kN", new Font("Arial", 11), SupportForceLabelPen, joint.XG + 20, -joint.YG);
                        }
                        else if (Dx < 0)
                        {
                            SupportForceVectorGraphics.DrawLine(SupportForceVectorPen, joint.XG, joint.YG, joint.XG - 20, joint.YG);
                            SupportForceLabelGraphics.DrawString($"{Math.Abs(Math.Round(joint.XS / 1e3, 4))} kN", new Font("Arial", 11), SupportForceLabelPen, joint.XG - 60, -joint.YG);
                        }
                    }
                }
                //Joints
                if (joint.IsSelected)//selcted joint
                {
                    if (joint.JT == "Joint")
                    {
                        JointGraphics.FillEllipse(new SolidBrush(Color.Orange), joint.XG - 5, joint.YG - 5, 10, 10);
                        JointGraphics.DrawEllipse(new Pen(Color.Gray, 1), joint.XG - 5, joint.YG - 5, 10, 10);
                    }
                    else if (joint.JT == "Roller")
                    {
                        PointF[] Corners = new PointF[3];
                        Corners[0] = new PointF(joint.XG, joint.YG);
                        Corners[1] = new PointF(
                            joint.XG - (float)(10 * Math.Cos((60 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(10 * Math.Sin((60 + joint.Slope) * Math.PI / 180)));
                        Corners[2] = new PointF(
                            joint.XG - (float)(10 * Math.Cos((120 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(10 * Math.Sin((120 + joint.Slope) * Math.PI / 180)));

                        JointGraphics.FillPolygon(new SolidBrush(Color.Orange), Corners);
                        JointGraphics.DrawPolygon(new Pen(Color.Gray, 1), Corners);

                        JointGraphics.DrawLine(
                            new Pen(Color.Black, 1),
                            joint.XG - (float)(13 * Math.Cos((60 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(13 * Math.Sin((60 + joint.Slope) * Math.PI / 180)),
                            joint.XG - (float)(13 * Math.Cos((120 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(13 * Math.Sin((120 + joint.Slope) * Math.PI / 180))
                            );
                    }
                    else if (joint.JT == "Hinge")
                    {
                        JointGraphics.FillEllipse(new SolidBrush(Color.Orange), joint.XG - 5, joint.YG - 5, 10, 10);
                        JointGraphics.DrawEllipse(new Pen(Color.Gray, 1), joint.XG - 5, joint.YG - 5, 10, 10);
                        JointGraphics.DrawEllipse(new Pen(Color.Black, 1), joint.XG - 7, joint.YG - 7, 14, 14);
                    }
                    else if (joint.JT == "Fixed")
                    {
                        PointF[] Corners = new PointF[4];
                        Corners[0] = new PointF(joint.XG + 5, joint.YG + 5);
                        Corners[1] = new PointF(joint.XG - 5, joint.YG + 5);
                        Corners[2] = new PointF(joint.XG - 5, joint.YG - 5);
                        Corners[3] = new PointF(joint.XG + 5, joint.YG - 5);

                        JointGraphics.FillPolygon(new SolidBrush(Color.Orange), Corners);
                        JointGraphics.DrawPolygon(new Pen(Color.Gray, 1), Corners);

                        JointGraphics.DrawLine(new Pen(Color.Black, 1), Corners[2], Corners[3]);
                    }
                }
                else//all unselected joints
                {
                    if (joint.JT == "Joint")
                    {
                        JointGraphics.FillEllipse(new SolidBrush(Color.Gray), joint.XG - 5, joint.YG - 5, 10, 10);
                        JointGraphics.DrawEllipse(new Pen(Color.Orange, 1), joint.XG - 5, joint.YG - 5, 10, 10);
                    }
                    else if (joint.JT == "Roller")
                    {
                        PointF[] Corners = new PointF[3];
                        Corners[0] = new PointF(joint.XG, joint.YG);
                        Corners[1] = new PointF(
                            joint.XG - (float)(10 * Math.Cos((60 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(10 * Math.Sin((60 + joint.Slope) * Math.PI / 180))
                            );
                        Corners[2] = new PointF(
                            joint.XG - (float)(10 * Math.Cos((120 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(10 * Math.Sin((120 + joint.Slope) * Math.PI / 180))
                            );

                        JointGraphics.FillPolygon(new SolidBrush(Color.Gray), Corners);
                        JointGraphics.DrawPolygon(new Pen(Color.Orange, 1), Corners);

                        JointGraphics.DrawLine(
                            new Pen(Color.Black, 1),
                            joint.XG - (float)(13 * Math.Cos((60 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(13 * Math.Sin((60 + joint.Slope) * Math.PI / 180)),
                            joint.XG - (float)(13 * Math.Cos((120 + joint.Slope) * Math.PI / 180)),
                            joint.YG - (float)(13 * Math.Sin((120 + joint.Slope) * Math.PI / 180))
                            );
                    }
                    else if (joint.JT == "Hinge")
                    {
                        JointGraphics.FillEllipse(new SolidBrush(Color.Gray), joint.XG - 5, joint.YG - 5, 10, 10);
                        JointGraphics.DrawEllipse(new Pen(Color.Orange, 1), joint.XG - 5, joint.YG - 5, 10, 10);
                        JointGraphics.DrawEllipse(new Pen(Color.Black, 1), joint.XG - 7, joint.YG - 7, 14, 14);
                    }
                    else if (joint.JT == "Fixed")
                    {
                        PointF[] Corners = new PointF[4];
                        Corners[0] = new PointF(joint.XG + 5, joint.YG + 5);
                        Corners[1] = new PointF(joint.XG - 5, joint.YG + 5);
                        Corners[2] = new PointF(joint.XG - 5, joint.YG - 5);
                        Corners[3] = new PointF(joint.XG + 5, joint.YG - 5);

                        JointGraphics.FillPolygon(new SolidBrush(Color.Gray), Corners);
                        JointGraphics.DrawPolygon(new Pen(Color.Orange, 1), Corners);

                        JointGraphics.DrawLine(new Pen(Color.Black, 1), Corners[2], Corners[3]);
                    }
                }
            }
        }
        #endregion

        #region Solver
        private void Calc_btn_Click(object sender, EventArgs e)
        {
            UnSelectAll();

            //reseting truss
            foreach (var joint in JointList)
            {
                joint.VT = 0;
                joint.UT = 0;
                joint.XS = 0;
                joint.YS = 0;
            }
            foreach (var Link in LinkList)
            {
                Link.Create_B_matrix();
                Link.Creat_T_Matrix();
                Link.Create_u_Matrix();

                Link.Compression = false;
                Link.Tension = false;
                Link.Neutral = true;

                Link.Strain = 0;
                Link.Stress = 0;
                Link.InternalForce = 0;
            }

            SynchListCollection(JointCollection, JointList);
            SynchListCollection(LinkCollection, LinkList);

            //joint remove acception messege box
            DialogResult result = MessageBox.Show("All unattached Joints are going to remove.\nWHIT NO POSSIBILITY TO RETRN!", "Do you accept this? ", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                //removing unattached joints
                foreach (Joint joint in JointList.ToArray())
                {
                    bool joint_is_unattached = true;

                    foreach (Link link in LinkList.ToArray()) //check if the joint is attached to a link
                    {
                        if (link.JF == joint || link.JI == joint)
                        {
                            joint_is_unattached = false;
                        }
                    }

                    if (joint_is_unattached)
                    {
                        TrussRootNode.Nodes.Remove(joint.Node);
                        JointCollection.Remove(joint);
                        SynchListCollection(JointCollection, JointList);
                        SketchPanel.Invalidate();
                    }
                }
            }
            else if (result == DialogResult.Cancel)
            {
                MessageBox.Show("Please modify connections.", "Error: unattached joints.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check External stability!
            if (!CheckRotationalgStability())
            {
                MessageBox.Show("Unstable truss!");
                return;
            }

            //Check Link Elasticity & SectionArea
            foreach (Link link in LinkList.ToArray())
            {
                if (!link.IsValid())//removing invalid links
                {
                    LinkList.Remove(link);
                    SynchListCollection(LinkCollection, LinkList);
                    return;
                }
                else
                {
                    if (link.Elasticity == 0 || link.Area == 0) 
                    {
                        PublicCharReq = true;
                    }
                }
            }

            //Use Public characteristics!
            if (PublicCharReq)
            {
                DialogResult Check_EA = MessageBox.Show("There are members with zero Section area or Elasticity modulus.\nDo you confirm to use public characteristics?", "Invalid inputs", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (Check_EA == DialogResult.Yes)
                {
                    if (PublicE_textbox.Text != string.Empty && PublicA_textbox.Text != string.Empty)
                    {
                        if (double.TryParse(PublicE_textbox.Text, out Out) && double.TryParse(PublicA_textbox.Text, out Out))
                        {
                            foreach (Link link in LinkList.ToArray())
                            {
                                if (link.Elasticity == 0)
                                {
                                    link.Elasticity = double.Parse(PublicE_textbox.Text) * 1e9;
                                }
                                if (link.Area == 0)
                                {
                                    link.Area = double.Parse(PublicA_textbox.Text);
                                }
                            }
                            PublicCharReq = false;
                            UnSelectAll();
                        }
                        else
                        {
                            MessageBox.Show("Please enter numbers only.\nand try again!", "Invalid inputs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter numbers only.\nand try again!", "Invalid inputs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please modify characteristics of members.", "zero characteristics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // check truss validity
            int length;//this will be "Length of global matrix" ::
            if (JointList.Count > 2)
            {
                length = 2 * JointList.Count + 1;
            }
            else
            {
                MessageBox.Show("Invalid truss.");
                return;
            }

            //give ID to remaining joints (from 1 to joint count) 
            int ID_count = 0;
            foreach (Joint joint in JointList)
            {
                ID_count++;
                joint.ID = ID_count.ToString();
            }

            //Creating Global matrix
            /*-------------------------
             * GlobalMatrix_Table initial formation:
             * ------------------------
             * --   u1  v1  u2  v2  ...
             * u1   0   0   0   0   ...
             * v1   0   0   0   0   ...
             * u2   0   0   0   0   ...
             * v2   0   0   0   0   ...
             * .    .   .   .   .   .
             * .    .   .   .   .    .
             * .    .   .   .   .     .
             */
            string[,] GlobalMatrix_Table = new string[length, length];
            foreach (Joint joint in JointList.ToArray())//filling 1st Row & Column
            {
                int id = int.Parse(joint.ID);

                GlobalMatrix_Table[0, 2 * id - 1] = joint.u;
                GlobalMatrix_Table[2 * id - 1, 0] = joint.u;

                GlobalMatrix_Table[0, 2 * id] = joint.v;
                GlobalMatrix_Table[2 * id, 0] = joint.v;
            }
            for (int i = 1; i < length; i++)//filling other elements
            {
                for (int j = 1; j < length; j++)
                {
                    GlobalMatrix_Table[i, j] = "0";
                }
            }
            foreach (Link link in LinkList.ToArray())
            {
                //creating local matix for each link
                link.CreatLocalMatrix();

                //adding each local matrix to the global matrix 
                int i = int.Parse(link.JI.ID), f = int.Parse(link.JF.ID);

                //1st row of local matrix
                GlobalMatrix_Table[2 * i - 1, 2 * i - 1] = (double.Parse(GlobalMatrix_Table[2 * i - 1, 2 * i - 1]) + double.Parse(link.LocalMartix[1, 1])).ToString();
                GlobalMatrix_Table[2 * i - 1, 2 * i] = (double.Parse(GlobalMatrix_Table[2 * i - 1, 2 * i]) + double.Parse(link.LocalMartix[1, 2])).ToString();
                GlobalMatrix_Table[2 * i - 1, 2 * f - 1] = (double.Parse(GlobalMatrix_Table[2 * i - 1, 2 * f - 1]) + double.Parse(link.LocalMartix[1, 3])).ToString();
                GlobalMatrix_Table[2 * i - 1, 2 * f] = (double.Parse(GlobalMatrix_Table[2 * i - 1, 2 * f]) + double.Parse(link.LocalMartix[1, 4])).ToString();
                //2nd row of local matrix
                GlobalMatrix_Table[2 * i, 2 * i - 1] = (double.Parse(GlobalMatrix_Table[2 * i, 2 * i - 1]) + double.Parse(link.LocalMartix[2, 1])).ToString();
                GlobalMatrix_Table[2 * i, 2 * i] = (double.Parse(GlobalMatrix_Table[2 * i, 2 * i]) + double.Parse(link.LocalMartix[2, 2])).ToString();
                GlobalMatrix_Table[2 * i, 2 * f - 1] = (double.Parse(GlobalMatrix_Table[2 * i, 2 * f - 1]) + double.Parse(link.LocalMartix[2, 3])).ToString();
                GlobalMatrix_Table[2 * i, 2 * f] = (double.Parse(GlobalMatrix_Table[2 * i, 2 * f]) + double.Parse(link.LocalMartix[2, 4])).ToString();
                //3rd row of local matrix
                GlobalMatrix_Table[2 * f - 1, 2 * i - 1] = (double.Parse(GlobalMatrix_Table[2 * f - 1, 2 * i - 1]) + double.Parse(link.LocalMartix[3, 1])).ToString();
                GlobalMatrix_Table[2 * f - 1, 2 * i] = (double.Parse(GlobalMatrix_Table[2 * f - 1, 2 * i]) + double.Parse(link.LocalMartix[3, 2])).ToString();
                GlobalMatrix_Table[2 * f - 1, 2 * f - 1] = (double.Parse(GlobalMatrix_Table[2 * f - 1, 2 * f - 1]) + double.Parse(link.LocalMartix[3, 3])).ToString();
                GlobalMatrix_Table[2 * f - 1, 2 * f] = (double.Parse(GlobalMatrix_Table[2 * f - 1, 2 * f]) + double.Parse(link.LocalMartix[3, 4])).ToString();
                //4th row of local matrix
                GlobalMatrix_Table[2 * f, 2 * i - 1] = (double.Parse(GlobalMatrix_Table[2 * f, 2 * i - 1]) + double.Parse(link.LocalMartix[4, 1])).ToString();
                GlobalMatrix_Table[2 * f, 2 * i] = (double.Parse(GlobalMatrix_Table[2 * f, 2 * i]) + double.Parse(link.LocalMartix[4, 2])).ToString();
                GlobalMatrix_Table[2 * f, 2 * f - 1] = (double.Parse(GlobalMatrix_Table[2 * f, 2 * f - 1]) + double.Parse(link.LocalMartix[4, 3])).ToString();
                GlobalMatrix_Table[2 * f, 2 * f] = (double.Parse(GlobalMatrix_Table[2 * f, 2 * f]) + double.Parse(link.LocalMartix[4, 4])).ToString();
            }

            //changin global matrix into calcutable form
            double[,] GlobalMatrix = new double[GlobalMatrix_Table.GetLength(0) - 1, GlobalMatrix_Table.GetLength(0) - 1];
            double[,] GlobalMatrix_Copy = new double[GlobalMatrix_Table.GetLength(0) - 1, GlobalMatrix_Table.GetLength(0) - 1];
            for (int i = 0; i < GlobalMatrix_Table.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < GlobalMatrix_Table.GetLength(0) - 1; j++)
                {
                    GlobalMatrix[i, j] = double.Parse(GlobalMatrix_Table[i + 1, j + 1]);
                    GlobalMatrix_Copy[i, j] = double.Parse(GlobalMatrix_Table[i + 1, j + 1]);
                }
            }


            //taking into account boundry condition of matrix ;)
            int RemovedLineCount = 0;
            bool[] IsLineRemoved = new bool[GlobalMatrix.GetLength(0)]; //which lines of global matrix have been removed??
            foreach (Joint joint in JointList.ToArray())
            {
                int JointId = int.Parse(joint.ID);

                //u
                if (joint.XT < 1e-12)
                {
                    GlobalMatrix = RemoveInactiveLines(GlobalMatrix, 2 * JointId - 2);
                    RemovedLineCount++;
                    IsLineRemoved[2 * JointId - 2] = true;
                }
                //v
                if (joint.YT < 1e-12)
                {
                    GlobalMatrix = RemoveInactiveLines(GlobalMatrix, 2 * JointId - 1);
                    RemovedLineCount++;
                    IsLineRemoved[2 * JointId - 1] = true;
                }
            }
            
            //solving finite element equation
            double[][] ActiveGlobalMatrix = new double[GlobalMatrix.GetLength(0) - RemovedLineCount][];
            for (int i = 0; i < GlobalMatrix.GetLength(0) - RemovedLineCount; ++i)
                ActiveGlobalMatrix[i] = new double[GlobalMatrix.GetLength(0) - RemovedLineCount];

            //double[][] ActiveGlobalMatrix = new double[GlobalMatrix.GetLength(0) - RemovedLineCount, GlobalMatrix.GetLength(0) - RemovedLineCount];
            int L = GlobalMatrix.GetLength(0), x = 0, y = 0;
            for (int i = 0; i < L; i++)
            {
                if (IsLineRemoved[i])
                    continue;

                for (int j = 0; j < L; j++)
                {
                    if (IsLineRemoved[j])
                        continue;

                    ActiveGlobalMatrix[x][y] = GlobalMatrix[i, j];
                    y++;
                    if (y == ActiveGlobalMatrix.GetLength(0))
                    {
                        y = 0;
                        x++;
                    }
                }
            }

            //Using JamesMcCaffrey method to find inverse matrix of ActiveGlobalMatrix.
            double[,] ActiveGlobalMatrix_Inverse = new double[ActiveGlobalMatrix.GetLength(0), ActiveGlobalMatrix.GetLength(0)];
            if (MatrixInverse(ActiveGlobalMatrix) != null)
            {
                for (int i = 0; i < ActiveGlobalMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < ActiveGlobalMatrix.GetLength(0); j++)
                    {
                        ActiveGlobalMatrix_Inverse[i, j] = MatrixInverse(ActiveGlobalMatrix)[i][j];
                    }
                }
            }
            else
            {
                //Truss is invalid
                MessageBox.Show("Unstable Truss!");
                return;
            }

            //External Forces
            double[] ExternalForces = new double[GlobalMatrix.GetLength(0)];
            foreach (Joint joint in JointList.ToArray())
            {
                int id = int.Parse(joint.ID);

                ExternalForces[2 * id - 2] = joint.XF;
                ExternalForces[2 * id - 1] = joint.YF;
            }

            //active external force marix
            double[] ActiveExternalForces = new double[ActiveGlobalMatrix.GetLength(0)];
            int activecounter = 0;
            for (int i = 0; i < GlobalMatrix.GetLength(0); i++)
            {
                if (!IsLineRemoved[i])//checking not be removed...
                {
                    ActiveExternalForces[activecounter] = ExternalForces[i];
                    activecounter++;
                }
            }

            //active Translation matrix
            double[] ActiveTranslationMatrix = new double[ActiveGlobalMatrix.GetLength(0)];
            for (int i = 0; i < ActiveGlobalMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ActiveGlobalMatrix.GetLength(0); j++)
                {
                    ActiveTranslationMatrix[i] += ActiveExternalForces[j] * ActiveGlobalMatrix_Inverse[i, j];
                }
            }

            //Translation Matrix
            double[] TranslationMatrix = new double[GlobalMatrix.GetLength(0)];
            activecounter = 0;
            for (int i = 0; i < GlobalMatrix.GetLength(0); i++)
            {
                if (IsLineRemoved[i])
                {
                    TranslationMatrix[i] = 0;
                }
                else
                {
                    TranslationMatrix[i] = ActiveTranslationMatrix[activecounter];
                    activecounter++;
                }
            }
            foreach (Joint joint in JointList.ToArray())
            {
                int id = int.Parse(joint.ID);

                joint.UT = TranslationMatrix[2 * id - 2];
                joint.VT = TranslationMatrix[2 * id - 1];
            }

            //Supports Force
            double[] SupportForce = new double[GlobalMatrix.GetLength(0)];
            for (int i = 0; i < GlobalMatrix.GetLength(0); i++)
            {
                if (IsLineRemoved[i])
                {
                    for (int j = 0; j < GlobalMatrix_Copy.GetLength(0); j++)
                    {
                        SupportForce[i] += GlobalMatrix_Copy[i, j] * TranslationMatrix[j];
                    }
                }

                //for (int j = 0; j < GlobalMatrix_Copy.GetLength(0); j++)
                //{
                //    SupportForce[i] += GlobalMatrix_Copy[i, j] * TranslationMatrix[j];
                //}
            }
            foreach (Joint joint in JointList.ToArray())
            {
                int id = int.Parse(joint.ID);

                joint.XS = SupportForce[2 * id - 2];
                joint.YS = SupportForce[2 * id - 1];
            }

            //calculating members internal forces
            foreach (Link link in LinkList.ToArray())
            {
                link.Create_B_matrix();
                link.Creat_T_Matrix();
                link.Create_u_Matrix();

                link.Strain = MatrixProduct(link.T_matrix, link.B_Matrix, link.U_Matrix);
                link.Stress = link.Elasticity * link.Strain;
                link.InternalForce = (link.Stress * (link.Area)); //Internal force: (N)

                if (link.InternalForce > 0) //tension:: Green
                {
                    link.Tension = true;

                    link.Compression = false;
                    link.Neutral = false;
                }
                else if (link.InternalForce == 0) // normal :: UnSellected
                {
                    link.Neutral = true;

                    link.Tension = false;
                    link.Compression = false;
                }
                else if (link.InternalForce < 0)//compression:: Red
                {
                    link.Compression = true;

                    link.Tension = false;
                    link.Neutral = false;
                }
            }
            SketchPanel.Invalidate();
        }

        private bool CheckRotationalgStability()
        {
            //cheking fo external stability
            foreach (Joint joint1 in JointList)
            {
                //Rotation
                if (joint1.YT == 0)
                {
                    foreach (Joint joint2 in JointList)
                    {
                        if (joint2.YT == 0)
                        {
                            if (joint1.XL != joint2.XL)
                            {
                                foreach (Joint joint in JointList)
                                {
                                    if (joint.XT == 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (joint1.XT == 0)
                {
                    foreach (Joint joint3 in JointList)
                    {
                        if (joint3.XT == 0)
                        {
                            if (joint1.YL != joint3.YL)
                            {
                                foreach (Joint joint in JointList)
                                {
                                    if (joint.YT == 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //remove inactive lines and rows from the global matrix
        double[,] RemoveInactiveLines(double[,] matrix, int LineToRemove)
        {
            int length = matrix.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                matrix[i, LineToRemove] = double.NaN;
                matrix[LineToRemove, i] = double.NaN;
            }
            return matrix;
        }

        public double MatrixProduct(double[,] Matrix_T, double[] Matrix_B, double[] Matrix_U)//Only for BTu!
        {
            //Calculation T * U:

            double[] Result_TU = new double[Matrix_T.GetLength(0)];

            for (int i = 0; i < Matrix_T.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix_U.GetLength(0); j++)
                {
                    Result_TU[i] += Matrix_T[i, j] * Matrix_U[j];
                }
            }

            //Calculationg B * T * u:
            double Result_BTu = new double();
            for (int i = 0; i < Result_TU.GetLength(0); i++)
            {
                Result_BTu += Matrix_B[i] * Result_TU[i];
            }

            return Result_BTu;
        }

        #region Inverse Matrix JamesMcCaffrey
        static double[][] MatrixCreate(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        static double[][] MatrixInverse(double[][] matrix)
        {
            int n = matrix.Length;
            double[][] result = MatrixDuplicate(matrix);

            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
            {
                //Invalid Truss!!
                return null;
            }

            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }

                double[] x = HelperSolve(lum, b);

                for (int j = 0; j < n; ++j)
                    result[j][i] = x[j];
            }
            return result;
        }

        static double[][] MatrixDuplicate(double[][] matrix)
        {
            // allocates/creates a duplicate of a matrix.
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // copy the values
                for (int j = 0; j < matrix[i].Length; ++j)
                    result[i][j] = matrix[i][j];
            return result;
        }

        static double[] HelperSolve(double[][] luMatrix, double[] b)
        {
            // before calling this helper, permute b using the perm array
            // from MatrixDecompose that generated luMatrix
            int n = luMatrix.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);

            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum;
            }

            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum / luMatrix[i][i];
            }

            return x;
        }

        static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
        {
            // Doolittle LUP decomposition with partial pivoting.
            // rerturns: result is L (with 1s on diagonal) and U;
            // perm holds row permutations; toggle is +1 or -1 (even or odd)
            int rows = matrix.Length;
            int cols = matrix[0].Length; // assume square
            if (rows != cols)
                throw new Exception("Attempt to decompose a non-square m");

            int n = rows; // convenience

            double[][] result = MatrixDuplicate(matrix);

            perm = new int[n]; // set up row permutation result
            for (int i = 0; i < n; ++i) { perm[i] = i; }

            toggle = 1; // toggle tracks row swaps.
                        // +1 -greater-than even, -1 -greater-than odd. used by MatrixDeterminant

            for (int j = 0; j < n - 1; ++j) // each column
            {
                double colMax = Math.Abs(result[j][j]); // find largest val in col
                int pRow = j;

                // reader Matt V needed this:
                for (int i = j + 1; i < n; ++i)
                {
                    if (Math.Abs(result[i][j]) > colMax)
                    {
                        colMax = Math.Abs(result[i][j]);
                        pRow = i;
                    }
                }
                // Not sure if this approach is needed always, or not.

                if (pRow != j) // if largest value not on pivot, swap rows
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;

                    int tmp = perm[pRow]; // and swap perm info
                    perm[pRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle; // adjust the row-swap toggle
                }

                // --------------------------------------------------
                // and replaces the 'return null' below.
                // if there is a 0 on the diagonal, find a good row
                // from i = j+1 down that doesn't have
                // a 0 in column j, and swap that good row with row j
                // --------------------------------------------------

                if (result[j][j] == 0.0)
                {
                    // find a good row to swap
                    int goodRow = -1;
                    for (int row = j + 1; row < n; ++row)
                    {
                        if (result[row][j] != 0.0)
                            goodRow = row;
                    }

                    if (goodRow == -1)
                    {
                        //Invalid Truss!
                        return null;
                    }
                    //throw new Exception("Cannot use Doolittle's method");

                    // swap rows so 0.0 no longer on diagonal
                    double[] rowPtr = result[goodRow];
                    result[goodRow] = result[j];
                    result[j] = rowPtr;

                    int tmp = perm[goodRow]; // and swap perm info
                    perm[goodRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle; // adjust the row-swap toggle
                }
                // --------------------------------------------------
                // if diagonal after swap is zero . .
                //if (Math.Abs(result[j][j]) less-than 1.0E-20) 
                //  return null; // consider a throw

                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                    {
                        result[i][k] -= result[i][j] * result[j][k];
                    }
                }
            } // main j column loop

            return result;
        }
        #endregion

        #endregion
    }
}
