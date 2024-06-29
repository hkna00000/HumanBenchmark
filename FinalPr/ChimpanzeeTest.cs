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
    public partial class ChimpanzeeTest : Form
    {
        private List<Button> buttons;
        private List<int> numbers;
        private int currentStep;
        private Random random;
        private int currentLevel;
        private const int InitialLevel = 4;
        private System.Windows.Forms.Timer timer;
        private List<Rectangle> buttonRectangles;
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        LoginInfo li = new LoginInfo();
        public ChimpanzeeTest()
        {
            InitializeComponent();
            buttonRectangles = new List<Rectangle>();
            InitializeGame();
            label5.Text = "Level " + GetHighestScore().ToString();   
        }
        private float GetHighestScore()
        {
            float highestScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(score) FROM visualmemory WHERE userid = @userid";

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
        private void ChimpanzeeTest_Load(object sender, EventArgs e)
        {

        }
        private void InitializeGame()
        {
            buttons = new List<Button>();
            numbers = new List<int>();
            random = new Random();
            currentStep = 0;
            currentLevel = InitialLevel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            StartNewLevel();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            
        }
        private void StartNewLevel()
        {
            GenerateNumbers(currentLevel);
            PlaceButtons();
            ShowNumbers();
        }
        private void GenerateNumbers(int level)
        {
            numbers.Clear();
            for (int i = 1; i <= level; i++)
            {
                numbers.Add(i);
            }
            numbers = numbers.OrderBy(x => x).ToList();
        }
        private void PlaceButtons()
        {
            gamePanel.Controls.Clear();
            buttons.Clear();
            bool overlapping;
            Point location;
            for (int i = 0; i < currentLevel; i++)
            {
                
                Button button = new Button();
                button.Size = new Size(50, 50);
                //button.Location = new Point(50 + (i % 3) * 60, 50 + (i / 3) * 60);
                do {
                    int x = random.Next(0, gamePanel.Width - button.Width);
                    int y = random.Next(0, gamePanel.Height - button.Height);
                    location = new Point(x, y);
                    Rectangle newRect = new Rectangle(location, button.Size);
                    overlapping = false;
                    foreach (Rectangle rect in buttonRectangles)
                    {
                        if (newRect.IntersectsWith(rect))
                        {
                            overlapping = true;
                            break;
                        }
                        if (!overlapping)
                        {
                            // If no overlap, add the rectangle to the list
                            buttonRectangles.Add(newRect);
                        }

                    }
                } while (overlapping);
                button.BackColor = Color.Orange;
                button.Font = new Font("Arial", 16, FontStyle.Bold);
                button.ForeColor = Color.White;
                button.Location = location;
                button.Tag = numbers[i];
                button.Click += Button_Click;
                gamePanel.Controls.Add(button);
                buttons.Add(button);
            }
        }
        private void ShowNumbers()
        {
            foreach (Button button in buttons)
            {
                button.Text = button.Tag.ToString();
            }

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000; // Show numbers for 2 seconds
            timer.Tick += (s, e) =>
            {
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
                timer.Stop();
            };
            timer.Start();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int number = (int)clickedButton.Tag;

            if (number == numbers[currentStep])
            {
                currentStep++;
                if (currentStep == numbers.Count)
                {
                    
                    string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Correct sound effect 正確音效.wav";
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                    MessageBox.Show("Level Passed!");
                    if (currentLevel > Global.GlobalData.highestChimpLevel)
                    {
                        Global.GlobalData.highestChimpLevel = currentLevel;
                    }
                    label6.Text = "Level " + currentLevel;
                    currentLevel++;
                    currentStep = 0;
                    StartNewLevel();
                }
            }
            else
            {
                gamePanel.Controls.Clear();
                button1.Show();
                string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - SUPER MARIO  game over  sound effect.wav";
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
                
                
                
                currentLevel = InitialLevel;
                currentStep = 0;
                MessageBox.Show("Game Over! Back to Level 4.");
                
                
                //StartNewLevel();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
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
                string query = "INSERT INTO visualmemory (testid, userid, submittime, score, numberoftest) " +
                               "VALUES (@testid, @userid, @submittime, @score, @numberoftest)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", 5);
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    command.Parameters.AddWithValue("@submittime", Global.GlobalData.submitTime);
                    command.Parameters.AddWithValue("@score", Global.GlobalData.highestChimpLevel);
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
                string query = "SELECT COUNT(*) FROM visualmemory WHERE userid = @userid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    connection.Open();
                    numberOfTests = (int)command.ExecuteScalar();
                }
            }

            return numberOfTests + 1;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
