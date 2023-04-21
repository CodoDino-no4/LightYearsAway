using System.Net;
using System.Numerics;

namespace Server
{
    /// <summary>
    /// Store the clients connected to the server
    /// </summary>
    public class ClientInfo
    {
        public IPEndPoint ep { get; set; }
        public int id { get; set; }
        public Vector2 position { get; set; }

        public ClientInfo(IPEndPoint ep, int id)
        {
            this.ep = ep;
            this.id = id;
        }
    }
}
