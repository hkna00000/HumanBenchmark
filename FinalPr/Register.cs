using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FinalPr
{
    public partial class Register : Form
    {
        private string loginUsername, loginPassword;
        UserController uc = new UserController();
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginUsername = textBox2.Text;
            loginPassword = textBox1.Text;
            if (loginUsername.Trim().Length != 0 && loginPassword.Trim().Length != 0)
            {
                Boolean a = uc.register(loginUsername, loginPassword);
                if (a)
                {
                    MessageBox.Show("Da dang ky thanh cong", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoginInfo li = new LoginInfo();
                    li.CurrentUser = null;
                    Form form1 = new Login();
                    form1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Dang ky khong thanh cong, vui long kiem tra lai thong tin", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ban dien thieu thong tin, vui long khong de trong", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            this.Hide();
            login.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
