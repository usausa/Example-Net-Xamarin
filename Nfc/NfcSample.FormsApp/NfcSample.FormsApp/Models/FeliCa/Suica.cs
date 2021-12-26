namespace NfcSample.FormsApp.Models.FeliCa
{
    using System.Buffers.Binary;

    public static class Suica
    {
        private static readonly Dictionary<byte, string> TerminalNames = new()
        {
            { 3, "精算機" },
            { 4, "携帯型端末" },
            { 5, "車載端末" },
            { 7, "券売機" },
            { 8, "券売機" },
            { 9, "入金機" },
            { 18, "券売機" },
            { 20, "券売機等" },
            { 21, "券売機等" },
            { 22, "改札機" },
            { 23, "簡易改札機" },
            { 24, "窓口端末" },
            { 25, "窓口端末" },
            { 26, "改札端末" },
            { 27, "携帯電話" },
            { 28, "乗継精算機" },
            { 29, "連絡改札機" },
            { 31, "簡易入金機" },
            { 199, "物販端末" },
            { 200, "自販機" }
        };

        private static readonly Dictionary<byte, string> ProcessNames = new()
        {
            { 1, "運賃支払" },
            { 2, "チャージ" },
            { 3, "磁気券購入" },
            { 4, "精算" },
            { 5, "入場精算" },
            { 6, "改札窓口処理" },
            { 7, "新規発行" },
            { 8, "窓口控除" },
            { 13, "バス(PiTaPa系)" },
            { 15, "バス(IruCa系)" },
            { 17, "再発行処理" },
            { 19, "新幹線利用" },
            { 20, "入場時AC" },
            { 21, "出場時AC" },
            { 31, "バスチャージ" },
            { 35, "バス路面電車企画券購入" },
            { 70, "物販" },
            { 72, "特典チャージ" },
            { 73, "レジ入金" },
            { 74, "物販取消" },
            { 75, "入場物販" }
        };

        private static readonly HashSet<byte> ProcessOfSales = new(new byte[] { 70, 72, 73, 74, 75 });

        private static readonly HashSet<byte> ProcessOfBus = new(new byte[] { 13, 15, 31, 35 });

        private static readonly Dictionary<int, string> RegionNames = new()
        {
            { 0, "首都圏" },
            { 1, "中部圏" },
            { 2, "近畿圏" },
            { 3, "その他" }
        };

        public static string ConvertTerminalString(byte type)
        {
            return TerminalNames.TryGetValue(type, out var value) ? value : type.ToString("X");
        }

        public static string ConvertProcessString(byte process)
        {
            var processType = ConvertProcessType(process);
            var withCache = (process & 0b10000000) != 0;

            var name = ProcessNames.TryGetValue(processType, out var value) ? value : processType.ToString("X");

            return withCache ? name + " 現金併用" : name;
        }

        public static byte ConvertProcessType(byte process)
        {
            return (byte)(process & 0b01111111);
        }

        public static bool IsProcessOfSales(byte process)
        {
            var processType = ConvertProcessType(process);
            return ProcessOfSales.Contains(processType);
        }

        public static bool IsProcessOfBus(byte process)
        {
            var processType = ConvertProcessType(process);
            return ProcessOfBus.Contains(processType);
        }

        public static string ConvertRegionString(int region)
        {
            return RegionNames.TryGetValue(region, out var value) ? value : region.ToString("X");
        }

        public static DateTime ConvertDate(byte[] bytes, int offset)
        {
            var year = 2000 + (bytes[offset] >> 1);
            var month = BinaryPrimitives.ReadUInt16BigEndian(bytes.AsSpan(offset, 2)) >> 5 & 0b1111;
            var day = bytes[offset + 1] & 0b11111;
            return new DateTime(year, month, day);
        }

        public static DateTime ConvertDateTime(byte[] bytes, int offset)
        {
            var year = 2000 + (bytes[offset] >> 1);
            var month = BinaryPrimitives.ReadUInt16BigEndian(bytes.AsSpan(offset, 2)) >> 5 & 0b1111;
            var day = bytes[offset + 1] & 0b11111;
            var hour = bytes[offset + 2] >> 3;
            var minute = BinaryPrimitives.ReadUInt16BigEndian(bytes.AsSpan(offset + 2, 2)) >> 5 & 0b111111;
            return new DateTime(year, month, day, hour, minute, 0);
        }

        public static SuicaAccessData ConvertToAccessData(byte[] data)
        {
            return new SuicaAccessData
            {
                Balance = BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(11, 2)),
                TransactionId = BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(14, 2))
            };
        }

        public static SuicaLogData? ConvertToLogData(byte[] data)
        {
            if (data[1] == 0x00)
            {
                return null;
            }

            return new SuicaLogData
            {
                Terminal = data[0],
                Process = data[1],
                DateTime = IsProcessOfSales(data[1]) ? ConvertDateTime(data, 4) : ConvertDate(data, 4),
                Balance = BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(10, 2)),
                TransactionId = BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(13, 2))
            };
        }
    }
}
