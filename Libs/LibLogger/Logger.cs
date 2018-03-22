using LibCfg;
using System;
using System.IO;
using System.Text;

namespace Program
{
    public class Logger
    {
        public string FileName { get; set; }

        private string tag = "[Tag]";

        public string Tag { get => tag; set
            {
                tag = '[' + value + ']';
            }
        }

        public const string VItemRegex = @"\:V\/\[.{0,20}\]";
        public const string IItemRegex = @"\:I\/\[.{0,20}\]";
        public const string EItemRegex = @"\:E\/\[.{0,20}\]";
        public const string DItemRegex = @"\:D\/\[.{0,20}\]";
        public const string DateRegex = @"\d{2}\.\d{2}\.\d{4}\-\d{2}:\d{2}:\d{2}";
        public const string LogStartRegex = DateRegex + @":[VIEWD]\/\[.{1,30}]";
        public const string LogFormat = "{0}:{1}: {2}\n";
        public const string DateFormat = "dd.MM.yyyy-hh:mm:ss";

        private FileStream fs;

        public void E(string Message)
        {
            Log(fs, "E/" + Tag, Message);
        }

        public void E(string Tag, string Message)
        {
            Log(fs, $"E/[{Tag}]", Message);
        }

        public void D(string Message)
        {
            Log(fs, "D/" + Tag, Message);
        }

        public void D(string Tag, string Message)
        {
            Log(fs, $"D/[{Tag}]", Message);
        }

        public void V(string Message)
        {
            Log(fs, "V/" + Tag, Message);
        }

        public void V(string Tag, string Message)
        {
            Log(fs, $"V/[{Tag}]", Message);
        }

        public void W(string Message)
        {
            Log(fs, "W/" + Tag, Message);
        }

        public void W(string Tag, string Message)
        {
            Log(fs, $"W/[{Tag}]", Message);
        }

        public void I(string Message)
        {
            Log(fs, "I/" + Tag, Message);
        }

        public void I(string Tag, string Message)
        {
            Log(fs, $"I/[{Tag}]", Message);
        }

        public void Close()
        {
            if (!RealTime) fs.Close();
        }

        private void Log(FileStream fs, string status, string message)
        {
            WriteToStr(fs, string.Format(LogFormat, DateTime.Now.ToString(DateFormat), status, message));
        }

        private void WriteToStr(FileStream fs, string str)
        {
            if (RealTime)
            {
                fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                fs.Write(Encoding.Default.GetBytes(str), 0, str.Length);
                fs.Close();
            } else fs.Write(Encoding.Default.GetBytes(str), 0, str.Length);
        }

        private bool RealTime = false;

        public Logger(string fileName, string tag, bool realTime)
        {
            RealTime = realTime;
            FileName = SCHEnvironment.LogDir + fileName + ".log";
            Tag = tag;

            if(!realTime)
                fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        public Logger(string fileName, string tag)
        {
            FileName = SCHEnvironment.LogDir + fileName + ".log";
            Tag = tag;
            fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        public Logger(string fileName)
        {
            FileName = SCHEnvironment.LogDir + fileName + ".log";
            if (!RealTime) fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        ~Logger()
        {
            if (!RealTime) Close();
        }
    }
}
