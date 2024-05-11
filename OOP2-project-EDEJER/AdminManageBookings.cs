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
    public partial class AdminManageBookings : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
        public AdminManageBookings()
        {
            InitializeComponent();
            DataGridViewBookings.AllowUserToAddRows = true;
            DataGridViewBookings.CellClick += DataGridViewBookings_CellContentClick;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LoadAllBookings();
        }
        private void LoadAllBookings()
        {
            try
            {
                string query = "SELECT BookingID, Username, SportType, BookedName, BookedAddress, BookedContactNumber, BookedDate, BookedStartingTime, BookedEndingTime, BookedNumberOfCourts, SelectedCourtNumbers, TotalPrice " +
                               "FROM BookedCourts";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DataGridViewBookings.DataSource = dataTable;

                        DataGridViewBookings.Columns["BookingID"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user bookings: " + ex.Message);
            }
        }

        private void btnPastBookings_Click(object sender, EventArgs e)
        {
            DisplayBookingsByDate("Past");
        }

        private void btnCurrentBookings_Click(object sender, EventArgs e)
        {
            DisplayBookingsByDate("Current");
        }

        private void btnFutureBookings_Click(object sender, EventArgs e)
        {
            DisplayBookingsByDate("Future");
        }
        private void DisplayBookingsByDate(string dateType)
        {
            //DataGridViewBookings.Rows.Clear();

            DateTime currentDate = DateTime.Today;
            string query = "";

            switch (dateType)
            {
                case "Past":
                    query = "SELECT BookingID, Username, SportType, BookedName, BookedAddress, BookedContactNumber, BookedDate, BookedStartingTime, BookedEndingTime, BookedNumberOfCourts, SelectedCourtNumbers, TotalPrice " +
                            "FROM BookedCourts " +
                            "WHERE BookedDate < ?";
                    break;
                case "Current":
                    query = "SELECT BookingID, Username, SportType, BookedName, BookedAddress, BookedContactNumber, BookedDate, BookedStartingTime, BookedEndingTime, BookedNumberOfCourts, SelectedCourtNumbers, TotalPrice " +
                            "FROM BookedCourts " +
                            "WHERE BookedDate = ?";
                    break;
                case "Future":
                    query = "SELECT BookingID, Username, SportType, BookedName, BookedAddress, BookedContactNumber, BookedDate, BookedStartingTime, BookedEndingTime, BookedNumberOfCourts, SelectedCourtNumbers, TotalPrice " +
                            "FROM BookedCourts " +
                            "WHERE BookedDate > ?";
                    break;
            }

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookedDate", currentDate);

                    connection.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridViewBookings.DataSource = dataTable;

                        DataGridViewBookings.Columns["BookingID"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching booked courts' information: " + ex.Message);
            }
        }

        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Connection successful!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed. Error message: " + ex.Message, "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Get the changes from the DataGridView
                    DataTable changes = ((DataTable)DataGridViewBookings.DataSource).GetChanges();

                    if (changes != null)
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM BookedCourts", connection))
                        {
                            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            adapter.Update(changes);
                            ((DataTable)DataGridViewBookings.DataSource).AcceptChanges();
                        }
                    }

                    MessageBox.Show("Changes saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllBookings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataGridViewBookings.SelectedRows.Count > 0)
            {
                // Get the BookingID of the selected row
                int bookingID = Convert.ToInt32(DataGridViewBookings.SelectedRows[0].Cells["BookingID"].Value);

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        // Construct the DELETE query
                        string deleteQuery = "DELETE FROM BookedCourts WHERE BookingID = ?";
                        using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@bookingID", bookingID);

                            // Execute the DELETE query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Booking deleted successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Refresh the DataGridView
                                LoadAllBookings();
                            }
                            else
                            {
                                MessageBox.Show("No booking found with the provided BookingID.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            // Check if the search keyword is empty
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a search keyword.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "SELECT BookingID, Username, SportType, BookedName, BookedAddress, BookedContactNumber, BookedNumberOfCourts, SelectedCourtNumbers, BookedStartingTime, BookedEndingTime, BookedDate FROM BookedCourts WHERE SportType LIKE ? OR BookedName LIKE ? OR BookedAddress LIKE ? OR Username LIKE ?";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@sportType", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@bookedName", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@bookedAddress", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@username", "%" + keyword + "%");

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DataGridViewBookings.Columns.Clear();

                        DataGridViewBookings.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching booked courts: " + ex.Message, "Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
