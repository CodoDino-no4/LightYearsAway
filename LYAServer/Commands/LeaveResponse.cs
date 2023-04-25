using System.Net;

namespace LYAServer.Commands
{
    /// <summary>
    /// Remove client from server client list
    /// </summary>
    public class LeaveResponse : CommandManager.ICommand
    {
        private IPEndPoint remoteEp;
        private List<ClientInfo> clients;

        public ServerPacket packetSend;
        public byte[] data;

        public LeaveResponse(IPEndPoint remoteEp, List<ClientInfo> clients) : base()
        {
            this.remoteEp = remoteEp;
            this.clients = clients;

            data = new byte[0];
            packetSend = new ServerPacket();
        }

        public void Execute()
        {
            var client = clients.Find(c => c.ep.Equals(remoteEp));
            var clientId = 0;

            if (client != null)
            {
                clientId = client.id;
                clients.Remove(client);

                Console.WriteLine($"Client ID: {clientId} has disconnected from the server");

                data = packetSend.ServerSendPacket("Leave", clientId, 0, 0, clients.Count().ToString());
            }
            else
            {
                data = packetSend.ServerSendPacket("Error", clientId, 0, 0, "Error on leaving server");
            }
        }

    }
}
