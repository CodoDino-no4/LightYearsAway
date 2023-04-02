﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LYA.Helpers;

namespace LYA.Networking
{
		public class ClientManager
		{

				public const int PORT = 11001;

				private UdpClient udpClient;
				private IPEndPoint serverEndPoint;
				
				private byte[] buffer;
				private string clientData;
				private int clientId;
				public bool isInit = false;

				private PacketFormer packetSent;
				private PacketFormer packetRecv;

				public void Init( IPAddress ip, int port )
				{
						buffer=new byte[ 512 ];

						serverEndPoint=new IPEndPoint( ip, port );

						udpClient=new UdpClient( PORT );
						udpClient.EnableBroadcast=true;
						udpClient.DontFragment=true;

						packetSent=new PacketFormer();
						packetRecv=new PacketFormer();

				}

				public void JoinServer()
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										await Send( packetSent.ClientSendPacket( "Join", "0", "Join Request" ) );
										Thread.Sleep( 1000 );

										buffer=udpClient.Receive( ref serverEndPoint );
										packetRecv.ClientRecvPacket( buffer );
										clientData=packetRecv.payload;
										clientId=Int32.Parse( clientData.Split( ':' ).Last() );
										packetSent.clientId=clientId;

										isInit=true;

								}
								catch (SocketException e)
								{
										Console.WriteLine( e );
								}
						} );
				}

				public void StartLoop( byte[] data)
				{
						_=Task.Factory.StartNew( async () =>
						{
								try
								{
										while (true)
										{
												await Send(data);
												Thread.Sleep( 1000 );

												buffer=udpClient.Receive( ref serverEndPoint );
												packetRecv.ClientRecvPacket( buffer );
												Console.WriteLine( $"Recieved packets from {serverEndPoint}:" );

										}
								}
								catch (SocketException e)
								{
										Console.WriteLine( e );

								}
								finally
								{
										udpClient.Close();
								}
						} );
				}

				public async Task Send( byte[] data )
				{
						_=await udpClient.SendAsync( data, serverEndPoint );
				}
		}
}