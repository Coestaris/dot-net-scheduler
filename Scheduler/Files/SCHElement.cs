using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Scheduler.Files
{
    public class SCHElement
    {
        public SCHMetadata Metadata { get; set; }
        public SCHMData Data { get; set; }

        public SCHElement()
        {
            Metadata = new SCHMetadata();
            Data = new SCHMData();
        }

        public byte[] ToBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Metadata.ToBytes());
            bytes.AddRange(Data.ToBytes());
            return bytes.ToArray();
        }

        public void Run()
        {
            new Thread(() =>
            {
                if (Data.ActionType == ActionType.CMDScript)
                {
                    try
                    {
                        Process.Start(Data.GetCMDScript());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
                else
                {
                    Assembly ass = Data.GetCSharpPrecompiled();
                    var main = ass.GetType("Program.Program").GetMethod("Main");
                    try
                    {
                        main.Invoke(null, null);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }).Start();
        }
    }
}