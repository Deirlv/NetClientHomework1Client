using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.178.34");

            IPEndPoint endPoint = new IPEndPoint(ipAddress, 1024);

            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            pass_socket.Bind(endPoint);
            pass_socket.Listen(10);

            Console.WriteLine($"Server started at port {endPoint.Port}");

            try
            {
                while (true)
                {
                    Socket ns = pass_socket.Accept();

                    Console.WriteLine($"Client {ns.LocalEndPoint} was connected");
                    Console.WriteLine($"Client {ns.RemoteEndPoint} was connected");

                    ns.Send(Encoding.Default.GetBytes($"Server {ns.LocalEndPoint} sent answer in {DateTime.Now}\n"));

                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
