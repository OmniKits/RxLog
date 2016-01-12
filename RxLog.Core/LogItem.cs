namespace RxLog
{
    public class LogItem
    {
        const LogItemLevel DefaultLevel = LogItemLevel.Information;

        public LogItemLevel Level { get; }
        public object Category { get; }
        public object Data { get; }

        public LogItem(object data, object category, LogItemLevel level = DefaultLevel)
        {
            Category = category;
            Data = data;
            Level = level;
        }
        public LogItem(object data, LogItemLevel level = DefaultLevel)
            : this(data, null)
        { }

        public static LogItem ToLogItem(object source)
        {
            var item = source as LogItem;
            if (item != null) return item;
            return new LogItem(source);
        }

        public virtual string GetPrefix()
            => $"{Level.GetPrefix()}{(Category == null ? null : $"[{Category}]")} ";

        public override string ToString()
            => GetPrefix() + Data;

        public override int GetHashCode()
            => (Data == null ? 0 : Data.GetHashCode()) ^ (int)Level;
    }
}
