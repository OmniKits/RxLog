using System;
using System.Linq;

namespace RxLog
{
    public class StandardErrorWriter : LogWriter
    {
        const LoggingLevel DefaultLevel = LoggingLevel.Error;

        public StandardErrorWriter(string timestampFormat, LoggingLevel level = DefaultLevel, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        { }

        public StandardErrorWriter(string argument = null)
            : base(null)
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
            => Console.Error.WriteLine(line);
    }
}
