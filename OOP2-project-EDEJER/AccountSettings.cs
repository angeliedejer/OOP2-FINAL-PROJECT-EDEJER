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
    public partial class AccountSettings : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb");
        private string username;
        private string password;
        public AccountSettings(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }

        public EditGeneralInfo EditGeneralInfo
        {
            get => default;
            set
            {
            }
        }

        public EditUserAcc EditUserAcc
        {
            get => default;
            set
            {
            }
        }

        private void AccountSettings_Load(object sender, EventArgs e)
        {
            txtUsername.Text = username;
            txtUsername.ReadOnly = true;
            txtPassword.Text = password;
            txtPassword.ReadOnly = true;

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
                    txtPhone.Text = reader["Phone"].ToString();
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
        private void btnEditAccountInfo_Click(object sender, EventArgs e)
        {
            EditUserAcc edit = new EditUserAcc(username, password);
            edit.Show();
        }

        private void btnDeleteAccInfo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete your account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("DELETE FROM Users WHERE Username = @username", connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Account deleted successfully.");
                    this.Close();
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form != this)
                        {
                            form.Hide();
                        }
                    }
                    Form1 f1 = new Form1();
                    f1.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting account: " + ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        private void btnEditGeneralInfo_Click(object sender, EventArgs e)
        {
            EditGeneralInfo general = new EditGeneralInfo(username, password);
            general.ShowDialog();
        }
    }
}
