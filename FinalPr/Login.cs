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
    public partial class Login : Form
    {
        private string loginUsername, loginPassword;
        
            
        UserController uc = new UserController();
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form register = new Register();
            this.Hide();
            register.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginUsername = textBox1.Text;
            loginPassword = textBox2.Text;
            User loginUser = uc.userLogin(loginUsername, loginPassword);
            if (loginUser == null)
            {
                MessageBox.Show("Tai khoan hoac mat khau khong dung", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LoginInfo a = new LoginInfo();
                a.CurrentUser = loginUser;
                Global.GlobalData.gotUser = 1;
                Form form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }
    }

    internal class LoginInfo
    {
        public static User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
    }
}
