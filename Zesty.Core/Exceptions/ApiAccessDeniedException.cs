using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class ApiAccessDeniedException : Exception
    {
        public ApiAccessDeniedException()
        {
        }

        public ApiAccessDeniedException(string message) : base(message)
        {
        }

        public ApiAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}