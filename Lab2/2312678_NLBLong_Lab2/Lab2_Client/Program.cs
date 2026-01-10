using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Lab2_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Dang ket noi voi server...");
            //serverSocket.Connect(serverEndPoint);
            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Khong the ket noi den server"); return;
            }
            if (serverSocket.Connected)
            {
                byte[] buff = new byte[1024];
                Console.WriteLine("Ket noi thanh cong voi server ...");
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
            }

            while (true)
            {
                byte[] buff;
                string str = Console.ReadLine();
                if (str.ToLower() == "exit")
                {
                    serverSocket.Shutdown(SocketShutdown.Both);  // Báo hiệu sắp đóng
                    serverSocket.Close();
                    break;
                }
                buff = Encoding.ASCII.GetBytes(str);
                serverSocket.Send(buff, 0, buff.Length, SocketFlags.None); buff = new byte[1024];
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);

            }

        }
    }
}
