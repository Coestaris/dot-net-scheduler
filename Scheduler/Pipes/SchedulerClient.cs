using Scheduler.Files;
using System;
using System.IO.Pipes;
using System.Security.Principal;

namespace Scheduler.Pipes
{
    public class SchedulerClient : IDisposable
    {
        public string ExMsg { get; private set; }
        private NamedPipeClientStream pipeClient;

        public SchedulerClient(string RemoteName = ".")
        {
            pipeClient =
             new NamedPipeClientStream(RemoteName, PipeConfig.PipeName,
                 PipeDirection.Out, PipeOptions.None,
                 TokenImpersonationLevel.Impersonation);
        }

        public bool Connect()
        {
            try
            {
                pipeClient.Connect(PipeConfig.ConnectionTimeout);
                return true;
            }
            catch(Exception e)
            {
                ExMsg = e.Message;
                return false;
            }
        }

        public bool SendElement(SCHElement element)
        {
            try
            {
                byte[] bytes = element.ToBytes();
                pipeClient.Write(bytes, 0, bytes.Length);
                return true;
            }
            catch (Exception e)
            {
                ExMsg = e.Message;
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                pipeClient.Close();
                return true;
            }
            catch (Exception e)
            {
                ExMsg = e.Message;
                return false;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        ~SchedulerClient()
        {
            Disconnect();
        }
    }
}
