using Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp_Client
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linklblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Thread(() => Application.Run(new frmSignup())).Start();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLoginIP.Text) ||
           string.IsNullOrEmpty(txtLoginUsername.Text) ||
           string.IsNullOrEmpty(txtLoginPassword.Text))
            {
                MessageBox.Show("Please fill all fields to login!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckLogin(txtLoginUsername.Text, txtLoginPassword.Text))
            {
                frmClient clientForm = new frmClient(
                    txtLoginIP.Text,
                    txtLoginUsername.Text,
                    txtLoginPassword.Text
                );

                this.Hide();
                clientForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Password or User!", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckLogin(string username, string password)
        {
            foreach (var acc in List.Instance.ListAccountUser)
            {
                if (acc.userName == username && acc.password == password)
                    return true;
            }
            return false;
        }



    }
}
