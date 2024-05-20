namespace Truss_2D
{
    partial class ChooseDiretion
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
            this.Slope90_Check = new System.Windows.Forms.CheckBox();
            this.Slope0_Check = new System.Windows.Forms.CheckBox();
            this.OptionPanel = new System.Windows.Forms.Panel();
            this.Slope270_Check = new System.Windows.Forms.CheckBox();
            this.Slope180_Check = new System.Windows.Forms.CheckBox();
            this.OptionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose roller support direction";
            // 
            // Slope90_Check
            // 
            this.Slope90_Check.AutoSize = true;
            this.Slope90_Check.Location = new System.Drawing.Point(120, 110);
            this.Slope90_Check.Name = "Slope90_Check";
            this.Slope90_Check.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Slope90_Check.Size = new System.Drawing.Size(15, 14);
            this.Slope90_Check.TabIndex = 1;
            this.Slope90_Check.UseVisualStyleBackColor = true;
            this.Slope90_Check.CheckedChanged += new System.EventHandler(this.Slope90_Check_CheckedChanged);
            // 
            // Slope0_Check
            // 
            this.Slope0_Check.AutoSize = true;
            this.Slope0_Check.Location = new System.Drawing.Point(120, 30);
            this.Slope0_Check.Name = "Slope0_Check";
            this.Slope0_Check.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Slope0_Check.Size = new System.Drawing.Size(15, 14);
            this.Slope0_Check.TabIndex = 2;
            this.Slope0_Check.UseVisualStyleBackColor = true;
            this.Slope0_Check.CheckedChanged += new System.EventHandler(this.Slope0_Check_CheckedChanged);
            // 
            // OptionPanel
            // 
            this.OptionPanel.BackColor = System.Drawing.Color.Transparent;
            this.OptionPanel.Controls.Add(this.Slope270_Check);
            this.OptionPanel.Controls.Add(this.Slope90_Check);
            this.OptionPanel.Controls.Add(this.Slope180_Check);
            this.OptionPanel.Controls.Add(this.Slope0_Check);
            this.OptionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OptionPanel.Location = new System.Drawing.Point(0, 34);
            this.OptionPanel.Name = "OptionPanel";
            this.OptionPanel.Size = new System.Drawing.Size(300, 160);
            this.OptionPanel.TabIndex = 3;
            this.OptionPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OptionPanel_Paint);
            // 
            // Slope270_Check
            // 
            this.Slope270_Check.AutoSize = true;
            this.Slope270_Check.Location = new System.Drawing.Point(270, 110);
            this.Slope270_Check.Name = "Slope270_Check";
            this.Slope270_Check.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Slope270_Check.Size = new System.Drawing.Size(15, 14);
            this.Slope270_Check.TabIndex = 1;
            this.Slope270_Check.UseVisualStyleBackColor = true;
            this.Slope270_Check.CheckedChanged += new System.EventHandler(this.Slope270_Check_CheckedChanged);
            // 
            // Slope180_Check
            // 
            this.Slope180_Check.AutoSize = true;
            this.Slope180_Check.Location = new System.Drawing.Point(270, 30);
            this.Slope180_Check.Name = "Slope180_Check";
            this.Slope180_Check.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Slope180_Check.Size = new System.Drawing.Size(15, 14);
            this.Slope180_Check.TabIndex = 2;
            this.Slope180_Check.UseVisualStyleBackColor = true;
            this.Slope180_Check.CheckedChanged += new System.EventHandler(this.Slope180_Check_CheckedChanged);
            // 
            // ChooseDiretion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(300, 194);
            this.ControlBox = false;
            this.Controls.Add(this.OptionPanel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseDiretion";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChooseDirection_KeyDown);
            this.OptionPanel.ResumeLayout(false);
            this.OptionPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Slope90_Check;
        private System.Windows.Forms.CheckBox Slope0_Check;
        private System.Windows.Forms.Panel OptionPanel;
        private System.Windows.Forms.CheckBox Slope270_Check;
        private System.Windows.Forms.CheckBox Slope180_Check;
    }
}