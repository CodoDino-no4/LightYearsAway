using LYA.Helpers;
using SharpFont;
using System.Diagnostics;
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

						try
						{
								udpClient=new UdpClient();
								udpClient.Client.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true );
								udpClient.ExclusiveAddressUse=false;
								udpClient.EnableBroadcast=true;
								udpClient.DontFragment=true;
						
								udpClient.Connect( serverEndPoint );
						}
						catch {
								Debug.WriteLine( "ERROR ON UDPCLIENT" );
						}

						packetJoin=new PacketFormer();
						packetRecv=new PacketFormer();
						packetSent=new PacketFormer();

				}

				public async void JoinServer()
				{
						_=Task.Factory.StartNew( () =>
						{
								try
								{
										udpClient.Send( packetJoin.ClientSendPacket( "Join", 0, null ));
								}
								catch (SocketException e)
								{
										Debug.WriteLine( e );
								}
						});
				}

				public async void MessageLoop()
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										if (isInit)
										{
												await udpClient.SendAsync( packetSent.ClientSendPacket("Move", Globals.clientId, "This is a test") );
										}

										var res = await udpClient.ReceiveAsync();
										recvBuff=res.Buffer;
										packetRecv.ClientRecvPacket( recvBuff );

										// Join response
										if (packetRecv.cmd==1)
										{
												clientData=packetRecv.payload;
												Globals.clientId=Int32.Parse( clientData.Split( ':' ).First() );
												Globals.playerCount=Int32.Parse( clientData.Split( ':' ).Last() );
												isInit=true;
												Debug.WriteLine( "join server complete", isInit );
										}

								}
								catch (SocketException e)
								{
										Debug.WriteLine( e );

								}
						});
				}
		}
}
