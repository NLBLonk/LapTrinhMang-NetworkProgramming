using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class MessageReceivedEventArgs
    {
        public string Client { get; set; }
        public string Message { get; set; }

        public MessageReceivedEventArgs(string client, string message)
        {
            Client = client;
            Message = message;
        }
    }
}
