using System.Configuration;

namespace RxLog
{
    public class RxLoggerNamedElement : RxLoggerElement
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