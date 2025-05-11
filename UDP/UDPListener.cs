using System.Net.Sockets;

namespace SmartDeviceHub.UDPListener
{

    /// <summary>
    /// Listens for incoming UDP packets on a specified port and raises an event when a packet is received.
    /// This class abstracts the handling of UDP communication and allows subscribers to react to received packets.
    /// </summary>
    /// <remarks>
    /// Creates a new UDP listener bound to the specified port.
    /// </remarks>

    public class UDPListener(int pPort) : IDisposable
    {
        // =====================================
        // ** FIELDS **
        // =====================================

        private readonly int _port = pPort;
        private UdpClient? _udpClient;

        // =====================================
        // ** EVENTS **
        // =====================================
        public event EventHandler<UdpReceiveResult>? PacketReceived;

        // =========================================
        // ** PRIVATE METHODS **
        // =========================================

        /// Raises the PacketReceived event.
        protected virtual void OnPacketReceived(UdpReceiveResult pResult)
        {
            PacketReceived?.Invoke(this, pResult);  // Trigger the event
        }

        // Protected implementation of Dispose to clean up unmanaged resources
        protected virtual void Dispose(bool pDisposing)
        {
            if (pDisposing)
            {
                _udpClient?.Dispose();
            }
        }

        // ==========================================
        // ** PUBLIC METHODS **
        // ==========================================

        /// <summary>
        /// Starts listening for UDP packets until the operation is canceled.
        /// </summary>
        public async Task StartServer(CancellationToken pCancellationToken)
        {
            _udpClient = new UdpClient(_port);

            while (!pCancellationToken.IsCancellationRequested)
            {

                try
                {
                    UdpReceiveResult result = await _udpClient.ReceiveAsync(pCancellationToken);
                    OnPacketReceived(result);
                }
                catch (Exception pException)
                {
                    Console.WriteLine($"[UDPListener] Error: {pException.Message}");

                }

            }

            _udpClient.Close();
        }

        /// <summary>
        /// Disposes of the UDPListener, releasing any resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}