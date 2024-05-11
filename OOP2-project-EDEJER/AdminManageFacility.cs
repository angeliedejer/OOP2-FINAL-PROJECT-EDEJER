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
    public partial class AdminManageFacility : Form
    {
        private string selectedMunicipality;
        private GMapOverlay markersOverlay;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
        public AdminManageFacility()
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
            txtAddress.Text = address;

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

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LoadAllCourts();
        }
        private void LoadAllCourts()
        {
            try
            {
                string query = "SELECT CourtID, Availability, SportType, Municipality, Name, CourtAddress, ContactNumber, Latitude, Longitude, TotalNumberOfCourts, OpeningTime, ClosingTime, RatesPerHour, NumberOfCourtPICS, ImagePath " +
                               "FROM Courts";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataGridViewFacility.DataSource = dataTable;

                        DataGridViewFacility.Columns["CourtID"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user bookings: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Get the changes from the DataGridView
                    DataTable changes = ((DataTable)DataGridViewFacility.DataSource).GetChanges();

                    if (changes != null)
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Courts", connection))
                        {
                            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            adapter.Update(changes);
                            ((DataTable)DataGridViewFacility.DataSource).AcceptChanges();
                        }
                    }

                    MessageBox.Show("Changes saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllCourts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataGridViewFacility.SelectedRows.Count > 0)
            {
                // Get the CourtID of the selected row
                int courtID = Convert.ToInt32(DataGridViewFacility.SelectedRows[0].Cells["CourtID"].Value);

                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        // Construct the DELETE query
                        string deleteQuery = "DELETE FROM Courts WHERE CourtID = ?";
                        using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@courtID", courtID);

                            // Execute the DELETE query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Court deleted successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Refresh the DataGridView
                                LoadAllCourts();
                            }
                            else
                            {
                                MessageBox.Show("No court found with the provided CourtID.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting court: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnSearchInFacility_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchInFacility.Text.Trim();

            // Check if the search keyword is empty
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a search keyword.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string query = "SELECT CourtID, SportType, Municipality, Name, CourtAddress, ContactNumber, Latitude, Longitude, TotalNumberOfCourts, OpeningTime, ClosingTime, RatesPerHour, NumberOfCourtPICS, ImagePath " +
                               "FROM Courts " +
                               "WHERE SportType LIKE ? OR Municipality LIKE ? OR Name LIKE ? OR CourtAddress LIKE ?";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@sportType", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@municipality", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@name", "%" + keyword + "%");
                    command.Parameters.AddWithValue("@courtAddress", "%" + keyword + "%");

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        DataGridViewFacility.Columns.Clear();

                        DataGridViewFacility.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching courts: " + ex.Message, "Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewMaps_Click(object sender, EventArgs e)
        {
            panelViewMaps.Visible = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            panelViewMaps.Visible = false;
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
        
    }
}
