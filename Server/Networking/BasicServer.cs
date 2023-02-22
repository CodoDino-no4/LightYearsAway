using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Networking
{
		public class BasicServer : IDisposable
		{

				public const int PORT = 5000;

				private Socket socket;
				private EndPoint endPoint;

				private byte[] dataRecv;
				private ArraySegment<byte> dataRecvSegment;
				private bool disposedValue;

				public BasicServer()
				{
				}

				public void Connect()
				{
						dataRecv= new byte[4096];
						dataRecvSegment= new ArraySegment<byte>(dataRecv);

						endPoint = new IPEndPoint(IPAddress.Any, PORT);

						socket=new Socket( AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp );
						socket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.PacketInformation, true );
						socket.Bind( endPoint );
						Console.WriteLine( "Server connected on port"+PORT );
				}

				public void StartMessageLoop()
				{
						Task.Run( async () =>
						{
								SocketReceiveMessageFromResult res;
								while (true)
								{
										res=await socket.ReceiveMessageFromAsync( dataRecvSegment, SocketFlags.None, endPoint );
										await SendTo( res.RemoteEndPoint, Encoding.UTF8.GetBytes( "Hello back!" ) );
								}
						} );
				}

				public async Task SendTo( EndPoint recipient, byte[] data )
				{
						var segment = new ArraySegment<byte>(data);
						await socket.SendToAsync( segment, SocketFlags.None, recipient );
				}

				protected virtual void Dispose( bool disposing )
				{
						if (!disposedValue)
						{
								if (disposing)
								{
										// TODO: dispose managed state (managed objects)
								}

								// TODO: free unmanaged resources (unmanaged objects) and override finalizer
								// TODO: set large fields to null
								disposedValue=true;
						}
				}

				// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
				// ~BasicServer()
				// {
				//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
				//     Dispose(disposing: false);
				// }

				public void Dispose()
				{
						// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
						Dispose( disposing: true );
						GC.SuppressFinalize( this );
				}
		}
}
