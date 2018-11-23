using System.Text;

namespace Forza4.Classi
{
   public class Client : UdpBase
    {
        private Client() { }

        public static Client ConnectTo(string hostname, int port)
        {
            var connection = new Client();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }

    }
}