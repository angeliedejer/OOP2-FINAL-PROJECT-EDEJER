using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace OOP2_project_EDEJER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public CreateAccount CreateAccount
        {
            get => default;
            set
            {
            }
        }

        public SignIn SignIn
        {
            get => default;
            set
            {
            }
        }

        public CreateAccount CreateAccount1
        {
            get => default;
            set
            {
            }
        }

        public SignIn SignIn1
        {
            get => default;
            set
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SignIn s1 = new SignIn();
            s1.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            CreateAccount c1 = new CreateAccount();
            c1.Show();
        }
    }
}
