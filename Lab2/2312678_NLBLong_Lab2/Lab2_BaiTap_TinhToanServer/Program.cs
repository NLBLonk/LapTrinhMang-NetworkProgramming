using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_BaiTap_TinhToanServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TcpListener server = new TcpListener(5000);
            server.Start();
            Console.WriteLine("Server đang chạy...");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client đã kết nối!");

            NetworkStream stream = client.GetStream();

            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Nhận: " + data);

                if (data == "exit")
                {
                    Console.WriteLine("Client yêu cầu thoát...");
                    break;
                }

                string result = TinhToan(data);
                byte[] response = Encoding.ASCII.GetBytes(result);
                stream.Write(response, 0, response.Length);
            }

            client.Close();
            server.Stop();

            Console.WriteLine("Đóng server...");
            Environment.Exit(0);
        }

        static string TinhToan(string input)
        {
            try
            {
                string[] parts = input.Split(' ');
                double a = double.Parse(parts[0]);
                double b = double.Parse(parts[2]);
                string op = parts[1];

                double kq = 0;
                switch (op)
                {
                    case "+": kq = a + b; break;
                    case "-": kq = a - b; break;
                    case "*": kq = a * b; break;
                    case "/": kq = a / b; break;
                }

                return $"{a} {op} {b} = {kq}";
            }
            catch
            {
                return "Error!";
            }
        }
    }
}
