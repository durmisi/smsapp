using System;
using System.Collections.Generic;
using Mitto.SmsApp.Backend.Domain;

namespace Mitto.SmsApp.Backend.Core.Data.Contracts
{
    public interface ISMSRecordRepository
    {
        void Save(SMSRecord entity);

        IEnumerable<SMSRecord> GetAll(DateTime dateFrom, DateTime dateTo, List<string> mccList);

        IEnumerable<SMSRecord> GetAll(DateTime dateTimeFrom, DateTime dateTimeTo, int skip, int take, out int totalCount);
    }
}