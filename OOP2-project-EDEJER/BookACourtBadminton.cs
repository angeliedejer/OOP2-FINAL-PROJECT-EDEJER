using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;
using Guna.UI2.WinForms;
using static OOP2_project_EDEJER.BookACourtBadminton;
using GMapMarkerForms = GMap.NET.WindowsForms.GMapMarker;
using GMapMarkerPresentation = GMap.NET.WindowsPresentation.GMapMarker;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace OOP2_project_EDEJER
{
    public partial class BookACourtBadminton : Form
    {
        private string selectedMunicipality;
        private GMapOverlay markersOverlay;
        private string username;
        public BookACourtBadminton(string username)
        {
            InitializeComponent();
            this.username = username;

            guna2HScrollBar1.BindingContainer = dataGridViewBookedCourts;
            guna2HScrollBar1.AutoScroll = true;
            guna2VScrollBar1.BindingContainer = dataGridViewBookedCourts;
            guna2VScrollBar1.AutoScroll = true;

            gmapControl.MapProvider = GMapProviders.OpenStreetMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmapControl.Position = new PointLatLng(10.3157, 123.8854);
            gmapControl.MinZoom = 5;
            gmapControl.MaxZoom = 20;
            gmapControl.Zoom = 13;
            gmapControl.ShowCenter = false;
            gmapControl.DragButton = MouseButtons.Left;

            markersOverlay = new GMapOverlay("markers");
            gmapControl.Overlays.Add(markersOverlay);

            cmbCourtNumber.SelectedIndexChanged += cmbCourtNumber_SelectedIndexChanged;

            gmapControl.OnMarkerClick += GmapControl_OnMarkerClick;

            bookingPanel.Visible = false;
        }
        private void BookACourt_Load(object sender, EventArgs e)
        {
            
        }
        private void LoadMap()
        {
            double latitude = 0;
            double longitude = 0;

            switch (selectedMunicipality)
            {
                case "Lapu-Lapu City":
                    latitude = 10.3168;
                    longitude = 123.9650;
                    break;
                case "Mandaue City":
                    latitude = 10.3321;
                    longitude = 123.9357;
                    break;
                case "Cebu City":
                    latitude = 10.3157;
                    longitude = 123.8854;
                    break;
                case "Talisay City":
                    latitude = 10.2524;
                    longitude = 123.8392;
                    break;
                case "Minglanilla City":
                    latitude = 10.2433;
                    longitude = 123.7890;
                    break;
                case "Naga City":
                    latitude = 10.2085;
                    longitude = 123.7573;
                    break;
                case "Consolacion":
                    latitude = 10.367341;
                    longitude = 123.965547;
                    break;
            }
            gmapControl.Position = new PointLatLng(latitude, longitude);
            MarkBadmintonCourts(selectedMunicipality);

        }
        private void MarkBadmintonCourts(string municipality)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT Name, CourtAddress, Latitude, Longitude, Availability FROM Courts WHERE Municipality = ? AND SportType = 'Badminton'";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@municipality", municipality);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            string address = reader.GetString(1);
                            string latstr = reader.GetString(2);
                            string lngstr = reader.GetString(3);
                            bool availability = reader.GetBoolean(4);

                            double lat, lng;
                            if (double.TryParse(latstr, out lat) && double.TryParse(lngstr, out lng))
                            {
                                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue_dot);
                                marker.ToolTipText = $"Name: {name}\nAddress: {address}\nStatus: {(availability ? "Open" : "Closed")}";
                                markersOverlay.Markers.Add(marker);
                            }
                            else
                            {
                                MessageBox.Show("Invalid latitude or longitude format.");
                            }
                        }
                    }
                }
                gmapControl.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading badminton courts: " + ex.Message);
            }
        }
        private void GmapControl_OnMarkerClick(GMapMarkerForms item, MouseEventArgs e)
        {
            string[] info = item.ToolTipText.Split('\n');
            string name = info[0].Split(':')[1].Trim();
            string address = info[1].Split(':')[1].Trim();
            string status = info[2].Split(':')[1].Trim();

            txtStatus.Text = status;

            txtCourtName.Text = name;
            txtFinalCourtName.Text = name;

            txtCourtAddress.Text = address;
            txtFinalAddress.Text = address;

            string contactNumber = GetContactNumberFromDatabase(name);
            txtContactNumber.Text = contactNumber;
            txtFinalContactNumber.Text = contactNumber;

            string imagePath = GetImagePathFromDatabase(name);

            if (!string.IsNullOrEmpty(imagePath))
            {
                pictureBoxCourtImage.Image = System.Drawing.Image.FromFile(imagePath);
                pictureBoxCourtImage.Visible = true;

                pictureBoxFacilityImage.Image = System.Drawing.Image.FromFile(imagePath);
                pictureBoxFacilityImage.Visible = true;

            }
            else
            {
                pictureBoxCourtImage.Visible = false;
                pictureBoxFacilityImage.Visible = false;
            }
            
        }
        private string GetImagePathFromDatabase(string courtName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT ImagePath FROM Courts WHERE Name = ?";
            string imagePath = "";

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
                        imagePath = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching image path: " + ex.Message);
            }

            return imagePath;
        }
        private string GetContactNumberFromDatabase(string courtName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT ContactNumber FROM Courts WHERE Name = ?";
            string contactNumber = "";

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
                        contactNumber = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching contact number: " + ex.Message);
            }

            return contactNumber;
        }
        private double GetRates(string courtName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT RatesPerHour FROM Courts WHERE Name = ?";
            double rates = 0;

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
                        rates = Convert.ToDouble(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching contact number: " + ex.Message);
            }

            return rates;
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            selectedMunicipality = cmbMunicipality.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedMunicipality))
            {
                MessageBox.Show("Please select a municipality.");
                return;
            }

            LoadMap();
        }

        private void gmapControl_Load(object sender, EventArgs e)
        {

        }
        private bool IsFacilityAvailable(string facilityName)
        {
            bool isAvailable = true;

            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "SELECT Availability FROM Courts WHERE Name = ?";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@facilityName", facilityName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Check the value of the Availability field
                        // If the facility is marked as unavailable, set isAvailable to false
                        isAvailable = (bool)result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking facility availability: " + ex.Message);
                // Set isAvailable to true by default in case of any error
                isAvailable = true;
            }

            return isAvailable;
        }
        private void btnProceedBookingDetails_Click(object sender, EventArgs e)
        {
            string facilityName = txtCourtName.Text;
            bool isAvailable = IsFacilityAvailable(facilityName);

            if (!isAvailable)
            {
                MessageBox.Show("This facility is currently closed. You are unable to book at this time.", "Facility Closed", MessageBoxButtons.OK);
                return; // Exit the method without proceeding with booking
            }
            pictureBoxCourtImage.Visible = false;
            bookingPanel.Visible = true;

            string name = txtCourtName.Text;
            string address = txtCourtAddress.Text;
            string contactNumber = txtContactNumber.Text;
            double rates = GetRates(name);

            txtRates.Text = rates.ToString();
            txtFinalCourtName.Text = name;
            txtFinalAddress.Text = address;
            txtFinalContactNumber.Text = contactNumber;

            FacilityDetails facilityDetails = GetFacilityDetails(name);

            if (facilityDetails != null)
            {
                txtNumberOfCourtsAvailable.Text = facilityDetails.NumberOfCourts.ToString();

                PopulateTotalCourtNumbersComboBox(facilityDetails.NumberOfCourts);
                PopulateTimeComboBoxes(facilityDetails.OpeningTime, facilityDetails.ClosingTime);
            }
            InitializeDataGridView();
            DisplayBookedCourtsInformation(txtCourtName.Text, "Badminton");
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

        private FacilityDetails GetFacilityDetails(string courtName)
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
                    command.Parameters.AddWithValue("@courtName", courtName);
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
        private void btnBookBadmintonCourt_Click(object sender, EventArgs e)
        {
            string bookedName = txtFinalCourtName.Text;
            string bookedAddress = txtFinalAddress.Text;
            string bookedContactNumber = txtFinalContactNumber.Text;
            int bookedNumberOfCourts = Convert.ToInt32(txtFinalCourtNumber.Text);
            TimeSpan bookedStartingTime = TimeSpan.Parse(txtFinalStartTime.Text);
            TimeSpan bookedEndingTime = TimeSpan.Parse(txtFinalEndTime.Text);
            DateTime bookedDate = DateTime.Parse(txtFinalDate.Text);
            string selectedCourtNumbers = txtFinalSelectedCourtNumbers.Text;
            double totalPrice = Convert.ToDouble(txtTotalPrice.Text);

            string username = this.username;
            
            try
            {
                if (bookedStartingTime >= bookedEndingTime)
                {
                    MessageBox.Show("Ending time must be later than the starting time.");
                    return;
                }
                if (!IsTimeSlotAvailable(bookedName, bookedDate, bookedStartingTime, bookedEndingTime, selectedCourtNumbers))
                {
                    MessageBox.Show("The selected time slot is not available. Please choose another time.");
                    return;
                }

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "INSERT INTO BookedCourts (Username, BookedName, BookedAddress, BookedContactNumber, BookedNumberOfCourts, SelectedCourtNumbers, BookedStartingTime, BookedEndingTime, BookedDate, SportType, TotalPrice) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@bookedName", bookedName);
                    command.Parameters.AddWithValue("@bookedAddress", bookedAddress);
                    command.Parameters.AddWithValue("@bookedContactNumber", bookedContactNumber);
                    command.Parameters.AddWithValue("@bookedNumberOfCourts", bookedNumberOfCourts);
                    command.Parameters.AddWithValue("@selectedCourtNumbers", selectedCourtNumbers);
                    command.Parameters.AddWithValue("@bookedStartingTime", bookedStartingTime.ToString());
                    command.Parameters.AddWithValue("@bookedEndingTime", bookedEndingTime.ToString());
                    command.Parameters.AddWithValue("@bookedDate", bookedDate);
                    command.Parameters.AddWithValue("@sportType", "Badminton");
                    command.Parameters.AddWithValue("@totalPrice", totalPrice);

                    foreach (OleDbParameter parameter in command.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Booking successful!");
                        ClearBookingDetails();
                        DisplayBookedCourtsInformation(txtCourtName.Text, "Badminton");
                        GeneratePDFConfirmation(username, bookedName, bookedAddress, bookedContactNumber, bookedNumberOfCourts, bookedStartingTime, bookedEndingTime, bookedDate, selectedCourtNumbers, totalPrice);
                    }
                    else
                    {
                        MessageBox.Show("Booking failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error booking: " + ex.Message);
            }
        }
        private void GeneratePDFConfirmation(string username, string bookedName, string bookedAddress, string bookedContactNumber, int bookedNumberOfCourts, TimeSpan bookedStartingTime, TimeSpan bookedEndingTime, DateTime bookedDate, string selectedCourtNumbers, double totalPrice)
        {
            try
            {
                // Define the file path where the PDF should be saved
                string pdfFilePath = @"C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\PROOF OF BOOKINGS\ReservationReceipt.pdf";
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                // Retrieve user's first name and last name from the database
                string firstName;
                string lastName;
                string email;
                string phone;
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Firstname, Lastname, Email, Phone FROM Users WHERE Username = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                firstName = reader["Firstname"].ToString();
                                lastName = reader["Lastname"].ToString();
                                email = reader["Email"].ToString();
                                phone = reader["Phone"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("User not found in the database.");
                                return;
                            }
                        }
                    }
                }

                // Create the PDF file
                using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // Add content to the PDF
                    document.Add(new Paragraph("THANK YOU FOR CHOOSING SMASHZONE"));
                    document.Add(new Paragraph($"   "));
                    document.Add(new Paragraph("Reservation receipt"));
                    document.Add(new Paragraph($"   "));
                    document.Add(new Paragraph("PERSONAL INFORMATION:"));
                    document.Add(new Paragraph($"Username: {username}"));
                    document.Add(new Paragraph($"Customer: {firstName} {lastName}"));
                    document.Add(new Paragraph($"Email: {email}"));
                    document.Add(new Paragraph($"Phone: {phone}"));
                    document.Add(new Paragraph($"   "));
                    document.Add(new Paragraph("BOOKING DETAILS:"));
                    document.Add(new Paragraph($"Facility Name: {bookedName}"));
                    document.Add(new Paragraph($"Facility Address: {bookedAddress}"));
                    document.Add(new Paragraph($"Contact Number: {bookedContactNumber}"));
                    document.Add(new Paragraph($"   "));
                    document.Add(new Paragraph($"Date: {bookedDate.ToShortDateString()}"));
                    document.Add(new Paragraph($"Number of Courts: {bookedNumberOfCourts}"));
                    document.Add(new Paragraph($"Selected Court Numbers: {selectedCourtNumbers}"));
                    document.Add(new Paragraph($"Starting Time: {bookedStartingTime}"));
                    document.Add(new Paragraph($"Ending Time: {bookedEndingTime}"));
                    document.Add(new Paragraph($"   "));
                    document.Add(new Paragraph($"Total Price: {totalPrice}"));

                    document.Close();
                }

                MessageBox.Show("PDF confirmation saved to the specified directory.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF confirmation: " + ex.Message);
            }
        }

        private bool IsTimeSlotAvailable(string courtName, DateTime date, TimeSpan startTime, TimeSpan endTime, string selectedCourtNumbers)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT * FROM BookedCourts WHERE BookedName = ? AND BookedDate = ?";
            bool isAvailable = true;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@bookedName", courtName);
                    command.Parameters.AddWithValue("@bookedDate", date.Date);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string bookedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            TimeSpan bookedStartTime = TimeSpan.Parse(reader["BookedStartingTime"].ToString());
                            TimeSpan bookedEndTime = TimeSpan.Parse(reader["BookedEndingTime"].ToString());

                            if ((startTime < bookedEndTime && endTime > bookedStartTime) ||
                                (bookedStartTime < endTime && bookedEndTime > startTime))
                            {
                                if (IsCourtNumberBooked(selectedCourtNumbers, bookedCourtNumbers))
                                {
                                    isAvailable = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking time slot availability: " + ex.Message);
                isAvailable = false;
            }

            return isAvailable;
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
        private void ClearBookingDetails()
        {
            txtFinalCourtNumber.Text = "";
            txtFinalSelectedCourtNumbers.Text = "";
            txtFinalStartTime.Text = "";
            txtFinalEndTime.Text = "";
            txtFinalDate.Text = "";
            txtTotalPrice.Text = "";
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

                    txtTotalPrice.Text = totalPrice.ToString("0.00");
                }
                else
                {
                    MessageBox.Show("Error: Rates per hour not found.");
                    txtTotalPrice.Text = "";
                }
            }
            else
            {
                txtTotalPrice.Text = "";
            }
        }

        private double GetRatesPerHourFromDatabase()
        {
            string courtName = txtFinalCourtName.Text;
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
                datePicker.Value = DateTime.Today;
            }
            else
            {
                txtFinalDate.Text = selectedDate.ToShortDateString();
            }
        }
        private void InitializeDataGridView()
        {
            dataGridViewBookedCourts.Columns.Add("BookedName", "Facility");
            dataGridViewBookedCourts.Columns.Add("BookedNumberOfCourts", "Booked Number of Courts");
            dataGridViewBookedCourts.Columns.Add("SelectedCourtNumbers", "Selected Court Numbers");
            dataGridViewBookedCourts.Columns.Add("BookedStartingTime", "Booked Starting Time");
            dataGridViewBookedCourts.Columns.Add("BookedEndingTime", "Booked Ending Time");
            dataGridViewBookedCourts.Columns.Add("BookedDate", "Booked Date");
        }

        private void DisplayBookedCourtsInformation(string facilityName, string sportType)
        {
            dataGridViewBookedCourts.Rows.Clear();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT * FROM BookedCourts WHERE BookedName = ? AND SportType = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@courtName", facilityName);
                    command.Parameters.AddWithValue("@sportType", sportType);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string courtName = reader["BookedName"].ToString();
                            int bookedNumberOfCourts = Convert.ToInt32(reader["BookedNumberOfCourts"]);
                            string selectedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            TimeSpan bookedStartingTime = TimeSpan.Parse(reader["BookedStartingTime"].ToString());
                            TimeSpan bookedEndingTime = TimeSpan.Parse(reader["BookedEndingTime"].ToString());
                            DateTime bookedDate = Convert.ToDateTime(reader["BookedDate"]);
   
                            dataGridViewBookedCourts.Rows.Add(courtName, bookedNumberOfCourts, selectedCourtNumbers, bookedStartingTime, bookedEndingTime, bookedDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching booked courts' information: " + ex.Message);
            }
        }

        private void btnResetCourtInfo_Click(object sender, EventArgs e)
        {
            txtFinalCourtNumber.Clear();
            txtFinalSelectedCourtNumbers.Clear();
        }

        private void guna2HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void btnFacilityCourts_Click(object sender, EventArgs e)
        {
            panelNumOfCourts.Visible = true;
            string name = txtCourtName.Text;
            string numberOfCourtsPIC = GetNumOfCourtPICSFromDatabase(name);

            if (!string.IsNullOrEmpty(numberOfCourtsPIC))
            {
                pictureBoxNumOfCourts.Image = System.Drawing.Image.FromFile(numberOfCourtsPIC);
                pictureBoxNumOfCourts.Visible = true;

            }
            else
            {
                pictureBoxNumOfCourts.Visible = false;
            }
        }
        private string GetNumOfCourtPICSFromDatabase(string courtName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT NumberOfCourtPICS FROM Courts WHERE Name = ?";
            string numberOfCourtsPIC = "";

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
                        numberOfCourtsPIC = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching image path: " + ex.Message);
            }

            return numberOfCourtsPIC;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelNumOfCourts.Visible = false;
        }
    }
}
