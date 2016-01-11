using System;
using System.Diagnostics;

namespace RxLog
{
    public class TraceWriter : LogWriter
    {
        protected TraceWriter(LoggingLevel level = LoggingLevel.Trace, IFormatProvider formatProvider = null)
            : base(formatProvider, level)
        { }

        public object Console { get; private set; }

        protected override void FlushLine(string line)
            => Trace.WriteLine(line);
    }
}
