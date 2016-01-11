using System;

namespace RxLog
{
    public class StandardErrorWriter : LogWriter
    {
        public StandardErrorWriter(string timestampFormat = Defaults.TimestampFormat, LoggingLevel level = LoggingLevel.Error, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        { }

        protected override void FlushLine(string line)
            => Console.Error.WriteLine(line);
    }
}
