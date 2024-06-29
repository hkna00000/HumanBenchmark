using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static TheArtOfDev.HtmlRenderer.Adapters.RGraphicsPath;

namespace FinalPr
{
    public partial class Leaderboard : Form
    {
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        LoginInfo li = new LoginInfo();
        UserController uc = new UserController();
        public Leaderboard()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            label18.Text = "Hello, " + li.CurrentUser.Username;
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel1.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM reactiontest
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest, users.username
            FROM reactiontest s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            JOIN users on s.userid = users.userid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM reactiontest where userid = @userid order by highestscore asc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel3.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MIN(score) AS highestscore
                FROM aimtrainer
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM aimtrainer s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM aimtrainer where userid = @userid order by highestscore asc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel5.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM numbermemory
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM numbermemory s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM numbermemory where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel7.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM sequencetest
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM sequencetest s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM sequencetest where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void panel9_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel9.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(WPM) AS highestscore
                FROM typingtest
                GROUP BY userid
            )
            SELECT s.userid, s.WPM AS highestscore, s.submittime, s.numberoftest
            FROM typingtest s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.WPM = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 WPM AS highestscore, submittime, userid FROM typingtest where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void panel11_MouseClick(object sender, MouseEventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel11.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM visualmemory
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM visualmemory s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM visualmemory where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel17_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFormAsImageWithDialog();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel1.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM reactiontest
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM reactiontest s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM reactiontest where userid = @userid order by highestscore asc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel3.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MIN(score) AS highestscore
                FROM aimtrainer
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM aimtrainer s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM aimtrainer where userid = @userid order by highestscore asc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel5.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM numbermemory
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM numbermemory s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM numbermemory where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel7.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM sequencetest
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM sequencetest s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM sequencetest where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel9.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel11.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(WPM) AS highestscore
                FROM typingtest
                GROUP BY userid
            )
            SELECT s.userid, s.WPM AS highestscore, s.submittime, s.numberoftest
            FROM typingtest s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.WPM = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 WPM AS highestscore, submittime, userid FROM typingtest where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label10.Hide();
            label11.Hide();
            panel11.BackColor = Color.FromArgb(0xEF, 0x45, 0x65);
            panel3.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel5.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel7.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel9.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            panel1.BackColor = Color.FromArgb(0x3D, 0xA9, 0xFC);
            DataTable dataTable = new DataTable();
            string query = @"
            WITH UserHighestScores AS (
                SELECT userid, MAX(score) AS highestscore
                FROM visualmemory
                GROUP BY userid
            )
            SELECT s.userid, s.score AS highestscore, s.submittime, s.numberoftest
            FROM visualmemory s
            JOIN UserHighestScores uhs ON s.userid = uhs.userid AND s.score = uhs.highestscore
            WHERE s.userid = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            dataGridView1.DataSource = dataTable;
            string query2 = @"SELECT top 1 score AS highestscore, submittime, userid FROM visualmemory where userid = @userid order by highestscore desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    label10.Text = reader["highestscore"].ToString();
                    label11.Text = reader["submittime"].ToString();
                    label10.Show();
                    label11.Show();
                }
                reader.Close();
            }
        }
        private Bitmap CaptureFormAndSave()
        {
            // Create a new bitmap with the same size as the form
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            // Draw the form onto the bitmap
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

            // Save the bitmap to a file
            return bmp;
        }
        
        private void SaveFormAsImage(string filePath)
        {
            Bitmap bitmap = CaptureFormAndSave();
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }
        private void SaveFormAsImageWithDialog()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Files|*.png";
                saveFileDialog.Title = "Save DataGridView as Image";
                saveFileDialog.FileName = "Unknown.png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        SaveFormAsImage(saveFileDialog.FileName);
                        MessageBox.Show("Image saved successfully!");
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Leaderboard_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
