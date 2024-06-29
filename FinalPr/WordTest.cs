using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPr
{
    public partial class WordTest : Form
    {
        private int currentLevel = 1;
        private System.Windows.Forms.Timer wordDropTimer;
        
        private List<Label> wordLabels;
        private Random random;
        private List<string> wordList;
        private int wordsFallen;
        private int wordsToFall = 10;
        private int totalWord = 0;
        private double WPM;
        private Stopwatch stopwatch;
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        LoginInfo li = new LoginInfo();
        public WordTest()
        {
            InitializeComponent();
            InitializeGame();
            label9.Text = GetHighestScore().ToString();
        }
        private float GetHighestScore()
        {
            float highestScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(WPM) FROM typingtest WHERE userid = @userid";

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
        private void WordTest_Load(object sender, EventArgs e)
        {

        }
        private void InitializeGame()
        {
            wordDropTimer = new System.Windows.Forms.Timer();
            
            wordDropTimer.Interval = 5000; // Adjust for speed
            wordDropTimer.Tick += WordDropTimer_Tick;

            wordLabels = new List<Label>();
            random = new Random();

            wordList = new List<string>
    {
        "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew", "kiwi", "lemon",
        "orange", "pear", "plum", "peach", "apricot", "blackberry", "blueberry", "cantaloupe", "cranberry", "dragonfruit",
        "guava", "jackfruit", "kumquat", "lime", "mango", "nectarine", "papaya", "pineapple", "pomegranate", "raspberry",
        "strawberry", "tangerine", "watermelon", "avocado", "broccoli", "carrot", "cauliflower", "celery", "cucumber", "eggplant",
        "garlic", "lettuce", "mushroom", "onion", "pepper", "potato", "pumpkin", "radish", "spinach", "tomato",
        "turnip", "zucchini", "asparagus", "artichoke", "beet", "brussels", "chili", "corn", "pea", "bean",
        "kale", "leek", "okra", "parsnip", "rhubarb", "squash", "yam", "basil", "coriander", "dill",
        "fennel", "ginger", "lavender", "mint", "oregano", "parsley", "rosemary", "sage", "thyme", "vanilla",
        "almond", "cashew", "hazelnut", "peanut", "pecan", "pistachio", "walnut", "bread", "cheese", "cream",
        "egg", "milk", "yogurt", "butter", "ice", "fish", "ham", "meat", "tofu", "salad"
    };

            button1.Click += StartButton_Click;
            richTextBox1.KeyPress += UserInputTextBox_KeyPress;
            stopwatch = new Stopwatch();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            StartLevel(currentLevel);
            button1.Hide();
        }
        private void StartLevel(int level)
        {
            currentLevel = level;
            wordsFallen = 0;
            wordDropTimer.Interval = 5000 - (level - 1) * 500; // Increase speed with level
            label6.Text = $"Level: {level}";
            wordDropTimer.Start();
            stopwatch.Restart();
        }
        private void WordDropTimer_Tick(object sender, EventArgs e)
        {
            if (wordsFallen < wordsToFall)
            {
                DropNewWord();
                wordsFallen++;
            }
            else
            {
                wordDropTimer.Stop();
                currentLevel++;
                StartLevel(currentLevel);
            }
        }
        private void DropNewWord()
        {
            
            var word = wordList[random.Next(wordList.Count)];
            var wordLabel = new Label
            {
                Text = word,
                Location = new Point(random.Next(10, 300), 10),
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                ForeColor = Color.Black
            };

            this.Controls.Add(wordLabel);
            wordLabels.Add(wordLabel);

            var fallTimer = new System.Windows.Forms.Timer { Interval = 50 };
            wordLabel.Tag = fallTimer;
            fallTimer.Tick += (s, e) =>
            {
                wordLabel.Top += 5;
                if (wordLabel.Bounds.IntersectsWith(pictureBox2.Bounds))
                {
                    fallTimer.Stop();
                    wordDropTimer.Stop();
                    label5.Text = CalculateWPM().ToString();
                    string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - SUPER MARIO  game over  sound effect.wav";
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                    MessageBox.Show("Game Over! A word touched the bottom.");
                    WPM = CalculateWPM();
                    if (WPM > Global.GlobalData.highestWPM)
                    {
                        Global.GlobalData.highestWPM = WPM;
                    }

                    ResetGame();
                }
                if (wordLabel.Top > this.Height)
                {
                    fallTimer.Stop();
                    this.Controls.Remove(wordLabel);
                    wordLabels.Remove(wordLabel);
                }
            };
            fallTimer.Start();
        }
        private double CalculateWPM()
        {
            double elapsedMinutes = stopwatch.Elapsed.TotalMinutes;
            if (elapsedMinutes > 0)
            {
                return Math.Round(totalWord / elapsedMinutes, 2);
            }
            return 0;
        }
        private void UserInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                var input = richTextBox1.Text.Trim();
                richTextBox1.Clear();
                var matchedLabel = wordLabels.FirstOrDefault(label => label.Text == input);
                if (matchedLabel != null)
                {
                    var fallTimer = matchedLabel.Tag as System.Windows.Forms.Timer;
                    if (fallTimer != null)
                    {
                        fallTimer.Stop();
                    }
                    this.Controls.Remove(matchedLabel);
                    wordLabels.Remove(matchedLabel);
                    totalWord++;
                    label1.Text = $"Score: {totalWord}";
                    string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Correct sound effect 正確音效.wav";
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }
                else
                {
                    string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Wrong Answer Sound effect.wav";
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                }    
            }
        }
        private void ResetGame()
        {
            foreach (var wordLabel in wordLabels.ToList())
            {
                var fallTimer = wordLabel.Tag as System.Windows.Forms.Timer;
                if (fallTimer != null)
                {
                    fallTimer.Stop();
                }
                this.Controls.Remove(wordLabel);
            }
            wordLabels.Clear();
            currentLevel = 1;
            label6.Text = "Level: 1";
            totalWord = 0; // Reset score
            label1.Text = $"Score: {totalWord}";
            button1.Show();
            stopwatch.Reset();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            Global.GlobalData.submitTime = DateTime.Now;
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;"; ; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO typingtest (testid, userid, submittime, WPM, numberoftest) " +
                               "VALUES (@testid, @userid, @submittime, @WPM, @numberoftest)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", 4);
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    command.Parameters.AddWithValue("@submittime", Global.GlobalData.submitTime);
                    command.Parameters.AddWithValue("@WPM", Global.GlobalData.highestWPM);
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
                string query = "SELECT COUNT(*) FROM typingtest WHERE userid = @userid";

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
