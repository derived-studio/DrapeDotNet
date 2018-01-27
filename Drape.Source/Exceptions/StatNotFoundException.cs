using System;
using System.Runtime.Serialization;

namespace Drape.Exceptions
{
	[Serializable]
	public class StatNotFoundException : Exception
	{
		// Constructors
		public StatNotFoundException(string statCode)
			: base("Stat code:" + statCode + " not found.")
		{ }

		// Ensure Exception is Serializable
		protected StatNotFoundException(SerializationInfo info, StreamingContext ctxt)
			: base(info, ctxt)
		{ }
	}
}