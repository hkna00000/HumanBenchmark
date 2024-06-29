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
using static FinalPr.Form2;

namespace FinalPr
{
    public partial class SequenceForm : Form
    {
        private Panel[,] buttons;
        private int level = 1;
        private int layout = 3;
        private Random random;
        private List<int> fixedSequence;
        private List<Tuple<int, int>> currentPanelCoordinates;
        private int panelsToHighlight = 1;
        private int currentClickIndex = 0;
        private int buttonSize = 70;
        private int padding = 45;
        private int highestWaveCleared = 0;
        string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
        LoginInfo li = new LoginInfo();
        public SequenceForm()
        {
            random = new Random();
            InitializeComponent();
            InitializeButtons();
            GenerateFixedSequence();
            label5.Text = "Level " + GetHighestScore().ToString();
        }
        private void InitializeButtons()
        {
            buttons = new Panel[layout, layout];
            
            for (int i = 0; i < layout; i++)
            {
                for (int j = 0; j < layout; j++)
                {
                    Panel button = new Panel();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(padding + (buttonSize + padding) * j, padding + (buttonSize + padding) * i);
                    button.BackColor = Color.FromArgb(0x25, 0x73, 0xc1);
                    button.Click += Button_Click;
                    this.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }
        private float GetHighestScore()
        {
            float highestScore = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(score) FROM sequencetest WHERE userid = @userid";

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
        private void GenerateFixedSequence()
        {
            fixedSequence = Enumerable.Range(0, layout*layout).ToList();
        }
        private void ShufflePanelCoordinates()
        {
            var coordinates = new List<Tuple<int, int>>();
            for (int i = 0; i < layout; i++)
            {
                for (int j = 0; j < layout; j++)
                {
                    coordinates.Add(Tuple.Create(i, j));
                }
            }
            currentPanelCoordinates = coordinates.OrderBy(x => random.Next()).ToList();
        }

        private async Task HighlightPanels()
        {
            ShufflePanelCoordinates();
            for (int i = 0; i < panelsToHighlight; i++)
            {
                var coord = currentPanelCoordinates[fixedSequence[i]];
                buttons[coord.Item1, coord.Item2].BackColor = Color.Yellow;
                await Task.Delay(500); // Highlight the panel for 0.5 seconds
                buttons[coord.Item1, coord.Item2].BackColor = Color.FromArgb(0x25, 0x73, 0xc1); // Reset the color
                await Task.Delay(500); // Delay 0.5 second between highlights
            }
            currentClickIndex = 0;
        }
        private async void Button_Click(object sender, EventArgs e)
        {
            var clickedPanel = (Panel)sender;
            var clickedPanelCoordinates = GetPanelCoordinates(clickedPanel);
            var expectedPanelCoordinates = currentPanelCoordinates[fixedSequence[currentClickIndex]];

            if (clickedPanelCoordinates.Item1 == expectedPanelCoordinates.Item1 &&
                clickedPanelCoordinates.Item2 == expectedPanelCoordinates.Item2)
            {
                currentClickIndex++;
                if (currentClickIndex == panelsToHighlight)
                {
                    if (panelsToHighlight > highestWaveCleared)
                    {
                        highestWaveCleared = panelsToHighlight;
                        Global.GlobalData.highestSequence = highestWaveCleared;
                    }
                    panelsToHighlight++;
                    if (panelsToHighlight > 9)
                    {
                        MessageBox.Show("Congratulations! You have completed all waves.");
                        panelsToHighlight = 1; // Reset for a new game
                    }
                    label6.Text = (panelsToHighlight - 1).ToString();
                    await Task.Delay(500);
                    string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Correct sound effect 正確音效.wav";
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    player.Play();
                    await HighlightPanels();
                }
            }
            else
            {
                string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - SUPER MARIO  game over  sound effect.wav";
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
                MessageBox.Show("Incorrect sequence. Resetting.");
                panel2.Show();
                panelsToHighlight = 1;
                await Task.Delay(500);
                //await HighlightPanels();
            }
        }

        private Tuple<int, int> GetPanelCoordinates(Panel panel)
        {
            for (int i = 0; i < layout; i++)
            {
                for (int j = 0; j < layout; j++)
                {
                    if (buttons[i, j] == panel)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return Tuple.Create(-1, -1); // Invalid coordinates
        }
        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            string soundFilePath = @"C:\Users\hkna0\Desktop\C#\New folder\FinalPr\FinalPr\sound\y2mate.com - Correct sound effect 正確音效.wav";
            SoundPlayer player = new SoundPlayer(soundFilePath);
            player.Play();
            panel2.Hide();
            await HighlightPanels();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            Global.GlobalData.submitTime = DateTime.Now;
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;"; ; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO sequencetest (testid, userid, submittime, score, numberoftest) " +
                               "VALUES (@testid, @userid, @submittime, @score, @numberoftest)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", 5);
                    command.Parameters.AddWithValue("@userid", li.CurrentUser.Id);
                    command.Parameters.AddWithValue("@submittime", Global.GlobalData.submitTime);
                    command.Parameters.AddWithValue("@score", Global.GlobalData.highestSequence);
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
                string query = "SELECT COUNT(*) FROM sequencetest WHERE userid = @userid";

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
