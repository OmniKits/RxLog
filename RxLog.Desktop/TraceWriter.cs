#define TRACE

using System;
using System.Diagnostics;

namespace RxLog
{
    public class TraceWriter : LogWriter
    {
        protected TraceWriter(LoggingLevel level = LoggingLevel.Trace, IFormatProvider formatProvider = null)
            : base(formatProvider, level)
        { }

        protected override void FlushLine(string line)
            => Trace.WriteLine(line);
    }
}
