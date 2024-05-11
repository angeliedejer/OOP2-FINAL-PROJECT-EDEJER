namespace OOP2_project_EDEJER
{
    partial class AdminManageBookings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.btnConnectionTest = new Guna.UI2.WinForms.Guna2Button();
            this.btnFutureBookings = new Guna.UI2.WinForms.Guna2Button();
            this.btnCurrentBookings = new Guna.UI2.WinForms.Guna2Button();
            this.btnPastBookings = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoadAll = new Guna.UI2.WinForms.Guna2Button();
            this.DataGridViewBookings = new System.Windows.Forms.DataGridView();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 62);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Manage Bookings";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.AutoRoundedCorners = true;
            this.txtSearch.BorderRadius = 9;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FillColor = System.Drawing.Color.Silver;
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Location = new System.Drawing.Point(13, 84);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.PlaceholderText = "Search";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(281, 21);
            this.txtSearch.TabIndex = 124;
            // 
            // btnSearch
            // 
            this.btnSearch.Animated = true;
            this.btnSearch.AutoRoundedCorners = true;
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BorderColor = System.Drawing.Color.Transparent;
            this.btnSearch.BorderRadius = 9;
            this.btnSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(301, 84);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 21);
            this.btnSearch.TabIndex = 123;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseTransparentBackground = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnConnectionTest
            // 
            this.btnConnectionTest.Animated = true;
            this.btnConnectionTest.AutoRoundedCorners = true;
            this.btnConnectionTest.BackColor = System.Drawing.Color.Transparent;
            this.btnConnectionTest.BorderColor = System.Drawing.Color.Transparent;
            this.btnConnectionTest.BorderRadius = 11;
            this.btnConnectionTest.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConnectionTest.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConnectionTest.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConnectionTest.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConnectionTest.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnConnectionTest.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnConnectionTest.ForeColor = System.Drawing.Color.White;
            this.btnConnectionTest.Location = new System.Drawing.Point(711, 654);
            this.btnConnectionTest.Name = "btnConnectionTest";
            this.btnConnectionTest.Size = new System.Drawing.Size(131, 25);
            this.btnConnectionTest.TabIndex = 129;
            this.btnConnectionTest.Text = "Connection Test";
            this.btnConnectionTest.UseTransparentBackground = true;
            this.btnConnectionTest.Click += new System.EventHandler(this.btnConnectionTest_Click);
            // 
            // btnFutureBookings
            // 
            this.btnFutureBookings.Animated = true;
            this.btnFutureBookings.AutoRoundedCorners = true;
            this.btnFutureBookings.BackColor = System.Drawing.Color.Transparent;
            this.btnFutureBookings.BorderColor = System.Drawing.Color.Transparent;
            this.btnFutureBookings.BorderRadius = 11;
            this.btnFutureBookings.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFutureBookings.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFutureBookings.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFutureBookings.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFutureBookings.FillColor = System.Drawing.Color.SteelBlue;
            this.btnFutureBookings.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnFutureBookings.ForeColor = System.Drawing.Color.White;
            this.btnFutureBookings.Location = new System.Drawing.Point(361, 131);
            this.btnFutureBookings.Name = "btnFutureBookings";
            this.btnFutureBookings.Size = new System.Drawing.Size(156, 25);
            this.btnFutureBookings.TabIndex = 192;
            this.btnFutureBookings.Text = "View Future Bookings";
            this.btnFutureBookings.UseTransparentBackground = true;
            this.btnFutureBookings.Click += new System.EventHandler(this.btnFutureBookings_Click);
            // 
            // btnCurrentBookings
            // 
            this.btnCurrentBookings.Animated = true;
            this.btnCurrentBookings.AutoRoundedCorners = true;
            this.btnCurrentBookings.BackColor = System.Drawing.Color.Transparent;
            this.btnCurrentBookings.BorderColor = System.Drawing.Color.Transparent;
            this.btnCurrentBookings.BorderRadius = 11;
            this.btnCurrentBookings.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCurrentBookings.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCurrentBookings.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCurrentBookings.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCurrentBookings.FillColor = System.Drawing.Color.Chocolate;
            this.btnCurrentBookings.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCurrentBookings.ForeColor = System.Drawing.Color.White;
            this.btnCurrentBookings.Location = new System.Drawing.Point(176, 131);
            this.btnCurrentBookings.Name = "btnCurrentBookings";
            this.btnCurrentBookings.Size = new System.Drawing.Size(169, 25);
            this.btnCurrentBookings.TabIndex = 191;
            this.btnCurrentBookings.Text = "View Current Bookings";
            this.btnCurrentBookings.UseTransparentBackground = true;
            this.btnCurrentBookings.Click += new System.EventHandler(this.btnCurrentBookings_Click);
            // 
            // btnPastBookings
            // 
            this.btnPastBookings.Animated = true;
            this.btnPastBookings.AutoRoundedCorners = true;
            this.btnPastBookings.BackColor = System.Drawing.Color.Transparent;
            this.btnPastBookings.BorderColor = System.Drawing.Color.Transparent;
            this.btnPastBookings.BorderRadius = 11;
            this.btnPastBookings.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPastBookings.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPastBookings.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPastBookings.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPastBookings.FillColor = System.Drawing.Color.Orange;
            this.btnPastBookings.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnPastBookings.ForeColor = System.Drawing.Color.White;
            this.btnPastBookings.Location = new System.Drawing.Point(13, 131);
            this.btnPastBookings.Name = "btnPastBookings";
            this.btnPastBookings.Size = new System.Drawing.Size(148, 25);
            this.btnPastBookings.TabIndex = 190;
            this.btnPastBookings.Text = "View Past Bookings";
            this.btnPastBookings.UseTransparentBackground = true;
            this.btnPastBookings.Click += new System.EventHandler(this.btnPastBookings_Click);
            // 
            // btnLoadAll
            // 
            this.btnLoadAll.Animated = true;
            this.btnLoadAll.AutoRoundedCorners = true;
            this.btnLoadAll.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadAll.BorderColor = System.Drawing.Color.Transparent;
            this.btnLoadAll.BorderRadius = 11;
            this.btnLoadAll.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadAll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadAll.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoadAll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoadAll.FillColor = System.Drawing.Color.DarkOliveGreen;
            this.btnLoadAll.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadAll.ForeColor = System.Drawing.Color.White;
            this.btnLoadAll.Location = new System.Drawing.Point(756, 131);
            this.btnLoadAll.Name = "btnLoadAll";
            this.btnLoadAll.Size = new System.Drawing.Size(86, 25);
            this.btnLoadAll.TabIndex = 193;
            this.btnLoadAll.Text = "Load All";
            this.btnLoadAll.UseTransparentBackground = true;
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);
            // 
            // DataGridViewBookings
            // 
            this.DataGridViewBookings.AllowUserToOrderColumns = true;
            this.DataGridViewBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewBookings.Location = new System.Drawing.Point(13, 174);
            this.DataGridViewBookings.Name = "DataGridViewBookings";
            this.DataGridViewBookings.Size = new System.Drawing.Size(834, 462);
            this.DataGridViewBookings.TabIndex = 194;
            this.DataGridViewBookings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewBookings_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.AutoRoundedCorners = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderRadius = 17;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(17, 642);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 37);
            this.btnSave.TabIndex = 195;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseTransparentBackground = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Animated = true;
            this.btnDelete.AutoRoundedCorners = true;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BorderColor = System.Drawing.Color.Transparent;
            this.btnDelete.BorderRadius = 17;
            this.btnDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDelete.FillColor = System.Drawing.Color.Brown;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(114, 642);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 37);
            this.btnDelete.TabIndex = 196;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseTransparentBackground = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // AdminManageBookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(858, 690);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.DataGridViewBookings);
            this.Controls.Add(this.btnLoadAll);
            this.Controls.Add(this.btnFutureBookings);
            this.Controls.Add(this.btnCurrentBookings);
            this.Controls.Add(this.btnPastBookings);
            this.Controls.Add(this.btnConnectionTest);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.panel1);
            this.Name = "AdminManageBookings";
            this.Text = "AdminManageBookings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewBookings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2Button btnConnectionTest;
        private Guna.UI2.WinForms.Guna2Button btnFutureBookings;
        private Guna.UI2.WinForms.Guna2Button btnCurrentBookings;
        private Guna.UI2.WinForms.Guna2Button btnPastBookings;
        private Guna.UI2.WinForms.Guna2Button btnLoadAll;
        private System.Windows.Forms.DataGridView DataGridViewBookings;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
    }
}