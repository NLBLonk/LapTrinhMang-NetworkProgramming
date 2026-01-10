using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._5_UDP_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverSocket.Bind(serverEndPoint);

            Console.WriteLine("Dang cho client ket noi toi...");

            byte[] buff = new byte[1024];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            int byteReceive = serverSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
            string str = Encoding.ASCII.GetString(buff, 0, byteReceive);

            Console.WriteLine("Da ket voi client: " + remote.ToString());
            Console.WriteLine(str);

            // Nhận 5 thông điệp tiếp theo
            for (int i = 1; i <= 5; i++)
            {
                buff = new byte[1024];
                byteReceive = serverSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
                str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }
    }
}
