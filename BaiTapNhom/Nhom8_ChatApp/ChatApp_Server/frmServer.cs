using Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp_Server
{
    public partial class frmServer : Form
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private Dictionary<TcpClient, string> clientUsers = new Dictionary<TcpClient, string>();
        private Thread serverThread;
        private Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
        private bool isRunning = false;

        public frmServer()
        {
            InitializeComponent();
            AddLog("Server ready. Click Start to begin.", Color.Blue);
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            String IP = null;
            var host = Dns.GetHostByName(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.ToString().Contains('.'))
                {
                    IP = ip.ToString();
                }
            }
            if (IP == null)
            {
                MessageBox.Show("No network adapters with an IPv4 address in the system!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtIP.Text = IP;
            txtPort.Text = "2005";

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txtIP.Text);
            int port = int.Parse(txtPort.Text);

            server = new TcpListener(ip, port);
            server.Start();

            isRunning = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            serverThread = new Thread(StartListening);
            serverThread.IsBackground = true;
            serverThread.Start();

            AddLog($"Server started successfully", Color.Green);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StartListening()
        {
            while (isRunning)
            {
                 TcpClient client = server.AcceptTcpClient();
                 clients.Add(client);

                 Thread clientThread = new Thread(() => HandleClient(client));
                 clientThread.IsBackground = true;
                 clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0 && isRunning)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessMessage(client, data);
                }
            }
            catch (Exception ex)
            {
                AddLog($"Client disconnected", Color.DarkRed);
            }
            finally
            {
                RemoveClient(client);
                client.Close();
            }
        }

        private void ProcessMessage(TcpClient client, string data)
        {
           string[] parts = data.Split('|');
           if (parts.Length >= 4)
           {
              string type = parts[0]; string sender = parts[1];
              string receiver = parts[2]; string content = parts[3];
              if (type == "LOGIN")
                 HandleLogin(client, sender, content);
              else if (type == "SIGNUP")  
                    HandleSignup(client, sender, content);
              else if (type == "MESSAGE")
                 HandleMessage(sender, receiver, content);
              else if (type == "LOGOUT")
                 HandleLogout(client, sender);
                else if (type == "CREATE_GROUP")
                    HandleCreateGroup(sender, content);
                else if (type == "GROUP_MESSAGE")
                    HandleGroupMessage(sender, receiver, content);
            }
        }

        // content: "TV1;Long,Quy"
        private void HandleCreateGroup(string creator, string content)
        {
            string[] parts = content.Split(';');
            if (parts.Length < 2) return;

            string groupName = parts[0];

            List<string> members = parts[1]
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Distinct()
                .ToList();

            // đảm bảo người tạo cũng có trong nhóm
            if (!members.Contains(creator))
                members.Add(creator);

            groups[groupName] = members;

            AddLog($"Group created: {groupName} ({string.Join(", ", members)})",
                   System.Drawing.Color.DarkCyan);

            // gửi thông tin nhóm cho tất cả client
            string payload = groupName + ";" + string.Join(",", members);
            string msg = $"GROUP_CREATED|SERVER|ALL|{payload}";
            BroadcastToAll(msg);
        }

        private void HandleGroupMessage(string sender, string groupName, string message)
        {
            if (!groups.ContainsKey(groupName))
                return;

            AddLog($"[Group {groupName}] {sender}: {message}",
                   System.Drawing.Color.Purple);

            string data = $"GROUP_MESSAGE|{sender}|{groupName}|{message}";

            foreach (var kvp in clientUsers)  // kvp.Key: TcpClient, kvp.Value: username
            {
                string userName = kvp.Value;
                if (groups[groupName].Contains(userName))
                {
                    SendToClient(kvp.Key, data);
                }
            }
        }

        private void HandleLogin(TcpClient client, string username, string password)
        {
            if (AuthenticateUser(username, password))
            {
                clientUsers[client] = username;
                UpdateOnlineList();
                SendToClient(client, $"LOGIN_SUCCESS|SERVER|{username}|Welcome {username}!");
                BroadcastToAll($"SYSTEM|SERVER|ALL|{username} logged in!");
                AddLog($"{username} logged in!", Color.DarkGreen);
            }
            else
            {
                SendToClient(client, $"LOGIN_FAILED|SERVER|{username}|Invalid credentials");
                AddLog($"Login failed for {username}", Color.Red);
            }
        }

        private void HandleSignup(TcpClient client, string username, string password)
        {
            // Kiểm tra username đã tồn tại chưa
            if (IsUsernameExists(username))
            {
                SendToClient(client, $"SIGNUP_FAILED|SERVER|{username}|Username already exists");
                AddLog($"Signup failed: {username} already exists", Color.Red);
            }
            else
            {
                // Thêm user mới vào danh sách
                List.Instance.ListAccountUser.Add(new Account(username, password));
                SendToClient(client, $"SIGNUP_SUCCESS|SERVER|{username}|Registration successful");
                AddLog($"New user registered: {username}", Color.Green);
            }
        }

        private bool IsUsernameExists(string username)
        {
            return List.Instance.ListAccountUser.Exists(acc => acc.userName == username);
        }


        private void HandleMessage(string sender, string receiver, string message)
        {
            if (receiver == "ALL")
            {
                BroadcastToAll($"MESSAGE|{sender}|ALL|{message}");
                AddLog($"[{sender}] {message}", Color.Black);
            }
            else
            {
                foreach (var client in clientUsers)
                {
                    if (client.Value == receiver)
                    {
                        SendToClient(client.Key, $"MESSAGE|{sender}|{receiver}|{message}");
                        AddLog($"{message}", Color.DarkBlue);
                        break;
                    }
                }
            }
        }

        private void HandleLogout(TcpClient client, string username)
        {
            RemoveClient(client);
            BroadcastToAll($"SYSTEM|SERVER|ALL|{username} left the chat");
            AddLog($"{username} logged out", Color.DarkRed);
        }

        private bool AuthenticateUser(string username, string password)
        {
            foreach (Account acc in List.Instance.ListAccountUser)
            {
                if (acc.userName == username && acc.password == password)
                    return true;
            }
            return false;
        }

        private void SendToClient(TcpClient client, string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                AddLog($"Error sending to client: {ex.Message}", Color.Orange);
            }
        }

        private void BroadcastToAll(string message)
        {
            List<TcpClient> disconnectedClients = new List<TcpClient>();
            foreach (TcpClient client in clients)
            {
                if (clientUsers.ContainsKey(client) && client.Connected)
                {
                    try
                    {
                        SendToClient(client, message);
                    }
                    catch
                    {
                        disconnectedClients.Add(client);
                    }
                }
            }

            foreach (TcpClient client in disconnectedClients)
            {
                RemoveClient(client);
            }
        }

        private void UpdateOnlineList()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateOnlineList));
                return;
            }

            lbxOnline.Items.Clear();
            int index = 0;
            foreach (string user in clientUsers.Values)
            {
                lbxOnline.Items.Add($"▶ {user}");
                index++;
            }

            // QUAN TRỌNG: Gửi danh sách online users đến tất cả client
            string onlineUsers = string.Join(",", clientUsers.Values);
            BroadcastToAll($"ONLINE_USERS|SERVER|ALL|{onlineUsers}");
        }

        private void RemoveClient(TcpClient client)
        {
            if (clientUsers.ContainsKey(client))
            {
                string username = clientUsers[client];
                clientUsers.Remove(client);
                clients.Remove(client);

                UpdateOnlineList();
                AddLog($"User {username} disconnected", Color.DarkRed);
            }
        }

        private void AddLog(string message, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(AddLog), message, color);
                return;
            }

            rtbDialog.SelectionStart = rtbDialog.TextLength;
            rtbDialog.SelectionLength = 0;
            rtbDialog.SelectionColor = color;
            rtbDialog.AppendText(message + "\n");
            rtbDialog.ScrollToCaret();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (lbxOnline.SelectedItem != null && !string.IsNullOrEmpty(txtMessage.Text))
            {
                string targetUser = lbxOnline.SelectedItem.ToString().Replace("▶", "").Trim();
                HandleMessage("SERVER", targetUser, txtMessage.Text);
                txtMessage.Clear();
            }
            else
            {
                MessageBox.Show("Please select a user and enter a message", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                BroadcastToAll($"MESSAGE|SERVER|ALL|{txtMessage.Text}");
                AddLog($"{txtMessage.Text}", Color.DarkGreen);
                txtMessage.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a message", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lbxOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxOnline.SelectedItem != null)
            {
                string selectedUser = lbxOnline.SelectedItem.ToString();
                txtName.Text = selectedUser; // Hiển thị lên txtName

                AddLog($"Selected user: {selectedUser}", System.Drawing.Color.DarkCyan);
            }
        }


        private void StopServer()
        {
            isRunning = false;
            BroadcastToAll("SYSTEM|SERVER|ALL|Server is shutting down");

            if (server != null) server.Stop();
            foreach (TcpClient client in clients)
            {
                client.Close();
            }

            clients.Clear();
            clientUsers.Clear();
            lbxOnline.Items.Clear();

            btnStart.Enabled = true;
            btnStop.Enabled = false;

            AddLog("Server stopped", Color.Red);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isRunning)
            {
                StopServer();
            }
            base.OnFormClosing(e);
        }

     
    }

}
