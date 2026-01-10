using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._4_UDP_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Sử dụng Connect để thiết lập kết nối
            clientSocket.Connect(serverEndPoint);

            Console.WriteLine("Dang goi cau chao len Server...");

            string message = "Hello Server";
            byte[] buff = Encoding.ASCII.GetBytes(message);

            // Sau khi Connect, có thể dùng Send() thay vì SendTo()
            clientSocket.Send(buff);
            Console.WriteLine("Da goi cau chao len Server...");

            // Nhận phản hồi từ server (dùng Receive() thay vì ReceiveFrom())
            buff = new byte[1024];
            int bytesReceived = clientSocket.Receive(buff);
            string str = Encoding.ASCII.GetString(buff, 0, bytesReceived);
            Console.WriteLine(str);

            while (true)
            {
                Console.Write("Nhap thong diep: ");
                str = Console.ReadLine();

                if (str.ToLower() == "exit")
                    break;

                buff = Encoding.ASCII.GetBytes(str);
                // Gửi tin nhắn (dùng Send() thay vì SendTo())
                clientSocket.Send(buff);

                // Nhận phản hồi (dùng Receive() thay vì ReceiveFrom())
                buff = new byte[1024];
                bytesReceived = clientSocket.Receive(buff);
                str = Encoding.ASCII.GetString(buff, 0, bytesReceived);
                Console.WriteLine(str);
            }

            clientSocket.Close();
        }
    }
}
