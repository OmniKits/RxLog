using System;
using System.Threading;

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
            subject.Subscribe(LoggerUtility.GetConfigSubject("file"));
            subject.Subscribe(LoggerUtility.GetConfigSubject("debug"));
            subject.Subscribe(LoggerUtility.MakeSubjectFromConfig("trace"));

            subject.Log(@"OMG
WTF", (LogItemLevel)(-1));
            Thread.Sleep(1000);
            subject.Log("***", "whatever");

            subject.Trace(nameof(Trace));
            subject.Debug(nameof(Debug));
            subject.Info(nameof(Information));
            subject.Warn(nameof(Warning));
            subject.Error(nameof(Error));
            subject.Fatal(nameof(Fatal));

            Console.ReadKey();
        }
    }
}
