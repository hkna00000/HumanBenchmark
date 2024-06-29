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
    public partial class AimTrainerForm : Form
    {
        private Random random = new Random();
        private int pictureBoxCount = 0;
        private int maxPictureBoxes = 11;
        private StringBuilder log = new StringBuilder();
        private DateTime startTime;
        private DateTime endTime;
        private double highestAimTrainer = 0;
        LoginInfo li = new LoginInfo();
        private int miss = 10;
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        public AimTrainerForm()
        {
            InitializeComponent();
            label5.Text = GetHighestScore().ToString();
        }
        private float GetHighestScore()
        {
            float highestScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MIN(score) FROM aimtrainer WHERE userid = @userid";

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
        private void AimTrainerForm_Load(object sender, EventArgs e)
        {

        }
        private void GenerateRandomPictureBox()
        {
            if (pictureBoxCount >= maxPictureBoxes)
            {
                
                return;
            }

            // Define padding
            int padding = 10;
            int maxWidth = 300;
            int maxHeight = 300;

            // Generate random position within the defined padding
            int x = random.Next(padding, maxWidth - padding);
            int y = random.Next(padding, maxHeight - padding);

            if (pictureBoxCount == 0)
            {
                startTime = DateTime.Now;
            }
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            //log.AppendLine($"PictureBox {pictureBoxCount + 1} appeared at {timestamp}");
            // Create a new PictureBox
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(50, 50), // Example size, adjust as needed
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle,
                Image = Image.FromFile(@"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\icon\target.png"), // Load the image from file
                SizeMode = PictureBoxSizeMode.StretchImage 
            };

            // Add the PictureBox to the Form
            this.Controls.Add(pictureBox);

            // Attach the Click event handler
            pictureBox.Click += PictureBox_Click;

            // Record the appearance time
            
            UpdateLabel();

            // Increase the count
            pictureBoxCount++;
            if (pictureBoxCount == maxPictureBoxes)
            {
                // Record the appearance time of the last PictureBox
                endTime = DateTime.Now;

                // Calculate and display the total time
                TimeSpan totalTime = endTime - startTime;
                string a = totalTime.TotalSeconds.ToString();
                double b = double.Parse(a) * 100;
                double c = Math.Round(b, 0);
                label6.Text = c.ToString();
                if (c > highestAimTrainer)
                {
                    highestAimTrainer = c;
                    Global.GlobalData.highestAimTrainer = c;
                    label5.Text = highestAimTrainer.ToString();
                    pictureBox.Visible = false;
                }
                else
                {
                    label5.Text = c.ToString();
                    pictureBox.Visible = false;
                }
                //label5.Text = $"Total time for {maxPictureBoxes} PictureBoxes: {totalTime.TotalSeconds} seconds";
            }
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (pictureBoxCount < maxPictureBoxes)
            {
                // Generate another PictureBox at a different random location
                PictureBox clickedPictureBox = sender as PictureBox;
                string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Mouse Click Sound Effect-[AudioTrimmer.com].wav";
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
                GenerateAnotherPictureBox(clickedPictureBox.Location);
                clickedPictureBox.Visible = false;
            }

        }
        private void GenerateAnotherPictureBox(Point existingLocation)
        {
            // Define padding
            int padding = 10;
            int maxWidth = 300;
            int maxHeight = 300;

            // Generate random position within the defined padding and ensure it's different from the existing one
            int x, y;
            do
            {
                x = random.Next(padding, maxWidth - padding);
                y = random.Next(padding, maxHeight - padding);
            } while (x == existingLocation.X && y == existingLocation.Y);

            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            //log.AppendLine($"PictureBox {pictureBoxCount + 1} appeared at {timestamp}");
            // Create a new PictureBox
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(50, 50), // Example size, adjust as needed
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle,
                Image = Image.FromFile(@"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\icon\target.png"), // Load the image from file
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Add the PictureBox to the Form
            this.Controls.Add(pictureBox);

            // Attach the Click event handler to the new PictureBox
            pictureBox.Click += PictureBox_Click;

            // Record the appearance time
            
            UpdateLabel();

            // Increase the count
            pictureBoxCount++;
            if (pictureBoxCount == maxPictureBoxes)
            {
                // Record the appearance time of the last PictureBox
                endTime = DateTime.Now;

                // Calculate and display the total time
                TimeSpan totalTime = endTime - startTime;
                string a = totalTime.TotalSeconds.ToString();
                double b = double.Parse(a)*100;
                double c = Math.Round(b, 0);
                label6.Text = c.ToString();
                if(c > highestAimTrainer)
                {
                    highestAimTrainer = c;
                    pictureBox.Visible = false;
                }
                else
                {
                    pictureBox.Visible = false;
                }
                //label5.Text = $"Total time for {maxPictureBoxes} PictureBoxes: {totalTime.TotalSeconds} seconds";
            }
        }
        private void UpdateLabel()
        {
            
             label6.Text = log.ToString();
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            GenerateRandomPictureBox();
            panel2.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBoxCount = 0;
            maxPictureBoxes = 10;
            GenerateRandomPictureBox();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            GenerateRandomPictureBox();
            panel2.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            Global.GlobalData.submitTime = DateTime.Now;
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;"; ; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO aimtrainer (testid, userid, submittime, score, numberoftest) " +
                               "VALUES (@testid, @userid, @submittime, @score, @numberoftest)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", 3);
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    command.Parameters.AddWithValue("@submittime", Global.GlobalData.submitTime);
                    command.Parameters.AddWithValue("@score", highestAimTrainer);
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
                string query = "SELECT COUNT(*) FROM aimtrainer WHERE userid = @userid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    connection.Open();
                    numberOfTests = (int)command.ExecuteScalar();
                }
            }

            return numberOfTests + 1;
        }
        private void AimTrainerForm_MouseClick(object sender, MouseEventArgs e)
        {
            miss++;
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
