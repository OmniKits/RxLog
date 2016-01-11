using System;

namespace RxLog
{
    using System.Reactive.Subjects;

    public partial class LoggerUtility
    {
        public static Subject<object> Default { get; set; }

        [ThreadStatic]
        private static Subject<object> _ThreadCurrent;
        public static Subject<object> ThreadCurrent
        {
            get { return _ThreadCurrent ?? Default; }
            set { _ThreadCurrent = value; }
        }
    }
}
