using System;
using System.Net;
using System.Net.Mail;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Common
{
    class SmtpClient
    {
        internal static void Send(string to, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(Settings.Current.SmtpClient.Username);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Host = Settings.Current.SmtpClient.Host;
            client.Port = Settings.Current.SmtpClient.Port;
            client.EnableSsl = Settings.Current.SmtpClient.Ssl;

            string username = Settings.Current.SmtpClient.Username;
            string password = Settings.Current.SmtpClient.Password;

            if (!String.IsNullOrWhiteSpace(username))
            {
                client.Credentials = new NetworkCredential(username, password);
            }

            client.Send(message);
        }
    }
}
