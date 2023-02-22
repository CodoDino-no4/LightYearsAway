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
		public class Server
		{
				//192.168.1.101
				public ArrayList clientList;
				public Socket serverSocket;
				byte[] data;

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
								data=new byte[ 1024 ];
								// Initialise the ArrayList of connected clients
								clientList=new ArrayList();

								// Initialise the delegate which updates the status
								this.updateStatusDelegate=new UpdateStatusDelegate( this.UpdateStatus );

								// Initialise the socket
								serverSocket=new Socket( AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp );

								// Initialise the IPEndPoint for the server and listen on port 30000
								IPEndPoint server = new IPEndPoint(IPAddress.Any, 30000);

								// Associate the socket with this IP address and port
								serverSocket.Bind( server );

								// Initialise the IPEndPoint for the clients
								IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);

								// Initialise the EndPoint for the clients
								EndPoint epSender = (EndPoint)clients;

								// Start listening for incoming data
								serverSocket.BeginReceiveFrom( this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback( ReceiveData ), epSender );

								lblStatus.Text="Listening";

						} catch(Exception e) {

								lblStatus.Text="Error";
								Debug.WriteLine( "Load Error: "+e.Message);
						}
						
				}
				



		}
}
