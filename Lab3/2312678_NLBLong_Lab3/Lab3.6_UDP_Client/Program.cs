using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._6_UDP_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Console.WriteLine("Dang goi cau chao len Server...");

            string message = "Hello Server";
            byte[] buff = Encoding.ASCII.GetBytes(message);
            clientSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, serverEndPoint);
            Console.WriteLine("Da goi cau chao len Server...");

            // Nhận phản hồi đầu tiên
            buff = new byte[1024];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            int byteReceive = clientSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
            string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
            Console.WriteLine(str);

            // Phần gửi và nhận với buffer nhỏ và xử lý mất dữ liệu
            int i = 10; // Kích thước buffer ban đầu
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                    break;

                // Gửi tin nhắn
                byte[] sendData = Encoding.ASCII.GetBytes(input);
                clientSocket.SendTo(sendData, 0, sendData.Length, SocketFlags.None, serverEndPoint);

                // Nhận phản hồi với buffer nhỏ
                byte[] data = new byte[i];
                try
                {
                    int recv = clientSocket.ReceiveFrom(data, 0, data.Length, SocketFlags.None, ref remote);
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Canh bao: du lieu bi mat, hay thu lai");
                    i += 10; // Tăng kích thước buffer để lần sau nhận đủ dữ liệu
                }
            }

            clientSocket.Close();
        }
    }
}
