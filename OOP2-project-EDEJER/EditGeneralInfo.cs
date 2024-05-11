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
    public partial class EditGeneralInfo : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb");
        private string username;
        private string password;

        public EditGeneralInfo(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }

        private void GoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGeneralSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstname.Text;
            string lastName = txtLastname.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string address = txtAddress.Text;

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", username);
                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    OleDbCommand updateCommand = new OleDbCommand("UPDATE Users SET Firstname = @firstname, Lastname = @lastname, Email = @email, Phone = @phone, Address = @address WHERE Username = @username", connection);
                    updateCommand.Parameters.AddWithValue("@firstName", firstName);
                    updateCommand.Parameters.AddWithValue("@lastName", lastName);
                    updateCommand.Parameters.AddWithValue("@email", email);
                    updateCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    updateCommand.Parameters.AddWithValue("@address", address);
                    updateCommand.Parameters.AddWithValue("@username", username);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    OleDbCommand insertCommand = new OleDbCommand("INSERT INTO Users (Username, Firstname, Lastname, Email, Phone, Address) VALUES (@username, @firstname, @lastname, @email, @phone, @address)", connection);
                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@firstName", firstName);
                    insertCommand.Parameters.AddWithValue("@lastName", lastName);
                    insertCommand.Parameters.AddWithValue("@email", email);
                    insertCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    insertCommand.Parameters.AddWithValue("@address", address);
                    insertCommand.ExecuteNonQuery();
                }

                MessageBox.Show("General information saved successfully.");
                this.Close();
                /*foreach (Form form in Application.OpenForms)
                {
                    if (form != this)
                    {
                        form.Hide();
                    }
                }
                CustomerMenu customerMenu = new CustomerMenu(username, password);
                customerMenu.ShowDialog();

                AccountSettings accountSettingsForm = new AccountSettings(username, password);
                accountSettingsForm.Show();*/

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving general information: " + ex.Message);
            }
        }

        private void EditGeneralInfo_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM Users WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", username);
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    txtFirstname.Text = reader["Firstname"].ToString();
                    txtLastname.Text = reader["Lastname"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhoneNumber.Text = reader["Phone"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching general information: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
