﻿namespace Zesty.Core.Entities
{
    public class SmtpClient
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
