using System.Text;

public class ServerPacket
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
    public ServerPacket()
    {

    }

    // Converts server payload into a byte stream
    public byte[] ServerSendPacket(string command, string data)
    {
        // Byte stream
        List<byte> byteStream = new List<byte>();

        // Get cmd index number
        int cmdTmp = (int)(Command)Enum.Parse(typeof(Command), command, true);

        // Add the cmd to the datastream
        byteStream.AddRange(BitConverter.GetBytes(cmdTmp));

        // Add payload
        if (data != null)
        {
            byteStream.AddRange(Encoding.UTF8.GetBytes(data));
        }
        else
            byteStream.AddRange(BitConverter.GetBytes(0));

        return byteStream.ToArray();
    }

    // converts the bytes into a Packet Object
    public void ServerRecvPacket(byte[] data)
    {   
        // Decode the cmd
        // Length is always 1
        cmd = BitConverter.ToInt32(data, 0);
        Console.WriteLine(cmd);

        // Decode the clientId
        // Length is always 1
        clientId = BitConverter.ToInt32(data, 4);
        Console.WriteLine(clientId);

        // Decode the payload
        // Length is variable so get the length
        int dataLen = data.Length - 8;

        // Copy payload to new array and get the string
        byte[] dataSegment = new byte[dataLen];
        Buffer.BlockCopy(data, 8, dataSegment, 0, dataLen);

        payload = Encoding.UTF8.GetString(dataSegment);
        Console.WriteLine(payload);

    }
}
