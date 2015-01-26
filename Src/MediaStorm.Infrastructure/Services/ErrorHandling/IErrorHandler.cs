using System;
using System.Diagnostics.Contracts;

namespace MediaStorm.Infrastructure.Services.ErrorHandling
{
	[ContractClass(typeof(ContractForErrorHandler))]
	public interface IErrorHandler
	{
		void ReportError(string message);
		void ReportError(Exception exception);
		void ReportError(Exception exception, string additionalMessage, params object[] args);
	}
	
	[ContractClassFor(typeof(IErrorHandler))]
	public abstract class ContractForErrorHandler : IErrorHandler
	{

		public void ReportError(string message)
		{
			Contract.Requires(!string.IsNullOrEmpty(message));
		}

		public void ReportError(Exception exception)
		{}

		public void ReportError(Exception exception, string additionalMessage, params object[] args)
		{}
	}
}
