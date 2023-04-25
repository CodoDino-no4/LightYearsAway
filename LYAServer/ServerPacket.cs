using System.Text;

namespace LYAServer
{
    public class ServerPacket
    {
        /// <summary>
        /// Valid command types
        /// </summary>
        private enum Command
        {
            Null,   // Default
            Join,   // A Client joins the server
            Leave,  // A Client leaves the server
            Move,   // Astro movement
            Place,  // Place a tile
            Error, // Error message

        }

        // Current cmd
        public int cmd = 0;

        // Client unique ID
        public int clientId;

        // Cooridnate data
        public int posX;
        public int posY;

        // Payload sent within packetSent
        public string payload;

        /// <summary>
        /// Instantiate the class
        /// </summary>
        public ServerPacket()
        {

        }

        /// <summary>
        /// Converts data into a byte stream
        /// </summary>
        public byte[] ServerSendPacket(string command, int id, int x, int y, string data)
        {
            // Byte stream
            List<byte> byteStream = new List<byte>();

            // Get cmd index number
            int cmdVal = (int)(Command)Enum.Parse(typeof(Command), command, true);
            cmd = cmdVal;

            // Add the command
            byteStream.AddRange(BitConverter.GetBytes(cmdVal));

            // Add client ID
            byteStream.AddRange(BitConverter.GetBytes(id));
            clientId = id;

            // Add coordinates X
            byteStream.AddRange(BitConverter.GetBytes(x));
            posX = x;

            // Add coordinates Y
            byteStream.AddRange(BitConverter.GetBytes(y));
            posY = y;

            // Add payload
            if (data != null)
            {
                byteStream.AddRange(Encoding.UTF8.GetBytes(data));
                payload = data;
            }
            else
                byteStream.AddRange(BitConverter.GetBytes(0));

            return byteStream.ToArray();
        }

        /// <summary>
        /// Converts byte stream into packet data
        /// </summary>
        public void ServerRecvPacket(byte[] data)
        {
            // Decode the cmd
            // Length is always 1
            cmd = BitConverter.ToInt32(data, 0);

            // Decode the clientId
            // Length is always 1
            clientId = BitConverter.ToInt32(data, 4);

            // Decode coordinate X
            posX = BitConverter.ToInt32(data, 8);

            // Decode coordinate Y
            posY = BitConverter.ToInt32(data, 12);

            // Decode the payload
            // Length is variable so get the length
            int dataLen = data.Length - 16;

            // Copy payload to new array and get the string
            byte[] dataSegment = new byte[dataLen];
            Buffer.BlockCopy(data, 16, dataSegment, 0, dataLen);

            payload = Encoding.UTF8.GetString(dataSegment);
        }
    }

}
