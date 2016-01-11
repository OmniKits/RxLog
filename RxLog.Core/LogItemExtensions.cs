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
                    return "[TRACE]\t";
                case Debug:
                    return "[DEBUG]\t";
                case Information:
                    return "[INFO.]\t";
                case Warning:
                    return "[WARN.]\t";
                case Error:
                    return "[ERROR]\t";
                case Fatal:
                    return "[FATAL]\t";
            }
            return "[?????]\t";
        }

        public static bool ShouldSkip(this LoggingLevel logging, LogItemLevel logItem)
            => (int)logging > (int)logItem;

        public static LogItem ToLogItem(this object source)
            => LogItem.ToLogItem(source);
    }
}
