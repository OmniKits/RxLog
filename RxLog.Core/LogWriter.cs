using System;

namespace RxLog
{
    public abstract class LogWriter : LineWriter
    {
        public LoggingLevel Level { get; protected set; }
        protected string TimestampFormat { get; set; }
        protected LogItemLevel CurrentItemLevel { get; set; } = (LogItemLevel)(-1);
        protected DateTime CurrentItemTimestamp { get; set; } = DateTime.Now;

        protected LogWriter(IFormatProvider formatProvider)
            : base(formatProvider)
        { }

        protected LogWriter(string timestampFormat, IFormatProvider formatProvider, LoggingLevel level)
            : base(formatProvider)
        {
            TimestampFormat = timestampFormat;
            Level = level;
        }

        protected override string DecorateLine(string source)
            => $"{CurrentItemTimestamp.ToString(TimestampFormat, FormatProvider)}{CurrentItemLevel.GetPrefix()} {source}";

        public override void Write(object value)
        {
            var item = value.ToLogItem();
            if (Level.ShouldSkip(item.Level))
                return;
            CurrentItemTimestamp = DateTime.Now;
            CurrentItemLevel = item.Level;
            base.Write(item.Data);
        }
        public override void WriteLine(object value)
        {
            var item = value.ToLogItem();
            if (Level.ShouldSkip(item.Level))
                return;
            CurrentItemTimestamp = DateTime.Now;
            CurrentItemLevel = item.Level;
            base.WriteLine(item.Data);
        }
    }
}
