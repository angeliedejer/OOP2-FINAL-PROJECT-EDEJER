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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OOP2_project_EDEJER
{
    public partial class CreateAccount : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb");
        public CreateAccount()
        {
            InitializeComponent();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CreateACC_Click(object sender, EventArgs e)
        {
            SignIn s1 = new SignIn();
            s1.Show();
            this.Hide();
        }

        private void checkbxShow_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxPassword.PasswordChar = checkbxShow.Checked ? '\0' : '*';
            txtConfirmPass.PasswordChar = checkbxShow.Checked ? '\0' : '*';
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = txtBoxUsername.Text;
            string password = txtBoxPassword.Text;
            string confirmPassword = txtConfirmPass.Text;
            string userType = "";

            if (radioBtnAdmin.Checked)
            {
                userType = "Admin";
            }
            else if (radioBtnCustomer.Checked)
            {
                userType = "Customer";
            }
            else
            {
                MessageBox.Show("Please select a user type.");
                return;
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO Users (Username, [Password], UserType) VALUES (?, ?, ?)";


                OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("?", username);
                insertCommand.Parameters.AddWithValue("?", password);
                insertCommand.Parameters.AddWithValue("?", userType);


                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registration successful!");
                }
                else
                {
                    MessageBox.Show("Registration failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            txtBoxUsername.Clear();
            txtBoxPassword.Clear();
            txtConfirmPass.Clear();
            radioBtnAdmin.Checked = false;
            radioBtnCustomer.Checked = false;
        }
    }
}
