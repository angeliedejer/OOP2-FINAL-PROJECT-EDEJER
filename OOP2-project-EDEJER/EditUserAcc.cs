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
    public partial class EditUserAcc : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb");
        private string username;
        private string password;
        public EditUserAcc(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }

        private void checkbxShow_CheckedChanged(object sender, EventArgs e)
        {
            txtCurrentPassword.PasswordChar = checkbxShow.Checked ? '\0' : '*';
            txtNewPassword.PasswordChar = checkbxShow.Checked ? '\0' : '*';
            txtConfirmPassword.PasswordChar = checkbxShow.Checked ? '\0' : '*';
        }

        private void btnChangeSave_Click(object sender, EventArgs e)
        {
            string currentUsername = txtCurrentUsername.Text;
            string currentPassword = txtCurrentPassword.Text;
            string newUsername = txtNewUsername.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (currentPassword != password)
            {
                MessageBox.Show("Incorrect current password.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(newUsername) && newUsername != username)
            {
                if (IsUsernameExists(newUsername))
                {
                    MessageBox.Show("Username already exists. Please choose a different one.");
                    return;
                }
                UpdateUsername(newUsername);
            }
            if (!string.IsNullOrWhiteSpace(newPassword) && newPassword != currentPassword)
            {
                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New password and confirm password do not match.");
                    return;
                }

                // Update the password in the database
                UpdatePassword(newPassword);

                // Update the local password variable
                //password = newPassword;
            }

            MessageBox.Show("Account details updated successfully.");
            this.Close();
            foreach (Form form in Application.OpenForms)
            {
                if (form != this)
                {
                    form.Hide();
                }
            }
            CustomerMenu customerMenu = new CustomerMenu(username, password);
            customerMenu.ShowDialog();

            AccountSettings accountSettingsForm = new AccountSettings(username, password);
            accountSettingsForm.Show();

        }
        private bool IsUsernameExists(string username)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", connection);
            command.Parameters.AddWithValue("@username", username);
            int count = (int)command.ExecuteScalar();
            connection.Close();
            return count > 0;
        }

        private void UpdateUsername(string newUsername)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("UPDATE Users SET [Username] = ? WHERE [Username] = ?", connection);
            command.Parameters.AddWithValue("@newUsername", newUsername);
            command.Parameters.AddWithValue("@username", username);
            command.ExecuteNonQuery();
            connection.Close();
            username = newUsername;
        }

        private void UpdatePassword(string newPassword)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("UPDATE Users SET [Password] = ? WHERE [Username] = ?", connection);
            command.Parameters.AddWithValue("@newPassword", newPassword);
            command.Parameters.AddWithValue("@username", username);
            command.ExecuteNonQuery();
            connection.Close();
            password = newPassword;
        }
        private void EditUserAcc_Load(object sender, EventArgs e)
        {
            txtCurrentUsername.Text = username;
            txtCurrentUsername.ReadOnly = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCurrentPassword.Clear();
            txtNewUsername.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void GoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
