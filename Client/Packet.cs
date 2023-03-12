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
        Zoom,   // Zoom in/out
        Place,  // PLace a tile

    }

    // Current command
    private int command = 0;

    // Client unique ID
    private string clientID;

    // Payload sent within packet
    private string data;

    //Creates an instance of the packet and sets the data
    public Packet(string command)
    {
        // Set command
        this.command = (int)(Command)System.Enum.Parse(typeof(Command), command);
        this.data = "What is up from client";

    }

    // Converts the Packet into an array of bytes
    public byte[] MakeBytes()
    {
        // Byte stream
        List<byte> dataStream = new List<byte>();

        // Add the command
        dataStream.AddRange(BitConverter.GetBytes((int)command));

        // Add client ID
        if (clientID != null)
        {
            dataStream.AddRange(BitConverter.GetBytes(clientID.Length));
            dataStream.AddRange(Encoding.UTF8.GetBytes(clientID));
        }
        else
            dataStream.AddRange(BitConverter.GetBytes(0));

        // Add data
        if (data != null)
        {
            dataStream.AddRange(BitConverter.GetBytes(data.Length));
            dataStream.AddRange(Encoding.UTF8.GetBytes(data));
        }
        else
            dataStream.AddRange(BitConverter.GetBytes(0));

        return dataStream.ToArray();
    }

    public Packet()
    {

    }


    // Description   -> |dataIdentifier|name length|message length|    name   |    message   |
    // Size in bytes -> |       4      |     4     |       4      |name length|message length|

    // Description   -> |    command   |name length|message length|    name   |    message   |
    // Size in bytes -> |       4      |     4     |       4      |name length|message length|

    // converts the bytes into a Packet
    public void MakePacket(byte[] data)
    {
        // Gets the formation of the packet length

        // Command (1 byte)
        command = BitConverter.ToInt32(data, 0);

        // Store the length of the clientID (4 bytes)
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
