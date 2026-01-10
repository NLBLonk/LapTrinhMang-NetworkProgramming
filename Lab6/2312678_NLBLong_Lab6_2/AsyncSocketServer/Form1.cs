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

namespace AsyncSocketServer
{
    public partial class Form1 : Form
    {
        AsyncSocketTCPServer mServer;
        public Form1()
        {
            InitializeComponent();
            mServer = new AsyncSocketTCPServer();
            mServer.ClientConnectedEvent += HandleClientConnected;
            mServer.ClientDisconnectedEvent += HandleClientDisconnected;
            mServer.MessageReceivedEvent += HandleMessageReceived;

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(txtMessage.Text.Trim());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs e)
        {
            txtClientInfo.AppendText(string.Format("{0} - New Client Connected - {1}\r\n", 
                DateTime.Now, e.NewClient));
        }

        void HandleClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            txtClientInfo.AppendText(string.Format("{0} - Client Disconnected - {1} - Remaining: {2}\r\n",
                DateTime.Now, e.DisconnectedClient, e.RemainingClients));
        }

        void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            txtMessageFromClient.AppendText(string.Format("{0} - From {1}: {2}\r\n",
                DateTime.Now, e.Client, e.Message));
        }



    }
}
