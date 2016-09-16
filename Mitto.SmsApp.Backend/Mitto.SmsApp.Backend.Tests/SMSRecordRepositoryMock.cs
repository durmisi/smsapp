using System;
using System.Collections.Generic;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;
using Mitto.SmsApp.Backend.Domain;

namespace Mitto.SmsApp.Backend.Tests
{
    public class SMSRecordRepositoryMock : ISMSRecordRepository
    {
        private readonly List<SMSRecord> _SMSRecords = (new[]
            {
                new SMSRecord(new Country("262", "49", "Germany", Convert.ToDecimal(0.055)), "The Sender",
                    "+491234567890", "Hello World"),
                new SMSRecord(new Country("262", "49", "Germany", Convert.ToDecimal(0.055)), "The Sender",
                    "+491234567890", "Hello World"),
                new SMSRecord(new Country("262", "49", "Germany", Convert.ToDecimal(0.055)), "The Sender",
                    "+491234567890", "Hello World"),
                new SMSRecord(new Country("262", "49", "Germany", Convert.ToDecimal(0.055)), "The Sender",
                    "+491234567890", "Hello World"),
                new SMSRecord(new Country("262", "49", "Germany", Convert.ToDecimal(0.055)), "The Sender",
                    "+491234567890", "Hello World"),
                new SMSRecord(new Country("262", "49", "Germany", Convert.ToDecimal(0.055)), "The Sender",
                    "+491234567890", "Hello World"),
            }).ToList();

        private List<SMSRecord> GetRecords()
        {
            return _SMSRecords.ToList();
        }

        public void Save(SMSRecord entity)
        {
            _SMSRecords.Add(entity);
        }

        public IEnumerable<SMSRecord> GetAll(DateTime dateFrom, DateTime dateTo, List<string> mccList)
        {
            var query = GetRecords().Where(x => dateFrom <= x.SentTime && x.SentTime <= dateTo);
            if (mccList != null && mccList.Any())
            {
                query = query.Where(x => mccList.Contains(x.country.Mcc));
            }

            return query.ToList();
        }

        public IEnumerable<SMSRecord> GetAll(DateTime dateTimeFrom, DateTime dateTimeTo, int skip, int take, out int totalCount)
        {
            var query = GetRecords()
                .Where(x => dateTimeFrom <= x.SentTime && x.SentTime <= dateTimeTo).Skip(skip).Take(take)
                .ToList();

            totalCount = query.Count();

            return query.ToList();
        }
    }
}