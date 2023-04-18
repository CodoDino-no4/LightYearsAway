using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientInfo
    {
        public IPEndPoint ep { get; set; }
        public int id { get; set; }
        public string position { get; set; }

        public ClientInfo(IPEndPoint ep, int id)
        {
            this.ep = ep;
            this.id = id;
        }
    }
}
