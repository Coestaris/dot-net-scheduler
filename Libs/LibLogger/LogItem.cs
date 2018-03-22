using System;
using System.Text.RegularExpressions;

namespace Program
{
    public class LogItem
    {
        public static Regex startRegex = new Regex(Logger.LogStartRegex);

        public char Tag;
        public DateTime DateTime;
        public string TagName;
        public string Message;

        public LogItem()
        {
        }

        public LogItem(char tag, DateTime dateTime, string tagName, string message)
        {
            Tag = tag;
            DateTime = dateTime;
            TagName = tagName;
            Message = message;
        }
    }
}
