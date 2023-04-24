using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /// <summary>
    /// Handle move data
    /// </summary>
    public class MoveResponse : CommandManager.ICommand
    {
        private IPEndPoint remoteEp;
        private List<ClientInfo> clients;

        public ServerPacket packetRecv;
        public ServerPacket packetSend;
        public byte[] data;

        public MoveResponse(IPEndPoint remoteEp, List<ClientInfo> clients, ServerPacket packetRecv) : base()
        {
            this.remoteEp = remoteEp;
            this.clients = clients;
            this.packetRecv = packetRecv;

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
                client.position = new Vector2(packetRecv.posX, packetRecv.posY);

                data = packetSend.ServerSendPacket("Move", clientId, packetRecv.posX, packetRecv.posY, "");
            }
            else
            {
                data = packetSend.ServerSendPacket("Error", clientId, 0, 0, "Error on move");
            }
        }  

    }
}
