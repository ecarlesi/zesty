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

            message.From = new MailAddress(Settings.Get("SmtpClient.Username"));
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Host = Settings.Get("SmtpClient.Host");
            client.Port = Settings.GetInt("SmtpClient.Port");
            client.EnableSsl = Settings.GetBool("SmtpClient.Ssl");

            string username = Settings.Get("SmtpClient.Username");
            string password = Settings.Get("SmtpClient.Password");

            if (!String.IsNullOrWhiteSpace(username))
            {
                client.Credentials = new NetworkCredential(username, password);
            }

            client.Send(message);
        }
    }
}
