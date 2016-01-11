#define DEBUG

using System;
using System.Diagnostics;

namespace RxLog
{
    public class DebugWriter : LogWriter
    {
        public DebugWriter(LoggingLevel level = LoggingLevel.Debug, IFormatProvider formatProvider = null)
            : base(formatProvider, level)
        { }

        protected override void FlushLine(string line)
            => Debug.WriteLine(line);
    }
}
