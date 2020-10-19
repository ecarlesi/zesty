using System;
using System.Runtime.Serialization;

namespace Zesty.Core.Exceptions
{
    [Serializable]
    internal class MissingRequiredProperty : Exception
    {
        public MissingRequiredProperty()
        {
        }

        public MissingRequiredProperty(string message) : base(String.Format(Messages.MissingRequireArgument, message))
        {

        }

        public MissingRequiredProperty(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingRequiredProperty(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}