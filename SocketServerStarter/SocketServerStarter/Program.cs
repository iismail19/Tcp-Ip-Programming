using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketServerStarter
{
    class Program
    {
        static void Main(string[] args)
        {

            // takes 3 Paramaters, Param 1: means we are useing IPv4, 2: using streaming, 3: TCP protocol
            Socket listnerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Any; // this means it will listen to incoming connections on any availble IP-address

            IPEndPoint endpoint = new IPEndPoint(ipAddress, 23000); // IPAddress and port number = 23000

            // bind socket to endpoint
            listnerSocket.Bind(endpoint);
            // will now listen, the number supplied will tell it how many clients can wait for a connection at
            // any time while the system is busy with other connections
            listnerSocket.Listen(5);

            // call accept methond on listener socket
            listnerSocket.Accept();
        }
    }
}
