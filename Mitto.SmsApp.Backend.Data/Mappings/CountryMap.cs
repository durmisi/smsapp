using FluentNHibernate.Mapping;
using Mitto.SmsApp.Backend.Domain;

namespace Mitto.SmsApp.Backend.Data.Mappings
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Table("countries");
            Schema("mitto_smsapp");

            Id(x => x.Id).GeneratedBy.Identity().Column("`Id`");

            Map(x => x.Cc)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`cc`");

            Map(x => x.Mcc)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`mcc`");

            Map(x => x.Name)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`name`");

            Map(x => x.PricePerSMS)
                .Access.CamelCaseField(Prefix.Underscore)
                .Column("`pricePerSMS`");
        }
    }
}