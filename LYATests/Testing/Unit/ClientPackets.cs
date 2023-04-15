using LYA.Helpers;
using LYA.Networking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Text;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// Checks a packet can be endcoded and decoded correctly
    /// </summary>
    [TestClass()]
    public class ClientPackets
    {
        /// <summary>
        /// Initialises the game and runs one frame
        /// </summary>
        private LYA game;

        [TestInitialize()]
        public void Setup()
        {
            Globals.testing = true;

            game = new LYA();
            game.SuppressDraw();
            game.RunOneFrame();
        }

        [TestMethod()]
        public void PacketEncodeTest()
        {
            PacketFormer packet = new PacketFormer();
            packet.ClientSendPacket("Move", 2, "Test");

            Assert.IsTrue(Encoding.UTF8.GetString(packet.sendData) == "\u0003\0\0\0\u0002\0\0\0Test");

        }

        [TestMethod()]
        public void PacketDecodeTest()
        {
            PacketFormer packet = new PacketFormer();
            packet.ClientSendPacket("Move", 2, "Test");
            byte[] data = packet.sendData;

            packet.ClientRecvPacket(data);

            Assert.IsTrue(packet.cmd == 3);
            Assert.IsTrue(packet.clientId == 2);
            Assert.IsTrue(packet.payload == "Test");
        }

        [TestMethod()]
        public void PayloadDecodeTest()
        {
            PacketFormer packet = new PacketFormer();
            packet.ClientSendPacket("Move", 2, new Vector2(20, 20).ToString());
            byte[] data = packet.sendData;

            packet.ClientRecvPacket(data);

            Assert.IsTrue(game.clientManager.Decode(packet) == new Vector2(20, 20));
        }
    }
}
