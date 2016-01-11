using System;

namespace RxLog
{
    public abstract class LogWriter : LineWriter
    {
        public LoggingLevel Level { get; }
        protected LogItemLevel CurrentItemLevel { get; set; } = (LogItemLevel)(-1);

        protected LogWriter(IFormatProvider formatProvider, LoggingLevel level)
            : base(formatProvider)
        {
            Level = level;
        }

        protected override string DecorateLine(string source)
            => $"{CurrentItemLevel.GetPrefix()}{source}";

        public override void Write(object value)
        {
            var item = value.ToLogItem();
            if (Level.ShouldSkip(item.Level))
                return;
            CurrentItemLevel = item.Level;
            base.Write(item.Data);
        }
        public override void WriteLine(object value)
        {
            var item = value.ToLogItem();
            if (Level.ShouldSkip(item.Level))
                return;
            CurrentItemLevel = item.Level;
            base.WriteLine(item.Data);
        }
    }
}
