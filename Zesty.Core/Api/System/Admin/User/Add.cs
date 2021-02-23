using System;
using System.Collections.Generic;
using System.Linq;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Add : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            AddRequest request = GetEntity<AddRequest>(input);

            Entities.User user = new Entities.User()
            {
                Username = request.Username,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

            Guid id = Business.User.Add(user);

            List<Translation> translations = StorageManager.Instance.GetTranslations("en");

            string subject = translations.Where(x => x.Original == "User created").FirstOrDefault().Translated;
            string body = translations.Where(x => x.Original == "Go to the portal and reset your password to grant access").FirstOrDefault().Translated;

            Common.SmtpClient.Send(request.Email, subject, body);

            return GetOutput(new { Id = id });
        }
    }

    public class AddRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
