using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// Checks a packet can be endcoded and decoded correctly
    /// </summary>
    [DoNotParallelize()]
    [TestClass()]
    public class ServerPackets
    {
        private ServerPacket packetEncode;
        private ServerPacket packetDecode;

        [TestMethod()]
        public void PacketEncodeTest()
        {
            packetEncode = new ServerPacket();

            Assert.IsTrue(Encoding.UTF8.GetString(packetEncode.ServerSendPacket("Move", 2, 0, 0, "Test")) == "\u0003\0\0\0\u0002\0\0\0\0\0\0\0\0\0\0\0Test");
        }

        [TestMethod()]
        public void PacketDecodeTest()
        {
            packetDecode = new ServerPacket();

            byte[] data = packetDecode.ServerSendPacket("Move", 2, 0, 0, "Test");

            packetDecode.ServerRecvPacket(data);

            Assert.IsTrue(packetDecode.cmd == 3);
            Assert.IsTrue(packetDecode.clientId == 2);
            Assert.IsTrue(packetDecode.payload == "Test");
        }

        [TestCleanup()]
        public void CleanUp()
        {

            packetDecode = null;
            packetEncode = null;

        }
    }
}
