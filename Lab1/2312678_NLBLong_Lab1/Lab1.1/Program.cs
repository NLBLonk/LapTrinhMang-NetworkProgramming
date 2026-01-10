using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lab1._1
{
    internal class Program
    {
        static void GetHostInfo(string host)
        {
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(host);
                Console.WriteLine("Ten mien: " + hostInfo.HostName);
                Console.WriteLine("Dia chi IP: ");
                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                    Console.Write(ipaddr.ToString() + " ");
                }
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Khong phan giai duoc ten mien:" + host + "\n");
            }
        }
        static void Main(string[] args)
        {
            foreach (String arg in args) 
            { 
                Console.WriteLine("Phan giai ten mien:" + arg); 
                GetHostInfo(arg); 
            }
            Console.ReadKey();
        }
    }
}
