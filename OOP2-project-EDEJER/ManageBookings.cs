using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OOP2_project_EDEJER
{
    public partial class ManageBookings : Form
    {
        private string username;
        public ManageBookings(string username)
        {
            InitializeComponent();
            this.username = username;

            InitializeDataGridView();
            DisplayBookedCourtsForUser(username);
        }

        public UpdateBooking UpdateBooking
        {
            get => default;
            set
            {
            }
        }

        private void InitializeDataGridView()
        {
            dataGridViewBookedCourts.Columns.Add("BookingID", "Booking ID");
            dataGridViewBookedCourts.Columns.Add("SportType", "Sport Type");
            dataGridViewBookedCourts.Columns.Add("BookedName", "Facility");
            dataGridViewBookedCourts.Columns.Add("BookedAddress", "Address");
            dataGridViewBookedCourts.Columns.Add("BookedContactNumber", "Contact Number");
            dataGridViewBookedCourts.Columns.Add("BookedNumberOfCourts", "Booked Number of Courts");
            dataGridViewBookedCourts.Columns.Add("SelectedCourtNumbers", "Selected Court Numbers");
            dataGridViewBookedCourts.Columns.Add("BookedStartingTime", "Booked Starting Time");
            dataGridViewBookedCourts.Columns.Add("BookedEndingTime", "Booked Ending Time");
            dataGridViewBookedCourts.Columns.Add("BookedDate", "Booked Date");
            dataGridViewBookedCourts.Columns.Add("TotalPrice", "Total Price");
        }
        private void DisplayBookedCourtsForUser(string username)
        {
            dataGridViewBookedCourts.Rows.Clear();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT BookingID, * FROM BookedCourts WHERE Username = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookingID = Convert.ToInt32(reader["BookingID"]);

                            string sportType = reader["SportType"].ToString();
                            string bookedName = reader["BookedName"].ToString();
                            string bookedAddress = reader["BookedAddress"].ToString();
                            string bookedContactNumber = reader["BookedContactNumber"].ToString();
                            int bookedNumberOfCourts = Convert.ToInt32(reader["BookedNumberOfCourts"]);
                            string selectedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            TimeSpan bookedStartingTime = TimeSpan.Parse(reader["BookedStartingTime"].ToString());
                            TimeSpan bookedEndingTime = TimeSpan.Parse(reader["BookedEndingTime"].ToString());
                            DateTime bookedDate = Convert.ToDateTime(reader["BookedDate"]);
                            double totalPrice;

                            if (reader["TotalPrice"] != DBNull.Value)
                            {
                                totalPrice = Convert.ToDouble(reader["TotalPrice"]);
                            }
                            else
                            {
                                totalPrice = 0;
                            }

                            dataGridViewBookedCourts.Rows.Add(bookingID, sportType, bookedName, bookedAddress, bookedContactNumber, bookedNumberOfCourts, selectedCourtNumbers, bookedStartingTime, bookedEndingTime, bookedDate, totalPrice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching booked courts' information: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewBookedCourts.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewBookedCourts.SelectedRows[0];

                // Get the booked date from the selected row
                DateTime bookedDate = Convert.ToDateTime(selectedRow.Cells["BookedDate"].Value);

                // Check if the booked date is in the past
                if (bookedDate.Date < DateTime.Today)
                {
                    MessageBox.Show("You cannot update bookings for past dates.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int bookingID = Convert.ToInt32(selectedRow.Cells["BookingID"].Value);
                string facilityName = selectedRow.Cells["BookedName"].Value.ToString();
                string selectedCourts = selectedRow.Cells["SelectedCourtNumbers"].Value.ToString();
                string numberOfCourts = selectedRow.Cells["BookedNumberOfCourts"].Value.ToString();
                string startingTime = selectedRow.Cells["BookedStartingTime"].Value.ToString();
                string endingTime = selectedRow.Cells["BookedEndingTime"].Value.ToString();
                DateTime date = Convert.ToDateTime(selectedRow.Cells["BookedDate"].Value);
                string rates = selectedRow.Cells["TotalPrice"].Value.ToString();

                UpdateBooking updateForm = new UpdateBooking(bookingID, username, facilityName, selectedCourts, numberOfCourts, startingTime, endingTime, date, rates);
                updateForm.ShowDialog();

                DisplayBookedCourtsForUser(username);
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void btnCLearTXT_Click(object sender, EventArgs e)
        {
            txtSportType.Clear();
            txtFacilityName.Clear();
            txtAddress.Clear();
            txtContactNumber.Clear();
            txtSelectedCourts.Clear();
            txtBookedNumberOfCourts.Clear();
            txtStartingTime.Clear();
            txtEndingTime.Clear();
            txtPrice.Clear();
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            if (dataGridViewBookedCourts.SelectedRows.Count > 0)
            {
                string message = "Are you sure you want to cancel this booking?";
                string title = "Cancel Booking";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int bookingID = Convert.ToInt32(dataGridViewBookedCourts.SelectedRows[0].Cells["BookingID"].Value);

                    CancelBooking(bookingID);

                    DisplayBookedCourtsForUser(username);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to cancel.", "Cancel Booking", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void CancelBooking(int bookingID)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "DELETE FROM BookedCourts WHERE BookingID = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@bookingID", bookingID);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Booking cancelled successfully!", "Cancel Booking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to cancel booking. Please try again.", "Cancel Booking", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling booking: " + ex.Message, "Cancel Booking", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string query = "SELECT BookingID, SportType, BookedName, BookedAddress, BookedContactNumber, BookedNumberOfCourts, SelectedCourtNumbers, BookedStartingTime, BookedEndingTime, BookedDate FROM BookedCourts WHERE SportType LIKE ? OR BookedName LIKE ? OR BookedAddress LIKE ?";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@sportType", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@bookedName", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@bookedAddress", "%" + keyword + "%");

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridViewBookedCourts.Columns.Clear();

                        dataGridViewBookedCourts.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching booked courts: " + ex.Message, "Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dataGridViewBookedCourts.Rows.Clear();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";

            DateTime currentDate = DateTime.Today;

            string query = "";
            switch (dateType)
            {
                case "Past":
                    query = "SELECT BookingID, * FROM BookedCourts WHERE Username = ? AND BookedDate < ?";
                    break;
                case "Current":
                    query = "SELECT BookingID, * FROM BookedCourts WHERE Username = ? AND BookedDate = ?";
                    break;
                case "Future":
                    query = "SELECT BookingID, * FROM BookedCourts WHERE Username = ? AND BookedDate > ?";
                    break;
            }

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@bookedDate", currentDate);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookingID = Convert.ToInt32(reader["BookingID"]);

                            string sportType = reader["SportType"].ToString();
                            string bookedName = reader["BookedName"].ToString();
                            string bookedAddress = reader["BookedAddress"].ToString();
                            string bookedContactNumber = reader["BookedContactNumber"].ToString();
                            int bookedNumberOfCourts = Convert.ToInt32(reader["BookedNumberOfCourts"]);
                            string selectedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            TimeSpan bookedStartingTime = TimeSpan.Parse(reader["BookedStartingTime"].ToString());
                            TimeSpan bookedEndingTime = TimeSpan.Parse(reader["BookedEndingTime"].ToString());
                            DateTime bookedDate = Convert.ToDateTime(reader["BookedDate"]);
                            double totalPrice;

                            if (reader["TotalPrice"] != DBNull.Value)
                            {
                                totalPrice = Convert.ToDouble(reader["TotalPrice"]);
                            }
                            else
                            {
                                totalPrice = 0;
                            }

                            dataGridViewBookedCourts.Rows.Add(bookingID, sportType, bookedName, bookedAddress, bookedContactNumber, bookedNumberOfCourts, selectedCourtNumbers, bookedStartingTime, bookedEndingTime, bookedDate, totalPrice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching booked courts' information: " + ex.Message);
            }
        }

        private void dataGridViewBookedCourts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewBookedCourts.Rows.Count)
            {
                DataGridViewRow row = dataGridViewBookedCourts.Rows[e.RowIndex];

                txtSportType.Text = row.Cells["SportType"].Value.ToString();
                txtFacilityName.Text = row.Cells["BookedName"].Value.ToString();
                txtAddress.Text = row.Cells["BookedAddress"].Value.ToString();
                txtContactNumber.Text = row.Cells["BookedContactNumber"].Value == DBNull.Value ? string.Empty : row.Cells["BookedContactNumber"].Value.ToString();
                txtSelectedCourts.Text = row.Cells["SelectedCourtNumbers"].Value.ToString();
                txtBookedNumberOfCourts.Text = row.Cells["BookedNumberOfCourts"].Value.ToString();
                txtStartingTime.Text = row.Cells["BookedStartingTime"].Value.ToString();
                txtEndingTime.Text = row.Cells["BookedEndingTime"].Value.ToString();
                datePicker.Value = Convert.ToDateTime(row.Cells["BookedDate"].Value);
                txtPrice.Text = row.Cells["TotalPrice"].Value.ToString();
            }
        }
    }
}
