using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._7_UDP_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverSocket.Bind(serverEndPoint);

            Console.WriteLine("Dang cho Client ket noi den...");

            byte[] buff = new byte[1024];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                buff = new byte[1024];
                int byteReceive = serverSocket.ReceiveFrom(buff, ref remote);
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);

                Console.WriteLine("Thong diep duoc nhan tu " + remote.ToString() + ":");
                Console.WriteLine(str);

                // Gửi phản hồi
                string response = "Hello Client";
                if (str == "xin chao")
                    response = "xin chao";

                byte[] responseData = Encoding.ASCII.GetBytes(response);
                serverSocket.SendTo(responseData, responseData.Length, SocketFlags.None, remote);
            }
        }
    }
}
