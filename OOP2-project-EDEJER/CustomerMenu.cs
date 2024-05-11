using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OOP2_project_EDEJER
{
    public partial class CustomerMenu : Form
    {
        private string username;
        private string password;
        private Timer timer;
        public CustomerMenu(string username, string password)
        {
            InitializeComponent();
            customizeDesign();
            this.username = username;
            this.password = password;

            txtUsername.Text = username;

            timer = new Timer();
            timer.Interval = 180000; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void customizeDesign()
        {
            panelBookACourt.Visible = false;
            panelMenu.Visible = false;
            panelCourtAvailability.Visible = false;
            panelAccountSettings.Visible = false;
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
        private void btnBadminton_Click_1(object sender, EventArgs e)
        {
            openForms(new BookACourtBadminton(username));
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            openForms(new Home(username));
        }

        private void btnBookACourt_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelBookACourt);
        }

        private void btnTennis_Click_1(object sender, EventArgs e)
        {
            openForms(new BookACourtTennis(username));
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMenu);
        }

        private void btnManagBookings_Click(object sender, EventArgs e)
        {
            openForms(new ManageBookings(username));
        }

        private void btnCourtAvailability_Click(object sender, EventArgs e)
        {
            showSubMenu(panelCourtAvailability);
        }

        private void btnCheckCourtAvailability_Click_1(object sender, EventArgs e)
        {
            openForms(new CheckCourtAvailability());
        }

        private void btnSearchFacility_Click_1(object sender, EventArgs e)
        {
            openForms(new SearchFacility());
        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {
            showSubMenu(panelAccountSettings);
        }

        private void btnProfile_Click_1(object sender, EventArgs e)
        {
            openForms(new AccountSettings(username, password));
        }

        private void btnFeedbackRating_Click_1(object sender, EventArgs e)
        {
            openForms(new FeedbackRating(username));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            string message = "Do you really want to log out?";
            string title = "Log-out";
            DialogResult res = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                this.Close();
                Form1 f1 = new Form1();
                f1.FormClosed += (s, args) => this.Close();
                f1.Show();

            }
            else
            {
                return;
            }
        }

        private void btnNotfication_Click(object sender, EventArgs e)
        {
            Alerts alerts = new Alerts();
            alerts.ShowAlert("Upcoming Bookings Notification", username);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckUpcomingBookings();
        }
        private void CheckUpcomingBookings()
        {
            try
            {
                // Get the current date and time
                DateTime currentTime = DateTime.Now;

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "SELECT BookedName, BookedStartingTime FROM BookedCourts WHERE Username = ? AND BookedDate = ?";

                bool alertShown = false;

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@bookedDate", DateTime.Today);

                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string bookedName = reader["BookedName"].ToString();
                            DateTime bookedStartingTime = DateTime.Parse(reader["BookedStartingTime"].ToString());

                            // Check if the starting time of the booked court is within the next 15 minutes
                            if ((bookedStartingTime - currentTime).TotalMinutes <= 30 && (bookedStartingTime - currentTime).TotalMinutes > 0)
                            {
                                if (!alertShown)
                                {
                                    string message = $"Your booking for {bookedName} is starting soon at {bookedStartingTime.ToString("HH:mm")}.";
                                    MessageBox.Show(message, "Upcoming Booking Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    alertShown = true; // Set the flag to true to prevent further alerts
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking upcoming bookings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Home Home
        {
            get => default;
            set
            {
            }
        }

        public BookACourtBadminton BookACourtBadminton
        {
            get => default;
            set
            {
            }
        }

        public BookACourtTennis BookACourtTennis
        {
            get => default;
            set
            {
            }
        }

        public Alerts Alerts
        {
            get => default;
            set
            {
            }
        }

        public CheckCourtAvailability CheckCourtAvailability
        {
            get => default;
            set
            {
            }
        }

        public SearchFacility SearchFacility
        {
            get => default;
            set
            {
            }
        }

        public ManageBookings ManageBookings
        {
            get => default;
            set
            {
            }
        }

        public FeedbackRating FeedbackRating
        {
            get => default;
            set
            {
            }
        }

        public AccountSettings AccountSettings
        {
            get => default;
            set
            {
            }
        }
    }
}

