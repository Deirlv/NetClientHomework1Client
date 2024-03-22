using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace NetClientHomework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPAddress address = IPAddress.Parse("192.168.178.34");

            IPEndPoint endPoint = new IPEndPoint(address, 8080);

            while(true)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                try
                {
                    socket.Connect(endPoint);
                    if (socket.Connected)
                    {
                        Console.Write("Enter message: ");
                        string message = Console.ReadLine();

                        socket.Send(Encoding.Default.GetBytes(message)); //

                        string query = "GET\r\n\r\n";
                        socket.Send(Encoding.Default.GetBytes(query));

                        byte[] buffer = new byte[1024];

                        int len;

                        do
                        {
                            len = socket.Receive(buffer);
                            Console.WriteLine(Encoding.Default.GetString(buffer, 0, len));
                        } while (socket.Available > 0);
                    }
                    else
                    {
                        Console.Error.WriteLine("Error");
                    }
                }
                finally
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            
        }
    }
}
