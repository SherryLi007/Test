using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Inventec.Common
{

    public static class MailHelper
    {
        //NotesSession NSession;
        //NotesDatabase NDataBase;

        public static bool SendMail(string tomail, string tomailName, string mailSubject, string mailContent)
        {
            string MailSmtp = "appmail-dmz.mail.porsche.org";
            string MailUsername = "porsche.eflow@porsche.cn";
            string MailPassword = "";
            string MailFrom = "porsche.eflow@porsche.cn";

            System.Net.Mail.SmtpClient client = new SmtpClient(MailSmtp);

            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(MailUsername, MailPassword);

            MailAddress addressFrom = new MailAddress(MailFrom, "Porsche E-flow");
            MailAddress addressTo = new MailAddress(tomail, tomailName);

            System.Net.Mail.MailMessage message = new MailMessage(addressFrom, addressTo);
            message.Headers.Add("Disposition-Notification-To", MailUsername);
            message.Headers.Add("Return-Receipt-To", MailUsername);
            message.Sender = new MailAddress(MailFrom);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = mailSubject;
            message.Body = mailContent + "<Br><br>Thanks and best regards,<Br>Porsche E-flow Approval Platform</a><br><a href='https://eflow.pcn.porsche.org/' target='_blank'>https://eflow.pcn.porsche.org/</a>";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
