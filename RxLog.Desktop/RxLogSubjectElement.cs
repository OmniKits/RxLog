using System.Configuration;

namespace RxLog
{
    public class RxLogSubjectElement : RxLoggerCollection
    {
        const string NameProperty = "name";
        [ConfigurationProperty(NameProperty, IsKey = true)]
        public string Name
        {
            get { return (string)this[NameProperty]; }
            set { this[NameProperty] = value; }
        }
    }
}
