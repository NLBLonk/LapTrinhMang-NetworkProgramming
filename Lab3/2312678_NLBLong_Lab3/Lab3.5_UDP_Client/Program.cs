using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._5_UDP_Client
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

            // Gửi 5 thông điệp phân biệt
            for (int i = 1; i <= 5; i++)
            {
                message = "Thong Diep " + i.ToString();
                buff = Encoding.ASCII.GetBytes(message);
                clientSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, serverEndPoint);
            }

            Console.ReadKey();
        }
    }
}
