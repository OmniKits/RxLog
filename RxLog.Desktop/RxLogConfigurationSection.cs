using System.Configuration;

namespace RxLog
{
    public class RxLogConfigurationSection : ConfigurationSection
    {
        const string DefaultProperty = "default";
        [ConfigurationProperty(DefaultProperty)]
        public RxLoggerCollection Default
        {
            get { return (RxLoggerCollection)this[DefaultProperty]; }
            set { this[DefaultProperty] = value; }
        }

        const string SubjectsProperty = "subjects";
        [ConfigurationProperty(SubjectsProperty)]
        public RxLogSubjectCollection Subjects
        {
            get { return (RxLogSubjectCollection)this[SubjectsProperty]; }
            set { this[SubjectsProperty] = value; }
        }
    }
}
