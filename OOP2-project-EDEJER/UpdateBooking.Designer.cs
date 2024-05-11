namespace OOP2_project_EDEJER
{
    partial class UpdateBooking
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbCourtNumber = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNumberOfCourtsAvailable = new System.Windows.Forms.TextBox();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.cmbEndingTime = new System.Windows.Forms.ComboBox();
            this.cmbStartingTime = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFinalCourtNumber = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtFinalSelectedCourtNumbers = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtFinalDate = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtFinalEndTime = new System.Windows.Forms.TextBox();
            this.txtFinalStartTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUpdate = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.txtFacilityName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnResetCourtInfo = new Guna.UI2.WinForms.Guna2Button();
            this.txtRates = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtFinalPrice = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(113, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 28);
            this.label1.TabIndex = 53;
            this.label1.Text = "Update Booking";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::OOP2_project_EDEJER.Properties.Resources.image_removebg_preview__2_;
            this.pictureBox1.Location = new System.Drawing.Point(149, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // cmbCourtNumber
            // 
            this.cmbCourtNumber.BackColor = System.Drawing.Color.Silver;
            this.cmbCourtNumber.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCourtNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCourtNumber.FormattingEnabled = true;
            this.cmbCourtNumber.Location = new System.Drawing.Point(163, 287);
            this.cmbCourtNumber.Name = "cmbCourtNumber";
            this.cmbCourtNumber.Size = new System.Drawing.Size(195, 23);
            this.cmbCourtNumber.TabIndex = 201;
            this.cmbCourtNumber.SelectedIndexChanged += new System.EventHandler(this.cmbCourtNumber_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(56, 289);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(99, 16);
            this.label24.TabIndex = 200;
            this.label24.Text = "Court Number/s";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(67, 178);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(138, 16);
            this.label16.TabIndex = 199;
            this.label16.Text = "Total Number of Courts";
            // 
            // txtNumberOfCourtsAvailable
            // 
            this.txtNumberOfCourtsAvailable.BackColor = System.Drawing.Color.Silver;
            this.txtNumberOfCourtsAvailable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumberOfCourtsAvailable.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfCourtsAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNumberOfCourtsAvailable.Location = new System.Drawing.Point(211, 178);
            this.txtNumberOfCourtsAvailable.Multiline = true;
            this.txtNumberOfCourtsAvailable.Name = "txtNumberOfCourtsAvailable";
            this.txtNumberOfCourtsAvailable.ReadOnly = true;
            this.txtNumberOfCourtsAvailable.Size = new System.Drawing.Size(150, 16);
            this.txtNumberOfCourtsAvailable.TabIndex = 198;
            // 
            // datePicker
            // 
            this.datePicker.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePicker.Location = new System.Drawing.Point(163, 256);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(195, 21);
            this.datePicker.TabIndex = 196;
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // cmbEndingTime
            // 
            this.cmbEndingTime.BackColor = System.Drawing.Color.Silver;
            this.cmbEndingTime.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEndingTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbEndingTime.FormattingEnabled = true;
            this.cmbEndingTime.Location = new System.Drawing.Point(163, 352);
            this.cmbEndingTime.Name = "cmbEndingTime";
            this.cmbEndingTime.Size = new System.Drawing.Size(195, 23);
            this.cmbEndingTime.TabIndex = 195;
            this.cmbEndingTime.SelectedIndexChanged += new System.EventHandler(this.cmbEndingTime_SelectedIndexChanged);
            // 
            // cmbStartingTime
            // 
            this.cmbStartingTime.BackColor = System.Drawing.Color.Silver;
            this.cmbStartingTime.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStartingTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbStartingTime.FormattingEnabled = true;
            this.cmbStartingTime.Location = new System.Drawing.Point(163, 320);
            this.cmbStartingTime.Name = "cmbStartingTime";
            this.cmbStartingTime.Size = new System.Drawing.Size(195, 23);
            this.cmbStartingTime.TabIndex = 194;
            this.cmbStartingTime.SelectedIndexChanged += new System.EventHandler(this.cmbStartingTime_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(120, 259);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 16);
            this.label17.TabIndex = 193;
            this.label17.Text = "Date";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(80, 354);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 16);
            this.label14.TabIndex = 192;
            this.label14.Text = "Ending Time";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(77, 322);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 16);
            this.label15.TabIndex = 191;
            this.label15.Text = "Starting Time";
            // 
            // txtFinalCourtNumber
            // 
            this.txtFinalCourtNumber.BackColor = System.Drawing.Color.Silver;
            this.txtFinalCourtNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFinalCourtNumber.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalCourtNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFinalCourtNumber.Location = new System.Drawing.Point(163, 437);
            this.txtFinalCourtNumber.Multiline = true;
            this.txtFinalCourtNumber.Name = "txtFinalCourtNumber";
            this.txtFinalCourtNumber.ReadOnly = true;
            this.txtFinalCourtNumber.Size = new System.Drawing.Size(195, 16);
            this.txtFinalCourtNumber.TabIndex = 211;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(56, 464);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 16);
            this.label23.TabIndex = 210;
            this.label23.Text = "Selected Courts";
            // 
            // txtFinalSelectedCourtNumbers
            // 
            this.txtFinalSelectedCourtNumbers.BackColor = System.Drawing.Color.Silver;
            this.txtFinalSelectedCourtNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFinalSelectedCourtNumbers.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalSelectedCourtNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFinalSelectedCourtNumbers.Location = new System.Drawing.Point(163, 464);
            this.txtFinalSelectedCourtNumbers.Multiline = true;
            this.txtFinalSelectedCourtNumbers.Name = "txtFinalSelectedCourtNumbers";
            this.txtFinalSelectedCourtNumbers.ReadOnly = true;
            this.txtFinalSelectedCourtNumbers.Size = new System.Drawing.Size(195, 16);
            this.txtFinalSelectedCourtNumbers.TabIndex = 209;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(50, 437);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 16);
            this.label10.TabIndex = 208;
            this.label10.Text = "Number of Courts";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(120, 612);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(34, 16);
            this.label21.TabIndex = 207;
            this.label21.Text = "Date";
            // 
            // txtFinalDate
            // 
            this.txtFinalDate.BackColor = System.Drawing.Color.Silver;
            this.txtFinalDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFinalDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFinalDate.Location = new System.Drawing.Point(163, 612);
            this.txtFinalDate.Multiline = true;
            this.txtFinalDate.Name = "txtFinalDate";
            this.txtFinalDate.ReadOnly = true;
            this.txtFinalDate.Size = new System.Drawing.Size(195, 16);
            this.txtFinalDate.TabIndex = 206;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(77, 584);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 16);
            this.label19.TabIndex = 205;
            this.label19.Text = "Ending Time";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(74, 557);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 16);
            this.label18.TabIndex = 204;
            this.label18.Text = "Starting Time";
            // 
            // txtFinalEndTime
            // 
            this.txtFinalEndTime.BackColor = System.Drawing.Color.Silver;
            this.txtFinalEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFinalEndTime.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalEndTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFinalEndTime.Location = new System.Drawing.Point(163, 585);
            this.txtFinalEndTime.Multiline = true;
            this.txtFinalEndTime.Name = "txtFinalEndTime";
            this.txtFinalEndTime.ReadOnly = true;
            this.txtFinalEndTime.Size = new System.Drawing.Size(195, 16);
            this.txtFinalEndTime.TabIndex = 203;
            // 
            // txtFinalStartTime
            // 
            this.txtFinalStartTime.BackColor = System.Drawing.Color.Silver;
            this.txtFinalStartTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFinalStartTime.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalStartTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFinalStartTime.Location = new System.Drawing.Point(163, 558);
            this.txtFinalStartTime.Multiline = true;
            this.txtFinalStartTime.Name = "txtFinalStartTime";
            this.txtFinalStartTime.ReadOnly = true;
            this.txtFinalStartTime.Size = new System.Drawing.Size(195, 16);
            this.txtFinalStartTime.TabIndex = 202;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.label8.Location = new System.Drawing.Point(28, 397);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(198, 19);
            this.label8.TabIndex = 212;
            this.label8.Text = "Updated Booking Details";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Animated = true;
            this.btnUpdate.AutoRoundedCorners = true;
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.BorderColor = System.Drawing.Color.Transparent;
            this.btnUpdate.BorderRadius = 11;
            this.btnUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUpdate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnUpdate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(286, 641);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 25);
            this.btnUpdate.TabIndex = 213;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseTransparentBackground = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::OOP2_project_EDEJER.Properties.Resources.x_icon_white_7;
            this.btnClose.Location = new System.Drawing.Point(5, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 17);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 214;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtFacilityName
            // 
            this.txtFacilityName.BackColor = System.Drawing.Color.Silver;
            this.txtFacilityName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFacilityName.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFacilityName.Location = new System.Drawing.Point(211, 149);
            this.txtFacilityName.Multiline = true;
            this.txtFacilityName.Name = "txtFacilityName";
            this.txtFacilityName.ReadOnly = true;
            this.txtFacilityName.Size = new System.Drawing.Size(150, 16);
            this.txtFacilityName.TabIndex = 215;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(157, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 216;
            this.label2.Text = "Facility";
            // 
            // btnResetCourtInfo
            // 
            this.btnResetCourtInfo.Animated = true;
            this.btnResetCourtInfo.AutoRoundedCorners = true;
            this.btnResetCourtInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnResetCourtInfo.BorderColor = System.Drawing.Color.Transparent;
            this.btnResetCourtInfo.BorderRadius = 11;
            this.btnResetCourtInfo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnResetCourtInfo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnResetCourtInfo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnResetCourtInfo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnResetCourtInfo.FillColor = System.Drawing.Color.Brown;
            this.btnResetCourtInfo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnResetCourtInfo.ForeColor = System.Drawing.Color.White;
            this.btnResetCourtInfo.Location = new System.Drawing.Point(286, 489);
            this.btnResetCourtInfo.Name = "btnResetCourtInfo";
            this.btnResetCourtInfo.Size = new System.Drawing.Size(75, 25);
            this.btnResetCourtInfo.TabIndex = 217;
            this.btnResetCourtInfo.Text = "Clear";
            this.btnResetCourtInfo.UseTransparentBackground = true;
            this.btnResetCourtInfo.Click += new System.EventHandler(this.btnResetCourtInfo_Click);
            // 
            // txtRates
            // 
            this.txtRates.BackColor = System.Drawing.Color.Silver;
            this.txtRates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRates.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRates.Location = new System.Drawing.Point(211, 210);
            this.txtRates.Multiline = true;
            this.txtRates.Name = "txtRates";
            this.txtRates.ReadOnly = true;
            this.txtRates.Size = new System.Drawing.Size(150, 16);
            this.txtRates.TabIndex = 227;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(70, 216);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(122, 16);
            this.label27.TabIndex = 226;
            this.label27.Text = "(per hour per court)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(154, 200);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(38, 16);
            this.label26.TabIndex = 225;
            this.label26.Text = "Rates";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(117, 532);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 16);
            this.label25.TabIndex = 229;
            this.label25.Text = "Price";
            // 
            // txtFinalPrice
            // 
            this.txtFinalPrice.BackColor = System.Drawing.Color.Silver;
            this.txtFinalPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFinalPrice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFinalPrice.Location = new System.Drawing.Point(163, 532);
            this.txtFinalPrice.Multiline = true;
            this.txtFinalPrice.Name = "txtFinalPrice";
            this.txtFinalPrice.ReadOnly = true;
            this.txtFinalPrice.Size = new System.Drawing.Size(195, 16);
            this.txtFinalPrice.TabIndex = 228;
            // 
            // UpdateBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(446, 683);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtFinalPrice);
            this.Controls.Add(this.txtRates);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.btnResetCourtInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFacilityName);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFinalCourtNumber);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtFinalSelectedCourtNumbers);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtFinalDate);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtFinalEndTime);
            this.Controls.Add(this.txtFinalStartTime);
            this.Controls.Add(this.cmbCourtNumber);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtNumberOfCourtsAvailable);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.cmbEndingTime);
            this.Controls.Add(this.cmbStartingTime);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateBooking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateBooking";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbCourtNumber;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNumberOfCourtsAvailable;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.ComboBox cmbEndingTime;
        private System.Windows.Forms.ComboBox cmbStartingTime;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFinalCourtNumber;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtFinalSelectedCourtNumbers;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtFinalDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtFinalEndTime;
        private System.Windows.Forms.TextBox txtFinalStartTime;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFacilityName;
        private Guna.UI2.WinForms.Guna2Button btnResetCourtInfo;
        private System.Windows.Forms.TextBox txtRates;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtFinalPrice;
    }
}