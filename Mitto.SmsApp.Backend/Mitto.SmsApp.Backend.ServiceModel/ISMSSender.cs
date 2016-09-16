using Mitto.SmsApp.Backend.Domain;
using System;
using System.IO;

namespace Mitto.SmsApp.Backend.ServiceModel
{
    public interface ISMSSender
    {
        bool SendSMS(SMSRecord smsRecord);
    }

    public class DummySMSSender : ISMSSender
    {
        private string tempDir = @"C:\temp\";
        private string fileName = @"sms_records.txt";

        public bool SendSMS(SMSRecord smsRecord)
        {
            try
            {
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }

                var filePath = Path.Combine(tempDir, fileName);
                var contents = string.Format("from:{0};to:{1};text:{2};{3}", smsRecord.From, smsRecord.To, smsRecord.Text, Environment.NewLine);

                File.AppendAllText(filePath, contents);

                return true;
            }
            catch (Exception)
            {
                //todo: log error and rethrow
            }

            return false;
        }
    }
}