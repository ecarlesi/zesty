using System;
using System.Collections.Generic;

namespace Zesty.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid ResetToken { get; set; }
        public long Created { get; set; }
        public long Deleted { get; set; }
        public long PasswordChanged { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Guid DomainId { get; set; }
        public Domain Domain { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public List<Authorization> Authorizations { get; set; }
    }
}
