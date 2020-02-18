using System;
using System.Runtime.Serialization;

namespace StucturedConsoleApp.Core
{
    [Serializable]
    internal class StructuredArgumentErrorException : Exception
    {
        private GenericCommand req;

        public StructuredArgumentErrorException()
        {
        }

        public StructuredArgumentErrorException(GenericCommand req)
        {
            this.req = req;
        }

        public StructuredArgumentErrorException(string message) : base(message)
        {
        }

        public StructuredArgumentErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StructuredArgumentErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}