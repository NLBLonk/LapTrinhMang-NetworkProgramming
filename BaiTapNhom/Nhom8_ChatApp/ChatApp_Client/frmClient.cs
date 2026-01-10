using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatApp_Client
{
    public partial class frmClient : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread listenThread;
        private bool isConnected = false;
        private string username;

        public frmClient(string serverIP, string username, string password)
        {
            InitializeComponent();
            ConnectToServer(serverIP, username, password);
        }

        private void ConnectToServer(string serverIP, string username, string password)
        {
            try
            {
                client = new TcpClient(serverIP, 2005);
                stream = client.GetStream();
                isConnected = true;
                this.username = username;

                // Gửi thông tin đăng nhập
                string loginMessage = $"LOGIN|{username}|SERVER|{password}";
                byte[] buffer = Encoding.UTF8.GetBytes(loginMessage);
                stream.Write(buffer, 0, buffer.Length);

                // Bắt đầu lắng nghe tin nhắn
                listenThread = new Thread(ListenForMessages);
                listenThread.IsBackground = true;
                listenThread.Start();

                this.Text = $"Messenger - {username}";
                AddChatLog("Connected to server successfully!", System.Drawing.Color.Green);

                txtMessage.Enabled = true;
                btnSend.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ListenForMessages()
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while (isConnected)
            {
                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessServerMessage(message);
                }
                catch
                {
                    if (isConnected)
                    {
                        AddChatLog("Connection lost!", System.Drawing.Color.Red);
                        Disconnect();
                    }
                    break;
                }
            }
        }

        private void ProcessServerMessage(string message)
        {
            string[] parts = message.Split('|');
            if (parts.Length >= 4)
            {
                string type = parts[0];
                string sender = parts[1];
                string receiver = parts[2];
                string content = parts[3];

                if (type == "LOGIN_SUCCESS")
                {
                    AddChatLog("Login successful! Welcome to chat.", System.Drawing.Color.Green);
                }
                else if (type == "LOGIN_FAILED")
                {
                    AddChatLog("Login failed: Invalid credentials", System.Drawing.Color.Red);
                    Disconnect();
                }
                else if (type == "MESSAGE")
                {
                    if (sender == "SERVER")
                        AddChatLog($"[SERVER] {content}", System.Drawing.Color.Blue);
                    else
                        AddChatLog($"[{sender} to SERVER] {content}", System.Drawing.Color.Black);
                }
                else if (type == "SYSTEM")
                {
                    AddChatLog($"[SYSTEM] {content}", System.Drawing.Color.Gray);
                }
                else if (type == "ONLINE_USERS")
                {
                    UpdateOnlineUsers(content); // Gọi update danh sách online
                }

                else if (type == "GROUP_CREATED")
                {
                    HandleGroupCreated(content);
                }
                else if (type == "GROUP_MESSAGE")
                {
                    HandleGroupMessageFromServer(sender, receiver, content);
                }
            }
        }

        // content: "TV1;Long,Quy"
        private void HandleGroupCreated(string content)
        {
            string[] parts = content.Split(';');
            if (parts.Length < 2) return;

            string groupName = parts[0];

            string[] members = parts[1]
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // chỉ add group nếu username hiện tại nằm trong danh sách member
            if (members.Contains(username))
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(AddGroupToList), groupName);
                }
                else
                {
                    AddGroupToList(groupName);
                }

                AddChatLog($"You were added to group {groupName}",
                           System.Drawing.Color.Purple);
            }
        }

        private void AddGroupToList(string groupName)
        {
            if (!lbxGroupChat.Items.Contains(groupName))
                lbxGroupChat.Items.Add(groupName);
        }

        // Tin nhắn từ server gửi về cho group
        private void HandleGroupMessageFromServer(string sender, string groupName, string content)
        {
            AddChatLog($"{sender}<{groupName}>: {content}", System.Drawing.Color.Purple);
        }

        private void UpdateOnlineUsers(string usersData)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateOnlineUsers), usersData);
                return;
            }

            lbxOnline.Items.Clear();

            string[] onlineUsers = usersData.Split(',');
            foreach (string user in onlineUsers)
            {
                if (!string.IsNullOrEmpty(user) && user != username)
                {
                    lbxOnline.Items.Add($"▶ {user}");
                }
            }



            AddChatLog($"Online users updated: {lbxOnline.Items.Count} users", System.Drawing.Color.DarkCyan);
        }

        private void AddChatLog(string message, System.Drawing.Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, System.Drawing.Color>(AddChatLog), message, color);
                return;
            }

            rtbDialog.SelectionStart = rtbDialog.TextLength;
            rtbDialog.SelectionLength = 0;
            rtbDialog.SelectionColor = color;
            rtbDialog.AppendText($"{DateTime.Now:HH:mm:ss} {message}\n");
            rtbDialog.ScrollToCaret();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(txtMessage.Text) && isConnected)
            {
                string type = "MESSAGE";
                string receiver = "ALL";

                if (lbxOnline.SelectedItem != null)
                {
                    receiver = lbxOnline.SelectedItem.ToString().Replace("▶", "").Trim();
                    type = "MESSAGE"; // chat riêng
                }
                else if (lbxGroupChat.SelectedItem != null)
                {
                    receiver = lbxGroupChat.SelectedItem.ToString().Trim();
                    type = "GROUP_MESSAGE"; // chat nhóm
                }

                string message = $"{type}|{username}|{receiver}|{txtMessage.Text}";

                try
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);

                    if (receiver == "ALL")
                        AddChatLog($"[You] {txtMessage.Text}", System.Drawing.Color.DarkGreen);
                    else if (lbxOnline.SelectedItem != null)
                        AddChatLog($"[You to {receiver}] {txtMessage.Text}", System.Drawing.Color.DarkBlue);
                    else
                        AddChatLog($"[You to group {receiver}] {txtMessage.Text}", System.Drawing.Color.Purple);

                    txtMessage.Clear();
                    txtMessage.Focus();
                }
                catch (Exception ex)
                {
                    AddChatLog($"Send failed: {ex.Message}", System.Drawing.Color.Red);
                }
            }
        }


        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            var f = new frmCreateGroup(username);
            f.GroupCreated += FrmCreateGroup_GroupCreated;   // đăng ký event
            f.ShowDialog(this);

  

        }

        private void FrmCreateGroup_GroupCreated(string groupName, List<string> members)
        {
            // content: "TV1;Long,Quy"
            string content = groupName + ";" + string.Join(",", members);

            string msg = $"CREATE_GROUP|{username}|SERVER|{content}";

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                stream.Write(buffer, 0, buffer.Length);

                AddChatLog($"Created group: {groupName}", System.Drawing.Color.Green);
            }
            catch (Exception ex)
            {
                AddChatLog($"Create group failed: {ex.Message}", System.Drawing.Color.Red);
            }
        }

        private void Disconnect()
        {
            isConnected = false;
            if (client != null && client.Connected)
            {
               string logoutMessage = $"LOGOUT|{username}|SERVER|";
               byte[] buffer = Encoding.UTF8.GetBytes(logoutMessage);
               stream.Write(buffer, 0, buffer.Length);
               client.Close();
            }

            AddChatLog("Disconnected from server", System.Drawing.Color.Red);
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
            base.OnFormClosing(e);
        }

        private void lbxOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxOnline.SelectedIndex != -1)
            {
                lbxGroupChat.ClearSelected();
            }
        }

        private void lbxGroupChat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxGroupChat.SelectedIndex != -1)
            {
                lbxOnline.ClearSelected();
            }
        }

        private void lbxOnline_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbxOnline.IndexFromPoint(e.Location);

            if (index == ListBox.NoMatches)
            {
                // click vào khoảng trống -> TRỐNG
                lbxOnline.ClearSelected();
                lbxGroupChat.ClearSelected();
            }
        }

        private void lbxGroupChat_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbxGroupChat.IndexFromPoint(e.Location);

            if (index == ListBox.NoMatches)
            {
                lbxGroupChat.ClearSelected();
                lbxOnline.ClearSelected();
            }
        }
    }
}