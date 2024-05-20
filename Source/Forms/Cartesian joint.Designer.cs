namespace Truss_2D
{
    partial class Cartesian_Form
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
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.Add_cartesian_Joint = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.CreateNewLink_CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // textBox_X
            // 
            this.textBox_X.Location = new System.Drawing.Point(53, 6);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(100, 20);
            this.textBox_X.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(159, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y:";
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(200, 6);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(100, 20);
            this.textBox_Y.TabIndex = 2;
            // 
            // Add_cartesian_Joint
            // 
            this.Add_cartesian_Joint.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add_cartesian_Joint.Location = new System.Drawing.Point(321, 6);
            this.Add_cartesian_Joint.Name = "Add_cartesian_Joint";
            this.Add_cartesian_Joint.Size = new System.Drawing.Size(75, 23);
            this.Add_cartesian_Joint.TabIndex = 3;
            this.Add_cartesian_Joint.Text = "Add";
            this.Add_cartesian_Joint.UseVisualStyleBackColor = true;
            this.Add_cartesian_Joint.Click += new System.EventHandler(this.Add_cartesian_Joint_Click);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(12, 70);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.ErrorLabel.TabIndex = 4;
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
            // Cartesian_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 92);
            this.Controls.Add(this.CreateNewLink_CheckBox);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.Add_cartesian_Joint);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cartesian_Form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please enter cartesian coordinates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox_X;
        public System.Windows.Forms.TextBox textBox_Y;
        public System.Windows.Forms.Button Add_cartesian_Joint;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.CheckBox CreateNewLink_CheckBox;
    }
}