using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Employee_Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Employee emp1 = new Employee();
            emp1.EmployeeID = 1;
            emp1.LastName = "Nguyen";
            emp1.FirstName = "Van A";
            emp1.YearsService = 12;
            emp1.Salary = 3500000;

            UdpClient client = new UdpClient();
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            byte[] data = emp1.GetBytes();
            int size = emp1.size;
            byte[] packsize = BitConverter.GetBytes((short)size);

            Console.WriteLine("Kich thuoc goi tin = {0}", size);

            // Gộp kích thước và dữ liệu thành một mảng
            byte[] sendData = new byte[2 + size];
            Buffer.BlockCopy(packsize, 0, sendData, 0, 2);
            Buffer.BlockCopy(data, 0, sendData, 2, size);

            // Gửi dữ liệu
            client.Send(sendData, sendData.Length, serverEP);

            client.Close();
        }
    }
}
