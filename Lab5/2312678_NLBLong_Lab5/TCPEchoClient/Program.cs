using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string serverIP = "127.0.0.1";
            int port = 9001;
            string message = "Data send to server";

            // Nếu có tham số từ command line
            if (args.Length >= 1) serverIP = args[0];
            if (args.Length >= 2) port = int.Parse(args[1]);
            if (args.Length >= 3) message = args[2];

            try
            {
                using (TcpClient client = new TcpClient())
                {
                    Console.WriteLine($"Attempting to connect to {serverIP}:{port}...");
                    client.Connect(serverIP, port);

                    Console.WriteLine("Connected successfully!");
                    Console.WriteLine($"Sending message: {message}");

                    // Chuyển đổi message thành mảng byte
                    byte[] data = Encoding.ASCII.GetBytes(message);

                    // Gửi dữ liệu đến server
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Sent {data.Length} bytes to server");

                    // Nhận phản hồi từ server
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"Received {bytesRead} bytes from server: '{response}'");
                    Console.WriteLine("Echo test completed successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Make sure the server is running on port {port}!");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
