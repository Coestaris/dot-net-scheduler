using Scheduler.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Scheduler.Pipes
{
    internal class SchedulerServer : IDisposable
    {
        private NamedPipeServerStream pipeServer;

        public SchedulerServer()
        {
            PipeSecurity ps = new PipeSecurity();
            SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            ps.AddAccessRule(new PipeAccessRule(sid, PipeAccessRights.ReadWrite, AccessControlType.Allow));

            pipeServer = new NamedPipeServerStream(PipeConfig.PipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.None, 1, 1, ps);
        }

        public void Disconnect()
        {
            pipeServer.Close();
        }

        public void Dispose()
        {
            Disconnect();
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public List<SCHElement> ParseElement()
        {
            pipeServer.WaitForConnection();
            byte[] rawBytes = ReadFully(pipeServer);
            MemoryStream ms = new MemoryStream(rawBytes);
            var newElems = SCHFile.ParseByteArray(ms);
            foreach (var item in newElems)
                item.Metadata.CreationDate = DateTime.Now;

            ms.Close();
            return newElems;
        }
    }
}
