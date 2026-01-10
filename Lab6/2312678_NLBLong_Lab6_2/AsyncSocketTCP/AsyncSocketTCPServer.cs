using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class AsyncSocketTCPServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTCPListener;

        List<TcpClient> mClients;

        public EventHandler<ClientConnectedEventArgs> ClientConnectedEvent;
        public EventHandler<ClientDisconnectedEventArgs> ClientDisconnectedEvent;
        public EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        protected virtual void OnClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = ClientConnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClientDisconnectedEvent(ClientDisconnectedEventArgs e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = ClientDisconnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMessageReceivedEvent(MessageReceivedEventArgs e)
        {
            EventHandler<MessageReceivedEventArgs> handler = MessageReceivedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool KeepRunning { get; set; }

        public AsyncSocketTCPServer()
        {
            mClients = new List<TcpClient>();
        }

        public async void StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 9001)
        {
            if (ipaddr == null)
            {
                ipaddr = IPAddress.Any;
            }

            if (port <= 0)
            {
                port = 9001;
            }

            mIP = ipaddr;
            mPort = port;

            System.Diagnostics.Debug.WriteLine(string.Format("IP Address: {0} - Port: {1}", mIP.ToString(), mPort));

            mTCPListener = new TcpListener(mIP, mPort);

            try
            {
                mTCPListener.Start();

                KeepRunning = true;

                while (KeepRunning)
                {
                    var returnedByAccept = await mTCPListener.AcceptTcpClientAsync();

                    mClients.Add(returnedByAccept);

                    OnClientConnectedEvent(new ClientConnectedEventArgs(returnedByAccept.Client.RemoteEndPoint.ToString()));

                    Debug.WriteLine(
                        string.Format("Client connected successfully, count: {0} - {1}",
                        mClients.Count, returnedByAccept.Client.RemoteEndPoint)
                    );
                    TakeCareOfTCPClient(returnedByAccept);
                }
            }
            catch (Exception excp)
            {
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }

        public void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                string clientInfo = paramClient.Client.RemoteEndPoint.ToString();
                mClients.Remove(paramClient);

                // Thông báo client ngắt kết nối
                OnClientDisconnectedEvent(new ClientDisconnectedEventArgs(
                    clientInfo,
                    mClients.Count
                ));

                Debug.WriteLine(String.Format("Client removed, count: {0}", mClients.Count));
            }
        }

        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[64];

                while (KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");

                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    System.Diagnostics.Debug.WriteLine("Returned: " + nRet);

                    if (nRet == 0)
                    {
                        RemoveClient(paramClient);
                        System.Diagnostics.Debug.WriteLine("Socket disconnected");
                        break;
                    }

                    string receivedText = new string(buff, 0, nRet);

                    System.Diagnostics.Debug.WriteLine("*** RECEIVED: " + receivedText);

                    // Gửi sự kiện tin nhắn nhận được
                    OnMessageReceivedEvent(new MessageReceivedEventArgs(
                        paramClient.Client.RemoteEndPoint.ToString(),
                        receivedText
                    ));

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception excp)
            {
                RemoveClient(paramClient);
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }

        public async void SendToAll(string leMessage)
        {
            if (string.IsNullOrEmpty(leMessage)) return;

            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage);
                foreach (TcpClient c in mClients)
                {
                    await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                if (mTCPListener != null) mTCPListener.Stop();

                foreach (TcpClient c in mClients)
                {
                    c.Close();
                }
                mClients.Clear();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }



    }
}
