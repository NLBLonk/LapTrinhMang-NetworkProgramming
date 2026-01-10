using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._3_UDP_Client
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

            clientSocket.SendTo(buff, serverEndPoint);
            Console.WriteLine("Da goi cau chao len Server...");

            // Nhận phản hồi từ server
            buff = new byte[1024];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            int byteReceive = clientSocket.ReceiveFrom(buff, ref remote);
            string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
            Console.WriteLine(str);

            while (true)
            {
                Console.Write("Nhap thong diep: ");
                str = Console.ReadLine();
                buff = Encoding.ASCII.GetBytes(str);
                clientSocket.SendTo(buff, serverEndPoint);

                buff = new byte[1024];
                byteReceive = clientSocket.ReceiveFrom(buff, ref remote);
                str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }
    }
}
