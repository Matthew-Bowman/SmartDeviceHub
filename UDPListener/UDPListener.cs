using System.Net.Sockets;
using System.Text;

public class UDPListener
{
    // Member Variables
    private int _port;
    private UdpClient? _udpClient;

    // Events
    public event EventHandler<UdpReceiveResult>? PacketReceived;

    // Constructors
    public UDPListener(int pPort)
    {
        // Member Variable Assigning
        this._port = pPort;
    }

    // Private Methods
    protected virtual void OnPacketReceived(UdpReceiveResult pResult)
    {
        PacketReceived?.Invoke(this, pResult);  // Trigger the event
    }

    // Public Methods
    public async Task StartServer(CancellationToken pCancellationToken)
    {
        // Start UDP Client
        this._udpClient = new UdpClient(this._port);

        // Listen For Packet
        while (!pCancellationToken.IsCancellationRequested) {

            try
            {

                UdpReceiveResult result = await this._udpClient.ReceiveAsync();
                OnPacketReceived(result);

            }
            catch (Exception pException) {

                Console.WriteLine($"[UDPListener] Error: {pException.Message}");

            }

        }

        // Close UDP Client
        this._udpClient.Close();
    }
}
