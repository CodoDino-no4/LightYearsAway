﻿using Microsoft.Xna.Framework;
using System.Net;

namespace LYA.Networking
{
    /// <summary>
    /// Store the clients connected to the server
    /// </summary>
    public class ClientInfo
    {
        public int id { get; set; }
        public Vector2 position { get; set; }

				public bool hasLeft;

				public bool isAdded;

        public ClientInfo(int id)
        {
						hasLeft=false;
						isAdded=false;
            this.id = id;
        }
    }
}