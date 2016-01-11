using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace RxLog
{
    using System.Reactive.Subjects;

    partial class LoggerUtility
    {
        static LoggerUtility()
        {
            Default = MakeDefaultSubject();
        }

        public static Subject<object> MakeDefaultSubject()
        {
            var section = (RxLogConfigurationSection)ConfigurationManager.GetSection("rxLog");

            var defaultLoggers = section.Default.Cast<RxLoggerElement>()
                .Select(e => TypeUtility.GetInstance<TextWriter>(TypeUtility.GetType(e.SourceType), e.MemberName, e.Argument))
                .ToArray();

            var subject = new Subject<object>();

            foreach (var logger in defaultLoggers)
                subject.Subscribe(logger.WriteLine);

            return subject;
        }
    }
}
