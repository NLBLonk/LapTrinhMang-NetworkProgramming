using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);
            Socket clientSocket = serverSocket.Accept();

            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine(clientEndPoint.ToString());

            byte[] buff;
            string hello = "Hello Client";
            buff = Encoding.ASCII.GetBytes(hello); clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);

            while (true)
            {
                try
                {
                    buff = new byte[1024];
                    int byteReceive = clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                    if (byteReceive == 0)
                    {
                        Console.WriteLine("Client da ngat ket noi. Tam biet va hen gap lai");
                        break;
                    }
                    string str = Encoding.ASCII.GetString(buff, 0, byteReceive); Console.WriteLine(str);
                    clientSocket.Send(buff, 0, byteReceive, SocketFlags.None);
                }
                catch (SocketException sx)
                {
                    Console.WriteLine("Client da ngat ket noi dot ngot. Loi: " + sx.Message);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi: " + ex.Message);
                    break;
                }

            }

            Console.ReadLine();

            // Đóng socket
            clientSocket.Close();
            serverSocket.Close();
        }
    }
}
