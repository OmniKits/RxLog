using System;

namespace RxLog
{
    public abstract class LogWriter : LineWriter
    {
        public LoggingLevel Level { get; protected set; }
        public string TimestampFormat { get; protected set; }

        protected DateTime CurrentTimestamp { get; set; } = DateTime.Now;
        protected object CurrentItemCategory { get; set; }
        protected LogItemLevel CurrentItemLevel { get; set; } = (LogItemLevel)(-1);
        protected string CurrentItemPrefix { get; set; }

        protected LogWriter(string timestampFormat, IFormatProvider formatProvider, LoggingLevel level)
            : base(formatProvider)
        {
            TimestampFormat = timestampFormat;
            Level = level;
        }

        protected override string DecorateLine(string source)
            => CurrentItemPrefix + source;

        protected virtual string GetLogItemPrefix(LogItem item)
            => $"{CurrentTimestamp.ToString(TimestampFormat, FormatProvider)}{item.GetPrefix()}";
        private void ReadyCurrentItem(LogItem item)
        {
            CurrentTimestamp = DateTime.Now;
            CurrentItemLevel = item.Level;
            CurrentItemCategory = item.Category;

            CurrentItemPrefix = GetLogItemPrefix(item);
        }

        public override void Write(object value)
        {
            var item = value.ToLogItem();
            if (Level.ShouldSkip(item.Level))
                return;

            ReadyCurrentItem(item);

            base.Write(item.Data);
        }
        public override void WriteLine(object value)
        {
            var item = value.ToLogItem();
            if (Level.ShouldSkip(item.Level))
                return;

            ReadyCurrentItem(item);
            
            base.WriteLine(item.Data);
        }
    }
}
