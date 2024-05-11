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

namespace OOP2_project_EDEJER
{
    public partial class SearchFacility : Form
    {
        private string selectedMunicipality;
        private GMapOverlay markersOverlay;
        public SearchFacility()
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
            string query = "SELECT Name, CourtAddress, Latitude, Longitude, SportType, Availability FROM Courts WHERE Municipality = ?";

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
                            bool availability = reader.GetBoolean(5);

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

                                marker.ToolTipText = $"Name: {name}\nAddress: {address}\nSport: {sportType}\nStatus: {(availability ? "Open" : "Closed")}";
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
            string name = info[0].Split(':')[1].Trim();
            string address = info[1].Split(':')[1].Trim();
            string sportType = info[2].Split(':')[1].Trim();
            string availability = info[3].Split(':')[1].Trim();

            txtStatus.Text = availability;
            txtSportType.Text = sportType;
            txtCourtName.Text = name;
            txtCourtAddress.Text = address;

            string contactNumber = GetContactNumberFromDatabase(name);
            txtContactNumber.Text = contactNumber;

            string imagePath = GetImagePathFromDatabase(name);

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
            string query = "SELECT Name, CourtAddress, Latitude, Longitude, SportType, Availability FROM Courts WHERE Name LIKE ? OR CourtAddress LIKE ?";
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
                            bool availability = reader.GetBoolean(5);

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
                MessageBox.Show("Error searching: " + ex.Message);
            }
        }

        private void btnFacilityCourts_Click(object sender, EventArgs e)
        {
            panelNumOfCourts.Visible = true;
            string name = txtCourtName.Text;
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
