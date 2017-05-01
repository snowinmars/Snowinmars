using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
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
