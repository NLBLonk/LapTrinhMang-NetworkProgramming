using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Client_Advanced_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string continueAnswer;

            do
            {
                Employee emp1 = new Employee();

                Console.WriteLine("=== NHAP THONG TIN NHAN VIEN ===");
                Console.Write("Nhap EmployeeID: ");
                emp1.EmployeeID = int.Parse(Console.ReadLine());
                Console.Write("Nhap LastName: ");
                emp1.LastName = Console.ReadLine();
                Console.Write("Nhap FirstName: ");
                emp1.FirstName = Console.ReadLine();
                Console.Write("Nhap YearsService: ");
                emp1.YearsService = int.Parse(Console.ReadLine());
                Console.Write("Nhap Salary: ");
                emp1.Salary = double.Parse(Console.ReadLine());

                // Kết nối và gửi dữ liệu
                TcpClient client;
                try
                {
                    client = new TcpClient("127.0.0.1", 9050);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Khong ket noi duoc voi server");
                    return;
                }

                NetworkStream ns = client.GetStream();
                byte[] data = emp1.GetBytes();
                int size = emp1.size;
                byte[] packsize = new byte[2];
                Console.WriteLine("Kich thuoc goi tin = {0}", size);
                packsize = BitConverter.GetBytes((short)size);
                ns.Write(packsize, 0, 2);
                ns.Write(data, 0, size);
                ns.Flush();

                ns.Close();
                client.Close();

                // Hỏi người dùng có tiếp tục không
                Console.Write("\nBan co muon tiep tuc? (Nhap 'Khong' de thoat): ");
                continueAnswer = Console.ReadLine();

            } while (!continueAnswer.Equals("Khong", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Chuong trinh ket thuc.");
        }
    }
}
