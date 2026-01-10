using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._1_UDP_Client
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

            while (true)
            {
                Console.Write("Nhap tin nhan (exit de dong client, exit all de dong ca server): ");
                string input = Console.ReadLine();

                buff = Encoding.ASCII.GetBytes(input);
                clientSocket.SendTo(buff, serverEndPoint);

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Dong client...");
                    break;
                }
                else if (input.ToLower() == "exit all")
                {
                    Console.WriteLine("Gui yeu cau dong server...");
                    break;
                }
            }

            clientSocket.Close();
            Console.WriteLine("Da dong ket noi");
            Console.ReadKey();
        }
    }
}
