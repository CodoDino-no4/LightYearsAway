﻿namespace Server
{
    public class Program
    {
        public static void Main()
        {
            var server = new BasicServer();

            server.Init();
            server.StartLoop();

            Console.ReadLine();
        }
    }
}
