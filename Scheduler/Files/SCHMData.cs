using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

////::ДАННЫЕ::
//
// Поля:
//   тип действия, Действие (cmd или .NET код), Тип (одноразовая, повторения). 
//                Если одноразовая:  Дата выполнения.
//                Если многоразовая: Тип (монотонная, конкретная).
//                      Если монотонная: Дата начала, Период, Кол-во повторений
//                      Если конкретная: Кол-во дат (Nд), Даты 
//
// Структура:
//   1. (1  байт ) Тип действия
//   2. (2  байта) Длина действия (N1)
//   3. (N1 байт ) Длина действия
//   4. (1  байт ) Тип команды
//      ===== ДЛЯ ОДНОРАЗОВОЙ  =====
//      5. (4  байта) Дата выполнения
//      ===== ДЛЯ МНОГОРАЗОВОЙ =====
//      5. (1  байт ) Тип многоразовой команды
//          ===== ДЛЯ МОНОТОННОЙ =====
//          6. (4 байт ) Дата начала
//          7. (4 байт ) Период
//          8. (2 байт ) Кол-во повторений
//          ===== ДЛЯ КОНКРЕТНОЙ =====
//          6. (2 байт ) Кол-во дат (N2)
//          7. (N2 * 4 байт) N2 дат выполнения
//
//   Дата в видe UNIX Timestamp'a.
////

namespace Scheduler.Files
{
    public class SCHMData
    {
        public ActionType ActionType { get; set; }
        public byte[] ActionData { get; set; }

        public CommandType CommandType { get; set; }

        //      ===== ДЛЯ ОДНОРАЗОВОЙ  =====
        public DateTime OneTimeDate { get; set; }

        //      ===== ДЛЯ МНОГОРАЗОВОЙ =====
        public RepeatableType RepeatableType { get; set; }
        //          ДЛЯ МОНОТОННОЙ
        public DateTime MonotonousStartTime { get; set; }
        public UInt32 MonotonousPeriod { get; set; }
        public UInt16 MonotonousRepeatCount { get; set; }
        //          ДЛЯ КОНКРЕТНОЙ
        public List<DateTime> SpecificDates { get; set; }


        public void SetCSharpSource(string[] refs, string code, string libDir)
        {
            string reffStr = string.Join("|", refs);

            for (int i = 0; i < refs.Length; i++)
                refs[i] = refs[i].Replace("{-lib-}", libDir);

            string outputAssemblyPath = libDir + "tmp.dll";

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.AddRange(refs);
            parameters.GenerateInMemory = false;
            parameters.OutputAssembly = libDir + "tmp.dll";
            parameters.GenerateExecutable = true;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }
                throw new InvalidOperationException(sb.ToString());
            }

            byte[] assemblyBytes = File.ReadAllBytes(outputAssemblyPath);

            List<byte> bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes((UInt32)reffStr.Length));
            bytes.AddRange(Encoding.UTF8.GetBytes(reffStr));
            bytes.AddRange(BitConverter.GetBytes((UInt32)code.Length));
            bytes.AddRange(Encoding.UTF8.GetBytes(code));
            bytes.AddRange(BitConverter.GetBytes((UInt32)assemblyBytes.Length));
            bytes.AddRange(assemblyBytes);
            ActionData = bytes.ToArray();

            _cSharpData = new CSharpData()
            {
                Assembly = assemblyBytes,
                Refs = refs,
                Source = code
            };

            File.Delete(outputAssemblyPath);
        }

        internal class CSharpData
        {
            private byte[] ReadToBuffer(Stream stream, int length)
            {
                byte[] buffer = new byte[length];
                stream.Read(buffer, 0, length);
                return buffer;
            }

            public string[] Refs;
            public string Source;
            public byte[] Assembly;

            public CSharpData()
            {

            }

            public CSharpData(byte[] data)
            {
                MemoryStream ms = new MemoryStream(data);
                UInt32 refsLen = BitConverter.ToUInt32(ReadToBuffer(ms, 4), 0);
                Refs = Encoding.UTF8.GetString(ReadToBuffer(ms, (int)refsLen)).Split('|');

                UInt32 scrLen = BitConverter.ToUInt32(ReadToBuffer(ms, 4), 0);
                Source = Encoding.UTF8.GetString(ReadToBuffer(ms, (int)scrLen));

                UInt32 asmLen = BitConverter.ToUInt32(ReadToBuffer(ms, 4), 0);
                Assembly = ReadToBuffer(ms, (int)asmLen);
                ms.Close();
            }

        }

        public void ProceedMonotonous()
        {
            SpecificDates = new List<DateTime>();
            for (int i = 0; i < MonotonousRepeatCount; i++)
                SpecificDates.Add(MonotonousStartTime.AddSeconds(i * MonotonousPeriod));
        }

        internal CSharpData _cSharpData;

        public String[] GetCSharpRefs()
        {
            if (ActionType != ActionType.CSharpCode)
                throw new ArgumentException(nameof(ActionType) + " must be " + nameof(ActionType.CSharpCode));

            return _cSharpData.Refs;
        }

        public String[] GetCSharpRefs(string LibDir)
        {
            var reffsArr = GetCSharpRefs();
            for (int i = 0; i < reffsArr.Length; i++)
                reffsArr[i] = reffsArr[i].Replace("{-lib-}", LibDir);
            return reffsArr;
        }

        public byte[] GetCSharpPrecompiledBytes()
        {
            if (ActionType != ActionType.CSharpCode)
                throw new ArgumentException(nameof(ActionType) + " must be " + nameof(ActionType.CSharpCode));

            return _cSharpData.Assembly;
        }


        public Assembly GetCSharpPrecompiled()
        {
            if (ActionType != ActionType.CSharpCode)
                throw new ArgumentException(nameof(ActionType) + " must be " + nameof(ActionType.CSharpCode));

            return Assembly.Load(_cSharpData.Assembly);
        }

        public string GetCMDScript()
        {
            if (ActionType != ActionType.CMDScript)
                throw new ArgumentException(nameof(ActionType) + " must be " + nameof(ActionType.CMDScript));

            return Encoding.UTF8.GetString(ActionData);
        }

        public void SetCMDScript(string script)
        {
            if (ActionType != ActionType.CMDScript)
                throw new ArgumentException(nameof(ActionType) + " must be " + nameof(ActionType.CMDScript));
            ActionData = Encoding.UTF8.GetBytes(script);
        }

        public String GetCSharpCode()
        {
            if (ActionType != ActionType.CSharpCode)
                throw new ArgumentException(nameof(ActionType) + " must be " + nameof(ActionType.CSharpCode));

            return _cSharpData.Source;
        }

        internal byte[] ToBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)ActionType);
            bytes.AddRange(BitConverter.GetBytes((UInt16)ActionData.Length));
            bytes.AddRange(ActionData);
            bytes.Add((byte)CommandType);

            if(CommandType == CommandType.OneTime)
            {
                bytes.AddRange(BitConverter.GetBytes(TimeHelper.DateTimeToUnixTimestamp(OneTimeDate)));
            } else
            {
                bytes.Add((byte)RepeatableType);
                if(RepeatableType == RepeatableType.Monotonous)
                {
                    bytes.AddRange(BitConverter.GetBytes(TimeHelper.DateTimeToUnixTimestamp(MonotonousStartTime)));
                    bytes.AddRange(BitConverter.GetBytes(MonotonousPeriod));
                    bytes.AddRange(BitConverter.GetBytes(MonotonousRepeatCount));
                } else
                {
                    bytes.AddRange(BitConverter.GetBytes((UInt16)SpecificDates.Count));
                    foreach (var item in SpecificDates)
                    {
                        bytes.AddRange(BitConverter.GetBytes(TimeHelper.DateTimeToUnixTimestamp(item)));
                    }
                }
            }
            return bytes.ToArray();
        }
    }
}