namespace Truss_2D
{
    partial class Polar_Form
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_R = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Theta = new System.Windows.Forms.TextBox();
            this.Add_polar_Joint = new System.Windows.Forms.Button();
            this.error_label = new System.Windows.Forms.Label();
            this.CreateNewLink_CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "R:";
            // 
            // textBox_R
            // 
            this.textBox_R.Location = new System.Drawing.Point(53, 6);
            this.textBox_R.Name = "textBox_R";
            this.textBox_R.Size = new System.Drawing.Size(100, 20);
            this.textBox_R.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(159, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "θ:";
            // 
            // textBox_Theta
            // 
            this.textBox_Theta.Location = new System.Drawing.Point(200, 6);
            this.textBox_Theta.Name = "textBox_Theta";
            this.textBox_Theta.Size = new System.Drawing.Size(100, 20);
            this.textBox_Theta.TabIndex = 2;
            // 
            // Add_polar_Joint
            // 
            this.Add_polar_Joint.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add_polar_Joint.Location = new System.Drawing.Point(321, 6);
            this.Add_polar_Joint.Name = "Add_polar_Joint";
            this.Add_polar_Joint.Size = new System.Drawing.Size(75, 23);
            this.Add_polar_Joint.TabIndex = 3;
            this.Add_polar_Joint.Text = "Add";
            this.Add_polar_Joint.UseVisualStyleBackColor = true;
            this.Add_polar_Joint.Click += new System.EventHandler(this.Add_Polar_Joint_Click);
            // 
            // error_label
            // 
            this.error_label.AutoSize = true;
            this.error_label.Location = new System.Drawing.Point(12, 70);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(0, 13);
            this.error_label.TabIndex = 4;
            // 
            // CreateNewLink_CheckBox
            // 
            this.CreateNewLink_CheckBox.AutoSize = true;
            this.CreateNewLink_CheckBox.Checked = true;
            this.CreateNewLink_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreateNewLink_CheckBox.Location = new System.Drawing.Point(12, 42);
            this.CreateNewLink_CheckBox.Name = "CreateNewLink_CheckBox";
            this.CreateNewLink_CheckBox.Size = new System.Drawing.Size(105, 17);
            this.CreateNewLink_CheckBox.TabIndex = 5;
            this.CreateNewLink_CheckBox.Text = "Create new link?";
            this.CreateNewLink_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Polar_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 92);
            this.Controls.Add(this.CreateNewLink_CheckBox);
            this.Controls.Add(this.error_label);
            this.Controls.Add(this.Add_polar_Joint);
            this.Controls.Add(this.textBox_Theta);
            this.Controls.Add(this.textBox_R);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Polar_Form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please enter polar coordinates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox_R;
        public System.Windows.Forms.TextBox textBox_Theta;
        public System.Windows.Forms.Button Add_polar_Joint;
        private System.Windows.Forms.Label error_label;
        private System.Windows.Forms.CheckBox CreateNewLink_CheckBox;
    }
}