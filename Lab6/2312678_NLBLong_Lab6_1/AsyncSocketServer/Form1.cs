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



    }
}
