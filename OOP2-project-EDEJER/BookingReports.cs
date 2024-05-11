using Guna.Charts.Interfaces;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
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

namespace OOP2_project_EDEJER
{
    public partial class BookingReports : Form
    {
        private Dictionary<string, Dictionary<string, double>> salesDataByMonth = new Dictionary<string, Dictionary<string, double>>();
        public BookingReports()
        {
            InitializeComponent();
            guna2VScrollBar1.BindingContainer = DataGridViewSales;
            guna2VScrollBar1.AutoScroll = true;
            InitializeSalesData();
            PopulateFacilityComboBox();
        }
        private void InitializeSalesData()
        {
            // Initialize sales data dictionary for each category
            salesDataByMonth.Add("Sales", new Dictionary<string, double>());
        }

        private void LoadDataForFacility(string facilityName)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT BookedDate, TotalPrice FROM BookedCourts WHERE BookedName = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@facilityName", facilityName);
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime bookedDate = DateTime.Parse(reader["BookedDate"].ToString());
                            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(bookedDate.Month);
                            double totalPrice = Convert.ToDouble(reader["TotalPrice"]);

                            // Add total price to sales data dictionary for the corresponding month
                            if (!salesDataByMonth["Sales"].ContainsKey(monthName))
                            {
                                salesDataByMonth["Sales"].Add(monthName, totalPrice);
                            }
                            else
                            {
                                salesDataByMonth["Sales"][monthName] += totalPrice;
                            }
                        }
                    }
                }

                // Add column labels for DataGridView
                DataGridViewTextBoxColumn columnMonth = new DataGridViewTextBoxColumn();
                columnMonth.HeaderText = "Month";
                DataGridViewTextBoxColumn columnSales = new DataGridViewTextBoxColumn();
                columnSales.HeaderText = "Sales";
                DataGridViewSales.Columns.AddRange(new DataGridViewColumn[] { columnMonth, columnSales });

                // Populate DataGridView
                foreach (var month in salesDataByMonth["Sales"])
                {
                    int rowIndex = DataGridViewSales.Rows.Add(month.Key, month.Value);
                    DataGridViewSales.Rows[rowIndex].Tag = "Sales";
                }

                // Populate GunaChart
                foreach (var month in salesDataByMonth["Sales"])
                {
                    Sales.DataPoints.Add(month.Key, month.Value);
                }

                gunaLineChart.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data for facility: " + ex.Message);
            }
        }

        private void cmbFacilityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            salesDataByMonth["Sales"].Clear();
            DataGridViewSales.Rows.Clear();
            Sales.DataPoints.Clear();

            DataGridViewSales.Columns.Clear();

            string selectedFacility = cmbFacilityName.SelectedItem.ToString();

            // Load data for the selected facility
            LoadDataForFacility(selectedFacility);
        }
        private void PopulateFacilityComboBox()
        {
            cmbFacilityName.Items.Clear();
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT DISTINCT BookedName FROM BookedCourts";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string facilityName = reader["BookedName"].ToString();
                            cmbFacilityName.Items.Add(facilityName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading facility names: " + ex.Message);
            }
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            // Clear existing data before loading new data
            salesDataByMonth["Sales"].Clear();
            DataGridViewSales.Rows.Clear();
            Sales.DataPoints.Clear();

            DataGridViewSales.Columns.Clear();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
            string query = "SELECT BookedName, TotalPrice FROM BookedCourts";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string facilityName = reader["BookedName"].ToString();
                            double totalPrice = Convert.ToDouble(reader["TotalPrice"]);

                            // Add total price to sales data dictionary for the corresponding facility
                            if (!salesDataByMonth["Sales"].ContainsKey(facilityName))
                            {
                                salesDataByMonth["Sales"].Add(facilityName, totalPrice);
                            }
                            else
                            {
                                salesDataByMonth["Sales"][facilityName] += totalPrice;
                            }
                        }
                    }
                }

                // Add column labels for DataGridView
                DataGridViewTextBoxColumn columnFacility = new DataGridViewTextBoxColumn();
                columnFacility.HeaderText = "Facility";
                DataGridViewTextBoxColumn columnSales = new DataGridViewTextBoxColumn();
                columnSales.HeaderText = "Sales";
                DataGridViewSales.Columns.AddRange(new DataGridViewColumn[] { columnFacility, columnSales });

                // Populate DataGridView
                foreach (var facility in salesDataByMonth["Sales"])
                {
                    DataGridViewSales.Rows.Add(facility.Key, facility.Value);
                }

                // Populate GunaLineChart
                foreach (var facility in salesDataByMonth["Sales"])
                {
                    Sales.DataPoints.Add(facility.Key, facility.Value);
                }

                // Update GunaLineChart
                gunaLineChart.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
    }
}
