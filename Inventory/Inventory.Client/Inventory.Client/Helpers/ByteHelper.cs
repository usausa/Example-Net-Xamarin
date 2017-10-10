namespace Inventory.Client.Helpers
{
    using System;

    public static class ByteHelper
    {
        public static int CompareBytes(byte[] bytes1, byte[] bytes2)
        {
            var length1 = CalcTrimBytesLength(bytes1);
            var length2 = CalcTrimBytesLength(bytes2);

            for (var i = 0; i < Math.Min(length1, length2); i++)
            {
                var c = bytes1[i] - bytes2[i];
                if (c != 0)
                {
                    return c;
                }
            }

            return length1 - length2;
        }

        public static int CalcTrimBytesLength(byte[] bytes)
        {
            var length = bytes.Length;
            while ((length > 0) && ((bytes[length - 1] == 0x20) || (bytes[length - 1] == 0x00)))
            {
                length--;
            }

            return length;
        }
    }
}
