using System;

namespace RxLog
{
    public class StdErrWriter : LogWriter
    {
        protected StdErrWriter(LoggingLevel level = LoggingLevel.Error, IFormatProvider formatProvider = null)
            : base(formatProvider, level)
        { }

        protected override void FlushLine(string line)
            => Console.Error.WriteLine(line);
    }
}
