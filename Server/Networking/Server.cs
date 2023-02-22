using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LYA.Networking
{
		public class Server : IDisposable
		{
				//192.168.1.101
				public ArrayList clientList;
				public Socket serverSocket;
				byte[] dataStream;
				public Packet packet;

				struct ClientInfo
				{
						public EndPoint endPoint;
						public string clientId;
						public string clientName;
				}

				private void LoadServer()
				{
						try
						{
								dataStream=new byte[ 1024 ];

								clientList=new ArrayList();

								// Initialise the socket
								serverSocket=new Socket( AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp );

								// Initialise the IPEndPoint for the server and listen on port 8888
								IPEndPoint server = new IPEndPoint(IPAddress.Any, 8888);

								// Associate the socket with this IP address and port
								serverSocket.Bind( server );

								Console.WriteLine( "UDP Server started on port 8888" );

								// Recieve data from any client
								IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);

								// Initialise the EndPoint for the clients
								EndPoint epSender = clients;

								// Start listening for incoming data
								serverSocket.BeginReceiveFrom( dataStream, 0, dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback( ReceiveData ), epSender );

								//lblStatus.Text="Listening";

						} catch(Exception e) {

								//lblStatus.Text="Error";
								Debug.WriteLine( "Load Error: "+e.Message);
						}
						
				}

				private void ReceiveData( IAsyncResult ar )
				{
						throw new NotImplementedException();
				}

				public void Dispose()
				{
						throw new NotImplementedException();
				}
		}
}
