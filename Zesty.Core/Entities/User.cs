using System;
using System.Collections.Generic;

namespace Zesty.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid ResetToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Entities.Domain Domain { get; set; }
        public DateTime PasswordChanged { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public List<Authorization> Authorizations { get; set; }
    }
}
