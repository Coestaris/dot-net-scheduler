using System;
using System.Collections.Generic;
using System.Text;

////
//::МЕТАДАННЫЕ::
//
// Поля: 
//   Название, Автор, Описание, Дата занесения.
// Структура: 
//   1. (2  байта) Длина названия (N1)
//   2. (N1 байт ) Название
//   3. (1  байт ) Длина автора (N2)
//   4. (N2 байт ) Автор
//   5. (2  байта) Длина Описания (N3)
//   6. (N3 байт ) Описание
//   7. (8  байт ) Дата занесения
//
//   Дата в видe UNIX Timestamp'a.
////

namespace Scheduler.Files
{
    public class SCHMetadata
    {
        public string Name { get; set; }
        public string Autor { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public byte[] ToBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes((UInt16)Name.Length));
            bytes.AddRange(Encoding.UTF8.GetBytes(Name));
            bytes.Add((byte)Autor.Length);
            bytes.AddRange(Encoding.UTF8.GetBytes(Autor));
            bytes.AddRange(BitConverter.GetBytes((UInt16)Description.Length));
            bytes.AddRange(Encoding.UTF8.GetBytes(Description));
            bytes.AddRange(BitConverter.GetBytes(TimeHelper.DateTimeToUnixTimestamp(CreationDate)));
            return bytes.ToArray();
        }
    }
}