namespace FeliCaReader.FormsApp.Helpers
{
    public static class ByteConverter
    {
        public static int ToInt32(byte[] bytes, int offset, int length)
        {
            var sum = 0;
            for (var i = 0; i < length; i++)
            {
                sum *= 256;
                sum += bytes[offset + i];
            }

            return sum;
        }

        public static int ToInt32L(byte[] bytes, int offset, int length)
        {
            var sum = 0;
            for (var i = length - 1; i >= 0; i--)
            {
                sum *= 256;
                sum += bytes[offset + i];
            }

            return sum;
        }
    }
}
