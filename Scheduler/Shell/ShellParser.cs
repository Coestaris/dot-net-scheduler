using Scheduler.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Shell
{
    public static class ShellParser
    {
        public static readonly List<AllowedToken> AllowedTokens = new List<AllowedToken>
        {
            new AllowedToken(TokenKey.FName, "Source filename"),
            new AllowedToken(TokenKey.ActionType, "Action type"),
            new AllowedToken(TokenKey.CommandType, "Command type"),
            new AllowedToken(TokenKey.RepType, "Repeatable type"),
            new AllowedToken(TokenKey.MonPeriod, "Monotonous period"),
            new AllowedToken(TokenKey.MonRepeatCount, "Monotonous repeat count"),
            new AllowedToken(TokenKey.MonStartTime, "Monotonous start time"),
            new AllowedToken(TokenKey.OnetimeDate, "One time date"),
            new AllowedToken(TokenKey.SpecificDates, "Specific dates"),
            new AllowedToken(TokenKey.Name, "Name of the script"),
            new AllowedToken(TokenKey.Autor, "Autor of the script"),
            new AllowedToken(TokenKey.Descr, "Description of the script"),
            new AllowedToken(TokenKey.Refs, "Refferences for CSharp source")
        };

        public static ShellData ParseData(string[] args)
        {
            ShellData result = new ShellData();
            if (args.Length % 2 != 0)
                throw new ArgumentException($"Length of params {args.Length} must be odd");
            for (int i = 0; i < args.Length; i += 2)
            {
                var name = args[i].Trim('-', ' ', '/', '\\');
                if (!AllowedTokens.Select(p => p.Name).Contains(name.ToLower()))
                    throw new ArgumentException($"{args[i]} is not allowed value");
                try
                {
                    result.AssignValue(name, args[i + 1]);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            try
            {
                result.ValidParams();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static string ShellDataToCmdString(ShellData shellData)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("-{0} \"{1}\" ", TokenKey.FName, shellData.FileName);
            sb.AppendFormat("-{0} {1} ", TokenKey.ActionType, shellData.AT == ActionType.CMDScript ? "cmd" : "csharp");
            sb.AppendFormat("-{0} {1} ", TokenKey.CommandType, shellData.CT == CommandType.OneTime ? "o" : "r");
            sb.AppendFormat("-{0} {1} ", TokenKey.RepType, shellData.RT == RepeatableType.Monotonous ? "m" : "s");
            if (shellData.CT == CommandType.OneTime)
            {
                sb.AppendFormat("-{0} {1} ", TokenKey.OnetimeDate, TimeHelper.DateTimeToUnixTimestamp(shellData.OTD));
            }
            else
            {
                if (shellData.RT == RepeatableType.Monotonous)
                {
                    sb.AppendFormat("-{0} {1} ", TokenKey.MonPeriod, shellData.MP);
                    sb.AppendFormat("-{0} {1} ", TokenKey.MonStartTime, TimeHelper.DateTimeToUnixTimestamp(shellData.MST));
                    sb.AppendFormat("-{0} {1} ", TokenKey.MonRepeatCount, shellData.MRC);
                }
                else
                {
                    sb.AppendFormat("-{0} {1} ", TokenKey.SpecificDates, string.Join(".", shellData.SD.Select(p => TimeHelper.DateTimeToUnixTimestamp(p)).ToArray()));
                }
            }
            sb.AppendFormat("-{0} \"{1}\" ", TokenKey.Name, shellData.Name);
            if(shellData.Autor != null) sb.AppendFormat("-{0} \"{1}\" ", TokenKey.Autor, shellData.Autor);
            if (shellData.Descr != null) sb.AppendFormat("-{0} \"{1}\" ", TokenKey.Descr, shellData.Descr);
            if (shellData.Refs != null) sb.AppendFormat("-{0} \"{1}\" ", TokenKey.Refs, string.Join("_", shellData.Refs));
            return sb.ToString();
        }

        public static SCHElement Parse(string[] args, string libDir)
        {
            var data = ParseData(args);
            SCHElement element = new SCHElement();
            element.Metadata.Name = data.Name;
            element.Metadata.Autor = data.Autor ?? "";
            element.Metadata.Description = data.Descr ?? "";
            element.Metadata.CreationDate = DateTime.Now;
            element.Data.ActionType = data.AT;
            element.Data.CommandType = data.CT;
            element.Data.RepeatableType = data.RT;
            if (data.AT == ActionType.CMDScript)
                element.Data.SetCMDScript(File.ReadAllText(data.FileName));
            else
            {
                element.Data.SetCSharpSource(data.Refs,
                    File.ReadAllText(data.FileName),
                    libDir);
            }
            if (data.CT == CommandType.OneTime)
            {
                element.Data.OneTimeDate = data.OTD;
            }
            else
            {
                if (data.RT == RepeatableType.Monotonous)
                {
                    element.Data.MonotonousPeriod = (uint)data.MP;
                    element.Data.MonotonousRepeatCount = (ushort)data.MRC;
                    element.Data.MonotonousStartTime = data.MST;
                }
                else
                {
                    element.Data.SpecificDates = data.SD;
                }
            }
            return element;
        }
    }
}

