using System.Net;
using System.Numerics;

namespace LYAServer.Commands
{
    /// <summary>
    /// Handle place data
    /// </summary>
    public class PlaceResponse : CommandManager.ICommand
    {
        private IPEndPoint remoteEp;
        private List<ClientInfo> clients;
        private List<Vector2> tiles;

        public ServerPacket packetRecv;
        public ServerPacket packetSend;
        public byte[] data;

        public PlaceResponse(IPEndPoint remoteEp, List<ClientInfo> clients, ServerPacket packetRecv) : base()
        {
            this.remoteEp = remoteEp;
            this.clients = clients;
            this.packetRecv = packetRecv;
            tiles = new List<Vector2>();

            data = new byte[0];
            packetSend = new ServerPacket();
        }

        public void Execute()
        {
            var client = clients.Find(c => c.ep.Equals(remoteEp));
            var clientId = 0;
            var position = new Vector2(packetRecv.posX, packetRecv.posY);

            if (client != null)
            {
                clientId = client.id;

                if (!tiles.Contains(position))
                {
                    tiles.Add(position);
                    data = packetSend.ServerSendPacket("Place", clientId, (int)position.X, (int)position.Y, "");
                }
                else
                {
                    data = packetSend.ServerSendPacket("Error", clientId, 0, 0, "Tile already placed here");
                }
            }
            else
            {
                data = packetSend.ServerSendPacket("Error", clientId, 0, 0, "Error on place");
            }
        }

    }
}
