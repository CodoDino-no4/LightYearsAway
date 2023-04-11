using LYA.Helpers;
using Microsoft.Xna.Framework;
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

				// Store encoded coordinates
				public Vector2 coords;

				// Has initalised and joined server
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

						packetJoin = new PacketFormer();
						packetRecv=new PacketFormer();
						packetSent=new PacketFormer();

						JoinServer();

				}

				public void JoinServer()
				{
						try
						{
								udpClient.Send(Globals.Packet.ClientSendPacket( "Join", Globals.ClientId, "" ) );

								var res = udpClient.Receive(ref serverEndPoint);
								recvBuff=res;
								packetJoin.ClientRecvPacket( recvBuff );

								// Join response parse
								if (packetJoin.cmd==1)
								{
										if (Globals.ClientId==0)
										{
												Globals.ClientId=packetJoin.clientId;
												isInit=true;
												Debug.WriteLine( "join server complete", isInit );
										}

										Globals.PlayerCount=Int32.Parse( packetJoin.payload);
								}
						}
						catch (SocketException e)
						{
								if (e.SocketErrorCode.ToString()=="ConnectionReset")
								{
										isInit=false;
										Debug.WriteLine( "The server port is unreachable" );
								}
						}

						Globals.IsMulti=true;
				}

				public async void LeaveServer()
				{
						_=Task.Factory.StartNew(() =>
						{
								try
								{
										udpClient.Send( Globals.Packet.ClientSendPacket("Leave", Globals.ClientId, "" ));
								}
								catch (SocketException e)
								{
										Debug.WriteLine( e );
								}
						} );
				}
				public Vector2 Decode()
				{
						if (packetRecv.cmd==3||packetRecv.cmd==4)
						{
								if (packetRecv.payload !=null)
								{
										string remCurlys = packetRecv.payload.Substring(1, packetRecv.payload.Length - 2); //"0:{X:0 Y:-6}"
										string xPair = remCurlys.Split(' ').First();
										string yPair = remCurlys.Split(' ').Last();

										string xValue = xPair.Split(":").Last();
										string yValue = yPair.Split(":").Last();

										int x = Int32.Parse(xValue);
										int y = Int32.Parse(yValue);

										coords=new Vector2( x, y );

								}
								
						}
								return coords;
				}

				//public void Serialize( BinaryWriter writer )
				//{
				//		writer.Write( position.X );
				//		writer.Write( position.Y );
				//		writer.Write( speed );
				//}

				//public void Deserialize( BinaryReader reader )
				//{
				//		position.X=reader.ReadSingle();
				//		position.Y=reader.ReadSingle();
				//		speed=reader.ReadSingle();
				//}

				public async void MessageLoop()
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										if (isInit)
										{
												await udpClient.SendAsync( Globals.Packet.sendData );
										}

										var res = await udpClient.ReceiveAsync();
										recvBuff=res.Buffer;
										packetRecv.ClientRecvPacket( recvBuff );

										// Join response parse
										if (packetRecv.cmd==1)
										{
												if (Globals.ClientId>0)
												{
														Globals.PlayerCount=Int32.Parse( packetRecv.payload);
														isInit=true;
												}
										}

										// Leave response parse
										if (packetRecv.cmd==2)
										{
												//packetRecv.clientId;
												Globals.PlayerCount=Int32.Parse( packetRecv.payload);
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
