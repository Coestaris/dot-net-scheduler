using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Program
{
    public static class LogParser
    {
        public static List<LogItem> Parse(string FileName)
        {
            var Items = new List<LogItem>();
            var lines = File.ReadAllLines(FileName);
            foreach (var item in lines)
            {
                //Парсинг завязан на текущем формате лога!
                if (LogItem.startRegex.IsMatch(item))
                {
                    var parts = item.Split('/');
                    var lastStr = parts[0].Substring(parts[0].Length - 2, 2);
                    parts[0] = parts[0].Remove(parts[0].Length - 2, 2);
                    var dt = DateTime.ParseExact(parts[0], Logger.DateFormat, CultureInfo.InvariantCulture);
                    var tag = lastStr[1];
                    var parts1 = parts[1].Split(':');
                    var stringTag = parts1[0].Trim('[', ']');
                    var message = string.Join(":", parts1.Skip(1).Select(p => p.Trim()).ToArray());
                    LogItem li = new LogItem()
                    {
                        DateTime = dt,
                        Message = message,
                        Tag = tag,
                        TagName = stringTag
                    };
                    Items.Add(li);
                }
                else
                {
                    Items.Last().Message += item.Trim(' ', '\t', '\n');
                }
            }
            return Items;
        }
    }
}
