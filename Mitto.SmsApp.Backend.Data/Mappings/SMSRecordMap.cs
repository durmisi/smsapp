using FluentNHibernate.Mapping;
using Mitto.SmsApp.Backend.Domain;

namespace Mitto.SmsApp.Backend.Data.Mappings
{
    public class SMSRecordMap : ClassMap<SMSRecord>
    {
        public SMSRecordMap()
        {
            Table("sms_records");
            Schema("mitto_smsapp");

            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.country)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`country_id`");

            Map(x => x.From)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`From`");

            Map(x => x.To)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`To`");

            Map(x => x.Text)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`Text`");

            Map(x => x.State)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`State`");

            Map(x => x.SentTime)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`SentTime`");
        }
    }
}