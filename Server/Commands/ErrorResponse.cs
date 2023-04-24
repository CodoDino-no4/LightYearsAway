using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /// <summary>
    /// Handle errors
    /// </summary>
    public class ErrorResponse : CommandManager.ICommand
    {
        private IPEndPoint remoteEp;
        private List<ClientInfo> clients;

        public ServerPacket packetRecv;
        public ServerPacket packetSend;
        public byte[] data;


        public ErrorResponse(IPEndPoint remoteEp, List<ClientInfo> clients, ServerPacket packerRecv) : base()
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
            Console.WriteLine($"Client ID: {client.id}, ERROR: {packetRecv.payload}");
            data = packetSend.ServerSendPacket("Error", client.id, 0, 0, packetRecv.payload);
        }  

    }
}
