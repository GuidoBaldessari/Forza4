//Server
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Forza4.Classi
{
    public class Server : UdpBase
    {
        private IPEndPoint _listenOn;

        public Server() : this(new IPEndPoint(IPAddress.Any, 32123))
        {
        }

        public Server(IPEndPoint endpoint)
        {
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }

        public void Reply(string message, IPEndPoint endpoint)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length, endpoint);
            
        }

    }
}