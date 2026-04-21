namespace DVLD
{
    partial class frmTestLevels
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
            this.lblTitleForTest = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pbForTest = new System.Windows.Forms.PictureBox();
            this.ctrlDrivingLicenseAndApplication_BasicInfo1 = new DVLD.ctrlDrivingLicenseAndApplication_BasicInfo();
            this.ctrlAppointments2 = new DVLD.ctrlAppointments();
            ((System.ComponentModel.ISupportInitialize)(this.pbForTest)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitleForTest
            // 
            this.lblTitleForTest.AutoSize = true;
            this.lblTitleForTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleForTest.ForeColor = System.Drawing.Color.Red;
            this.lblTitleForTest.Location = new System.Drawing.Point(207, 126);
            this.lblTitleForTest.Name = "lblTitleForTest";
            this.lblTitleForTest.Size = new System.Drawing.Size(580, 55);
            this.lblTitleForTest.TabIndex = 2;
            this.lblTitleForTest.Text = "Vition Test Appointments";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(844, 904);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 37);
            this.button1.TabIndex = 12;
            this.button1.Text = "close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbForTest
            // 
            this.pbForTest.Image = global::DVLD.Properties.Resources.Vision_512;
            this.pbForTest.Location = new System.Drawing.Point(373, 12);
            this.pbForTest.Name = "pbForTest";
            this.pbForTest.Size = new System.Drawing.Size(236, 111);
            this.pbForTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbForTest.TabIndex = 3;
            this.pbForTest.TabStop = false;
            // 
            // ctrlDrivingLicenseAndApplication_BasicInfo1
            // 
            this.ctrlDrivingLicenseAndApplication_BasicInfo1.Location = new System.Drawing.Point(15, 184);
            this.ctrlDrivingLicenseAndApplication_BasicInfo1.Name = "ctrlDrivingLicenseAndApplication_BasicInfo1";
            this.ctrlDrivingLicenseAndApplication_BasicInfo1.Size = new System.Drawing.Size(961, 509);
            this.ctrlDrivingLicenseAndApplication_BasicInfo1.TabIndex = 13;
            // 
            // ctrlAppointments2
            // 
            this.ctrlAppointments2.Location = new System.Drawing.Point(12, 681);
            this.ctrlAppointments2.Name = "ctrlAppointments2";
            this.ctrlAppointments2.Size = new System.Drawing.Size(964, 217);
            this.ctrlAppointments2.TabIndex = 15;
            // 
            // frmLevelsTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 947);
            this.Controls.Add(this.ctrlAppointments2);
            this.Controls.Add(this.ctrlDrivingLicenseAndApplication_BasicInfo1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbForTest);
            this.Controls.Add(this.lblTitleForTest);
            this.Name = "frmLevelsTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmVitionTestAppointments";
            this.Load += new System.EventHandler(this.frmVitionTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbForTest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitleForTest;
        private System.Windows.Forms.PictureBox pbForTest;
        private System.Windows.Forms.Button button1;
        private ctrlDrivingLicenseAndApplication_BasicInfo ctrlDrivingLicenseAndApplication_BasicInfo1;
        private ctrlAppointments ctrlAppointments2;
    }
}