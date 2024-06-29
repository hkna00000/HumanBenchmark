using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPr
{
    public partial class NumberTest : Form
    {
        private Random random = new Random();
        private int key = new int();
        private int level = new int();
        LoginInfo li = new LoginInfo();
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        public NumberTest()
        {
            InitializeComponent();
            SetCursorInMiddle();
            level = 1;
            label2.Text = "";
            label5.Text = "Level " + GetHighestScore();
        }
        private float GetHighestScore()
        {
            float highestScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(score) FROM numbermemory WHERE userid = @userid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        highestScore = Convert.ToSingle(result);
                    }
                }
            }

            return highestScore;
        }
        private void NumberTest_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void SetCursorInMiddle()
        {
            int middlePosition = richTextBox1.TextLength / 2;
            richTextBox1.SelectionStart = middlePosition;
            richTextBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            for(int i = 0; i < level; i++)
            {
                char randomChar = GenerateRandomCharacter();
                label2.Text += randomChar;
            }
            timer1.Stop();
            timer1.Tick -= timer1_Tick; // Unsubscribe previous event handlers
            timer1.Tick += new EventHandler(timer1_Tick); // Subscribe the event handler again
            timer1.Interval = 500;

            progressBar1.Value = 0;
            progressBar1.Maximum = 10;

            // Show label and progress bar
            label2.Show();
            progressBar1.Show();

            // Start the timer
            timer1.Enabled = true;
            timer1.Start();
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 10)
            {
                progressBar1.Value= progressBar1.Value + 1;
            }
            else
            {
                richTextBox1.Show();
                progressBar1.Hide();
                label2.Hide();
                button2.Show();
                timer1.Stop();
                timer1.Enabled = false;
            }
        }
        private char GenerateRandomCharacter()
        {
            const string chars = "0123456789";
            int index = random.Next(chars.Length);
            return chars[index];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Show();
            button2.Hide();
            key = int.Parse(label2.Text);
            if(key == int.Parse(richTextBox1.Text))
                {
                    label6.Text = "Level " + level;
                    if (level > Global.GlobalData.highestNumber)
                    {
                        Global.GlobalData.highestNumber = level;
                    }
                    level++;
                    label2.Text = "";
                    richTextBox1.Hide();
                    string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Correct sound effect 正確音效.wav";
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
            }
            else 
            {
                string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Wrong Answer Sound effect.wav";
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
                MessageBox.Show("Try again.");
                label6.Text = "Level " + level;
                level = 1;
                label2.Text = "";
            }

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            Global.GlobalData.submitTime = DateTime.Now;
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;"; ; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO numbermemory (testid, userid, submittime, score, numberoftest) " +
                               "VALUES (@testid, @userid, @submittime, @score, @numberoftest)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", 2);
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    command.Parameters.AddWithValue("@submittime", Global.GlobalData.submitTime);
                    command.Parameters.AddWithValue("@score", Global.GlobalData.highestNumber);
                    command.Parameters.AddWithValue("@numberoftest", GetNumberOfTestsForCurrentUser(li.CurrentUser.Id));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Điểm của bạn đã được lưu lại", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int GetNumberOfTestsForCurrentUser(int userId)
        {
            int numberOfTests = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM numbermemory WHERE userid = @userid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    connection.Open();
                    numberOfTests = (int)command.ExecuteScalar();
                }
            }

            return numberOfTests + 1;
        }
    }
}
