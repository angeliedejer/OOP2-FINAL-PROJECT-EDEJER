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
    public partial class ManageUsers : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb");
        public ManageUsers()
        {
            InitializeComponent();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {

        }
        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                MessageBox.Show("Connection test successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection test failed: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["UserID"].Index && e.RowIndex >= 0)
            {
                guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtUsernameSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a search keyword.");
                return;
            }

            try
            {
                connection.Open();
                string query = "SELECT Username, [Password], UserType, Firstname, Lastname, Email, Phone, Address FROM Users WHERE " +
                    "Username LIKE ? OR [Password] LIKE ? OR UserType LIKE ? OR Firstname LIKE ? OR Lastname LIKE ? OR Email LIKE ? OR Phone LIKE ? OR Address LIKE ?";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@username", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@password", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@userType", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@firstname", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@lastname", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@email", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@phone", "%" + keyword + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@address", "%" + keyword + "%");
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                guna2DataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching user accounts: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Users", connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.Update((DataTable)guna2DataGridView1.DataSource);
                MessageBox.Show("Changes saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                int userID = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells["UserID"].Value);

                try
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Users WHERE UserID = ?";
                    using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User deleted successfully!");
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("No user found with the provided UserID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            try
            {
                connection.Open();
                string query = "SELECT UserID, Username, [Password], UserType, Firstname, Lastname, Email, Phone, Address FROM Users";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                guna2DataGridView1.DataSource = dataTable;

                guna2DataGridView1.Columns["UserID"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user accounts: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
