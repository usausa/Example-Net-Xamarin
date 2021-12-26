namespace NfcSample.FormsApp.Components.Nfc;

using System.Diagnostics.CodeAnalysis;

public class ReadBlock
{
    public byte BlockNo { get; set; }

    [AllowNull]
    public byte[] BlockData { get; set; }
}

public static class FeliCaExtensions
{
    public static byte[] ExecutePolling(this INfc nfc, short systemCode)
    {
        var command = new byte[6];
        command[0] = (byte)command.Length;
        command[1] = 0x00;
        command[2] = (byte)(systemCode >> 8);
        command[3] = (byte)(systemCode & 0xFF);
        command[4] = 0x01;
        command[5] = 0x00;

        var response = nfc.Access(command);
        if (response.Length < 18)
        {
            return Array.Empty<byte>();
        }

        return response.SubArray(2, 8);
    }

    public static bool ExecuteReadWoe(this INfc nfc, byte[] idm, short serviceCode, params ReadBlock[] blocks)
    {
        var command = new byte[14 + (blocks.Length * 2)];
        command[0] = (byte)command.Length;
        command[1] = 0x06;
        Buffer.BlockCopy(idm, 0, command, 2, idm.Length);
        command[10] = 1;
        command[11] = (byte)(serviceCode & 0xff);
        command[12] = (byte)(serviceCode >> 8);
        command[13] = (byte)blocks.Length;
        for (var i = 0; i < blocks.Length; i++)
        {
            var offset = 14 + (i * 2);
            command[offset] = 0x80;
            command[offset + 1] = blocks[i].BlockNo;
        }

        var response = nfc.Access(command);
        if (response.Length < 12)
        {
            return false;
        }

        if ((response[10] != 0x00) || (response[11] != 0x00))
        {
            return false;
        }

        if (response.Length < (13 + (response[12] * 16)))
        {
            return false;
        }

        for (var i = 0; i < blocks.Length; i++)
        {
            var offset = 13 + (16 * i);
            blocks[i].BlockData = response.SubArray(offset, 16);
        }

        return true;
    }

    private static byte[] SubArray(this byte[] array, int offset, int length)
    {
        var bytes = new byte[Math.Min(length, array.Length - offset)];
        Buffer.BlockCopy(array, offset, bytes, 0, bytes.Length);
        return bytes;
    }
}
