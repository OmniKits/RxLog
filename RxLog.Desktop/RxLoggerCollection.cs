using System.Configuration;

namespace RxLog
{
    public class RxLoggerCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
            => new RxLoggerNamedElement();

        protected override object GetElementKey(ConfigurationElement element)
            => ((RxLoggerNamedElement)element).Name;
    }
}