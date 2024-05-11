using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMapMarkerForms = GMap.NET.WindowsForms.GMapMarker;
using GMapMarkerPresentation = GMap.NET.WindowsPresentation.GMapMarker;
using GMap.NET.WindowsForms.Markers;
using System.Data.OleDb;
using System.Xml.Linq;

namespace OOP2_project_EDEJER
{
    public partial class CheckCourtAvailability : Form
    {
        private string selectedMunicipality;
        private GMapOverlay markersOverlay;
        private string selectedFacilityName;
        public CheckCourtAvailability()
        {
            InitializeComponent();
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

            gmapControl.OnMarkerClick += GmapControl_OnMarkerClick;
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
            MarkCourts(selectedMunicipality);

        }
        private void MarkCourts(string municipality)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT Name, CourtAddress, Latitude, Longitude, SportType FROM Courts WHERE Municipality = ?";

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
                            string sportType = reader.GetString(4);

                            double lat, lng;
                            if (double.TryParse(latstr, out lat) && double.TryParse(lngstr, out lng))
                            {
                                GMarkerGoogle marker;
                                if (sportType == "Badminton")
                                {
                                    marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue_dot);
                                }
                                else if (sportType == "Tennis")
                                {
                                    marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.green_dot);
                                }
                                else
                                {
                                    marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red_dot);
                                }

                                marker.ToolTipText = $"Name: {name}\nAddress: {address}\nSport Type: {sportType}";
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
                MessageBox.Show("Error loading courts: " + ex.Message);
            }
        }
        private void GmapControl_OnMarkerClick(GMapMarkerForms item, MouseEventArgs e)
        {
            string[] info = item.ToolTipText.Split('\n');
            selectedFacilityName = info[0].Split(':')[1].Trim();

            string imagePath = GetImagePathFromDatabase(selectedFacilityName);

            if (!string.IsNullOrEmpty(imagePath))
            {
                pictureBoxCourtImage.Image = Image.FromFile(imagePath);
                pictureBoxCourtImage.Visible = true;
            }
            else
            {
                pictureBoxCourtImage.Visible = false;
            }
        }
        private bool IsFullyBooked(string facilityName, DateTime selectedDate)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT OpeningTime, ClosingTime, TotalNumberOfCourts FROM Courts WHERE Name = ?";

            DateTime openingTime, closingTime;
            int totalCourts;
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
                            openingTime = DateTime.Parse(reader["OpeningTime"].ToString());
                            closingTime = DateTime.Parse(reader["ClosingTime"].ToString());
                            totalCourts = int.Parse(reader["TotalNumberOfCourts"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Facility not found.");
                            return false;
                        }
                    }
                }

                query = "SELECT SelectedCourtNumbers, BookedStartingTime, BookedEndingTime FROM BookedCourts WHERE BookedName = ? AND BookedDate = ?";
                int totalAvailableSlots = 0;
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@facilityName", facilityName);
                    command.Parameters.AddWithValue("@selectedDate", selectedDate);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string selectedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            DateTime bookedStartingTime = DateTime.Parse(reader["BookedStartingTime"].ToString());
                            DateTime bookedEndingTime = DateTime.Parse(reader["BookedEndingTime"].ToString());

                            TimeSpan bookedDuration = bookedEndingTime - bookedStartingTime;
                            totalAvailableSlots += (int)bookedDuration.TotalHours; // Increment total available slots by booked hours
                        }
                    }
                }

                int totalAvailableHours = (closingTime - openingTime).Hours * totalCourts; // Calculate total available hours for all courts
                return totalAvailableSlots >= totalAvailableHours; // If total available slots is greater or equal to total available hours, then fully booked
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking availability: " + ex.Message);
                return false;
            }
           
        }

        private void DisplayBookedCourts(string facilityName, DateTime selectedDate)
        {
            dataGridViewBookedCourts.Rows.Clear();
            dataGridViewBookedCourts.Columns.Clear();

            dataGridViewBookedCourts.Columns.Add("SportType", "Sport Type");
            dataGridViewBookedCourts.Columns.Add("FacilityName", "Facility Name");
            dataGridViewBookedCourts.Columns.Add("BookedNumberOfCourts", "Number of Courts");
            dataGridViewBookedCourts.Columns.Add("SelectedCourtNumbers", "Selected Court Numbers");
            dataGridViewBookedCourts.Columns.Add("BookedStartingTime", "Starting Time");
            dataGridViewBookedCourts.Columns.Add("BookedEndingTime", "Ending Time");
            dataGridViewBookedCourts.Columns.Add("BookedDate", "Booked Date");

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT * FROM BookedCourts WHERE BookedName = ? AND BookedDate = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@facilityName", facilityName);
                    command.Parameters.AddWithValue("@selectedDate", selectedDate);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sportType = reader["SportType"].ToString();
                            int numberOfCourts = Convert.ToInt32(reader["BookedNumberOfCourts"]);
                            string selectedCourtNumbers = reader["SelectedCourtNumbers"].ToString();
                            string startingTime = reader["BookedStartingTime"].ToString();
                            string endingTime = reader["BookedEndingTime"].ToString();
                            DateTime bookedDate = Convert.ToDateTime(reader["BookedDate"]);

                            dataGridViewBookedCourts.Rows.Add(sportType, facilityName, numberOfCourts, selectedCourtNumbers, startingTime, endingTime, bookedDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching booked courts: " + ex.Message);
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
        private void btnSearchFacility_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchFacility.Text.Trim();

            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a name or address to search.");
                return;
            }

            SearchByNameOrAddress(searchQuery);
        }
        private void SearchByNameOrAddress(string searchQuery)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT Name, CourtAddress, Latitude, Longitude, SportType FROM Courts WHERE Name LIKE ? OR CourtAddress LIKE ?";
            markersOverlay.Markers.Clear();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                    command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            string address = reader.GetString(1);
                            string latstr = reader.GetString(2);
                            string lngstr = reader.GetString(3);
                            string sportType = reader.GetString(4);

                            double lat, lng;
                            if (double.TryParse(latstr, out lat) && double.TryParse(lngstr, out lng))
                            {
                                GMarkerGoogle marker;
                                if (sportType == "Badminton")
                                {
                                    marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue_dot);
                                }
                                else if (sportType == "Tennis")
                                {
                                    marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.green_dot);
                                }
                                else
                                {
                                    marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red_dot);
                                }

                                marker.ToolTipText = $"Name: {name}\nAddress: {address}\nSport Type: {sportType}";
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
                MessageBox.Show("Error searching: " + ex.Message);
            }
        }

        private void txtSearchFacility_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            selectedMunicipality = cmbMunicipality.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedMunicipality))
            {
                MessageBox.Show("Please select a municipality.");
                return;
            }

            LoadMap();
        }

        private void datePicker_ValueChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFacilityName))
            {
                MessageBox.Show("Please select a facility.");
                return;
            }

            DateTime selectedDate = datePicker.Value.Date;

            if (IsFullyBooked(selectedFacilityName, selectedDate))
            {
                txtAvailability.Text = "Fully Booked";
            }
            else
            {
                txtAvailability.Text = "Available";
            }
            DisplayBookedCourts(selectedFacilityName, selectedDate);
        }

        private void btnFacilityCourts_Click(object sender, EventArgs e)
        {
            panelNumOfCourts.Visible = true;
            string name = selectedFacilityName;
            string numberOfCourtsPIC = GetNumOfCourtPICSFromDatabase(name);

            if (!string.IsNullOrEmpty(numberOfCourtsPIC))
            {
                pictureBoxNumOfCourts.Image = Image.FromFile(numberOfCourtsPIC);
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
