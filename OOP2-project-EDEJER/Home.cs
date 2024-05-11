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

namespace OOP2_project_EDEJER
{
    public partial class Home : Form
    {
        private string username;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
        public Home(string username)
        {
            InitializeComponent();
            guna2VScrollBar1.BindingContainer = DataGridViewBookings;
            guna2VScrollBar1.AutoScroll = true;
            this.username = username;

            DateTime currentDate = DateTime.Today;

            // Convert the current date to a string with the desired format
            string formattedDate = currentDate.ToString("MMMM dd, yyyy");

            // Set the formatted date string to the label
            lblDate.Text = formattedDate;

            txtUsername.Text = username;
            LoadTotals();
            LoadUserBookings();
        }
        private void LoadTotals()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string queryBookings = "SELECT COUNT(*) FROM BookedCourts";
                    using (OleDbCommand commandBookings = new OleDbCommand(queryBookings, connection))
                    {
                        int totalBookings = (int)commandBookings.ExecuteScalar();
                        txtTotalBookings.Text = totalBookings.ToString();
                    }
                    string querySales = "SELECT SUM(TotalPrice) FROM BookedCourts";
                    using (OleDbCommand commandSales = new OleDbCommand(querySales, connection))
                    {
                        object totalSalesObj = commandSales.ExecuteScalar();
                        if (totalSalesObj != DBNull.Value)
                        {
                            decimal totalSales = Convert.ToDecimal(totalSalesObj);
                            txtTotalSales.Text = totalSales.ToString();
                        }
                        else
                        {
                            txtTotalSales.Text = "0";
                        }
                    }
                    string queryCustomers = "SELECT COUNT(*) FROM Users WHERE UserType = 'Customer'";
                    using (OleDbCommand commandCustomers = new OleDbCommand(queryCustomers, connection))
                    {
                        int totalCustomers = (int)commandCustomers.ExecuteScalar();
                        txtTotalCustomers.Text = totalCustomers.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading totals: " + ex.Message);
            }
        }
        private void LoadUserBookings()
        {
            try
            {
                DateTime currentDate = DateTime.Today;

                string query = "SELECT BookedDate, BookedStartingTime, SportType, BookedName, SelectedCourtNumbers " +
                               "FROM BookedCourts " +
                               "WHERE Username = ? AND BookedDate = ?";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@bookedDate", currentDate);

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridViewBookings.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user bookings: " + ex.Message);
            }
        }
    }
}
