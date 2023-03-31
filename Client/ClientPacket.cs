using System.Text;

public class ClientPacket
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

    // Current cmd
    private int cmd = 0;

    // Client unique ID
    private int clientId;

    // Payload sent within packetSent
    private string payload;

    //Creates an instance of packetSent
    public ClientPacket()
    {

    }

    // Converts data into an array of bytes
    public byte[] ClientSendPacket(string command, string clientId, string data)
    {
        // Set packet data
        cmd = (int)(Command)Enum.Parse(typeof(Command), command, true);
        this.clientId = Int32.Parse(clientId);
        payload = data;

        // Byte stream
        List<byte> dataStream = new List<byte>();
        
        // Add the command
        dataStream.AddRange(BitConverter.GetBytes(this.cmd));

        // Add client ID
        if (this.clientId != 0)
        {
            dataStream.AddRange(BitConverter.GetBytes(this.clientId));
        }
        else
            dataStream.AddRange(BitConverter.GetBytes(0));

        // Add data
        if (data != null)
        {
            dataStream.AddRange(Encoding.UTF8.GetBytes(this.payload));
        }
        else
            dataStream.AddRange(BitConverter.GetBytes(0));

        return dataStream.ToArray();
    }

    // converts the bytes into a Packet
    public void ClientRecvPacket(byte[] data)
    {
        // Decode the cmd
        // Length is always 1
        cmd = BitConverter.ToInt32(data, 0);
        Console.WriteLine(cmd);

        // Decode the payload
        // Length is variable so get the length
        int dataLen = data.Length - 4;

        // Copy payload to new array and get the string
        byte[] dataSegment = new byte[dataLen];
        Buffer.BlockCopy(data, 4, dataSegment, 0, dataLen);

        payload = Encoding.UTF8.GetString(dataSegment);
        Console.WriteLine(payload);

    }
}
