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
        All,
        Trace = All,
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
        Off,
    }
}
