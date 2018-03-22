using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scheduler.Files
{
    public class SCHFile
    {
        private const int nameBlockLength = 2;
        private const int autorBlockLength = 1;
        private const int descrBlockLength = 2;
        private const int dateTimeBlockLength = 8;
        private const int actionDataBlockLength = 2;
        private const int periodBlockLength = 4;
        private const int countBlockLength = 2;
        
        public List<SCHElement> Elements { get; set; }

        private static byte[] ReadToBuffer(Stream stream, int length)
        {
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            return buffer;
        }

        internal static List<SCHElement> ParseByteArray(Stream stream)
        {
            List<SCHElement> Elements = new List<SCHElement>();
            unchecked
            {
                while (stream.Position != stream.Length)
                {

                    //===== Reading metadata =====
                    SCHMetadata metadata = new SCHMetadata();

                    //Name
                    UInt16 nameLen = BitConverter.ToUInt16(ReadToBuffer(stream, nameBlockLength), 0);
                    metadata.Name = Encoding.UTF8.GetString(ReadToBuffer(stream, nameLen));

                    //Autor
                    byte autorLen = ReadToBuffer(stream, autorBlockLength)[0];
                    metadata.Autor = Encoding.UTF8.GetString(ReadToBuffer(stream, autorLen));

                    //Autor
                    UInt16 descrLen = BitConverter.ToUInt16(ReadToBuffer(stream, descrBlockLength), 0);
                    metadata.Description = Encoding.UTF8.GetString(ReadToBuffer(stream, descrLen));

                    //Creation date
                    metadata.CreationDate = TimeHelper.UnixTimestampToDateTime(
                    BitConverter.ToDouble(ReadToBuffer(stream, dateTimeBlockLength), 0));

                    //===== Reading data =====
                    SCHMData data = new SCHMData();

                    //Action type
                    data.ActionType = (ActionType)ReadToBuffer(stream, autorBlockLength)[0];

                    //Action data
                    UInt16 actionDataLen = BitConverter.ToUInt16(ReadToBuffer(stream, actionDataBlockLength), 0);
                    data.ActionData = ReadToBuffer(stream, actionDataLen);
                    if (data.ActionType == ActionType.CSharpCode)
                        data._cSharpData = new SCHMData.CSharpData(data.ActionData);

                    //Command type
                    data.CommandType = (CommandType)ReadToBuffer(stream, autorBlockLength)[0];

                    if (data.CommandType == CommandType.OneTime)
                    {
                        //One time date
                        data.OneTimeDate = TimeHelper.UnixTimestampToDateTime(
                            BitConverter.ToDouble(ReadToBuffer(stream, dateTimeBlockLength), 0));
                    }
                    else
                    {
                        //RepeatableType
                        data.RepeatableType = (RepeatableType)ReadToBuffer(stream, autorBlockLength)[0];
                        if (data.RepeatableType == RepeatableType.Monotonous)
                        {
                            //Monotonous Start Time
                            data.MonotonousStartTime = TimeHelper.UnixTimestampToDateTime(
                                BitConverter.ToDouble(ReadToBuffer(stream, dateTimeBlockLength), 0));

                            //Monotonous Period
                            data.MonotonousPeriod = BitConverter.ToUInt32(ReadToBuffer(stream, periodBlockLength), 0);

                            //Monotonous Repeat Count
                            data.MonotonousRepeatCount = BitConverter.ToUInt16(ReadToBuffer(stream, countBlockLength), 0);

                            data.ProceedMonotonous();

                        }
                        else
                        {
                            //Specific Dates Count
                            UInt16 count = BitConverter.ToUInt16(ReadToBuffer(stream, countBlockLength), 0);
                            List<DateTime> dates = new List<DateTime>();

                            for (UInt16 i = 0; i < count; i++)
                            {
                                var date = TimeHelper.UnixTimestampToDateTime(BitConverter.ToDouble(ReadToBuffer(stream, dateTimeBlockLength), 0));
                                if (DateTime.Now < date)
                                    dates.Add(date);
                            }

                            data.SpecificDates = dates;
                        }
                    }

                    if (data.ActionData != null && data.ActionData.Length != 0)
                        Elements.Add(new SCHElement() { Data = data, Metadata = metadata });
                }
            }
            return Elements;
        }

        public void LoadFromFile(string fileName)
        {
            FileStream fs = File.OpenRead(fileName);
            Elements = ParseByteArray(fs);
            fs.Close();
        }

        public void SaveToFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
            foreach (var item in Elements)
            {
                byte[] metaData = item.Metadata.ToBytes();
                fs.Write(metaData, 0, metaData.Length);
                byte[] data = item.Data.ToBytes();
                fs.Write(data, 0, data.Length);
            }
            fs.Close();
        }

        public SCHFile()
        {
            Elements = new List<SCHElement>();
        }

        public SCHFile(List<SCHElement> elements)
        {
            Elements = elements;
        }

        public SCHFile(string fileName)
        {
            LoadFromFile(fileName);
        }
    }
}
