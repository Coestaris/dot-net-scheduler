using System;

namespace Scheduler.Shell
{
    internal sealed class TokenKey
    {
        private readonly String name;

        public static readonly TokenKey FName = new TokenKey("fname");
        public static readonly TokenKey ActionType = new TokenKey("at");
        public static readonly TokenKey CommandType = new TokenKey("ct");
        public static readonly TokenKey RepType = new TokenKey("rt");
        public static readonly TokenKey MonPeriod = new TokenKey("mp");
        public static readonly TokenKey MonRepeatCount = new TokenKey("mrc");
        public static readonly TokenKey MonStartTime = new TokenKey("mst");
        public static readonly TokenKey OnetimeDate = new TokenKey("otd");
        public static readonly TokenKey SpecificDates = new TokenKey("sd");
        public static readonly TokenKey Name = new TokenKey("name");
        public static readonly TokenKey Autor = new TokenKey("autor");
        public static readonly TokenKey Descr = new TokenKey("descr");
        public static readonly TokenKey Refs = new TokenKey("refs");

        private TokenKey(String name)
        {
            this.name = name;
        }

        public override String ToString()
        {
            return name;
        }

        public static implicit operator string(TokenKey val)
        {
            return val.ToString();
        }
    }
}

