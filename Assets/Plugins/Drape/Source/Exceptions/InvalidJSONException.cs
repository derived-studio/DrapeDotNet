using System;
using System.Runtime.Serialization;

namespace Drape.Exceptions
{
    [Serializable]
    public class InvalidJSONException : Exception
    {
        // Constructors
        public InvalidJSONException(string msg, string json)
            : base(msg + "\n" + json)
        { }

        // Ensure Exception is Serializable
        protected InvalidJSONException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }
}