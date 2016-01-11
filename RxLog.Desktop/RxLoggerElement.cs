using System.Configuration;

namespace RxLog
{
    public class RxLoggerElement : ConfigurationElement
    {
        const string SourceProperty = "source";
        [ConfigurationProperty(SourceProperty, IsRequired = true)]
        public string SourceType
        {
            get { return (string)this[SourceProperty]; }
            set { this[SourceProperty] = value; }
        }

        const string MemberProperty = "member";
        [ConfigurationProperty(MemberProperty)]
        public string MemberName
        {
            get { return (string)this[MemberProperty]; }
            set { this[MemberProperty] = value; }
        }

        const string ArgumentProperty = "arg";
        [ConfigurationProperty(ArgumentProperty)]
        public string Argument
        {
            get { return (string)this[ArgumentProperty]; }
            set { this[ArgumentProperty] = value; }
        }
    }
}