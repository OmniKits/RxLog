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
            var args = argument.ReadCommandLineArguments(TextualArgumentOptions.MixEcmaScriptLikeEscape);
            //var args = argument.Split('?');

            FilePathFormat = args[0].Trim();

            if (args.Length == 1) return;

            LoggingLevel level;
            if (!Enum.TryParse(args[1], true, out level))
                level = DefaultLevel;
            Level = level;

            if (args.Length == 2) return;
            switch (args.Length)
            {
                case 2:
                    return;
                case 3:
                    break;
                default:
                    throw new ArgumentException();
            }

            TimestampFormat = args[2];
        }

        protected override void FlushLine(string line)
        {
            var path = string.Format(FilePathFormat, CurrentTimestamp);
            var dir = Environment.ExpandEnvironmentVariables(Path.GetDirectoryName(path));
            path = Path.Combine(Directory.CreateDirectory(dir).FullName, Path.GetFileName(path));

            if (_CurrentFile == null)
                goto CreateNew;
            if (_CurrentFile.Item1 == path)
                goto WriteLine;

            _CurrentFile.Item2.Close();

        CreateNew:
            var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
            var fw = new StreamWriter(fs, Encoding.UTF8);
            fw.AutoFlush = true;
            _CurrentFile = Tuple.Create(path, fw);

        WriteLine:
            _CurrentFile.Item2.WriteLine(line);
        }
    }
}
