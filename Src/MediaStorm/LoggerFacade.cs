using System.Diagnostics;
using Microsoft.Practices.Prism.Logging;

using MediaStorm.Infrastructure.Logging;

namespace MediaStorm
{
	class LoggerFacade : ILoggerFacade
	{
		public void Log(string message, Category category, Priority priority)
		{
			LogLevel logLevel;
			
			switch (category)
			{
				case Category.Debug:
					logLevel = LogLevel.Verbose;
					break;

				case Category.Exception:
					logLevel = LogLevel.Error;
					break;

				case Category.Info:
					logLevel = LogLevel.Info;
					break;

				case Category.Warn:
					logLevel = LogLevel.Warning;
					break;

				default:
					logLevel = LogLevel.Verbose;
					Debug.Assert(false);
					break;
			}

			Logger.Write(logLevel, message);
		}
	}
}
