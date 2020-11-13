using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PortListener
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please enter a port number on the command line.");
                return;
            }

            try
            {
                var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(new IPEndPoint(IPAddress.Any, Convert.ToInt32(args[0])));
                server.Listen(int.MaxValue);
                while (true)
                {
                    var client = server.Accept();
                    var buffer = new byte[1024];
                    int count;
                    var stringBuffer = new StringBuilder();
                    do
                    {
                        count = client.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                        stringBuffer.Append(Encoding.ASCII.GetString(buffer, 0, count));
                    } while (count == buffer.Length);

                    Console.WriteLine(stringBuffer.ToString());
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: " + ex.Message);
            }
        }
    }
}