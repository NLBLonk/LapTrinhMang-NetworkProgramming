using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpEchoServerThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int serverPort;
            if (args.Length != 1)
            {
                Console.WriteLine("No port specified, using default port 9001");
                serverPort = 9001;  // Port mặc định
            }
            else
            {
                serverPort = Int32.Parse(args[0]);
            }

            TcpListener listener = new TcpListener(IPAddress.Any, serverPort);
            ILogger logger = new ConsoleLogger();

            Console.WriteLine($"Server starting on port {serverPort}...");
            listener.Start();
            Console.WriteLine($"Server listening on port {serverPort}");

            listener.Start();
            for (; ; )
            {
                try
                {
                    Socket client = listener.AcceptSocket();
                    EchoProtocol protocol = new EchoProtocol(client, logger);
                    Thread thread = new Thread(new ThreadStart(protocol.handleclient));
                    thread.Start();
                    logger.writeEntry("Created and started Thread = " + thread.GetHashCode());
                }
                catch (System.IO.IOException e)
                {
                    logger.writeEntry("Error:" + e.Message);
                }
            }
        }
    }
}
