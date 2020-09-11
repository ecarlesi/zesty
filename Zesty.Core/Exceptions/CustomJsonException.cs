using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class CustomJsonException : Exception
    {
        public CustomJsonException()
        {
        }

        public CustomJsonException(string message) : base(message)
        {
        }

        public CustomJsonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomJsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}