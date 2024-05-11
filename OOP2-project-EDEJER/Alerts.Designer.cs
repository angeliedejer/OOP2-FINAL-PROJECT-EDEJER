namespace OOP2_project_EDEJER
{
    partial class Alerts
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
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnViewDetails = new System.Windows.Forms.PictureBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.notificationCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Interval = 5000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(84, 26);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(54, 18);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.Text = "label1";
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Image = global::OOP2_project_EDEJER.Properties.Resources.notifications_icon_1791x2048_rk2vz974;
            this.btnViewDetails.Location = new System.Drawing.Point(12, 17);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(49, 38);
            this.btnViewDetails.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnViewDetails.TabIndex = 14;
            this.btnViewDetails.TabStop = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 25;
            this.guna2Elipse1.TargetControl = this;
            // 
            // notificationCount
            // 
            this.notificationCount.AutoSize = true;
            this.notificationCount.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificationCount.ForeColor = System.Drawing.Color.Brown;
            this.notificationCount.Location = new System.Drawing.Point(48, 10);
            this.notificationCount.Name = "notificationCount";
            this.notificationCount.Size = new System.Drawing.Size(24, 25);
            this.notificationCount.TabIndex = 15;
            this.notificationCount.Text = "0";
            // 
            // Alerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ClientSize = new System.Drawing.Size(344, 71);
            this.Controls.Add(this.notificationCount);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Alerts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Alerts";
            ((System.ComponentModel.ISupportInitialize)(this.btnViewDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox btnViewDetails;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label notificationCount;
    }
}