﻿using System;

namespace RxLog
{
    public class StandardErrorWriter : LogWriter
    {
        public StandardErrorWriter(LoggingLevel level = LoggingLevel.Error, IFormatProvider formatProvider = null)
            : base(formatProvider, level)
        { }

        protected override void FlushLine(string line)
            => Console.Error.WriteLine(line);
    }
}