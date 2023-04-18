using LYA.Helpers;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace LYA.Networking
{
		public class ClientManager
		{
				// Socket Setup
				private UdpClient udpClient;
				private IPEndPoint serverEndPoint;

				// Buffers
				private byte[] recvBuff;
				private byte[] tmpData;

				// Recieved and decoded coordinates
				public KeyValuePair<Vector2, int> astroCoords;
				public KeyValuePair<Vector2, int> tileCoords;

				// Has initalised and joined server
				public bool isInit;

				public PacketFormer packetJoin;
				public PacketFormer packetLeave;
				private PacketFormer packetRecv;

				public void Init( IPAddress ip, int port )
				{
						isInit=false;

						recvBuff=new byte[ 512 ];
						tmpData=new byte[ 512 ];

						try
						{
								serverEndPoint=new IPEndPoint( ip, port );

								udpClient=new UdpClient();
								udpClient.Client.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true );
								udpClient.ExclusiveAddressUse=false;
								udpClient.EnableBroadcast=true;
								udpClient.DontFragment=true;

								udpClient.Connect( serverEndPoint );
						}
						catch
						{
								Debug.WriteLine( "ERROR ON INITALISING UDPCLIENT" );
						}

						packetJoin=new PacketFormer();
						packetLeave=new PacketFormer();
						packetRecv=new PacketFormer();

						JoinServer();

				}

				public void JoinServer()
				{
						try
						{
								udpClient.Send( packetJoin.ClientSendPacket( "Join", 0, udpClient.Client.LocalEndPoint.ToString() ) );

								var res = udpClient.Receive(ref serverEndPoint);
								recvBuff=res;
								packetRecv.ClientRecvPacket( recvBuff );

								// Join response parse
								if (packetRecv.cmd==1)
								{
										Globals.ClientId=packetRecv.clientId;
										Globals.PlayerCount=packetRecv.clientId;
										isInit=true;
										Debug.WriteLine( "join server complete" );

										// Prevent sending join data more than once
										Globals.Packet.sendData=null;
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

				public void LeaveServer()
				{
						try
						{
								udpClient.Send( packetLeave.ClientSendPacket( "Leave", Globals.ClientId, "" ) );
						}
						catch (SocketException e)
						{
								Debug.WriteLine( e );
						}
				}
				public Vector2 Decode( PacketFormer packet )
				{
						Vector2 coords = new Vector2();

						if (packet.cmd==3||packet.cmd==4)
						{
								if (packet.payload!=null)
								{
										string remCurlys = packet.payload.Substring(1, packet.payload.Length - 2); //"0:{X:0 Y:-6}"
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

				public void MessageLoop()
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										if (isInit)
										{
												if (Globals.Packet.sendData!=null&&!tmpData.SequenceEqual( Globals.Packet.sendData ))
												{
														await udpClient.SendAsync( Globals.Packet.sendData );
												}

												if (Globals.Packet.sendData!=null)
												{
														tmpData=Globals.Packet.sendData;
												}
												else
												{
														tmpData=new byte[ 512 ];
												}

												var res = await udpClient.ReceiveAsync();

												recvBuff=res.Buffer;
												packetRecv.ClientRecvPacket( recvBuff );

												// Join response parse
												if (packetRecv.cmd==1)
												{
														if (Globals.ClientId!=0)
														{
																Globals.PlayerCount++;
														}
												}

												// Leave response parse
												if (packetRecv.cmd==2)
												{
														Globals.PlayerCount--;
												}

												// Move response parse
												if (packetRecv.cmd==3)
												{
														if (packetRecv.clientId!=Globals.ClientId)
														{
																astroCoords=new KeyValuePair<Vector2, int>( Decode( packetRecv ), packetRecv.clientId );
														}
												}

												// Place response parse
												if (packetRecv.cmd==4)
												{
														if (packetRecv.clientId!=Globals.ClientId)
														{
																tileCoords=new KeyValuePair<Vector2, int>( Decode( packetRecv ), packetRecv.clientId );
														}
												}
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
						} );
				}
		}
}
