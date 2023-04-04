using Microsoft.Xna.Framework;
using System.Text;

namespace LYA.Networking
{
		public class PacketFormer
		{
				// Command types
				private enum Command
				{
						Null,   // Default
						Join,   // A client joins the server
						Leave,  // A client leaves the server
						Move,   // Astro movement
						Place,  // PLace a tile

				}

				// Current cmd
				public int cmd = 0;

				// Client unique ID
				public int clientId;

				// Payload sent within packetSent
				public string payload;

				// 
				public byte[] byteStream;

				//Creates an instance of packetSent
				public PacketFormer()
				{
				}

				// Converts data into an array of bytes
				public void ClientSendPacket( string command, int id, string data )
				{
						// Set packet data
						cmd=(int) (Command) Enum.Parse( typeof( Command ), command, true );
						clientId=id;
						payload=data;

						// Data stream as list of bytes
						List<byte> dataStream = new List<byte>();

						// Add the command
						dataStream.AddRange( BitConverter.GetBytes( cmd ) );

						// Add client ID
						if (this.clientId!=0)
						{
								dataStream.AddRange( BitConverter.GetBytes( clientId ) );
						}
						else
								dataStream.AddRange( BitConverter.GetBytes( 0 ) );

						// Add data
						if (data!=null)
						{
								dataStream.AddRange( Encoding.UTF8.GetBytes( payload ) );
						}
						else
								dataStream.AddRange( BitConverter.GetBytes( 0 ) );

						byteStream=new byte[ dataStream.Count ];
						// Final result
						byteStream=dataStream.ToArray();
				}

				// converts the bytes into a Packet
				public void ClientRecvPacket( byte[] data )
				{
						// Decode the cmd
						// Length is always 1
						cmd=BitConverter.ToInt32( data, 0 );
						Console.WriteLine( cmd );

						// Decode the payload
						// Length is variable so get the length
						int dataLen = data.Length - 4;

						// Copy payload to new array and get the string
						byte[] dataSegment = new byte[dataLen];
						Buffer.BlockCopy( data, 4, dataSegment, 0, dataLen );

						payload=Encoding.UTF8.GetString( dataSegment );
						Console.WriteLine( payload );

				}

				public List<byte> Vector2Ser( Vector2 vector )
				{
						List<byte> bytes = new List<byte>();

						float x = vector.X;
						float y = vector.Y;

						bytes.AddRange( Encoding.UTF8.GetBytes( vector.ToString() ) );

						return bytes;
				}

				public Vector2 Vector2Deser( byte[] vector )
				{
						Vector2 result = new Vector2();

						result.X=BitConverter.ToSingle( vector, 0 );
						result.Y=BitConverter.ToSingle( vector, 2 );

						return result;

				}

		}
}
