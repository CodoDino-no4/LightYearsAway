using LYA.Helpers;
using SharpFont;
using System.Net;
using System.Net.Sockets;

namespace LYA.Networking
{
		public class ClientManager
		{

				public const int PORT = 11001;

				private UdpClient udpClient;
				private IPEndPoint serverEndPoint;

				private byte[] recvBuff;
				private byte[] sendBuff;
				private string clientData;
				public bool isInit;

				private PacketFormer packetJoin;
				private PacketFormer packetSent;
				private PacketFormer packetRecv;

				public void Init( IPAddress ip, int port )
				{
						isInit=false;

						recvBuff=new byte[ 512 ];
						sendBuff=new byte[ 512 ];

						serverEndPoint=new IPEndPoint( ip, port );

						udpClient=new UdpClient( PORT );
						udpClient.EnableBroadcast=true;
						udpClient.DontFragment=true;
						udpClient.Connect( serverEndPoint );

						packetJoin=new PacketFormer();
						packetRecv=new PacketFormer();
						packetSent=new PacketFormer();

				}

				public async void JoinServer()
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										packetJoin.ClientSendPacket( "Join", 0, null );

										await udpClient.SendAsync( packetJoin.byteStream );

										var res = await udpClient.ReceiveAsync();
										recvBuff=res.Buffer;

										packetRecv.ClientRecvPacket( recvBuff );
										clientData=packetRecv.payload;
										Globals.clientId=Int32.Parse( clientData.Split( ':' ).First() );
										Globals.playerCount=Int32.Parse( clientData.Split( ':' ).Last() );

										isInit=true;

								}
								catch (SocketException e)
								{
										Console.WriteLine( e );
								}
						});
				}

				public async void MessageLoop()
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										//sendBuff=packetSent.byteStream;

										//if (packetSent.byteStream!=sendBuff)
										//{
												await udpClient.SendAsync( packetSent.byteStream );
										//}

										var res = await udpClient.ReceiveAsync();
										recvBuff=res.Buffer;
										packetRecv.ClientRecvPacket( recvBuff );
										Console.WriteLine( $"Recieved packets {recvBuff}:" );

								}
								catch (SocketException e)
								{
										Console.WriteLine( e );

								}

						});
				}
		}
}
