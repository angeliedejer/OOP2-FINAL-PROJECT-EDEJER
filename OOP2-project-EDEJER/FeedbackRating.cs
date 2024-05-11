using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Data.OleDb;

namespace OOP2_project_EDEJER
{
    public partial class FeedbackRating : Form
    {
        public string username;
        public FeedbackRating(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void btnSendFeedback_Click(object sender, EventArgs e)
        {
            string feedback = txtFeedback.Text.Trim();

            if (string.IsNullOrEmpty(feedback))
            {
                MessageBox.Show("Please enter your feedback.", "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "INSERT INTO Feedbacks (Username, Feedback) VALUES (?, ?)";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@feedback", feedback);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Feedback submitted successfully!", "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit feedback. Please try again.", "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting feedback: " + ex.Message, "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FeedbackRating_Load(object sender, EventArgs e)
        {
            
        }

        private void btnViewAdminResponse_Click(object sender, EventArgs e)
        {
            panelAdminResponse.Visible = true;
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "SELECT Feedback, AdminResponse FROM Feedbacks WHERE Username = ?";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@username", username);

                    DataTable feedbackTable = new DataTable();
                    adapter.Fill(feedbackTable);

                    dataGridViewFeedback.DataSource = feedbackTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading admin responses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelAdminResponse.Visible = false;
        }
    }
}
