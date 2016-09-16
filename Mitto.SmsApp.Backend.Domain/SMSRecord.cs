using System;

namespace Mitto.SmsApp.Backend.Domain
{
    public class SMSRecord
    {
        private readonly Country _country;
        private string _from;
        private string _to;
        private string _text;
        private bool _state;
        private DateTime _sentTime;

        protected SMSRecord()
        {
        }

        public SMSRecord(Country country, string @from, string to, string text)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            if (@from == null) throw new ArgumentNullException(nameof(@from));
            if (to == null) throw new ArgumentNullException(nameof(to));
            if (text == null) throw new ArgumentNullException(nameof(text));

            _country = country;
            _from = @from;
            _to = to;
            _text = text;
            _state = false;
            _sentTime = DateTime.UtcNow;
        }

        public virtual Country country
        {
            get { return _country; }
        }

        public virtual int Id { get; protected set; }

        public virtual string From
        {
            get { return _from; }
        }

        public virtual string To
        {
            get { return _to; }
        }

        public virtual string Text
        {
            get { return _text; }
        }

        public virtual bool State
        {
            get { return _state; }
        }

        public virtual DateTime SentTime
        {
            get { return _sentTime; }
        }

        public virtual void SetState(bool state)
        {
            _state = state;
        }
    }
}