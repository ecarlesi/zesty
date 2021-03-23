using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class ApiTokenExpiredException : Exception
    {
        public ApiTokenExpiredException()
        {
        }

        public ApiTokenExpiredException(string message) : base(message)
        {
        }

        public ApiTokenExpiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiTokenExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}