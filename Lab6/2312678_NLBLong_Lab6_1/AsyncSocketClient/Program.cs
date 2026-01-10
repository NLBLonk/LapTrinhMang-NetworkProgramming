using AsyncSocketTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketClient
{
    internal class Program
    {
        // Giá trị mặc định
        private const string DEFAULT_IP = "127.0.0.1";
        private const int DEFAULT_PORT = 9001;

        static void Main(string[] args)
        {
            try
            {
                AsyncSocketTCPClient client = new AsyncSocketTCPClient();
                Console.WriteLine("*** Welcome to Async Socket Client ***");

                // Nhập IP Address với giá trị mặc định
                Console.Write($"Server IP Address [{DEFAULT_IP}]: ");
                string strIPAddress = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(strIPAddress))
                    strIPAddress = DEFAULT_IP;

                // Nhập Port với giá trị mặc định
                Console.Write($"Enter Port Number (0 - 65535) [{DEFAULT_PORT}]: ");
                string strPortInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(strPortInput))
                    strPortInput = DEFAULT_PORT.ToString();

                // Validate IP và Port
                if (!client.SetServerIPAddress(strIPAddress) || !client.SetPortNumber(strPortInput))
                {
                    Console.WriteLine(
                        string.Format(
                            "IP Address or port number invalid - {0} - {1} - Press a key to exit",
                            strIPAddress, strPortInput));
                    Console.ReadKey();
                    return;
                }

                // Kết nối đến server
                Console.WriteLine("Connecting to server...");
                client.ConnectToServer().Wait(5000); // Timeout 5 giây

                string strInputUser = null;

                do
                {
                    strInputUser = Console.ReadLine();
                    if (strInputUser.Trim() != "<EXIT>")
                    {
                        client.SendToServer(strInputUser).Wait(3000); // Timeout 3 giây
                    }
                    else if (strInputUser.Equals("<EXIT>"))
                    {
                        client.CloseAndDisconnect();
                    }
                } while (strInputUser != "<EXIT>");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

        }
    }
}
