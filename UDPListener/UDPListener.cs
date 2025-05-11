using System.Net.Sockets;

/// <summary>
/// Listens for incoming UDP packets on a specified port and raises an event when a packet is received.
/// This class abstracts the handling of UDP communication and allows subscribers to react to received packets.
/// </summary>

public class UDPListener
{
    // =====================================
    // ** FIELDS **
    // =====================================

    private int _port;
    private UdpClient? _udpClient;

    // =====================================
    // ** EVENTS **
    // =====================================
    public event EventHandler<UdpReceiveResult>? PacketReceived;

    // =====================================
    // ** CONSTRUCTORS **
    // =====================================

    /// <summary>
    /// Creates a new UDP listener bound to the specified port.
    /// </summary>
    public UDPListener(int pPort)
    {
        this._port = pPort;
    }

    // =========================================
    // ** PRIVATE METHODS **
    // =========================================

    /// <summary>
    /// Raises the PacketReceived event.
    /// </summary>
    protected virtual void OnPacketReceived(UdpReceiveResult pResult)
    {
        PacketReceived?.Invoke(this, pResult);  // Trigger the event
    }

    // ==========================================
    // ** PUBLIC METHODS **
    // ==========================================

    /// <summary>
    /// Starts listening for UDP packets until the operation is canceled.
    /// </summary>
    public async Task StartServer(CancellationToken pCancellationToken)
    {
        this._udpClient = new UdpClient(this._port);

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

        this._udpClient.Close();
    }
}
