using Scheduler.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scheduler.Shell
{
    public class ShellData
    {
        public String FileName { get; set; }
        public String[] Refs { get; set; }
        public ActionType AT { get; set; }
        public CommandType CT { get; set; }
        public RepeatableType RT { get; set; }
        public int MP { get; set; }
        public int MRC { get; set; }
        public DateTime MST { get; set; }
        public DateTime OTD { get; set; }
        public List<DateTime> SD { get; set; }
        public String Name { get; set; }
        public String Autor { get; set; }
        public String Descr { get; set; }

        internal void ValidParams()
        {
            if (String.IsNullOrEmpty(Name))
                throw new ArgumentException("Name cant be emtpy");

            if (AT == ActionType.CMDScript)
            {
                if (String.IsNullOrEmpty(FileName))
                    throw new ArgumentException("You must specify some .cmd source file");
            }
            else
            {
                if (String.IsNullOrEmpty(FileName))
                    throw new ArgumentException("You must specify some .cs source file");

                if (Refs == null || Refs.Length == 0)
                    throw new ArgumentException("You must specify some refferences");
            }

            if (CT == CommandType.Repeatable)
            {
                if (RT == RepeatableType.Monotonous)
                {
                    if (MRC == 0)
                        throw new ArgumentException("Repeat count can`t be 0");

                    if (MP == 0)
                        throw new ArgumentException("Period can`t be 0");
                }
                else
                {
                    if (SD == null || SD.Count == 0)
                        throw new ArgumentException("You must specify some specific dates");
                }
            }
        }

        internal void AssignValue(string name, string value)
        {
            if (name == TokenKey.Refs)
                Refs = value.Split('_').Select(p => p.Trim('\"')).ToArray();
            else if (name == TokenKey.FName)
            {
                FileName = (value).Trim('\"');

                if (!File.Exists(FileName))
                    throw new FileNotFoundException("File not found", value);

            }
            else if (name == TokenKey.ActionType)
            {

                if (value.ToLower() == "cmd")
                    AT = ActionType.CMDScript;
                else if (value.ToLower() == "csharp")
                    AT = ActionType.CSharpCode;
                else throw new ArgumentException(string.Format("{0} is wrong at value. Allowed cmd and csharp", value), nameof(value));

            }
            else if (name == TokenKey.CommandType)
            {

                if (value.ToLower() == "r")
                    CT = CommandType.Repeatable;
                else if (value.ToLower() == "o")
                    CT = CommandType.OneTime;
                else throw new ArgumentException(string.Format("{0} is wrong at value. Allowed r and o", value), nameof(value));

            }
            else if (name == TokenKey.RepType)
            {

                if (value.ToLower() == "m")
                    RT = RepeatableType.Monotonous;
                else if (value.ToLower() == "s")
                    RT = RepeatableType.Specific;
                else throw new ArgumentException(string.Format("{0} is wrong at value. Allowed m and s", value), nameof(value));

            }
            else if (name == TokenKey.MonPeriod)
                MP = int.Parse(value);
            else if (name == TokenKey.MonRepeatCount)
                MRC = int.Parse(value);
            else if (name == TokenKey.MonStartTime)
                MST = TimeHelper.UnixTimestampToDateTime(int.Parse(value));
            else if (name == TokenKey.OnetimeDate)
                OTD = TimeHelper.UnixTimestampToDateTime(int.Parse(value));
            else if (name == TokenKey.SpecificDates)
            {
                var parts = value.Split('.');
                List<DateTime> sd = new List<DateTime>();
                foreach (var item in parts)
                    sd.Add(TimeHelper.UnixTimestampToDateTime(long.Parse(item)));
            }
            else if (name == TokenKey.Name)
                Name = (value).Trim('\"');
            else if (name == TokenKey.Autor)
                Autor = (value).Trim('\"');
            else if (name == TokenKey.Descr)
                Descr = (value).Trim('\"');
        }
    }
}

