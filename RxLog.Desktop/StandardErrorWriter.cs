using System;

namespace RxLog
{
    public class StandardErrorWriter : LogWriter
    {
        public StandardErrorWriter(string timestampFormat, LoggingLevel level = LoggingLevel.Error, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        { }

        public StandardErrorWriter()
            : this(Defaults.TimestampFormat)
        { }

        protected override void FlushLine(string line)
            => Console.Error.WriteLine(line);
    }
}
