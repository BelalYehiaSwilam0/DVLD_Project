namespace DVLD
{
    partial class frmTest
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
            this.ctrlScheduleTest1 = new DVLD.ctrlScheduleTest();
            this.ctrlScheduleTest2 = new DVLD.ctrlScheduleTest();
            this.ctrlScheduleTest3 = new DVLD.ctrlScheduleTest();
            this.SuspendLayout();
            // 
            // ctrlScheduleTest1
            // 
            this.ctrlScheduleTest1.Enabled = false;
            this.ctrlScheduleTest1.Location = new System.Drawing.Point(-4, 0);
            this.ctrlScheduleTest1.Name = "ctrlScheduleTest1";
            this.ctrlScheduleTest1.Size = new System.Drawing.Size(613, 673);
            this.ctrlScheduleTest1.TabIndex = 0;
            this.ctrlScheduleTest1.TestTypeID = BusinessLayer_DVLD.clsTestType.enTestType.VisionTest;
            // 
            // ctrlScheduleTest2
            // 
            this.ctrlScheduleTest2.Location = new System.Drawing.Point(605, 12);
            this.ctrlScheduleTest2.Name = "ctrlScheduleTest2";
            this.ctrlScheduleTest2.Size = new System.Drawing.Size(613, 673);
            this.ctrlScheduleTest2.TabIndex = 1;
            this.ctrlScheduleTest2.TestTypeID = BusinessLayer_DVLD.clsTestType.enTestType.WrittenTest;
            // 
            // ctrlScheduleTest3
            // 
            this.ctrlScheduleTest3.Location = new System.Drawing.Point(1205, 12);
            this.ctrlScheduleTest3.Name = "ctrlScheduleTest3";
            this.ctrlScheduleTest3.Size = new System.Drawing.Size(613, 673);
            this.ctrlScheduleTest3.TabIndex = 2;
            this.ctrlScheduleTest3.TestTypeID = BusinessLayer_DVLD.clsTestType.enTestType.StreetTest;
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1818, 821);
            this.Controls.Add(this.ctrlScheduleTest3);
            this.Controls.Add(this.ctrlScheduleTest2);
            this.Controls.Add(this.ctrlScheduleTest1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "frmTest";
            this.Text = "frmTest";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlScheduleTest ctrlScheduleTest1;
        private ctrlScheduleTest ctrlScheduleTest2;
        private ctrlScheduleTest ctrlScheduleTest3;
    }
}