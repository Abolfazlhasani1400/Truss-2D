namespace Truss_2D
{
    partial class LinkData
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
            this.data_error_label = new System.Windows.Forms.Label();
            this.link_data_button = new System.Windows.Forms.Button();
            this.data_E_textbox = new System.Windows.Forms.TextBox();
            this.data_A_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // data_error_label
            // 
            this.data_error_label.AutoSize = true;
            this.data_error_label.Location = new System.Drawing.Point(7, 31);
            this.data_error_label.Name = "data_error_label";
            this.data_error_label.Size = new System.Drawing.Size(0, 13);
            this.data_error_label.TabIndex = 10;
            // 
            // link_data_button
            // 
            this.link_data_button.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_data_button.Location = new System.Drawing.Point(312, 6);
            this.link_data_button.Name = "link_data_button";
            this.link_data_button.Size = new System.Drawing.Size(75, 23);
            this.link_data_button.TabIndex = 2;
            this.link_data_button.Text = "Creat";
            this.link_data_button.UseVisualStyleBackColor = true;
            this.link_data_button.Click += new System.EventHandler(this.link_data_button_Click);
            // 
            // data_E_textbox
            // 
            this.data_E_textbox.Location = new System.Drawing.Point(206, 8);
            this.data_E_textbox.Name = "data_E_textbox";
            this.data_E_textbox.Size = new System.Drawing.Size(100, 20);
            this.data_E_textbox.TabIndex = 1;
            // 
            // data_A_textbox
            // 
            this.data_A_textbox.Location = new System.Drawing.Point(45, 8);
            this.data_A_textbox.Name = "data_A_textbox";
            this.data_A_textbox.Size = new System.Drawing.Size(100, 20);
            this.data_A_textbox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Elasticity:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Area:";
            // 
            // LinkData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 51);
            this.ControlBox = false;
            this.Controls.Add(this.data_error_label);
            this.Controls.Add(this.link_data_button);
            this.Controls.Add(this.data_E_textbox);
            this.Controls.Add(this.data_A_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "LinkData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please enter Member\'s characteristics";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LinkData_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label data_error_label;
        public System.Windows.Forms.Button link_data_button;
        public System.Windows.Forms.TextBox data_E_textbox;
        public System.Windows.Forms.TextBox data_A_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}