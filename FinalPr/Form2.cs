using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.SqlClient;

namespace FinalPr
{
    public partial class Form2 : Form
    {
        LoginInfo li = new LoginInfo();
        private System.Windows.Forms.Timer timer;
        private DateTime panel6ShownTime;
        private bool isTimerStarted;
        private DateTime lastClickTime;
        private Random random;
        private bool isPanel6Shown;
        private List<string> randomTexts;
        private double c,i=0,bestTime=999;
        UserController uc = new UserController();
        private double score;
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        public Form2()
        {
            InitializeComponent();
            InitializeCustomComponents();
            label5.Text = $"{GetHighestScore().ToString()} miliseconds";
            label14.Text = $"{GetAverageScore().ToString()} miliseconds";
        }
        private float GetHighestScore()
        {
            float highestScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MIN(score) FROM reactiontest WHERE userid = @userid";

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
    
    private void InitializeCustomComponents()
        {
            isPanel6Shown = false;
            isTimerStarted = false;
            timer = new System.Windows.Forms.Timer();
            random = new Random();
            timer.Tick += Timer_Tick;
            

        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            i = i + 1;
            var clickTime = DateTime.Now;
            var timeTaken = clickTime - panel6ShownTime;
            string temp = timeTaken.TotalSeconds.ToString();
            double temp1 = double.Parse(temp);
            temp1 = Math.Round(temp1 * 1000,0);
            score = temp1;
            label6.Text = $"{temp1.ToString()} miliseconds";
            panel6.Visible = false;
            panel1.Visible = true;
            isPanel6Shown = false;
            isTimerStarted = false;
            string a = timeTaken.TotalSeconds.ToString();
            double b = double.Parse(a);
            pictureBox7.Visible = false;
            panel1.BackColor = Color.FromArgb(0x25, 0x73, 0xc1);
        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {            
            panel8.Visible = false;
            panel1.Visible= true;
            isPanel6Shown = true;
            isTimerStarted = false;
            pictureBox7.Visible = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //panel1.BackColor = targetColor;
            timer.Stop();
            panel6.Visible = true;
            panel1.Visible = false;
            //isPanel6Shown = false;
            panel6ShownTime = DateTime.Now;
        }

        
        private void panel1_Click(object sender, MouseEventArgs e)
        {
            if (!isTimerStarted)
            {
                // Start the timer with a random interval between 1 and 5 seconds
                timer.Interval = random.Next(1000, 5000);
                timer.Start();
                isTimerStarted = true;
                panel1.BackColor = Color.Red;
                pictureBox7.Visible = true;
            }
            else if (!isPanel6Shown)
            {
                // Show Panel3 if Panel2 hasn't shown up yet
                panel8.Visible = true;
                panel1.Visible = false;
                timer.Stop();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string GetRandomText()
        {
            int index = random.Next(randomTexts.Count);
            return randomTexts[index];
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            Global.GlobalData.bestReactionTime = bestTime;
            Global.GlobalData.submitTime = DateTime.Now;
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;"; ; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO reactiontest (testid, userid, submittime, score, numberoftest) " +
                               "VALUES (@testid, @userid, @submittime, @score, @numberoftest)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", 1);
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    command.Parameters.AddWithValue("@submittime", Global.GlobalData.submitTime);
                    command.Parameters.AddWithValue("@score", score);
                    command.Parameters.AddWithValue("@numberoftest", GetNumberOfTestsForCurrentUser(li.CurrentUser.Id));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Điểm của bạn đã được lưu lại", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int GetNumberOfTestsForCurrentUser(int userId)
        {
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;"; // Replace with your actual connection string
            int numberOfTests = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM reactiontest WHERE userid = @userid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    connection.Open();
                    numberOfTests = (int)command.ExecuteScalar();
                }
            }

            return numberOfTests+1;
        }
        private float GetAverageScore()
        {
            float averageScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AVG(score) FROM reactiontest WHERE userid = @userid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        averageScore = Convert.ToSingle(result); ;
                    }
                }
            }

            return averageScore;
        }
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = 300 };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
                Button confirmation = new Button() { Text = "Ok", Left = 250, Width = 100, Top = 70, DialogResult = DialogResult.OK };

                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
