namespace DVLD
{
    partial class ctrlScheduleTest
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
            this.btnSave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblScheduleTestTitle = new System.Windows.Forms.Label();
            this.lblUserMessage = new System.Windows.Forms.Label();
            this.dtpTestDate = new System.Windows.Forms.DateTimePicker();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblTrialResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.lblFeesResult = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblNameResult = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblDClass = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.lblDLAPPIDResult = new System.Windows.Forms.Label();
            this.lblNationalNO = new System.Windows.Forms.Label();
            this.gbRetakeTestInfo = new System.Windows.Forms.GroupBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.lblRTestAppIDResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.lblRAPPFeesResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.lblTotalFeesResult = new System.Windows.Forms.Label();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.gbTestType = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.gbRetakeTestInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.gbTestType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(449, 616);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 39);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Vision_512;
            this.pictureBox1.Location = new System.Drawing.Point(174, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(236, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // lblScheduleTestTitle
            // 
            this.lblScheduleTestTitle.AutoSize = true;
            this.lblScheduleTestTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScheduleTestTitle.ForeColor = System.Drawing.Color.Red;
            this.lblScheduleTestTitle.Location = new System.Drawing.Point(177, 178);
            this.lblScheduleTestTitle.Name = "lblScheduleTestTitle";
            this.lblScheduleTestTitle.Size = new System.Drawing.Size(233, 37);
            this.lblScheduleTestTitle.TabIndex = 16;
            this.lblScheduleTestTitle.Text = "Schedule Test";
            // 
            // lblUserMessage
            // 
            this.lblUserMessage.AutoSize = true;
            this.lblUserMessage.Enabled = false;
            this.lblUserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserMessage.ForeColor = System.Drawing.Color.Red;
            this.lblUserMessage.Location = new System.Drawing.Point(73, 215);
            this.lblUserMessage.Name = "lblUserMessage";
            this.lblUserMessage.Size = new System.Drawing.Size(432, 20);
            this.lblUserMessage.TabIndex = 21;
            this.lblUserMessage.Text = "Cannot Sechule, Vision Test Should be Passed First.";
            this.lblUserMessage.Visible = false;
            // 
            // dtpTestDate
            // 
            this.dtpTestDate.CustomFormat = "dd/M/yyyy";
            this.dtpTestDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTestDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTestDate.Location = new System.Drawing.Point(227, 412);
            this.dtpTestDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpTestDate.Name = "dtpTestDate";
            this.dtpTestDate.Size = new System.Drawing.Size(150, 22);
            this.dtpTestDate.TabIndex = 97;
            this.dtpTestDate.Value = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.Count_32;
            this.pictureBox4.Location = new System.Drawing.Point(174, 371);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 96;
            this.pictureBox4.TabStop = false;
            // 
            // lblTrialResult
            // 
            this.lblTrialResult.AutoSize = true;
            this.lblTrialResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrialResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTrialResult.Location = new System.Drawing.Point(223, 371);
            this.lblTrialResult.Name = "lblTrialResult";
            this.lblTrialResult.Size = new System.Drawing.Size(49, 20);
            this.lblTrialResult.TabIndex = 95;
            this.lblTrialResult.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 94;
            this.label2.Text = "Trial :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Calendar_32;
            this.pictureBox2.Location = new System.Drawing.Point(174, 412);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 93;
            this.pictureBox2.TabStop = false;
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfBirth.Location = new System.Drawing.Point(100, 412);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(58, 20);
            this.lblDateOfBirth.TabIndex = 92;
            this.lblDateOfBirth.Text = "Date :";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DVLD.Properties.Resources.money_32;
            this.pictureBox6.Location = new System.Drawing.Point(174, 453);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(31, 35);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 91;
            this.pictureBox6.TabStop = false;
            // 
            // lblFeesResult
            // 
            this.lblFeesResult.AutoSize = true;
            this.lblFeesResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeesResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFeesResult.Location = new System.Drawing.Point(223, 453);
            this.lblFeesResult.Name = "lblFeesResult";
            this.lblFeesResult.Size = new System.Drawing.Size(49, 20);
            this.lblFeesResult.TabIndex = 90;
            this.lblFeesResult.Text = "[$$$]";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(99, 453);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(59, 20);
            this.lblEmail.TabIndex = 89;
            this.lblEmail.Text = "Fees :";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD.Properties.Resources.Person_32;
            this.pictureBox5.Location = new System.Drawing.Point(174, 330);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 35);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 88;
            this.pictureBox5.TabStop = false;
            // 
            // lblNameResult
            // 
            this.lblNameResult.AutoSize = true;
            this.lblNameResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNameResult.Location = new System.Drawing.Point(223, 330);
            this.lblNameResult.Name = "lblNameResult";
            this.lblNameResult.Size = new System.Drawing.Size(49, 20);
            this.lblNameResult.TabIndex = 87;
            this.lblNameResult.Text = "[???]";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(100, 330);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 20);
            this.lblName.TabIndex = 86;
            this.lblName.Text = "Name :";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD.Properties.Resources.Lost_Driving_License_32;
            this.pictureBox3.Location = new System.Drawing.Point(174, 289);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 85;
            this.pictureBox3.TabStop = false;
            // 
            // lblDClass
            // 
            this.lblDClass.AutoSize = true;
            this.lblDClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDClass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDClass.Location = new System.Drawing.Point(223, 289);
            this.lblDClass.Name = "lblDClass";
            this.lblDClass.Size = new System.Drawing.Size(49, 20);
            this.lblDClass.TabIndex = 84;
            this.lblDClass.Text = "[???]";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.Location = new System.Drawing.Point(77, 289);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(81, 20);
            this.lblGender.TabIndex = 83;
            this.lblGender.Text = "D.Class :";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox7.Location = new System.Drawing.Point(174, 248);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(31, 35);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 82;
            this.pictureBox7.TabStop = false;
            // 
            // lblDLAPPIDResult
            // 
            this.lblDLAPPIDResult.AutoSize = true;
            this.lblDLAPPIDResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDLAPPIDResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDLAPPIDResult.Location = new System.Drawing.Point(223, 248);
            this.lblDLAPPIDResult.Name = "lblDLAPPIDResult";
            this.lblDLAPPIDResult.Size = new System.Drawing.Size(49, 20);
            this.lblDLAPPIDResult.TabIndex = 81;
            this.lblDLAPPIDResult.Text = "[???]";
            // 
            // lblNationalNO
            // 
            this.lblNationalNO.AutoSize = true;
            this.lblNationalNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNationalNO.Location = new System.Drawing.Point(50, 248);
            this.lblNationalNO.Name = "lblNationalNO";
            this.lblNationalNO.Size = new System.Drawing.Size(108, 20);
            this.lblNationalNO.TabIndex = 80;
            this.lblNationalNO.Text = "D.L.App ID :";
            // 
            // gbRetakeTestInfo
            // 
            this.gbRetakeTestInfo.Controls.Add(this.pictureBox8);
            this.gbRetakeTestInfo.Controls.Add(this.lblRTestAppIDResult);
            this.gbRetakeTestInfo.Controls.Add(this.label1);
            this.gbRetakeTestInfo.Controls.Add(this.pictureBox9);
            this.gbRetakeTestInfo.Controls.Add(this.lblRAPPFeesResult);
            this.gbRetakeTestInfo.Controls.Add(this.label3);
            this.gbRetakeTestInfo.Controls.Add(this.pictureBox10);
            this.gbRetakeTestInfo.Controls.Add(this.lblTotalFeesResult);
            this.gbRetakeTestInfo.Controls.Add(this.lblTotalFees);
            this.gbRetakeTestInfo.Location = new System.Drawing.Point(16, 497);
            this.gbRetakeTestInfo.Name = "gbRetakeTestInfo";
            this.gbRetakeTestInfo.Size = new System.Drawing.Size(546, 112);
            this.gbRetakeTestInfo.TabIndex = 98;
            this.gbRetakeTestInfo.TabStop = false;
            this.gbRetakeTestInfo.Text = "RetakeTestInfo";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox8.Location = new System.Drawing.Point(158, 63);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 35);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 83;
            this.pictureBox8.TabStop = false;
            // 
            // lblRTestAppIDResult
            // 
            this.lblRTestAppIDResult.AutoSize = true;
            this.lblRTestAppIDResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRTestAppIDResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRTestAppIDResult.Location = new System.Drawing.Point(195, 69);
            this.lblRTestAppIDResult.Name = "lblRTestAppIDResult";
            this.lblRTestAppIDResult.Size = new System.Drawing.Size(49, 20);
            this.lblRTestAppIDResult.TabIndex = 82;
            this.lblRTestAppIDResult.Text = "[???]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 81;
            this.label1.Text = "R.Test.App ID :";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::DVLD.Properties.Resources.money_32;
            this.pictureBox9.Location = new System.Drawing.Point(158, 22);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(31, 35);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 80;
            this.pictureBox9.TabStop = false;
            // 
            // lblRAPPFeesResult
            // 
            this.lblRAPPFeesResult.AutoSize = true;
            this.lblRAPPFeesResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRAPPFeesResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRAPPFeesResult.Location = new System.Drawing.Point(195, 29);
            this.lblRAPPFeesResult.Name = "lblRAPPFeesResult";
            this.lblRAPPFeesResult.Size = new System.Drawing.Size(49, 20);
            this.lblRAPPFeesResult.TabIndex = 79;
            this.lblRAPPFeesResult.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 78;
            this.label3.Text = "R.App.Fees :";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::DVLD.Properties.Resources.money_32;
            this.pictureBox10.Location = new System.Drawing.Point(434, 21);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(31, 35);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 77;
            this.pictureBox10.TabStop = false;
            // 
            // lblTotalFeesResult
            // 
            this.lblTotalFeesResult.AutoSize = true;
            this.lblTotalFeesResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFeesResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalFeesResult.Location = new System.Drawing.Point(471, 28);
            this.lblTotalFeesResult.Name = "lblTotalFeesResult";
            this.lblTotalFeesResult.Size = new System.Drawing.Size(49, 20);
            this.lblTotalFeesResult.TabIndex = 76;
            this.lblTotalFeesResult.Text = "[???]";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFees.Location = new System.Drawing.Point(324, 28);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(104, 20);
            this.lblTotalFees.TabIndex = 75;
            this.lblTotalFees.Text = "Total Fees :";
            // 
            // gbTestType
            // 
            this.gbTestType.Controls.Add(this.lblScheduleTestTitle);
            this.gbTestType.Controls.Add(this.gbRetakeTestInfo);
            this.gbTestType.Controls.Add(this.pictureBox1);
            this.gbTestType.Controls.Add(this.dtpTestDate);
            this.gbTestType.Controls.Add(this.btnSave);
            this.gbTestType.Controls.Add(this.pictureBox4);
            this.gbTestType.Controls.Add(this.lblUserMessage);
            this.gbTestType.Controls.Add(this.lblTrialResult);
            this.gbTestType.Controls.Add(this.lblNationalNO);
            this.gbTestType.Controls.Add(this.label2);
            this.gbTestType.Controls.Add(this.lblDLAPPIDResult);
            this.gbTestType.Controls.Add(this.pictureBox2);
            this.gbTestType.Controls.Add(this.pictureBox7);
            this.gbTestType.Controls.Add(this.lblDateOfBirth);
            this.gbTestType.Controls.Add(this.lblGender);
            this.gbTestType.Controls.Add(this.pictureBox6);
            this.gbTestType.Controls.Add(this.lblDClass);
            this.gbTestType.Controls.Add(this.lblFeesResult);
            this.gbTestType.Controls.Add(this.pictureBox3);
            this.gbTestType.Controls.Add(this.lblEmail);
            this.gbTestType.Controls.Add(this.lblName);
            this.gbTestType.Controls.Add(this.pictureBox5);
            this.gbTestType.Controls.Add(this.lblNameResult);
            this.gbTestType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTestType.Location = new System.Drawing.Point(3, 5);
            this.gbTestType.Name = "gbTestType";
            this.gbTestType.Size = new System.Drawing.Size(599, 661);
            this.gbTestType.TabIndex = 99;
            this.gbTestType.TabStop = false;
            this.gbTestType.Text = "Test Type";
            // 
            // ctrlScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTestType);
            this.Name = "ctrlScheduleTest";
            this.Size = new System.Drawing.Size(613, 673);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.gbRetakeTestInfo.ResumeLayout(false);
            this.gbRetakeTestInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.gbTestType.ResumeLayout(false);
            this.gbTestType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblScheduleTestTitle;
        private System.Windows.Forms.Label lblUserMessage;
        private System.Windows.Forms.DateTimePicker dtpTestDate;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblTrialResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label lblFeesResult;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lblNameResult;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblDClass;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label lblDLAPPIDResult;
        private System.Windows.Forms.Label lblNationalNO;
        private System.Windows.Forms.GroupBox gbRetakeTestInfo;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label lblRTestAppIDResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label lblRAPPFeesResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label lblTotalFeesResult;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.GroupBox gbTestType;
    }
}
