using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Text;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// Checks a packet can be endcoded and decoded correctly
    /// </summary>
    [TestClass()]
    public class ServerPackets
    {
        ServerPacket packetEncode;
        ServerPacket packetDecode;

        [TestMethod()]
        public void PacketEncodeTest()
        {
            packetEncode = new ServerPacket();
            packetEncode.ServerSendPacket("Move", 2, "Test");

            Assert.IsTrue(Encoding.UTF8.GetString(packetEncode.sendData) == "\u0003\0\0\0\u0002\0\0\0Test");

        }

        [TestMethod()]
        public void PacketDecodeTest()
        {
            packetDecode = new ServerPacket();
            packetDecode.ServerSendPacket("Move", 2, "Test");
            byte[] data = packetDecode.sendData;

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
