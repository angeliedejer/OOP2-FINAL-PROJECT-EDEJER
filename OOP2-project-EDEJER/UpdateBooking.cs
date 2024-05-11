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
    public partial class UpdateBooking : Form
    {
        private int bookingID;
        private string username;
        private string facilityName;
        private string selectedCourts;
        private string numberOfCourts;
        private string startingTime;
        private string endingTime;
        private DateTime date;
        private string totalPrice;
        public UpdateBooking(int bookingID, string username, string facilityName, string selectedCourts, string numberOfCourts, string startingTime, string endingTime, DateTime date, string totalPrice)
        {
            InitializeComponent();
            this.bookingID = bookingID;
            this.username = username;
            this.facilityName = facilityName;
            this.selectedCourts = selectedCourts;
            this.startingTime = startingTime;
            this.endingTime = endingTime;
            this.date = date;
            this.totalPrice = totalPrice;

            txtFacilityName.Text = facilityName;
            txtFinalSelectedCourtNumbers.Text = selectedCourts;
            txtFinalCourtNumber.Text = numberOfCourts;
            txtFinalStartTime.Text = startingTime;
            txtFinalEndTime.Text = endingTime;
            datePicker.Value = date;
            txtFinalPrice.Text = totalPrice;

            FacilityDetails facilityDetails = GetFacilityDetails(facilityName);

            if (facilityDetails != null)
            {
                txtRates.Text = GetRates(facilityName);
                txtNumberOfCourtsAvailable.Text = facilityDetails.NumberOfCourts.ToString();

                PopulateTotalCourtNumbersComboBox(facilityDetails.NumberOfCourts);
                PopulateTimeComboBoxes(facilityDetails.OpeningTime, facilityDetails.ClosingTime);
            }
        }
        private string GetRates(string facilityName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT RatesPerHour FROM Courts WHERE Name = ?";
            string rates = "";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@courtName", facilityName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        rates = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching contact number: " + ex.Message);
            }

            return rates;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void PopulateTotalCourtNumbersComboBox(int numberOfCourts)
        {
            cmbCourtNumber.Items.Clear();
            for (int i = 1; i <= numberOfCourts; i++)
            {
                cmbCourtNumber.Items.Add($"Court {i}");
            }
        }


        private void PopulateTimeComboBoxes(TimeSpan openingTime, TimeSpan closingTime)
        {
            cmbStartingTime.Items.Clear();
            cmbEndingTime.Items.Clear();

            TimeSpan currentTime = openingTime;
            while (currentTime <= closingTime)
            {
                cmbStartingTime.Items.Add(currentTime.ToString(@"hh\:mm"));
                cmbEndingTime.Items.Add(currentTime.ToString(@"hh\:mm"));
                currentTime = currentTime.Add(TimeSpan.FromMinutes(60));
            }
        }

        private FacilityDetails GetFacilityDetails(string facilityName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT TotalNumberOfCourts, OpeningTime, ClosingTime FROM Courts WHERE Name = ?";
            FacilityDetails facilityDetails = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@facilityName", facilityName);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int numberOfCourts = reader.GetInt32(0);
                            string openingTimeStr = reader.GetString(1);
                            string closingTimeStr = reader.GetString(2);

                            DateTime openingDateTime = DateTime.ParseExact(openingTimeStr, "h:mm tt", CultureInfo.InvariantCulture);
                            DateTime closingDateTime = DateTime.ParseExact(closingTimeStr, "h:mm tt", CultureInfo.InvariantCulture);

                            TimeSpan openingTime = openingDateTime.TimeOfDay;
                            TimeSpan closingTime = closingDateTime.TimeOfDay;

                            facilityDetails = new FacilityDetails(numberOfCourts, openingTime, closingTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching facility details: " + ex.Message);
            }

            return facilityDetails;
        }

        private class FacilityDetails
        {
            public int NumberOfCourts { get; }
            public TimeSpan OpeningTime { get; }
            public TimeSpan ClosingTime { get; }

            public FacilityDetails(int numberOfCourts, TimeSpan openingTime, TimeSpan closingTime)
            {
                NumberOfCourts = numberOfCourts;
                OpeningTime = openingTime;
                ClosingTime = closingTime;
            }
        }
        private void CalculateAndDisplayTotalPrice()
        {
            if (!string.IsNullOrEmpty(txtFinalCourtNumber.Text) &&
                !string.IsNullOrEmpty(txtFinalStartTime.Text) &&
                !string.IsNullOrEmpty(txtFinalEndTime.Text))
            {
                int numberOfCourts = Convert.ToInt32(txtFinalCourtNumber.Text);
                TimeSpan startTime = TimeSpan.Parse(txtFinalStartTime.Text);
                TimeSpan endTime = TimeSpan.Parse(txtFinalEndTime.Text);

                double ratesPerHour = GetRatesPerHourFromDatabase();

                if (ratesPerHour > 0)
                {
                    double totalHours = (endTime - startTime).TotalHours;

                    double totalPrice = totalHours * ratesPerHour * numberOfCourts;

                    txtFinalPrice.Text = totalPrice.ToString("0.00");
                }
                else
                {
                    MessageBox.Show("Error: Rates per hour not found.");
                    txtFinalPrice.Text = "";
                }
            }
            else
            {
                txtFinalPrice.Text = "";
            }
        }

        private double GetRatesPerHourFromDatabase()
        {
            string courtName = txtFacilityName.Text;
            double ratesPerHour = 0;

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT RatesPerHour FROM Courts WHERE Name = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@courtName", courtName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        ratesPerHour = Convert.ToDouble(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching rates per hour: " + ex.Message);
            }

            return ratesPerHour;
        }
        private void cmbCourtNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCourtNumber.SelectedItem != null)
            {
                string selectedCourtNumber = cmbCourtNumber.SelectedItem.ToString();

                if (!txtFinalSelectedCourtNumbers.Text.Contains(selectedCourtNumber))
                {
                    if (!string.IsNullOrEmpty(txtFinalSelectedCourtNumbers.Text))
                    {
                        txtFinalSelectedCourtNumbers.Text += ", " + selectedCourtNumber;
                    }
                    else
                    {
                        txtFinalSelectedCourtNumbers.Text = selectedCourtNumber;
                    }
                    if (!int.TryParse(txtFinalCourtNumber.Text, out int selectedCourtsCount))
                    {
                        selectedCourtsCount = 0;
                    }

                    selectedCourtsCount++;

                    txtFinalCourtNumber.Text = selectedCourtsCount.ToString();

                    CalculateAndDisplayTotalPrice();
                }
                //cmbCourtNumber.Items.Remove(selectedCourtNumber);
            }
            
        }

        private void cmbStartingTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStartingTime = cmbStartingTime.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedStartingTime))
            {
                txtFinalStartTime.Text = selectedStartingTime;
                CalculateAndDisplayTotalPrice();
            }
        }

        private void cmbEndingTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEndingTime = cmbEndingTime.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedEndingTime))
            {
                txtFinalEndTime.Text = selectedEndingTime;
                CalculateAndDisplayTotalPrice();
            }
        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = datePicker.Value;

            if (selectedDate.Date < DateTime.Today)
            {
                MessageBox.Show("You cannot book courts on past dates. You can only book current or future dates.");
                datePicker.Value = DateTime.Today; // Reset date picker to today's date
            }
            else
            {
                txtFinalDate.Text = selectedDate.ToShortDateString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string updatedSelectedCourts = txtFinalSelectedCourtNumbers.Text;
            int updatedNumberOfCourts = int.Parse(txtFinalCourtNumber.Text);
            string updatedStartingTime = txtFinalStartTime.Text;
            string updatedEndingTime = txtFinalEndTime.Text;
            DateTime updatedDate = datePicker.Value;

            if ((updatedSelectedCourts != null && updatedSelectedCourts != selectedCourts) ||
                (numberOfCourts != null && updatedNumberOfCourts != int.Parse(numberOfCourts)) || 
                (updatedStartingTime != null && updatedStartingTime != startingTime) ||
                (updatedEndingTime != null && updatedEndingTime != endingTime) ||
                updatedDate != date)
            {
                bool isTimeSlotAvailable = IsTimeSlotAvailable(updatedDate, updatedStartingTime, updatedEndingTime, updatedSelectedCourts);

                if (isTimeSlotAvailable)
                {
                    UpdateBookingRecord(updatedSelectedCourts, updatedNumberOfCourts, updatedStartingTime, updatedEndingTime, updatedDate);
                }
                else
                {
                    MessageBox.Show("The selected time slot is not available. Please choose another time slot.");
                }
            }
            else
            {
                MessageBox.Show("No changes detected in booking details.");
            }
        }
        private bool IsTimeSlotAvailable(DateTime date, string startTime, string endTime, string selectedCourts)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT * FROM BookedCourts WHERE BookedName = ? AND BookedDate = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@bookedName", facilityName);
                    command.Parameters.AddWithValue("@bookedDate", date.Date);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookingID = Convert.ToInt32(reader["BookingID"]);
                            string bookedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            TimeSpan bookedStartTime = TimeSpan.Parse(reader["BookedStartingTime"].ToString());
                            TimeSpan bookedEndTime = TimeSpan.Parse(reader["BookedEndingTime"].ToString());
                            int bookedNumberOfCourts = Convert.ToInt32(reader["BookedNumberOfCourts"]);

                            if (bookingID != this.bookingID &&
                                ((bookedStartTime < TimeSpan.Parse(endTime) && bookedEndTime > TimeSpan.Parse(startTime)) ||
                                (TimeSpan.Parse(startTime) < bookedEndTime && TimeSpan.Parse(endTime) > bookedStartTime)))
                            {
                                if (IsCourtNumberBooked(selectedCourts, bookedCourtNumbers))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking time slot availability: " + ex.Message);
                return false;
            }

            return true;
        }

        private void UpdateBookingRecord(string updatedSelectedCourts, int updatedNumberOfCourts, string updatedStartingTime, string updatedEndingTime, DateTime updatedDate)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "UPDATE BookedCourts SET BookedNumberOfCourts = ?, SelectedCourtNumbers = ?, BookedStartingTime = ?, BookedEndingTime = ?, BookedDate = ? WHERE BookingID = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@numberOfCourts", updatedNumberOfCourts);
                    command.Parameters.AddWithValue("@selectedCourtNumbers", updatedSelectedCourts);
                    command.Parameters.AddWithValue("@startingTime", updatedStartingTime);
                    command.Parameters.AddWithValue("@endingTime", updatedEndingTime);
                    command.Parameters.AddWithValue("@date", updatedDate);
                    command.Parameters.AddWithValue("@bookingID", bookingID);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Booking updated successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No records updated. Please check the input values.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating booking: " + ex.Message);
            }
        }

        private bool IsCourtNumberBooked(string selectedCourtNumbers, string bookedCourtNumbers)
        {
            string[] selectedCourts = selectedCourtNumbers.Split(',');
            string[] bookedCourts = bookedCourtNumbers.Split(',');

            foreach (string selectedCourt in selectedCourts)
            {
                foreach (string bookedCourt in bookedCourts)
                {
                    if (selectedCourt.Trim() == bookedCourt.Trim())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void btnResetCourtInfo_Click(object sender, EventArgs e)
        {
            txtFinalCourtNumber.Clear();
            txtFinalSelectedCourtNumbers.Clear();
        }
    }
}
