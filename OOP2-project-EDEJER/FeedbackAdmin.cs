using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_project_EDEJER
{
    public partial class FeedbackAdmin : Form
    {
       
        public FeedbackAdmin()
        {
            InitializeComponent();
            LoadFeedback();
        }
        private void LoadFeedback()
        {
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                string query = "SELECT * FROM Feedbacks";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    DataTable feedbackTable = new DataTable();
                    adapter.Fill(feedbackTable);
                    dataGridViewFeedback.DataSource = feedbackTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading feedback: " + ex.Message, "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dataGridViewFeedback.SelectedRows.Count > 0)
            {
                int feedbackID = Convert.ToInt32(dataGridViewFeedback.SelectedRows[0].Cells["FeedbackID"].Value);
                string adminResponse = txtAdminResponse.Text.Trim();

                try
                {
                    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\Visual Studio 2022\OOP2-project-EDEJER\OOP2-project-EDEJER\bin\Debug\Accounts.accdb";
                    string query = "UPDATE Feedbacks SET AdminResponse = ? WHERE FeedbackID = ?";

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@adminResponse", adminResponse);
                        command.Parameters.AddWithValue("@feedbackID", feedbackID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Response sent successfully!", "Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadFeedback();
                        }
                        else
                        {
                            MessageBox.Show("Failed to send response. Please try again.", "Response", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending response: " + ex.Message, "Response", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a feedback to respond to.", "Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FeedbackAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewFeedback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewFeedback.Rows.Count)
            {
                DataGridViewRow row = dataGridViewFeedback.Rows[e.RowIndex];
                string feedbackMessage = row.Cells["Feedback"].Value.ToString();
                txtViewMessages.Text = feedbackMessage;
            }
        }
    }
}
