using System.Net;
using System.Net.Sockets;

namespace LYAServer.Commands
{
    public class Response : CommandManager.ICommand
    {
        private UdpReceiveResult res;
        public List<ClientInfo> clients;
        private ServerPacket packetRecv;
        public ServerPacket packetSend;
        public IPEndPoint remoteEp;
        public byte[] data;

        private JoinResponse joinResponse;
        private LeaveResponse leaveResponse;
        private MoveResponse moveResponse;
        private PlaceResponse placeResponse;
        private ErrorResponse errorResponse;

        public Response(UdpReceiveResult res, List<ClientInfo> clients)
        {
            packetRecv = new ServerPacket();
            packetRecv.ServerRecvPacket(res.Buffer);
            data = new byte[0];

            packetSend = new ServerPacket();
            remoteEp = res.RemoteEndPoint;

            this.clients = clients;

            joinResponse = new JoinResponse(remoteEp, clients);
            leaveResponse = new LeaveResponse(remoteEp, clients);
            moveResponse = new MoveResponse(remoteEp, clients, packetRecv);
            placeResponse = new PlaceResponse(remoteEp, clients, packetRecv);
            errorResponse = new ErrorResponse(remoteEp, clients, packetRecv);
        }

        public void Execute()
        {
            switch (packetRecv.cmd)
            {
                default:
                case 0:
                    break;

                case 1:

                    joinResponse.Execute();
                    packetSend = joinResponse.packetSend;
                    data = joinResponse.data;
                    break;

                case 2:

                    leaveResponse.Execute();
                    packetSend = leaveResponse.packetSend;
                    data = leaveResponse.data;
                    break;

                case 3:

                    moveResponse.Execute();
                    packetSend = moveResponse.packetSend;
                    data = moveResponse.data;
                    break;

                case 4:

                    placeResponse.Execute();
                    packetSend = placeResponse.packetSend;
                    data = placeResponse.data;
                    break;

                case 5:

                    errorResponse.Execute();
                    packetSend = errorResponse.packetSend;
                    data = errorResponse.data;
                    break;

            }
        }


    }
}