using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Networking
{
		public class Packet
		{
				public enum Command
				{
						Move,
						Zoom,
						Place,
						Null

				}

				private Command command;

				private string data;

				private string clientId;

				public Packet()
				{
						command=Command.Null;
						data=null;
						clientId=null;
				}


				// Description   -> |dataIdentifier|name length|message length|    name   |    message   |
				// Size in bytes -> |       4      |     4     |       4      |name length|message length|

				public Packet( byte[] dataStream )
				{
						// Command (4 bytes)
						command=(Command) BitConverter.ToInt32( dataStream, 0 );

						// Store the length of the clientID
						int nameLength = BitConverter.ToInt32( dataStream, 4 );

						// Store the length of the data (4 bytes)
						int msgLength = BitConverter.ToInt32(dataStream, 8);

						// Read the message field
						if (msgLength>0)
								data=Encoding.UTF8.GetString( dataStream, 12, msgLength );
						else
								data=null;
				}

				// Converts the packet into a byte array for sending/receiving 
				public byte[] GetDataStream()
				{
						List<byte> dataStream = new List<byte>();

						// Add the command
						dataStream.AddRange( BitConverter.GetBytes( (int) command ) );

						// Add the message length and message
						if (data!=null || clientId!=null)
						{
								dataStream.AddRange( BitConverter.GetBytes( clientId.Length ));
								dataStream.AddRange( BitConverter.GetBytes( data.Length ) );
								dataStream.AddRange( Encoding.UTF8.GetBytes( data ) );
						}
						else
								dataStream.AddRange( BitConverter.GetBytes( 0 ) );;

						return dataStream.ToArray();
				}

		}
}
