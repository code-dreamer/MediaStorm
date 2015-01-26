using System;
using System.Diagnostics.Contracts;
using System.Windows;

using MediaStorm.Resources.Lang;
using MediaStorm.Infrastructure.Logging;
using MediaStorm.Infrastructure.Services.ErrorHandling;

namespace MediaStorm.Core.ErrorHandling
{
	public class ErrorHandler : IErrorHandler
	{
		public void ReportError(Exception exception)
		{
			Logger.Error(string.Empty, exception);
			Logger.Flush();

			string errorMsg = exception == null ? Strings.UnknownErrorMessage : exception.Message;
			ReportError(errorMsg);
		}

		public void ReportError(string message)
		{
			MessageBox.Show(message, Strings.ErrorWndTitle, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public void ReportError(Exception exception, string additionalMessage, params object[] args)
		{
			if (string.IsNullOrEmpty(additionalMessage))
			{
				Contract.Assert(false);
				ReportError(exception);
				return;
			}
			if (exception == null)
			{
				Contract.Assert(false);
				ReportError(Strings.UnknownErrorMessage);
				return;
			}

			string message = string.Format(additionalMessage, args);

			Logger.Error(message, exception);
			Logger.Flush();

			string errorMsg = message + Environment.NewLine + exception.Message;
			ReportError(errorMsg);
		}
	}
}
