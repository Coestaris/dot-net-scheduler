using System;

namespace Program
{
    class Program
    {
        public static void Main()
        {
            Logger logger = new Logger("remover", "remover");
            logger.V("App started");

            try
            {
                LuckyVoyageDB.DeleteFromDB(uuid);
                logger.I("OK!");
            }
            catch (Exception e)
            {
                logger.E("FATAL: " + e.Message);
                logger.E("STACK: " + e.StackTrace);
            }

            logger.I("App ended");
        }
    }
}
