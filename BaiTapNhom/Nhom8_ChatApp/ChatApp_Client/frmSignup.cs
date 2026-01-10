using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp_Client
{
    public partial class frmSignup : Form
    {
        public frmSignup()
        {
            InitializeComponent();
        }


        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSignupIP.Text) ||
                string.IsNullOrEmpty(txtSignupUsername.Text) ||
                string.IsNullOrEmpty(txtSignupPassword.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSignupPassword.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            using (TcpClient client = new TcpClient(txtSignupIP.Text, 2005))
            using (NetworkStream stream = client.GetStream())
            {
                string signupMessage = $"SIGNUP|{txtSignupUsername.Text}|SERVER|{txtSignupPassword.Text}";
                byte[] buffer = Encoding.UTF8.GetBytes(signupMessage);
                stream.Write(buffer, 0, buffer.Length);

                byte[] responseBuffer = new byte[4096];
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

                if (response.Contains("SIGNUP_SUCCESS"))
                {
                    MessageBox.Show("Registration successful!");
                    frmLogin loginForm = new frmLogin();
                    this.Hide();
                    loginForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username already exists!");
                }
            }
        }


        private void linklblSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Thread(() => Application.Run(new frmSignup())).Start();
            this.Close();
        }
    }
}
