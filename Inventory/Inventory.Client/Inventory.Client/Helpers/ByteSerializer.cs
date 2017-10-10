namespace Inventory.Client.Helpers
{
    using System;
    using System.Globalization;
    using System.Text;

    using Smart;

    public static class ByteSerializer
    {
        private static readonly Encoding SjisEncoding = Encoding.GetEncoding("Shift_JIS");

        private static readonly char[] TrimChars = { (char)0x00, ' ' };

        // TO

        private static string ReadSjisString(byte[] buffer, int offset, int length)
        {
            return SjisEncoding.GetString(buffer, offset, length);
        }

        public static string ReadString(byte[] buffer, int offset, int length)
        {
            return ReadSjisString(buffer, offset, length).Trim(TrimChars);
        }

        public static long ReadLong(byte[] buffer, int offset, int length)
        {
            return Convert.ToInt64(ReadSjisString(buffer, offset, length).Trim(TrimChars));
        }

        public static int ReadInteger(byte[] buffer, int offset, int length)
        {
            return Convert.ToInt32(ReadSjisString(buffer, offset, length).Trim(TrimChars));
        }

        public static int? ReadIntegerNullable(byte[] buffer, int offset, int length)
        {
            var str = ReadSjisString(buffer, offset, length).Trim(TrimChars);
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            return Convert.ToInt32(str);
        }

        public static DateTimeOffset ReadDateTime(byte[] buffer, int offset, int length)
        {
            var str = ReadSjisString(buffer, offset, length);
            return new DateTime(
                Convert.ToInt32(str.Substring(0, 4)),
                Convert.ToInt32(str.Substring(4, 2)),
                Convert.ToInt32(str.Substring(6, 2)),
                Convert.ToInt32(str.Substring(8, 2)),
                Convert.ToInt32(str.Substring(10, 2)),
                Convert.ToInt32(str.Substring(12, 2)),
                Convert.ToInt32(str.Substring(14, 3)));
        }

        public static DateTimeOffset? ReadDateTimeNullable(byte[] buffer, int offset, int length)
        {
            var str = ReadSjisString(buffer, offset, length);
            if (String.IsNullOrEmpty(str.Trim(TrimChars)))
            {
                return null;
            }

            return new DateTime(
                Convert.ToInt32(str.Substring(0, 4)),
                Convert.ToInt32(str.Substring(4, 2)),
                Convert.ToInt32(str.Substring(6, 2)),
                Convert.ToInt32(str.Substring(8, 2)),
                Convert.ToInt32(str.Substring(10, 2)),
                Convert.ToInt32(str.Substring(12, 2)),
                Convert.ToInt32(str.Substring(14, 3)));
        }

        public static bool ReadBoolean(byte[] buffer, int offset, int length)
        {
            var str = ReadSjisString(buffer, offset, length).Trim(TrimChars);
            return !String.IsNullOrEmpty(str) && (str != "0");
        }

        // From

        public static void WriteString(string value, byte[] buffer, int offset, int length)
        {
            var source = SjisEncoding.GetBytes(value);
            buffer.Fill(offset, length, 0);
            Buffer.BlockCopy(source, 0, buffer, offset, Math.Min(source.Length, length));
        }

        public static void WriteLong(long value, byte[] buffer, int offset, int length)
        {
            var format = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", length);
            var str = String.Format(CultureInfo.InvariantCulture, format, value);
            WriteString(str, buffer, offset, length);
        }

        public static void WriteInteger(int value, byte[] buffer, int offset, int length)
        {
            var format = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", length);
            var str = String.Format(CultureInfo.InvariantCulture, format, value);
            WriteString(str, buffer, offset, length);
        }

        public static void WriteIntegerNullable(int? value, byte[] buffer, int offset, int length)
        {
            if (value.HasValue)
            {
                var format = String.Format(CultureInfo.InvariantCulture, "{{0:D{0}}}", length);
                var str = String.Format(CultureInfo.InvariantCulture, format, value.Value);
                WriteString(str, buffer, offset, length);
            }
            else
            {
                WriteString(string.Empty, buffer, offset, length);
            }
        }

        public static void WriteDateTime(DateTimeOffset value, byte[] buffer, int offset, int length)
        {
            var str = value.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            WriteString(str, buffer, offset, length);
        }

        public static void WriteDateTimeNullable(DateTimeOffset? value, byte[] buffer, int offset, int length)
        {
            if (value.HasValue)
            {
                var str = value.Value.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                WriteString(str, buffer, offset, length);
            }
            else
            {
                WriteString(string.Empty, buffer, offset, length);
            }
        }

        public static void WriteBoolean(bool value, byte[] buffer, int offset, int length)
        {
            var str = value ? "1" : "0";
            WriteString(str, buffer, offset, length);
        }

        // CR+LF

        public static void WriteCrLf(byte[] buffer, int offset)
        {
            buffer[offset] = 0x0A;
            buffer[offset + 1] = 0x0D;
        }
    }
}
