using System;
using System.Runtime.Serialization;

namespace Snowinmars.UserSlice.UserEntites
{
	public class UserIsMoronException : Exception
	{
		public UserIsMoronException()
		{
		}

		public UserIsMoronException(string message) : base(message)
		{
		}

		public UserIsMoronException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UserIsMoronException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
