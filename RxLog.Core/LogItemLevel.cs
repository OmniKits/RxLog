namespace RxLog
{
    public enum LogItemLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
    }

    public enum LoggingLevel
    {
        All = int.MinValue,
        Trace = LogItemLevel.Trace,
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
        Off,
    }
}
