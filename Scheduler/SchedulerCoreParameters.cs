using Program;
using System;

namespace Scheduler
{
    public class SchedulerCoreParameters
    {
        public int Interval { get; set; }
        public string FileName { get; set; }
        public string LibDir { get; set; }
        public Action ElapsedAction { get; set; }
        public Action DataChangedAction { get; set; }
        public Action<Exception, string> OnErrorAction { get; set; }
        public Logger Logger { get; set; }
    }
}
