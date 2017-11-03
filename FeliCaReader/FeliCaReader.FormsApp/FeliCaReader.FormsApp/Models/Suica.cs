namespace FeliCaReader.FormsApp.Models
{
    using System;
    using System.Collections.Generic;

    using FeliCaReader.FormsApp.Helpers;

    public static class Suica
    {
        private static readonly Dictionary<byte, string> TerminalNames = new Dictionary<byte, string>
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

        private static readonly Dictionary<byte, string> ProcessNames = new Dictionary<byte, string>
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

        private static readonly HashSet<byte> ProcessOfSales = new HashSet<byte>(new byte[] { 70, 72, 73, 74, 75 });

        private static readonly HashSet<byte> ProcessOfBus = new HashSet<byte>(new byte[] { 13, 15, 31, 35 });

        private static readonly Dictionary<int, string> RegionNames = new Dictionary<int, string>
        {
            { 0, "首都圏" },
            { 1, "中部圏" },
            { 2, "近畿圏" },
            { 3, "その他" }
        };

        public static string ConvertTerminalString(byte type)
        {
            return TerminalNames.TryGetValue(type, out string value) ? value : type.ToString("X");
        }

        public static string ConvertProcessString(byte process)
        {
            var processType = (byte)(process & 0b01111111);
            return ProcessNames.TryGetValue(processType, out string value) ? value : processType.ToString("X");
        }

        public static bool IsProcessOfSales(byte process)
        {
            var processType = (byte)(process & 0b01111111);
            return ProcessOfSales.Contains(processType);
        }

        public static bool IsProcessOfBus(byte process)
        {
            var processType = (byte)(process & 0b01111111);
            return ProcessOfBus.Contains(processType);
        }

        public static string ConvertRegionString(int region)
        {
            return RegionNames.TryGetValue(region, out string value) ? value : region.ToString("X");
        }

        public static DateTime ConvertDate(byte[] bytes, int offset)
        {
            var year = 2000 + (bytes[offset] >> 1);
            var month = ByteConverter.ToInt32(bytes, offset, 2) >> 5 & 0b1111;
            var day = bytes[offset + 1] & 0b11111;
            return new DateTime(year, month, day);
        }

        public static DateTime ConvertDateTime(byte[] bytes, int offset)
        {
            var year = 2000 + (bytes[offset] >> 1);
            var month = ByteConverter.ToInt32(bytes, offset, 2) >> 5 & 0b1111;
            var day = bytes[offset + 1] & 0b11111;
            var hour = bytes[offset + 2] >> 3;
            var minute = ByteConverter.ToInt32(bytes, offset + 2, 2) >> 5 & 0b111111;
            return new DateTime(year, month, day, hour, minute, 0);
        }

        public static SuicaAccessData ConvertToAccessData(byte[] data)
        {
            return new SuicaAccessData
            {
                Balance = ByteConverter.ToInt32L(data, 11, 2),
                TransactionId = ByteConverter.ToInt32(data, 14, 2)
            };
        }

        public static SuicaLogData ConvertToLogData(byte[] data)
        {
            if (data[1] == 0x00)
            {
                return null;
            }

            return new SuicaLogData
            {
                TerminalType = data[0],
                ProcessType = (byte)(data[1] & 0b01111111),
                WithCash = (data[1] & 0b10000000) != 0,
                DateTime = IsProcessOfSales(data[1]) ? ConvertDateTime(data, 4) : ConvertDate(data, 4),
                Balance = ByteConverter.ToInt32L(data, 10, 2),
                TransactionId = ByteConverter.ToInt32(data, 13, 2)
            };
        }
    }
}
