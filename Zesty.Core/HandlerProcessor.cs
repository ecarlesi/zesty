using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Zesty.Core.Common;

namespace Zesty.Core
{
    class HandlerProcessor
    {
        //TODO add cache support

        internal static void Process(List<string> executionHandlers, HttpContext context)
        {
            if (executionHandlers == null || executionHandlers.Count == 0)
            {
                return;
            }

            foreach (string typeName in executionHandlers)
            {
                IExecutionHandler handler = InstanceHelper.Create<IExecutionHandler>(typeName);

                handler.Process(context);
            }
        }
    }
}
