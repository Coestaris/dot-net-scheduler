using Scheduler.Files;
using Scheduler.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Timers;

namespace Scheduler
{
    public class SchedulerCore
    {
        public SCHFile File { get; }
        public SchedulerCoreParameters Parameters { get; }
        public DateTime LastElapsed { get; private set; }

        private System.Timers.Timer aTimer;
        private Thread pipeThread;
        private string LoggerTag => "Scheduler  ";

        public SchedulerCore(SchedulerCoreParameters parameters)
        {
            Parameters = parameters;
            File = new SCHFile();

            aTimer = new System.Timers.Timer(Parameters.Interval);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            if (System.IO.File.Exists(Parameters.FileName))
            {
                Parameters.Logger?.I(LoggerTag, "The specified file has been found");
                try
                {
                    File.LoadFromFile(Parameters.FileName);
                    Parameters.Logger?.I(LoggerTag, "Data have been loaded from file");

                }
                catch (Exception e)
                {
                    File = new SCHFile();
                    Parameters.OnErrorAction?.Invoke(e, "Unable to read data from file");
                }
            }
            else
            {
                File = new SCHFile();
                Parameters.Logger?.I(LoggerTag, "File not found =c");
            }

            pipeThread = new Thread(PipeThreadProc);
            pipeThread.Start();
        }

        ~SchedulerCore()
        {
            Close();
        }

        [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        public void Close()
        {
            aTimer.Stop();
            aTimer.Dispose();
            pipeThread.Abort();
            Parameters.Logger?.I(LoggerTag, "All threads have been aborted");
        }

        private void RunElement(SCHElement element)
        {
            try
            {
                element.Run();
            } catch(Exception e)
            {
                Parameters.OnErrorAction?.Invoke(e, "Unable to run() element");
            }
        }

        private void Proceed()
        {
            //Тут ищем просроченные задачи, пока только oneTime, остальные удаляем...
            List<int> toBeRemoved = new List<int>();
            for(int i = 0; i < File.Elements.Count; i++)
            {
                var item = File.Elements[i];
                if(item.Data.CommandType == CommandType.OneTime)
                {
                    if (DateTime.Now > item.Data.OneTimeDate)
                    {
                        RunElement(item);
                        toBeRemoved.Add(i);
                    }
                }
                else
                {
                    if(item.Data.RepeatableType == RepeatableType.Monotonous)
                    {
                        //Все к херам просрочено
                        DateTime endTime = item.Data.MonotonousStartTime.AddSeconds(item.Data.MonotonousPeriod * item.Data.MonotonousRepeatCount);
                        if (DateTime.Now > endTime)
                        {
                            //Сделаем 1 раз для приличия
                            RunElement(item);
                            toBeRemoved.Add(i);
                            continue;
                        }

                        //А тут все нормас 
                        for (int j = item.Data.SpecificDates.Count - 1; j >= 0; j--)
                            if (DateTime.Now > item.Data.SpecificDates[j])
                            {
                                RunElement(item);
                                item.Data.SpecificDates.RemoveAt(j);
                            }
                    }
                    else
                    {
                        bool chached = false;
                        for (int j = item.Data.SpecificDates.Count - 1; j >= 0; j--)
                            if (DateTime.Now > item.Data.SpecificDates[j])
                            {
                                RunElement(item);
                                item.Data.SpecificDates.RemoveAt(j);
                                chached = true;
                            }

                        if (item.Data.SpecificDates.Count == 0)
                        {
                            toBeRemoved.Add(i);
                            continue;
                        }

                        if(chached)
                            Parameters.DataChangedAction?.Invoke();
                    }
                }
            }

            if (toBeRemoved.Count != 0)
            {
                foreach (var item in toBeRemoved.OrderByDescending(p => p))
                    File.Elements.RemoveAt(item);
                File.SaveToFile(Parameters.FileName);

                Parameters.Logger?.I(LoggerTag, "Data have been saved to file");

                Parameters.DataChangedAction?.Invoke();
            }
        }

        private void PipeThreadProc()
        {
            while (true)
            {
                using(SchedulerServer server = new SchedulerServer())
                {
                    try
                    {
                        var elems = server.ParseElement();
                        File.Elements.AddRange(elems);
                        Parameters.Logger?.I(LoggerTag, $"Added new {elems.Count} element(s)");
                        foreach (var e in elems)
                            Parameters.Logger?.I(LoggerTag, $" - Name: {e.Metadata.Name}. Action Type: {e.Data.ActionType}. Command Type: {e.Data.CommandType}.");

                        throw new ArgumentNullException("askdjalsjdlaksjdlaksjd");

                        Parameters.DataChangedAction?.Invoke();
                        File.SaveToFile(Parameters.FileName);

                        Parameters.Logger?.I(LoggerTag, "Data have been saved to file");
                    }
                    catch(Exception e)
                    {
                        Parameters.OnErrorAction?.Invoke(e, "Unable to parse or get value");
                    }
                }
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            LastElapsed = DateTime.Now;
            Parameters.ElapsedAction?.Invoke();

            try
            {
                Proceed();
            }
            catch(Exception ex)
            {
                Parameters.OnErrorAction?.Invoke(ex, "Unable to preceed file");
            }
        }
    }
}
