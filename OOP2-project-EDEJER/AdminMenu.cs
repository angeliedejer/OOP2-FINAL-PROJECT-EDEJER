using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_project_EDEJER
{
    public partial class AdminMenu : Form
    {
        private string username;
        private string password;
        public AdminMenu(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;

            txtUsername.Text = username;
            customizeDesign();
        }
        private void customizeDesign()
        {

        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            openForms(new Home(username));
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private Form activeForm = null;
        private void openForms(Form Forms)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = Forms;
            Forms.TopLevel = false;
            Forms.FormBorderStyle = FormBorderStyle.None;
            Forms.Dock = DockStyle.Fill;
            panelOpenForms.Controls.Add(Forms);
            panelOpenForms.Tag = Forms;
            Forms.BringToFront();
            Forms.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            openForms(new BookingReports());
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            openForms(new ManageUsers());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            string message = "Do you really want to log out?";
            string title = "Log-out";
            DialogResult res = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                this.Hide();

                Form1 f1 = new Form1();
                f1.FormClosed += (s, args) => this.Close();
                f1.Show();

            }
            else
            {
                return;
            }
        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {
            openForms(new AccountSettings(username, password));
        }

        private void btnFAQsandFeedbacks_Click(object sender, EventArgs e)
        {
            openForms(new FeedbackAdmin());
        }

        private void btnManageBookings_Click(object sender, EventArgs e)
        {
            openForms(new AdminManageBookings());
        }

        private void btnManageFacilities_Click(object sender, EventArgs e)
        {
            openForms(new AdminManageFacility());
        }

        public AccountSettings AccountSettings
        {
            get => default;
            set
            {
            }
        }

        public AdminManageBookings AdminManageBookings
        {
            get => default;
            set
            {
            }
        }

        public ManageUsers ManageUsers
        {
            get => default;
            set
            {
            }
        }

        public BookingReports BookingReports
        {
            get => default;
            set
            {
            }
        }

        public AdminManageFacility AdminManageFacility
        {
            get => default;
            set
            {
            }
        }

        public FeedbackAdmin FeedbackAdmin
        {
            get => default;
            set
            {
            }
        }
    }
}
