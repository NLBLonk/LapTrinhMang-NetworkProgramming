using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Employee_Server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            UdpClient server = new UdpClient(9050);
            IPEndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);

            // Nhận dữ liệu từ client
            byte[] receivedData = server.Receive(ref clientEP);

            // Đọc kích thước gói tin (2 byte đầu)
            int packsize = BitConverter.ToInt16(receivedData, 0);
            Console.WriteLine("Kich thuoc goi tin = {0}", packsize);

            // Tách phần dữ liệu nhân viên (bỏ qua 2 byte kích thước)
            byte[] employeeData = new byte[packsize];
            Buffer.BlockCopy(receivedData, 2, employeeData, 0, packsize);

            Employee emp1 = new Employee(employeeData);
            Console.WriteLine("emp1.EmployeeID = {0}", emp1.EmployeeID);
            Console.WriteLine("emp1.LastName = {0}", emp1.LastName);
            Console.WriteLine("emp1.FirstName = {0}", emp1.FirstName);
            Console.WriteLine("emp1.YearsService = {0}", emp1.YearsService);
            Console.WriteLine("emp1.Salary = {0}\n", emp1.Salary);

            server.Close();
        }
    }
}
