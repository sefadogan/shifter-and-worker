using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace SAW.UI.Helpers
{
    public class SendEmail
    {
        public static bool SendEmailSmtp(string replyTo, string subject, string body, List<string> to)
        {
            MailMessage mail = new MailMessage();
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            foreach (var item in to)
                mail.To.Add(new MailAddress(item));

            mail.From = new MailAddress("contact@sefadogan.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpClient client = new SmtpClient("mail.sefadogan.com", 587);
            NetworkCredential credential = new NetworkCredential
            {
                UserName = "contact@sefadogan.com",
                Password = "%sz401eR."
            };

            client.UseDefaultCredentials = false;
            client.Credentials = credential;
            client.EnableSsl = false;

            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}