using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._4_UDP_Server
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

            int bytesReceived = serverSocket.ReceiveFrom(buff, ref remote);
            string message = Encoding.ASCII.GetString(buff, 0, bytesReceived);

            Console.WriteLine("Da ket noi voi client: " + remote.ToString());
            Console.WriteLine(message);

            serverSocket.SendTo(buff, 0, bytesReceived, SocketFlags.None, remote);

            while (true)
            {
                buff = new byte[1024];
                bytesReceived = serverSocket.ReceiveFrom(buff, ref remote);
                message = Encoding.ASCII.GetString(buff, 0, bytesReceived);
                Console.WriteLine(message);

                serverSocket.SendTo(buff, 0, bytesReceived, SocketFlags.None, remote);
            }

            Console.ReadKey();
        }
    }
}
