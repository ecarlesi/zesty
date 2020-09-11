using System;
using Microsoft.AspNetCore.Http;

namespace Zesty.Core
{
    public interface IExecutionHandler
    {
        void Process(HttpContext context);
    }
}
