using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OOP2_project_EDEJER
{
    public partial class SignIn : Form
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb");
        
        public SignIn()
        {
            InitializeComponent();
        }

        public CustomerMenu CustomerMenu
        {
            get => default;
            set
            {
            }
        }

        public AdminMenu AdminMenu
        {
            get => default;
            set
            {
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CreateACC_Click(object sender, EventArgs e)
        {
            CreateAccount c1 = new CreateAccount();
            c1.Show();
            this.Hide();
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void checkbxShow_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxPassword.PasswordChar = checkbxShow.Checked ? '\0' : '*';
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = txtBoxUsername.Text;
            string password = txtBoxPassword.Text;
            string userType = "";

            if (radioBtnAdmin.Checked)
            {
                userType = "Admin";
            }
            else if (radioBtnCustomer.Checked)
            {
                userType = "Customer";
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }
            string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password AND UserType = @userType";

            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@userType", userType);

            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Login successful!");
                this.Hide();

                foreach (Form form in Application.OpenForms)
                {
                    if (form != this)
                    {
                        form.Hide();
                    }
                }
                if (userType == "Admin")
                {
                    AdminMenu adminMenu = new AdminMenu(username, password);
                    adminMenu.ShowDialog();
                }
                else if (userType == "Customer")
                {
                    CustomerMenu customerMenu = new CustomerMenu(username, password);
                    customerMenu.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                txtBoxUsername.Clear();
                txtBoxPassword.Clear();
            }
            connection.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            txtBoxUsername.Clear();
            txtBoxPassword.Clear();
            radioBtnAdmin.Checked = false;
            radioBtnCustomer.Checked = false;
        }
    }
}
