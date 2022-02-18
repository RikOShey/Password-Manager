using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace MinApp
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSite.Text != "" && txtUser.Text != "" && txtPass.Text != "")
            {
                string cs = @"URI=file:.\password.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                    string query = "INSERT INTO account(site, user, pass) VALUES(@site, @user, @pass)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@site", txtSite.Text);
                    cmd.Parameters.AddWithValue("@user", txtUser.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Nytt Konto skapades!", "SQLite Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtSite.Clear(); txtUser.Clear(); txtPass.Clear();
            }
            else
            {
                MessageBox.Show("Du måste fylla i alla fälten!", "SQLite Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random res = new Random();
            string str = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!_@#%&()[]$?+-*=";

            int pwLength = 10; // Längd på lösenordet som skapas (antal tecken)
            string randomstring = "";
            for (int i = 0; i < pwLength; i++)
            {
                int x;
                if (i == 0 || i == pwLength - 1)
                {
                    x = res.Next(47); // Lösenordet börjar och slutar alltid med en bokstav!
                }
                else
                {
                    x = res.Next(str.Length);
                }
                randomstring = randomstring + str[x];
            }
            txtPass.Text = randomstring;

        }
    }
}
