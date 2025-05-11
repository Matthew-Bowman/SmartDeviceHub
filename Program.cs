using System.Net.Sockets;
using System.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Declare Variables
        CancellationTokenSource cts = new CancellationTokenSource();
        int udpPort = 5000;



        // Initialize components
        var udpListener = new UDPListener(udpPort);



        // Subscribe Events
        udpListener.PacketReceived += OnUDPMessage;
        
        
        
        // Start Systems
        await udpListener.StartServer(cts.Token);
    }

    public static void OnUDPMessage(object? pSender, UdpReceiveResult pUDPResult) {
        Console.WriteLine(Encoding.UTF8.GetString(pUDPResult.Buffer));
    }
}
