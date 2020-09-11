using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class ApiNotFoundException : Exception
    {
        public ApiNotFoundException()
        {
        }

        public ApiNotFoundException(string message) : base(message)
        {
        }

        public ApiNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}