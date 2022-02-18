using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace MinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cs = @"URI=file:.\password.db";
            SQLiteConnection conn = new SQLiteConnection(cs);
            conn.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter("SELECT id, site FROM account", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Välj Konto";
            dt.Rows.InsertAt(row, 0);

            comboBox1.DisplayMember = "site";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = dt;
            conn.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (mtxtPassword.Visible == true)
            {
                btnShow.Image = Properties.Resources.Hide_20;
                mtxtPassword.Visible = false;
                txtPassword.Visible = true;
            }
            else
            {
                btnShow.Image = Properties.Resources.Show_20;
                txtPassword.Visible = false;
                mtxtPassword.Visible = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cs = @"URI=file:.\password.db";
            SQLiteConnection conn = new SQLiteConnection(cs);
            conn.Open();
                string query = "SELECT * FROM account";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (comboBox1.Text == dr["site"].ToString())
                    {
                        txtUsername.Text = dr["user"].ToString();
                        txtPassword.Text = dr["pass"].ToString();
                    }
                }
            conn.Close();

            if (mtxtPassword.Visible != true)
            {
                btnShow_Click(sender, new EventArgs());
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            addForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Vill du radera detta Konto? ", "SQLite Fråga", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string cs = @"URI=file:.\password.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                    string query = "DELETE FROM account WHERE id='" + comboBox1.SelectedValue + "';";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                conn.Close();

                toolStripStatusLabel1.Text = "Kontot togs bort!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Vill du uppdatera detta Konto? ", "SQLite Fråga", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string cs = @"URI=file:.\password.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                    string query = "UPDATE account SET user='" + txtUsername.Text + "', pass='" + txtPassword.Text + "' WHERE id='" + comboBox1.SelectedValue + "';";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                conn.Close();

                toolStripStatusLabel1.Text = comboBox1.Text + ": Kontot uppdaterades!";
            }
        }
    }
}
