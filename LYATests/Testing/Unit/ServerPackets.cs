using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// Checks a packet can be endcoded and decoded correctly
    /// </summary>
    [TestClass()]
    public class ServerPackets
    {
        [TestMethod()]
        public void PacketEncodeTest()
        {
            ServerPacket packet = new ServerPacket();
            packet.ServerSendPacket("Move", 2, "Test");

            Assert.IsTrue(Encoding.UTF8.GetString(packet.sendData) == "\u0003\0\0\0\u0002\0\0\0Test");

        }

        [TestMethod()]
        public void PacketDecodeTest()
        {
            ServerPacket packet = new ServerPacket();
            packet.ServerSendPacket("Move", 2, "Test");
            byte[] data = packet.sendData;

            packet.ServerRecvPacket(data);

            Assert.IsTrue(packet.cmd == 3);
            Assert.IsTrue(packet.clientId == 2);
            Assert.IsTrue(packet.payload == "Test");
        }
    }
}
