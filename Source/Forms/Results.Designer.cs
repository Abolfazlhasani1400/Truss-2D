namespace Truss_2D
{
    partial class Results
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Table = new System.Windows.Forms.DataGridView();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InitialLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Final_Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).BeginInit();
            this.SuspendLayout();
            // 
            // Table
            // 
            this.Table.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.End,
            this.A,
            this.E,
            this.InitialLength,
            this.Final_Length,
            this.Strain,
            this.Stress});
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.Location = new System.Drawing.Point(0, 0);
            this.Table.Name = "Table";
            this.Table.Size = new System.Drawing.Size(844, 411);
            this.Table.TabIndex = 0;
            // 
            // Start
            // 
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            // 
            // End
            // 
            this.End.HeaderText = "End";
            this.End.Name = "End";
            // 
            // A
            // 
            this.A.HeaderText = "Section area";
            this.A.Name = "A";
            // 
            // E
            // 
            this.E.HeaderText = "Elastic modulus";
            this.E.Name = "E";
            // 
            // InitialLength
            // 
            this.InitialLength.HeaderText = "L0";
            this.InitialLength.Name = "InitialLength";
            // 
            // Final_Length
            // 
            this.Final_Length.HeaderText = "Length";
            this.Final_Length.Name = "Final_Length";
            // 
            // Strain
            // 
            this.Strain.HeaderText = "Strain";
            this.Strain.Name = "Strain";
            // 
            // Stress
            // 
            this.Stress.HeaderText = "Stress";
            this.Stress.Name = "Stress";
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(844, 411);
            this.Controls.Add(this.Table);
            this.Name = "Results";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Results";
            ((System.ComponentModel.ISupportInitialize)(this.Table)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Table;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn E;
        private System.Windows.Forms.DataGridViewTextBoxColumn InitialLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn Final_Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Strain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stress;
    }
}
