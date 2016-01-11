namespace RxLog
{
    public class LogItem
    {
        const LogItemLevel DefaultLevel = LogItemLevel.Information;

        public LogItemLevel Level { get; }
        public object Data { get; }

        public LogItem(object data, LogItemLevel level = DefaultLevel)
        {
            Data = data;
            Level = level;
        }

        public static LogItem ToLogItem(object source)
        {
            var item = source as LogItem;
            if (item != null) return item;
            return new LogItem(source);
        }

        public override string ToString()
            => $"{Level.GetPrefix()}\t{Data}";

        public override int GetHashCode()
            => (Data == null ? 0 : Data.GetHashCode()) ^ (int)Level;
    }
}
