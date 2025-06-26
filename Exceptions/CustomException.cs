namespace TestService.Exceptions
{
	/// <summary>
	/// Represents a custom business-level exception thrown by services.
	/// </summary>
	public class CustomException : Exception
	{
		/// <summary>
		/// Optional custom error code provided by the underlying library.
		/// </summary>
		public int? ErrorCode { get; }

		public CustomException(string message, int? errorCode = null)
			: base(message)
		{
			ErrorCode = errorCode;
		}
	}
}
