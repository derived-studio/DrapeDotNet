using System;
using System.Runtime.Serialization;

namespace Drape.Exceptions
{
	[Serializable]
	public class InvalidJsonException : Exception
	{
		// Constructors
		public InvalidJsonException(string msg, string json)
			: base(msg + "\n" + json)
		{ }

		// Ensure Exception is Serializable
		protected InvalidJsonException(SerializationInfo info, StreamingContext ctxt)
			: base(info, ctxt)
		{ }
	}
}