#define TRACE

using System;
using System.Diagnostics;

namespace RxLog
{
    public class TraceWriter : LogWriter
    {
        public TraceWriter(string timestampFormat = Defaults.TimestampFormat, LoggingLevel level = LoggingLevel.Trace, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        { }

        protected override void FlushLine(string line)
            => Trace.WriteLine(line);
    }
}
