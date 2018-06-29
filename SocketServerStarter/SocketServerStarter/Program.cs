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

            // IPAddress and port number = 23000
            IPEndPoint endpoint = new IPEndPoint(ipAddress, 23000);

            try
            {
                // bind socket to endpoint
                listnerSocket.Bind(endpoint);
                // will now listen, the number supplied will tell it how many clients can wait for a connection at
                // any time while the system is busy with other connections
                listnerSocket.Listen(5);

                Console.WriteLine("About to accept incoming connection");

                // call accept methond on listener socket
                Socket client = listnerSocket.Accept();

                Console.WriteLine("Client connected. " + client.ToString() + " IP End Point: " + client.RemoteEndPoint.ToString());

                // the data is received in Byte form means we can even send images through this socket.. or any other data type
                byte[] buff = new byte[128];

                while (true)
                {
                    int numberOfReceivedBytes = 0;
                    numberOfReceivedBytes = client.Receive(buff);
                    Console.WriteLine("Number of received bytes: " + numberOfReceivedBytes);
                    Console.WriteLine("Data sent by client is: " + buff);

                    // Convert the buffer to Ascii Human readable characters
                    string receivedText = Encoding.ASCII.GetString(buff, 0, numberOfReceivedBytes);

                    Console.WriteLine("Data sent by client is: " + receivedText);

                    // Send Data Back to Client:
                    // Send the data in the buffer back to the sender.. basically echo to Sender.
                    client.Send(buff);

                    if (receivedText == "x") // this is to avoid infinite loop
                        break;

                    //clean the array buffer right before starting another read opereration, other wise data will be gobbled
                    Array.Clear(buff, 0, buff.Length);
                    numberOfReceivedBytes = 0;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }



        }
    }
}
