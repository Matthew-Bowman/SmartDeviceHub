using System.Net.Sockets;
using System.Text;

namespace SmartDeviceHub
{

    /// <summary>
    /// The applications main entry point
    /// </summary>
    public class Program
    {
        public static async Task Main()
        {
            // Declare Variables
            CancellationTokenSource cts = new CancellationTokenSource();
            List<Task> tasks = new List<Task>();
            int udpPort = 5000;



            // Initialize components
            var udpListener = new UDPListener(udpPort);



            // Subscribe Events
            udpListener.PacketReceived += OnUDPMessage;



            // Start Systems
            tasks.Add(Task.Run(() => udpListener.StartServer(cts.Token)));

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// A temporary method for tesing the event subscription.
        /// </summary>
        public static void OnUDPMessage(object? pSender, UdpReceiveResult pUDPResult)
        {
            Console.WriteLine(Encoding.UTF8.GetString(pUDPResult.Buffer));
        }
    }
}