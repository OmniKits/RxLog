using System.Configuration;

namespace RxLog
{
    public class RxLogSubjectCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
            => new RxLogSubjectElement();

        protected override object GetElementKey(ConfigurationElement element)
            => ((RxLogSubjectElement)element).Name;
    }
}