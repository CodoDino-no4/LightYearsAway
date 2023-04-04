using LYA.Helpers;
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
						_=Task.Run( async () =>
						{
								try
								{
										packetJoin.ClientSendPacket( "Join", 0, null );

										await Send( packetJoin.byteStream );
										Thread.Sleep( 1000 );

										recvBuff=udpClient.Receive( ref serverEndPoint );
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

						} );
				}

				public async void MessageLoop()
				{
						_=Task.Run( async () =>
						{
								try
								{
										sendBuff=packetSent.byteStream;

										if (packetSent.byteStream!=sendBuff)
										{
												await Send( packetSent.byteStream );
										}

										var data = await udpClient.ReceiveAsync();
										packetRecv.ClientRecvPacket( recvBuff );
										Console.WriteLine( $"Recieved packets from {serverEndPoint}:" );

								}
								catch (SocketException e)
								{
										Console.WriteLine( e );

								}

						} );
				}

				public async Task Send( byte[] data )
				{
						_=await udpClient.SendAsync( data, serverEndPoint );
				}
		}
}
