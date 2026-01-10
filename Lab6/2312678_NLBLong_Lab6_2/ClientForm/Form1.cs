using AsyncSocketTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        AsyncSocketTCPClient client;
        public Form1()
        {
            InitializeComponent();
            client = new AsyncSocketTCPClient();
            client.MessageReceivedEvent += HandleMessageReceived;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ipAddress = txtIPAddress.Text.Trim();
            string port = txtPort.Text.Trim();

            if (!client.SetServerIPAddress(ipAddress) || !client.SetPortNumber(port))
            {
                MessageBox.Show("IP Address or port number invalid!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                client.ConnectToServer();
                txtServer.AppendText($"Connected to server: {ipAddress}:{port}\r\n");
                btnConnect.Enabled = false;
                btnSend.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            txtServer.AppendText($"{e.Client}: {e.Message}\r\n");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtClient.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                client.SendToServer(message);
                txtServer.AppendText($"Me: {message}\r\n");
                txtClient.Clear();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.CloseAndDisconnect();
        }
    }
}
