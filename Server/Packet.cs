using System.Text;

public class Packet
{
    // Command types
    private enum Command
    {
        Null,   // Default
        Join,   // A client joins the server
        Leave,  // A client leaves the server
        Move,   // Astro movement
        Place,  // PLace a tile

    }

    // Current command
    private int command = 0;

    // Client unique ID
    private string clientId;

    // Payload sent within packetSent
    private string data;

    //Creates an instance of packetSent
    public Packet()
    {

    }

    // Converts data into an array of bytes
    public byte[] MakeBytes(string command, string data)
    {
        // Set packetSent data
        this.command = (int)(Command)Enum.Parse(typeof(Command), command, true);
        this.data = data;

        // Byte stream
        List<byte> dataStream = new List<byte>();

        // Add the command
        dataStream.AddRange(BitConverter.GetBytes(this.command));

        // Add data
        if (data != null)
        {
            dataStream.AddRange(BitConverter.GetBytes(this.data.Length));
            dataStream.AddRange(Encoding.UTF8.GetBytes(this.data));
        }
        else
            dataStream.AddRange(BitConverter.GetBytes(0));

        return dataStream.ToArray();
    }

    // converts the bytes into a Packet
    public void MakePacket(byte[] data)
    {
        // Gets the formation of the packetSent length

        // Command (1 byte)
        command = BitConverter.ToInt32(data, 0);

        // Store the length of the clientId (4 bytes)
        int clientIDLen = BitConverter.ToInt32(data, 1);

        // Store the length of the data (4 bytes)
        int dataLen = BitConverter.ToInt32(data, 5);

        // Read the message field
        if (clientIDLen > 0 && dataLen > 0)
            this.data = Encoding.UTF8.GetString(data, 9 + clientIDLen, dataLen);
        else
            this.data = null;

    }
}
