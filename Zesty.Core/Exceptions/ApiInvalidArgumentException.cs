using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class ApiInvalidArgumentException : Exception
    {
        public ApiInvalidArgumentException()
        {
        }

        public ApiInvalidArgumentException(string message) : base(message)
        {
        }

        public ApiInvalidArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiInvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}