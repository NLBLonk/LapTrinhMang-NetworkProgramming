using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Server_Advanced_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            TcpListener server = new TcpListener(IPAddress.Any, 9050);
            server.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream ns = client.GetStream();

                byte[] size = new byte[2];
                int recv = ns.Read(size, 0, 2);
                int packsize = BitConverter.ToInt16(size, 0);
                Console.WriteLine("Kich thuoc goi tin = {0}", packsize);

                recv = ns.Read(data, 0, packsize);
                Employee emp1 = new Employee(data);

                // Xuất ra màn hình
                Console.WriteLine("\n=== THONG TIN NHAN VIEN MOI ===");
                Console.WriteLine("EmployeeID = {0}", emp1.EmployeeID);
                Console.WriteLine("LastName = {0}", emp1.LastName);
                Console.WriteLine("FirstName = {0}", emp1.FirstName);
                Console.WriteLine("YearsService = {0}", emp1.YearsService);
                Console.WriteLine("Salary = {0}", emp1.Salary);

                // Ghi vào file (mở/đóng file mỗi lần ghi)
                using (StreamWriter writer = new StreamWriter("employees.txt", true))
                {
                    writer.WriteLine("=== THONG TIN NHAN VIEN ===");
                    writer.WriteLine("EmployeeID: {0}", emp1.EmployeeID);
                    writer.WriteLine("LastName: {0}", emp1.LastName);
                    writer.WriteLine("FirstName: {0}", emp1.FirstName);
                    writer.WriteLine("YearsService: {0}", emp1.YearsService);
                    writer.WriteLine("Salary: {0}", emp1.Salary);
                    writer.WriteLine("-----------------------------------");
                }

                // Hiển thị toàn bộ danh sách từ file
                Console.WriteLine("\n=== TOAN BO DANH SACH TU FILE ===");
                try
                {
                    if (File.Exists("employees.txt"))
                    {
                        string[] allLines = File.ReadAllLines("employees.txt");
                        foreach (string line in allLines)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("File chua co du lieu");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi doc file: " + ex.Message);
                }
                Console.WriteLine("===================================\n");

                ns.Close();
                client.Close();
            }
        }
    }
}
