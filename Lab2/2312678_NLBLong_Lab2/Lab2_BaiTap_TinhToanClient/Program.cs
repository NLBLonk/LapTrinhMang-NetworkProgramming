using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_BaiTap_TinhToanClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("Đã kết nối server!");
            Console.WriteLine("Biểu thức cộng trừ nhân chia (+, -, *, /)");
            Console.WriteLine("Nhập với biểu thức như sao: {số1} {phéptính} {số2} (ví dụ: 5 + 3)");
            Console.WriteLine("Nhập 'exit' để thoát");

            while (true)
            {
                Console.Write("Nhập: ");
                string input = Console.ReadLine();

                if (input == "exit")
                {
                    byte[] exitdata = Encoding.ASCII.GetBytes("exit");
                    stream.Write(exitdata, 0, exitdata.Length);
                    break;
                }

                // Gửi lên server
                byte[] data = Encoding.ASCII.GetBytes(input);
                stream.Write(data, 0, data.Length);

                // Nhận kết quả
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string result = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Kết quả: " + result);
            }

            client.Close();
        }
    }
}
