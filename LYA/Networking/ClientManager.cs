using LYA.Helpers;
using LYA.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens.Transitions;
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
				public KeyValuePair<int, Vector2> astroCoords;
				public KeyValuePair<int, Vector2> tileCoords;
				public List<KeyValuePair<int, Vector2>> clients;

				// Has initalised and joined server
				public bool isInit;

				public PacketFormer packetJoin;
				public PacketFormer packetRecv;
				public PacketFormer packetLeave;

				private new LYA Game;

				public ClientManager(Game game)
				{
						Game=(LYA) game;
				}

				public void Init( IPAddress ip, int port )
				{
						isInit=false;

						recvBuff=new byte[ 128 ];
						tmpData=new byte[ 128 ];

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
								isInit=false;
								Globals.ScreenManager.LoadScreen( new MultiMenu( Game, this ), new FadeTransition( Game.GraphicsDevice, Color.Black, 4 ) );
						}

						packetJoin=new PacketFormer();
						packetRecv=new PacketFormer();
						packetLeave=new PacketFormer();
						clients=new List<KeyValuePair<int, Vector2>>();

						JoinServer();

				}

				public void GetExistingClients(string payload)
				{
						// Get player count
						Globals.PlayerCount=Int32.Parse(payload.Split("?").First());

						string newClients = payload.Split("?").Last();
						string[] newClientsList = newClients.Split("client");

						foreach (var newClient in newClientsList)
						{
								if (newClient!="")
								{
										// Get player id and the player current position
										string[] data = newClient.Split(":");
										clients.Add( new KeyValuePair<int, Vector2>( Int32.Parse(data[ 0 ]), new Vector2( Int32.Parse(data[ 1 ]), Int32.Parse(data[ 2 ] ))) );
								}
						}
				}

				public void GetExisitngTiles()
				{

				}

				public void JoinServer()
				{
						try
						{
								udpClient.Send( packetJoin.ClientSendPacket( "Join", 0, 0, 0, udpClient.Client.LocalEndPoint.ToString() ) );

								var res = udpClient.Receive(ref serverEndPoint);
								recvBuff=res;
								packetRecv.ClientRecvPacket( recvBuff );

								// Join response parse
								if (packetRecv.cmd==1)
								{
										Globals.ClientId=packetRecv.clientId;

										if (Globals.ClientId>1)
										{
												GetExistingClients(packetRecv.payload); 
										}

										isInit=true;
										Debug.WriteLine( "join server complete" );

										// Prevent sending join data more than once
										Globals.Packet.sendData=null;
								}

								// Error response parse
								if (packetRecv.cmd==5)
								{
										isInit=false;
										Globals.ScreenManager.LoadScreen( new MultiMenu( Game, this ), new FadeTransition( Game.GraphicsDevice, Color.Black, 4 ) );
								}

						}
						catch (SocketException e)
						{
								if (e.SocketErrorCode.ToString()=="ConnectionReset")
								{
										isInit=false;
										Globals.ScreenManager.LoadScreen( new MultiMenu( Game, this ), new FadeTransition( Game.GraphicsDevice, Color.Black, 4 ) );
								}
						}

						Globals.IsMulti=true;
				}

				public void LeaveServer()
				{
						try
						{
								udpClient.Send( packetLeave.ClientSendPacket( "Leave", Globals.ClientId, 0, 0, "" ) );
						}
						catch (SocketException e)
						{
								Debug.WriteLine( e );
						}
				}

				public void MessageLoop()
				{
						_=Task.Run( async () =>
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
																astroCoords=new KeyValuePair<int, Vector2>(  packetRecv.clientId, new Vector2(packetRecv.posX, packetRecv.posY) );
														}
												}

												// Leave response parse
												if (packetRecv.cmd==2)
												{
														Globals.PlayerCount--;
														astroCoords=new KeyValuePair<int, Vector2>( packetRecv.clientId, new Vector2( packetRecv.posX, packetRecv.posY ) );
												}

												// Move response parse
												if (packetRecv.cmd==3)
												{
														if (packetRecv.clientId!=Globals.ClientId)
														{
																astroCoords=new KeyValuePair<int, Vector2>( packetRecv.clientId, new Vector2( packetRecv.posX, packetRecv.posY ) );
														}
												}

												// Place response parse
												if (packetRecv.cmd==4)
												{
														if (packetRecv.clientId!=Globals.ClientId)
														{
																tileCoords=new KeyValuePair<int, Vector2>(  packetRecv.clientId, new Vector2(packetRecv.posX, packetRecv.posY) );
														}
												}

												// Error response parse
												if (packetRecv.cmd==5)
												{
														
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
