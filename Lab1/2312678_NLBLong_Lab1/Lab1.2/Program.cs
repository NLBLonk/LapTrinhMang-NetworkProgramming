using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("THONG TIN GIAO THUC IP CUA LOCAL HOST");
            Console.WriteLine("=====================================");

            HienThiThongTinIPLocalHost();

            Console.WriteLine("\nNhan phim bat ky de thoat...");
            Console.ReadKey();
        }

        static void HienThiThongTinIPLocalHost()
        {
            // Lấy tất cả các interface mạng
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                // Chỉ hiển thị các interface đang hoạt động và có hỗ trợ IPv4
                if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                    networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    HienThiThongTinNetworkInterface(networkInterface);
                    Console.WriteLine("-------------------------------------");
                }
            }
        }

        static void HienThiThongTinNetworkInterface(NetworkInterface networkInterface)
        {
            Console.WriteLine($"\nGiao thuc mang: {networkInterface.Name}");
            Console.WriteLine($"Mo ta: {networkInterface.Description}");

            // Lấy thông tin IP của interface
            IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

            // Hiển thị địa chỉ IP và subnet mask
            HienThiDiaChiIPVaSubnetMask(ipProperties);

            // Hiển thị default gateway
            HienThiDefaultGateway(ipProperties);
        }

        static void HienThiDiaChiIPVaSubnetMask(IPInterfaceProperties ipProperties)
        {
            UnicastIPAddressInformationCollection unicastAddresses = ipProperties.UnicastAddresses;

            foreach (UnicastIPAddressInformation unicastAddress in unicastAddresses)
            {
                if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine($"Dia chi IP: {unicastAddress.Address}");
                    Console.WriteLine($"Subnet Mask: {unicastAddress.IPv4Mask}");
                }
            }
        }

        static void HienThiDefaultGateway(IPInterfaceProperties ipProperties)
        {
            GatewayIPAddressInformationCollection gatewayAddresses = ipProperties.GatewayAddresses;

            if (gatewayAddresses.Count > 0)
            {
                foreach (GatewayIPAddressInformation gatewayAddress in gatewayAddresses)
                {
                    Console.WriteLine($"Default Gateway: {gatewayAddress.Address}");
                }
            }
            else
            {
                Console.WriteLine("Default Gateway: Khong co");
            }
        }
    }
}
