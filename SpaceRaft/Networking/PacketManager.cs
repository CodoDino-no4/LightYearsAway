using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Networking
{
		public static class PacketManager
		{
				public static Packet packet;
				private static int command;
				private static string clientID;
				private static string data;

				public static void MakePacket()
				{

						packet=new Packet(command, clientID, data);
				}

				public static void SendPacket()
				{

				}
		}
}
