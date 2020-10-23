using System;
using Microsoft.AspNetCore.Http;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Init : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            input.Context.Session.SetString("init", Guid.NewGuid().ToString());

            return GetOutput(new InitResponse()
            {
                Message = Messages.Success
            });
        }
    }

    public class InitResponse
    {
        public string Message { get; set; }
    }
}
