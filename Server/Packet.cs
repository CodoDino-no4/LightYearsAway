using System.Text;

namespace Server
{
    public class Packet
    {
        public enum Command
        {
            Move, // Astro movement
            Zoom, // Zoom in/out
            Place, // PLace a tile
            Join, // A clinet joins the server
            Leave, // A client leaves the server
            Null // Default

        }

        private Command command;

        private string clientId;
        private string data;

        public Packet(int command, string clientId, string data)
        {
            this.command = (Command)command;
            this.clientId = clientId;
            this.data = data;
        }


        // Description   -> |dataIdentifier|name length|message length|    name   |    message   |
        // Size in bytes -> |       4      |     4     |       4      |name length|message length|

        public Packet(byte[] dataStream)
        {
            // Command (4 bytes)
            command = (Command)BitConverter.ToInt32(dataStream, 0);

            // Store the length of the clientID
            int clientIdLen = BitConverter.ToInt32(dataStream, 4);

            // Store the length of the data (4 bytes)
            int commandLen = BitConverter.ToInt32(dataStream, 8);

            // Read the message field
            if (commandLen > 0)
                data = Encoding.UTF8.GetString(dataStream, 12 + clientIdLen, commandLen);
            else
                data = null;
        }

        // Converts the packet into a byte array for sending/receiving 
        public byte[] GetDataStream()
        {
            List<byte> dataStream = new List<byte>();

            // Add the command
            dataStream.AddRange(BitConverter.GetBytes((int)command));

            // Add the message length and message
            if (data != null || clientId != null)
            {
                dataStream.AddRange(BitConverter.GetBytes(clientId.Length));
                dataStream.AddRange(BitConverter.GetBytes(data.Length));
                dataStream.AddRange(Encoding.UTF8.GetBytes(data));
            }
            else
                dataStream.AddRange(BitConverter.GetBytes(0)); ;

            return dataStream.ToArray();
        }

    }
}
