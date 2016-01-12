using System;
using System.IO;
using System.Linq;
using System.Text;

namespace RxLog
{
    public class FileLogWriter : LogWriter
    {
        const LoggingLevel DefaultLevel = LoggingLevel.Information;

        public string FilePathFormat { get; protected set; }

        private Tuple<string, StreamWriter> _CurrentFile = null;

        public FileLogWriter(string filePathFormat, string timestampFormat,
            LoggingLevel level = DefaultLevel, IFormatProvider formatProvider = null)
            : base(timestampFormat, formatProvider, level)
        {
            FilePathFormat = filePathFormat;
        }

        public FileLogWriter(string argument)
            : this(null, null, DefaultLevel)
        {
            var args = argument.Split('?');

            FilePathFormat = args[0].Trim();

            if (args.Length == 1) return;

            LoggingLevel level;
            if (!Enum.TryParse(args[1], true, out level))
                level = DefaultLevel;
            Level = level;

            args = args.Skip(2).ToArray();
            if (args.Length > 0)
                TimestampFormat = string.Join("?", args).Trim();
        }

        protected override void FlushLine(string line)
        {
            var path = string.Format(FilePathFormat, CurrentItemTimestamp);
            var dir = Path.GetDirectoryName(path);
            dir = Environment.ExpandEnvironmentVariables(dir);
            path = Path.Combine(Directory.CreateDirectory(dir).FullName, Path.GetFileName(path));

            if (_CurrentFile == null || _CurrentFile.Item1 != path)
            {
                var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Delete);
                var fw = new StreamWriter(fs, Encoding.UTF8);
                fw.AutoFlush = true;
                _CurrentFile = Tuple.Create(path, fw);
            }
            _CurrentFile.Item2.WriteLine(line);
        }
    }
}
