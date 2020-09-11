using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class ApiApplicationErrorException : Exception
    {
        public ApiApplicationErrorException()
        {
        }

        public ApiApplicationErrorException(string message) : base(message)
        {
        }

        public ApiApplicationErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiApplicationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}