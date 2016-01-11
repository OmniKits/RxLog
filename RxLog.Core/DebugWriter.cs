#define DEBUG

using System;
using System.Diagnostics;

namespace RxLog
{
    public class DebugWriter : LogWriter
    {
        public DebugWriter(string timestampFormat, LoggingLevel level = LoggingLevel.Debug, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        { }

        public DebugWriter()
            : this(Defaults.TimestampFormat)
        { }

        protected override void FlushLine(string line)
            => Debug.WriteLine(line);
    }
}
