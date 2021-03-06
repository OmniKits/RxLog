﻿#define DEBUG

using System;
using System.Diagnostics;

namespace RxLog
{
    public class DebugWriter : LogWriter
    {
        const LoggingLevel DefaultLevel = LoggingLevel.Debug;

        public DebugWriter(string timestampFormat, LoggingLevel level = DefaultLevel, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        { }

        public DebugWriter(string argument = null)
            : this(null, DefaultLevel)
        {
            if (string.IsNullOrEmpty(argument))
                argument = Defaults.TimestampFormat;

            var args = argument.Split(',');
            TimestampFormat = args[0].Trim();
            switch (args.Length)
            {
                case 1:
                    Level = DefaultLevel;
                    return;
                case 2:
                    Level = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), args[1], true);
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        protected override void FlushLine(string line)
            => Debug.WriteLine(line);
    }
}
