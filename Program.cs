using System.Net.Sockets;
using System.Text;
using SmartDeviceHub.UDP;

namespace SmartDeviceHub
{

    /// <summary>
    /// The applications main entry point
    /// </summary>
    public class Program
    {
        // ==========================================
        // ** PROGRAM ENTRY **
        // ==========================================
        public static async Task Main()
        {
            // Declare Variables
            CancellationTokenSource cts = new();
            List<Task> tasks = [];
            int udpPort = 5000;



            // Initialize components
            UDPListener udpListener = new(udpPort);



            // Subscribe Events
            /// None yet (TBD After MessageHandler Completion)



            // Start Systems
            tasks.Add(StartUDPListenerAsync(udpListener, cts));



            await Task.WhenAll(tasks);
        }

        // ==========================================
        // ** HELPER FUNCTIONS **
        // ==========================================

        // Begins the UDP listening task
        private static Task StartUDPListenerAsync(UDPListener pUDPListener, CancellationTokenSource pCTS)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await pUDPListener.StartServer(pCTS.Token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[StartUDPListenerAsync] Error: {ex.Message}");
                }
                finally
                {
                    pUDPListener.Dispose();
                }
            });
        }
    }
}