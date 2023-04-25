using Microsoft.Xna.Framework;

namespace LYA.Networking
{
		/// <summary>
		/// Store the clients connected to the server
		/// </summary>
		public class ClientInfo
		{
				public int id
				{
						get; set;
				}
				public Vector2 position
				{
						get; set;
				}

				public bool hasLeft;

				public bool isAdded;

				public ClientInfo( int id )
				{
						hasLeft=false;
						isAdded=false;
						this.id=id;
						// DO NOT COUPLE UP RENDERING AND NETCODE THIS IS BAD
				}
		}
}
