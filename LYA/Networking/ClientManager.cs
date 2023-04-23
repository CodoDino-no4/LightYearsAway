using LYA.Helpers;
using LYA.Screens;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens.Transitions;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
				public KeyValuePair<int, Vector2> tileCoords;
				public List<ClientInfo> clients;

				// Has initalised and joined server
				public bool isInit;

				public PacketFormer packetJoin;
				public PacketFormer packetRecv;
				public PacketFormer packetLeave;

				private new LYA Game;

				public Process proc;
				private ProcessStartInfo start;

				public ClientManager( Game game )
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
						clients=new List<ClientInfo>();

						JoinServer();

				}

				public void GetExistingClients( string payload )
				{
						string newClients = payload.Split("?").Last();
						string[] newClientsList = newClients.Split("client");

						foreach (var newClient in newClientsList)
						{
								if (newClient!="")
								{
										// Get player id and the player current position
										string[] data = newClient.Split(":");
										clients.Add( new ClientInfo( Int32.Parse(data[ 0 ] )) {position = new Vector2( Int32.Parse( data[ 1 ] ), Int32.Parse( data[ 2 ] ) ) } );
								}
						}
				}

				public void StartIntegratedServer()
				{
						Task.Run( () =>
						{
								// Setup server exe
								start = new ProcessStartInfo();
								start.CreateNoWindow=true;
								bool isRoot = false;

								string[] initalPath = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
								string rootPath = "";
								foreach (var dir in initalPath)
								{
										rootPath+=$"{dir}\\";

										if (dir=="SpaceRaftMono")
										{
												isRoot=true;
												break;
										}
								}

								string fullPath = rootPath + "Server\\bin\\Debug\\net7.0-windows\\Server.exe";
								start.FileName=fullPath;

								proc=Process.Start( start );

						} );

						Thread.Sleep( 2000 );
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
										clients.Add( new ClientInfo( Globals.ClientId ) { isAdded = true } );

										if (Globals.ClientId>1)
										{
												GetExistingClients( packetRecv.payload );
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

								// Kill integrated server if its running
								if (proc!=null)
								{
										proc.Kill();
								}
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
																clients.Add( new ClientInfo(packetRecv.clientId) );
														}
												}

												// Leave response parse
												if (packetRecv.cmd==2)
												{
														var client=clients.Find( c => c.id.Equals( packetRecv.clientId ) );
														client.hasLeft=true;
												}

												// Move response parse
												if (packetRecv.cmd==3)
												{
														if (packetRecv.clientId!=Globals.ClientId)
														{
																var client=clients.Find( c => c.id.Equals( packetRecv.clientId ) );
																client.position = new Vector2( packetRecv.posX, packetRecv.posY );
														}
												}

												// Place response parse
												if (packetRecv.cmd==4)
												{
														if (packetRecv.clientId!=Globals.ClientId)
														{
																tileCoords=new KeyValuePair<int, Vector2>( packetRecv.clientId, new Vector2( packetRecv.posX, packetRecv.posY ) );
														}
												}

												// Error response parse
												if (packetRecv.cmd==5)
												{
														Debug.WriteLine(packetRecv.payload);
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
