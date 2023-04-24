using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /// <summary>
    /// Add client to server client list
    /// </summary>
    public class JoinResponse : CommandManager.ICommand
    {
        private IPEndPoint remoteEp;
        private List<ClientInfo> clients;

        public ServerPacket packetSend;
        public byte[] data;

        public JoinResponse(IPEndPoint remoteEp, List<ClientInfo> clients) : base()
        {
            this.remoteEp = remoteEp;
            this.clients = clients;

            data = new byte[0];
            packetSend = new ServerPacket();
        }

        public void Execute() 
        {
            // Max players: 4
            if (clients.Count() < 4)
            {
                // Check client is not already connected
                var client = clients.Find(c => c.ep.Equals(remoteEp));
                var clientId = 0;

                // If client is not already connected
                if (client == null)
                {
                    clientId = clients.Count() + 1;
                    clients.Add(new ClientInfo(remoteEp, clientId));

                    // Create server world data
                    var serverData = $"{clients.Count()}?";

                    for (var i = 1; clients.Count() > i; i++)
                    {
                        var conn = clients.Find(c => c.id.Equals(i));
                        if (conn != null)
                        {
                            serverData += $"client{i}:{conn.position.X}:{conn.position.Y}";

                        }
                    }

                    Console.WriteLine($"{remoteEp} has joined the server as Client ID: {clientId}");

                    data = packetSend.ServerSendPacket("Join", clientId, 0, 0, serverData);
                }
                else
                {
                    data = packetSend.ServerSendPacket("Error", clientId, 0, 0, "Client is already connected on this IP address and port");
                }
            }
            else
            {
                data = packetSend.ServerSendPacket("Error", 0, 0, 0, "Server full");
            }
        }
    }  
}
