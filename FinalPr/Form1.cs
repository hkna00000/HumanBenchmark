using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPr
{
    public partial class Form1 : Form
    {
        LoginInfo li = new LoginInfo();
        UserController uc = new UserController();
        public Form1()
        {
            
            InitializeComponent();
            label18.Text = "Hello, " + li.CurrentUser.Username;
            panel2.MouseEnter += panel2_MouseEnter;
            panel2.MouseLeave += panel2_MouseLeave;
           
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            
            panel2.BackColor = Color.Orange;
            richTextBox1.Text = reaction;
            label17.Visible = true;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
            richTextBox1.Text = string.Empty;
            label17.Visible = false;
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        string reaction = "This is a simple tool to measure your reaction time.\r\n\r\nThe average (median) reaction time is 273 milliseconds, according to the data collected so far.\r\n\r\nIn addition to measuring your reaction time, this test is affected by the latency of your computer and monitor. Using a fast computer and low latency / high framerate monitor will improve your score.\r\n\r\nScores in this test are faster than the aim trainer test, because you can react instantly without moving the cursor.\r\n\r\nThis is discussed in further detail on the the statistics page. While an average human reaction time may fall between 200-250ms, your computer could be adding 10-50ms on top. Some modern TVs add as much as 150ms!";
        string reaction2 = "Memorize the sequence of buttons that light up, then press them in order.\r\n\r\nEvery time you finish the pattern, it gets longer.\r\n\r\nMake a mistake, and the test is over.";
        string reaction3 = "Click the targets as quickly and accurately as you can.\r\n\r\nThis tests reflexes and hand-eye coordination.\r\n\r\nOnce you've clicked 30 targets, your score and average time per target will be displayed.\r\n\r\nThis test is best taken with a mouse or tablet screen. Trackpads are difficult to score well with.";
        string reaction4 = "The average person can only remember 7 digit numbers reliably, but it's possible to do much better using mnemonic techniques. Some helpful links are provided below.";
        string reaction5 = "Chimpanzee test\r\nThis is a test of working memory, made famous by a study that found that chimpanzees consistently outperform humans on this task.\r\n\r\nIn the study, the chimps consistently outperformed humans, and some chimps were able to remember 9 digits over 90% of the time.\r\n\r\nThis test is a variant of that concept, that gets increasingly difficult every turn, starting at 4 digits, and adding one every turn. If you pass a level, the number increases. If you fail, you get a strike. Three strikes and the test is over.";
        string reaction6 = "This is a simple test of typing speed, measuring words per minute, or WPM.\r\n\r\nThe standard measure of WPM is (number of characters / 5) / (time taken). By that measurement, \"quick brown fox\" is 15 characters, including spaces.\r\n\r\nThe recorded score is WPM * Accuracy.";
        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form visual = new Form2();
            visual.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form visual = new Form2();
            visual.Show();
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form sequence = new SequenceForm();
            sequence.Show();
        }
        private void panel7_MouseEnter(object sender, EventArgs e)
        {

            panel7.BackColor = Color.Orange;
            richTextBox1.Text = reaction2;
            label17.Visible = true;
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.White;
            richTextBox1.Text = string.Empty;
            label17.Visible = false;
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.Orange;
            richTextBox1.Text = reaction;
            label17.Visible = true;
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = Color.White;
            richTextBox1.Text = string.Empty;
            label17.Visible = false;
        }

        private void panel9_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form aimtrainer = new AimTrainerForm();
            aimtrainer.Show();
        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form numberTest = new NumberTest();
            numberTest.Show();
        }

        private void panel8_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.Orange;
            richTextBox1.Text = reaction4;
            label17.Visible = true;
        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = Color.White;
            richTextBox1.Text = string.Empty;
            label17.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form aimtrainer = new AimTrainerForm();
            aimtrainer.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form numberTest = new NumberTest();
            numberTest.Show();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form leaderBoard = new Leaderboard();
            leaderBoard.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form chimpanzeeTest = new ChimpanzeeTest();
            chimpanzeeTest.Show();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel10_MouseEnter(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Orange;
            richTextBox1.Text = reaction;
            label17.Visible = true;
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            panel10.BackColor = Color.White;
            richTextBox1.Text = string.Empty;
            label17.Visible = false;
        }

        private void panel10_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form chimpanzeeTest = new ChimpanzeeTest();
            chimpanzeeTest.Show();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form wordTest = new WordTest();
            wordTest.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form wordTest = new WordTest();
            wordTest.Show();
        }

        private void panel11_MouseEnter(object sender, EventArgs e)
        {
            panel11.BackColor = Color.Orange;
            richTextBox1.Text = reaction6;
            label17.Visible = true;
        }

        private void panel11_MouseLeave(object sender, EventArgs e)
        {
            panel11.BackColor = Color.White;
            richTextBox1.Text = string.Empty;
            label17.Visible = false;
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            User loginUser = uc.userLogin(null, null);
            LoginInfo li = new LoginInfo();
            li.CurrentUser = loginUser;
            Form login = new Login();
            login.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            User loginUser = uc.userLogin(null, null);
            LoginInfo li = new LoginInfo();
            li.CurrentUser = loginUser;
            Form login = new Login();
            login.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            User loginUser = uc.userLogin(null, null);
            LoginInfo li = new LoginInfo();
            li.CurrentUser = loginUser;
            Form login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
