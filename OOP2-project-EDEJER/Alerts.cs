using OOP2_project_EDEJER.Properties;
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
using System.Windows.Forms.Design;

namespace OOP2_project_EDEJER
{
    public partial class Alerts : Form
    {
        private const int ALERT_DISPLAY_TIME = 5000;
        private Timer timer;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
        private string username;
        public Alerts()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = ALERT_DISPLAY_TIME;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Close();
        }
        public void ShowAlert(string message, string username)
        {
            this.username = username;

            int x = Screen.PrimaryScreen.WorkingArea.Width - Width - 5;
            int y = Screen.PrimaryScreen.WorkingArea.Height - Height - 5;
            Location = new Point(x, y);

            int upcomingBookingsCount = GetUpcomingBookingsCount(username);
            notificationCount.Text = upcomingBookingsCount.ToString();

            lblMessage.Text = $"You have upcoming bookings.";

            Show();
            timer.Start();
        }
        private int GetUpcomingBookingsCount(string username)
        {
            int upcomingBookingsCount = 0;

            DateTime currentDateTime = DateTime.Now;

            string query = "SELECT COUNT(*) FROM BookedCourts WHERE Username = @username AND BookedDate = @currentDate AND BookedStartingTime > @currentTime";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@currentDate", currentDateTime.Date);
                command.Parameters.AddWithValue("@currentTime", currentDateTime.ToString("HH:mm"));

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    upcomingBookingsCount = Convert.ToInt32(result);
                }
            }

            return upcomingBookingsCount;
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
