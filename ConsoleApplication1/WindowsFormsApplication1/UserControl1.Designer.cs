namespace WindowsFormsApplication1
{
    partial class UserControl1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customControl11 = new WindowsFormsApplication1.CustomControl1();
            this.customControl12 = new WindowsFormsApplication1.CustomControl1();
            this.customControl13 = new WindowsFormsApplication1.CustomControl1();
            this.customControl14 = new WindowsFormsApplication1.CustomControl1();
            this.customControl15 = new WindowsFormsApplication1.CustomControl1();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.customControl15);
            this.groupBox1.Controls.Add(this.customControl14);
            this.groupBox1.Controls.Add(this.customControl13);
            this.groupBox1.Controls.Add(this.customControl12);
            this.groupBox1.Controls.Add(this.customControl11);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // customControl11
            // 
            this.customControl11.Location = new System.Drawing.Point(10, 20);
            this.customControl11.MaxLength = 3;
            this.customControl11.Name = "customControl11";
            this.customControl11.Size = new System.Drawing.Size(28, 20);
            this.customControl11.TabIndex = 0;
            // 
            // customControl12
            // 
            this.customControl12.Location = new System.Drawing.Point(43, 20);
            this.customControl12.MaxLength = 3;
            this.customControl12.Name = "customControl12";
            this.customControl12.Size = new System.Drawing.Size(28, 20);
            this.customControl12.TabIndex = 1;
            // 
            // customControl13
            // 
            this.customControl13.Location = new System.Drawing.Point(77, 20);
            this.customControl13.MaxLength = 3;
            this.customControl13.Name = "customControl13";
            this.customControl13.Size = new System.Drawing.Size(28, 20);
            this.customControl13.TabIndex = 2;
            // 
            // customControl14
            // 
            this.customControl14.Location = new System.Drawing.Point(111, 20);
            this.customControl14.MaxLength = 3;
            this.customControl14.Name = "customControl14";
            this.customControl14.Size = new System.Drawing.Size(28, 20);
            this.customControl14.TabIndex = 3;
            // 
            // customControl15
            // 
            this.customControl15.Location = new System.Drawing.Point(145, 20);
            this.customControl15.MaxLength = 5;
            this.customControl15.Name = "customControl15";
            this.customControl15.Size = new System.Drawing.Size(42, 20);
            this.customControl15.TabIndex = 4;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(203, 51);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControl1 customControl15;
        private CustomControl1 customControl14;
        private CustomControl1 customControl13;
        private CustomControl1 customControl12;
        private CustomControl1 customControl11;
    }
}
