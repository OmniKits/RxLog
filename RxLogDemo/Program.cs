using System;
using System.Reactive.Subjects;

namespace RxLogDemo
{

    using RxLog;

    using static RxLog.LogItemLevel;


    class Program
    {
        static void Main(string[] args)
        {
            var subject = LoggerUtility.Default;
            //subject.Subscribe(LoggerUtility.Default);
            subject.Subscribe(LoggerUtility.GetConfigSubject("debug"));
            subject.Subscribe(LoggerUtility.MakeSubjectFromConfig("trace"));

            subject.OnNext(@"OMG
WTF");
            subject.OnNext(new LogItem("***", (LogItemLevel)(-1)));

            subject.OnNext(new LogItem(nameof(Trace), Trace));
            subject.OnNext(new LogItem(nameof(Debug), Debug));
            subject.OnNext(new LogItem(nameof(Information), Information));
            subject.OnNext(new LogItem(nameof(Warning), Warning));
            subject.OnNext(new LogItem(nameof(Error), Error));
            subject.OnNext(new LogItem(nameof(Fatal), Fatal));

            Console.ReadKey();
        }
    }
}
