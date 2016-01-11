namespace RxLog
{
    using static LogItemLevel;

    public static class LogItemExtensions
    {
        public static string GetPrefix(this LogItemLevel level)
        {
            switch (level)
            {
                case Trace:
                    return "[TRACE]";
                case Debug:
                    return "[DEBUG]";
                case Information:
                    return "[INFO.]";
                case Warning:
                    return "[WARN.]";
                case Error:
                    return "[ERROR]";
                case Fatal:
                    return "[FATAL]";
            }
            return "[?????]";
        }

        public static bool ShouldSkip(this LoggingLevel logging, LogItemLevel logItem)
            => (int)logging > (int)logItem;

        public static LogItem ToLogItem(this object source)
            => LogItem.ToLogItem(source);
    }
}
