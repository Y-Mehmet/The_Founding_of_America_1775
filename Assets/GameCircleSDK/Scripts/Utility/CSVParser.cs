using System.Linq;
using System.Text;
using UnityEngine;

namespace GameCircleSDK.Utility
{
    public static class CSVParser
    {
        private static readonly string[] TrueStrings = {"true", "t", "yes", "y" };
        private static readonly string[] FalseStrings = {"false", "f", "no", "n" };

        public static void Parse(string csv)
        {
            var json = TurnCsvToJson(csv);
            GameCircle.Settings.OverrideWithJson(json);
        }

        private static string TurnCsvToJson(string toParse)
        {
            toParse = toParse.Replace(" ", "");
            toParse = toParse.Replace("\r", "");
            toParse = toParse.Replace("\"", "");
            var toReturn = new StringBuilder("{");
            var lines = toParse.Split("\n");
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if(line == "") continue;
                var tuple = line.Split(",");
                var key = tuple[0];
                var value = tuple[1];
                if (key == "") continue;
                
                toReturn.Append(ParseValue(key) + ":" + ParseValue(value));
                if(i != lines.Length - 1)
                    toReturn.Append(",");
            }
            return toReturn + "}";
        }

        private static string ParseValue(string raw)
        {
            var rawLower = raw.ToLower();
            if (TrueStrings.Contains(rawLower)) return "true";
            if (FalseStrings.Contains(rawLower)) return "false";
            return "\"" + raw + "\"";
        }
    }
}
