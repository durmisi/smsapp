﻿using Mitto.SmsApp.Backend.Domain;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Mitto.SmsApp.Backend.Core.Data.Contracts;

namespace Mitto.SmsApp.Backend.Data.Repository
{
    public class SMSRecordRepository : ISMSRecordRepository
    {
        public ISessionFactory NHSessionFactory { get; set; }

        public SMSRecordRepository(ISessionFactory sessionFactory)
        {
            NHSessionFactory = sessionFactory;
        }

        public IEnumerable<SMSRecord> GetAll(DateTime dateFrom, DateTime dateTo, List<string> mccList)
        {
            using (var session = NHSessionFactory.OpenSession())
            {
                var query = session.Query<SMSRecord>()
                    .Fetch(x => x.country)
                    .Where(x => dateFrom <= x.SentTime && x.SentTime <= dateTo);

                if (mccList != null && mccList.Any())
                {
                    query = query.Where(x => mccList.Contains(x.country.Mcc));
                }

                return query.ToList();
            }
        }

        public IEnumerable<SMSRecord> GetAll(DateTime dateTimeFrom, DateTime dateTimeTo, int skip, int take, out int totalCount)
        {
            using (var session = NHSessionFactory.OpenSession())
            {
                var query = session.Query<SMSRecord>()
                    .Fetch(x => x.country)
                    .Where(x => dateTimeFrom <= x.SentTime && x.SentTime <= dateTimeTo).Skip(skip).Take(take);
                totalCount = query.Count();
                return query.ToList();
            }
        }

        public void Save(SMSRecord entity)
        {
            using (var session = NHSessionFactory.OpenSession())
            {
                session.SaveOrUpdate(entity);
            }
        }
    }
}