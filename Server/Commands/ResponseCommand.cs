using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public class ResponseCommand : CommandManager.ICommand
    {
        private UdpReceiveResult res;
        private ServerPacket packetRecv;
        private ServerPacket packetSend;

        public Dictionary<IPEndPoint, int> conns; // change to an array?? (or a better suited type) with the last known position of all sprites
        public byte[] data;
        public ResponseCommand(UdpReceiveResult res) 
        {
            this.res = res;
            conns = new Dictionary<IPEndPoint, int>();
            data = new byte[128];
            packetRecv = new ServerPacket();
            packetSend = new ServerPacket();

            packetRecv.ServerRecvPacket(res.Buffer);
        }

        public void Execute()
        {
            switch (packetRecv.cmd)
            {
                case 1:

                    if (!conns.ContainsKey(res.RemoteEndPoint))
                    {
                        conns.Add(res.RemoteEndPoint, conns.Count() + 1);
                        var clientId = conns.GetValueOrDefault(res.RemoteEndPoint);
                        packetSend.ServerSendPacket("Join", clientId, conns.Count.ToString());
                    }
                    else {

                        packetSend.ServerSendPacket("Error", 0, "Client already connected");
                    }

                    break;

                case 2:

                    if (conns.ContainsKey(res.RemoteEndPoint))
                    {
                        conns.Remove(res.RemoteEndPoint);
                        packetSend.ServerSendPacket("Leave", packetRecv.clientId, conns.Count.ToString());

                    }
                    else {
                        packetSend.ServerSendPacket("Error", 0, "Client does not exisit on this server");
                    }

                    break;
                case 3:

                    packetSend.ServerSendPacket("Move", packetRecv.clientId, packetRecv.payload);
                    break;

                case 4:

                    packetSend.ServerSendPacket("Place", packetRecv.clientId, packetRecv.payload);
                    break;

                default:
                    break;
            }

            data = packetSend.sendData;
        }
    }
}
