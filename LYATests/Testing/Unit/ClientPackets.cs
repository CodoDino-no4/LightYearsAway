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

    [DoNotParallelize()]
    [TestClass()]
    public class ClientPackets
    {

        private LYA game;
        private PacketFormer packetEncode;
        private PacketFormer packetDecode;
        private PacketFormer packetPayload;

        /// <summary>
        /// Initialises the game and runs one frame
        /// </summary>
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
            packetEncode = new PacketFormer();
            packetEncode.ClientSendPacket("Move", 2, 0, 0, "Test");

            Assert.IsTrue(Encoding.UTF8.GetString(packetEncode.sendData) == "\u0003\0\0\0\u0002\0\0\0Test");

        }

        [TestMethod()]
        public void PacketDecodeTest()
        {
            packetDecode = new PacketFormer();
            packetDecode.ClientSendPacket("Move", 2, 0, 0, "Test");
            byte[] data = packetDecode.sendData;

            packetDecode.ClientRecvPacket(data);

            Assert.IsTrue(packetDecode.cmd == 3);
            Assert.IsTrue(packetDecode.clientId == 2);
            Assert.IsTrue(packetDecode.payload == "Test");
        }

        [TestMethod()]
        public void PayloadDecodeTest()
        {
            packetPayload = new PacketFormer();
            packetPayload.ClientSendPacket("Move", 2, 20, 20, "");
            byte[] data = packetPayload.sendData;

            packetPayload.ClientRecvPacket(data);

            Assert.IsTrue(new Vector2(game.clientManager.packetRecv.posX, game.clientManager.packetRecv.posY) == new Vector2(20, 20));
        }


        [TestCleanup()]
        public void CleanUp()
        {
            game.Exit();
            game = null;

        }
    }
}
