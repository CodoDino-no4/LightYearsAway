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

				// Datastream
				public byte[] sendData;

				//Creates an instance of packetSent
				public PacketFormer()
				{
				}

				// Converts data into an array of bytes
				public byte[] ClientSendPacket( string command, int id, string data )
				{
						// Set Packet data
						int cmdParsed =(int) (Command) Enum.Parse( typeof( Command ), command, true );

						// Data stream as list of bytes
						List<byte> byteStream = new List<byte>();

						// Add the command
						byteStream.AddRange( BitConverter.GetBytes( cmdParsed ) );

						// Add client ID
						if (id!=0)
						{
								byteStream.AddRange( BitConverter.GetBytes( id ) );
						}
						else
								byteStream.AddRange( BitConverter.GetBytes( 0 ) );

						// Add data
						if (data!=null)
						{
								byteStream.AddRange( Encoding.UTF8.GetBytes( data ) );
						}
						else
								byteStream.AddRange( BitConverter.GetBytes( 0 ) );

						// Final result
						sendData=byteStream.ToArray();
						return byteStream.ToArray();
				}

				// converts the bytes into a Packet
				public void ClientRecvPacket( byte[] data )
				{
						// Decode the cmd
						// Length is always 1
						cmd=BitConverter.ToInt32( data, 0 );

						// Decode the clientId
						// Length is always 1
						clientId=BitConverter.ToInt32( data, 4 );

						// Decode the payload
						// Length is variable so get the length
						int dataLen = data.Length - 8;

						// Copy payload to new array and get the string
						byte[] dataSegment = new byte[dataLen];
						Buffer.BlockCopy( data, 8, dataSegment, 0, dataLen );

						payload=Encoding.UTF8.GetString( dataSegment );

				}
		}
}
