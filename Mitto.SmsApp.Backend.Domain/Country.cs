using System;

namespace Mitto.SmsApp.Backend.Domain
{
    public class Country
    {
        private string _mcc;
        private string _cc;
        private readonly string _name;
        private decimal _pricePerSMS;

        protected Country()
        {
        }

        public Country(string mcc, string cc, string name, decimal pricePerSms)
        {
            if (string.IsNullOrEmpty(mcc)) throw new ArgumentNullException(nameof(mcc));
            if (string.IsNullOrEmpty(cc)) throw new ArgumentNullException(nameof(cc));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (pricePerSms < 0) throw new ArgumentOutOfRangeException(nameof(pricePerSms));
            _mcc = mcc;
            _cc = cc;
            _name = name;
            _pricePerSMS = pricePerSms;
        }

        public virtual int Id { get; protected set; }

        public virtual string Mcc
        {
            get { return _mcc; }
        }

        public virtual string Cc
        {
            get { return _cc; }
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public virtual decimal PricePerSMS
        {
            get { return _pricePerSMS; }
        }
    }
}