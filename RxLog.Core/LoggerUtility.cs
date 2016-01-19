using System;

namespace RxLog
{
    using System.Reactive.Subjects;

    using static Defaults;

    public static partial class LoggerUtility
    {
        public static Subject<LogItem> Default { get; set; }

        [ThreadStatic]
        private static Subject<LogItem> _ThreadCurrent;
        public static Subject<LogItem> ThreadCurrent
        {
            get { return _ThreadCurrent ?? Default; }
            set { _ThreadCurrent = value; }
        }

        public static void Log(this Subject<LogItem> subject, LogItem item)
            => subject.OnNext(item);
        public static void Log(this Subject<LogItem> subject, object data, object category, LogItemLevel level = DefaultItemLevel)
            => subject.Log(new LogItem(data, category, level));
        public static void Log(this Subject<LogItem> subject, object data, LogItemLevel level = DefaultItemLevel)
            => subject.Log(data, null, level);

        public static void Debug(this Subject<LogItem> subject, object data, object category = null)
            => subject.OnNext(new LogItem(data, category, LogItemLevel.Debug));
        public static void Trace(this Subject<LogItem> subject, object data, object category = null)
            => subject.OnNext(new LogItem(data, category, LogItemLevel.Trace));
        public static void Info(this Subject<LogItem> subject, object data, object category = null)
            => subject.OnNext(new LogItem(data, category, LogItemLevel.Information));
        public static void Warn(this Subject<LogItem> subject, object data, object category = null)
            => subject.OnNext(new LogItem(data, category, LogItemLevel.Warning));
        public static void Error(this Subject<LogItem> subject, object data, object category = null)
            => subject.OnNext(new LogItem(data, category, LogItemLevel.Error));
        public static void Fatal(this Subject<LogItem> subject, object data, object category = null)
            => subject.OnNext(new LogItem(data, category, LogItemLevel.Fatal));
    }
}
