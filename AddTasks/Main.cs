using LibCfg;
using Program;
using Scheduler.Files;
using Scheduler.Pipes;
using Scheduler.Shell;
using System;

namespace AddTasks
{
    class Program
    {
        private static void Error(string message)
        {
            Console.WriteLine("ERROR: " + message);
        }

        static void Main(string[] args)
        {
            Logger logger = new Logger("AddTasks", "AddTasks");
            logger.V("Staring app");
            logger.I("Values " + string.Join(" ", args));

            SCHElement element = null;
            try
            {
                element = ShellParser.Parse(args, SCHEnvironment.LibDir);
                logger.I("Parse has been ended");
            } catch(Exception e)
            {
                logger.E("Parsing error");
                logger.E(e.ToString());

                Error(e.ToString());
                return;
            }
            
            using (SchedulerClient client = new SchedulerClient())
            {
                if (!client.Connect())
                {
                    Error("Unable to connect to server!");

                    logger.E("Unable to connect to server!");
                    logger.E(client.ExMsg);

                    Error(client.ExMsg);
                    return;
                }

                if (!client.SendElement(element))
                {
                    logger.E("Unable to send data");
                    logger.E(client.ExMsg);

                    Error("Unable to send data!");
                    Error(client.ExMsg);
                    return;
                }
                logger.I("OK!");
            }
        }
    }
}
