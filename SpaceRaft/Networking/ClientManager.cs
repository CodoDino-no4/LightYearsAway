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
										udpClient.Send( packetSent.ClientSendPacket( "Join", 0, null ));
								}
								catch (SocketException e)
								{
										Debug.WriteLine( e );
								}
						});
				}

				public async void LeaveServer()
				{
						_=Task.Factory.StartNew( () =>
						{
								try
								{
										udpClient.Send( packetSent.ClientSendPacket( "Leave", Globals.ClientId, null ) );
								}
								catch (SocketException e)
								{
										Debug.WriteLine( e );
								}
						} );
				}

				public async void MessageLoop( byte[] packet)
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										if (isInit)
										{
												await udpClient.SendAsync( packet );
										}

										var res = await udpClient.ReceiveAsync();
										recvBuff=res.Buffer;
										packetRecv.ClientRecvPacket( recvBuff );

										// Join response
										if (packetRecv.cmd==1)
										{
												clientData=packetRecv.payload;

												if (Globals.ClientId==0)
												{
														Globals.ClientId=Int32.Parse( clientData.Split( ':' ).First() );
														isInit=true;
														Debug.WriteLine( "join server complete", isInit );
												}

												Globals.PlayerCount=Int32.Parse( clientData.Split( ':' ).Last() );
										}

										if (packetRecv.cmd==2)
										{
												clientData = packetRecv.payload;
												Int32.Parse( clientData.Split( ':' ).First() );
												Globals.PlayerCount=Int32.Parse( clientData.Split( ':' ).Last() );


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
