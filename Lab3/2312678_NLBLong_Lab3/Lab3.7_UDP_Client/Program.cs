using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._7_UDP_Client
{
    internal class Program
    {
        public class RetryUdpClient
        {
            private byte[] data;

            public RetryUdpClient()
            {
                int recv;

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Hiển thị và thiết lập timeout
                int sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
                Console.WriteLine("Gia tri timeout mac dinh: {0}", sockopt);

                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
                sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
                Console.WriteLine("Gia tri timeout moi: {0}", sockopt);

                // Gửi câu chào ban đầu
                string welcome = "xin chao Server";
                data = Encoding.ASCII.GetBytes(welcome);
                recv = SndRcvData(server, data, ipep);

                if (recv > 0)
                {
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);
                }

                // Gửi tin nhắn thứ 2
                string message2 = "xin chao";
                recv = SndRcvData(server, Encoding.ASCII.GetBytes(message2), ipep);
                if (recv > 0)
                {
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);
                }

                // Gửi tin nhắn thứ 3
                string message3 = "xin chao";
                recv = SndRcvData(server, Encoding.ASCII.GetBytes(message3), ipep);
                if (recv > 0)
                {
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);
                }

                server.Close();
            }

            private int SndRcvData(Socket s, byte[] message, EndPoint rmtdevice)
            {
                int recv;
                int retry = 0;

                while (true)
                {
                    Console.WriteLine("Truyen lai lan thu: #{0}", retry);
                    try
                    {
                        s.SendTo(message, message.Length, SocketFlags.None, rmtdevice);
                        data = new byte[1024];
                        recv = s.ReceiveFrom(data, ref rmtdevice);
                    }
                    catch (SocketException)
                    {
                        recv = 0;
                    }

                    if (recv > 0)
                    {
                        return recv;
                    }
                    else
                    {
                        retry++;
                        if (retry > 4)
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        // Main method
        static void Main(string[] args)
        {
            RetryUdpClient client = new RetryUdpClient();
            Console.ReadKey();
        }
    }
}
