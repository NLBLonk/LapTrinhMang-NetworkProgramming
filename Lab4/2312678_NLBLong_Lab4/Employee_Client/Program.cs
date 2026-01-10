using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Client
{
    internal class Program
    {
        public static void Main()
        {
            Employee emp1 = new Employee();
            emp1.EmployeeID = 1;
            emp1.LastName = "Nguyen";
            emp1.FirstName = "Van A";
            emp1.YearsService = 12;
            emp1.Salary = 3500000;

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
        }
    }
}
