using System;
namespace Zesty.Core.Entities
{
    public class LoginOutput
    {
        public User User { get; set; }
        public LoginResult Result { get; set; }
    }
}
