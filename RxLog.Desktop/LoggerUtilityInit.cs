﻿using System;
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
        private static readonly Dictionary<string, Subject<object>> ConfigSubjects = new Dictionary<string, Subject<object>>();

        static LoggerUtility()
        {
            var section = (RxLogConfigurationSection)ConfigurationManager.GetSection("rxLog");

            Default = MakeSubject(section.Default);

            foreach (var element in section.Subjects.Cast<RxLogSubjectElement>())
                ConfigSubjects[element.Name] = MakeSubject(element);
        }

        public static Subject<object> GetConfigSubject(string name)
        {
            if (name == null) return Default;
            return ConfigSubjects[name];
        }

        public static Subject<object> MakeSubject(RxLoggerCollection element)
        {
            var loggers = element.Cast<RxLoggerElement>()
                .Select(e => TypeUtility.GetInstance<TextWriter>(TypeUtility.GetType(e.SourceType), e.MemberName, e.Argument))
                .ToArray();

            var subject = new Subject<object>();

            foreach (var logger in loggers)
                subject.Subscribe(logger.WriteLine);

            return subject;
        }

        public static Subject<object> MakeSubjectFromConfig(string name = null)
        {
            var section = (RxLogConfigurationSection)ConfigurationManager.GetSection("rxLog");

            if (name == null)
                return MakeSubject(section.Default);

            var map = section.Subjects.Cast<RxLogSubjectElement>().ToDictionary(e => e.Name);
            return MakeSubject(map[name]);
        }
    }
}
