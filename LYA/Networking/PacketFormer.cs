﻿using System.Text;

namespace LYA.Networking
{
		/// <summary>
		/// Creates a standardised packet
		/// </summary>
		public class PacketFormer
		{
				/// <summary>
				/// Valid command types
				/// </summary>
				private enum Command
				{
						Null,   // Default
						Join,   // A client joins the server
						Leave,  // A client leaves the server
						Move,   // Astro movement
						Place,  // PLace a tile
						Error, // Error message
				}

				// Current cmd
				public int cmd = 0;

				// Client unique ID
				public int clientId;

				// Cooridnate data
				public int posX;
				public int posY;

				// Payload sent within packetSent
				public string payload;

				// Datastream
				public byte[] sendData;

				/// <summary>
				/// Instantiate the class
				/// </summary>
				public PacketFormer()
				{
				}

				/// <summary>
				/// Converts data into a byte stream
				/// </summary>
				public byte[] ClientSendPacket( string command, int id, int x, int y, string data )
				{
						// Set Packet data
						int cmdVal =(int) (Command) Enum.Parse( typeof( Command ), command, true );

						// Data stream as list of bytes
						List<byte> byteStream = new List<byte>();

						// Add the command
						byteStream.AddRange( BitConverter.GetBytes( cmdVal ) );

						// Add client ID
						byteStream.AddRange( BitConverter.GetBytes( id ) );

						// Add coordinates X
						byteStream.AddRange( BitConverter.GetBytes( x ) );

						// Add coordinates Y
						byteStream.AddRange( BitConverter.GetBytes( y ) );

						// Add data
						if (data!=null)
						{
								byteStream.AddRange( Encoding.UTF8.GetBytes( data ) );
						}
						else
								byteStream.AddRange( BitConverter.GetBytes( 0 ) );

						// Final result
						sendData=byteStream.ToArray();
						return sendData;
				}

				/// <summary>
				/// Converts byte stream into packet data
				/// </summary>
				public void ClientRecvPacket( byte[] data )
				{
						// Decode the cmd
						// Length is always 1
						cmd=BitConverter.ToInt32( data, 0 );

						// Decode the clientId
						// Length is always 1
						clientId=BitConverter.ToInt32( data, 4 );

						// Decode coordinate X
						posX=BitConverter.ToInt32( data, 8 );

						// Decode coordinate Y
						posY=BitConverter.ToInt32( data, 12 );

						// Decode the payload
						// Length is variable so get the length
						int dataLen = data.Length - 16;

						// Copy payload to new array and get the string
						byte[] dataSegment = new byte[dataLen];
						Buffer.BlockCopy( data, 16, dataSegment, 0, dataLen );

						payload=Encoding.UTF8.GetString( dataSegment );

				}
		}
}
