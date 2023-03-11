﻿using Client.Networking;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {

            var server = new BasicServer();

            server.Init();
            Packet packet = new Packet("Leave");
            server.StartLoop(packet.MakeBytes());
            Console.WriteLine("Server Listening...");

            Console.ReadLine();
        }
    }
}
