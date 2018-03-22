namespace Scheduler.Shell
{
    public struct AllowedToken
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }

        public AllowedToken(string name, string desctiption)
        {
            Name = name;
            Description = desctiption;
        }
    }
}
