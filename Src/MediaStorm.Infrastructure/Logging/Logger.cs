using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaStorm.Infrastructure.Logging
{
	public enum LogLevel
	{
		// Can be useful for app debugging
		// "Point x pos changed to 104"
		Verbose,

		// Information messages. Can be useful for signaling about important events in application. 
		// This information can be interesting for administrators too.
		// "'Quake 2' game downloading started"
		Info,

		// Can be useful if some strange, but non-fatal has happen
		// "Partial allocation for 'Quake 2' failed. Prepare for full allocation..."
		Warning,

		// Error (not critical). Application may remain unstable but not crashing.
		// "Can't load icon 'smile.png'. Windows error 2."
		Error,

		// Fatal, unrecoverable error. Application in crash state and can't be recovered (unhandled exceptions handlers and etc).
		// "Unhanded Exception. Code 0xC0000005 (access violation). Address:
		Fatal
	}

	public static class Logger
	{
		private readonly static BlockingCollection<string> LogEntries = new BlockingCollection<string>();

		public static LogLevel CurrentLogLevel { get; set; }

		public static void Flush()
		{
			while (LogEntries.Count > 0)
			{
				Thread.Sleep(20);
			}
			Trace.Flush();
		}

		static Logger()
		{
			
			Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
#if (DEBUG)
			Trace.AutoFlush = true;
			CurrentLogLevel = LogLevel.Verbose;
#else
			CurrentLogLevel = LogLevel.Info;
#endif

			Task.Run(() =>
			{
				foreach (string msg in LogEntries.GetConsumingEnumerable())
				{
					System.Diagnostics.Trace.WriteLine(msg);
				}
			});

			WriteMessage("\n");
		}

		
		#region Verbose
		public static void Verbose(string message, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Verbose, message, e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Verbose(string format, object arg0, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Verbose, string.Format(format, arg0), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Verbose(string format, object arg0, object arg1, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Verbose, string.Format(format, arg0, arg1), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Verbose(string format, object arg0, object arg1, object arg2, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Verbose, string.Format(format, arg0, arg1, arg2), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Verbose(string format, object arg0, object arg1, object arg2, object arg3, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Verbose, string.Format(format, arg0, arg1, arg2, arg3), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		#endregion // Verbose

		#region Info
		public static void Info(string message, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Info, message, e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Info(string format, object arg0, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Info, string.Format(format, arg0), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Info(string format, object arg0, object arg1, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Info, string.Format(format, arg0, arg1), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Info(string format, object arg0, object arg1, object arg2, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Info, string.Format(format, arg0, arg1, arg2), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Info(string format, object arg0, object arg1, object arg2, object arg3, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Info, string.Format(format, arg0, arg1, arg2, arg3), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		#endregion // Info

		#region Warning
		public static void Warning(string message, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Warning, message, e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Warning(string format, object arg0, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Warning, string.Format(format, arg0), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Warning(string format, object arg0, object arg1, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Warning, string.Format(format, arg0, arg1), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Warning(string format, object arg0, object arg1, object arg2, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Warning, string.Format(format, arg0, arg1, arg2), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Warning(string format, object arg0, object arg1, object arg2, object arg3, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Warning, string.Format(format, arg0, arg1, arg2, arg3), e, callerMemberName, callerFilePath, callerLineNumber);
		}
		
		#endregion // Warning

		#region Error
		public static void Error(string message, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Error, message, e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Error(string format, object arg0, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Error, string.Format(format, arg0), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Error(string format, object arg0, object arg1, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Error, string.Format(format, arg0, arg1), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Error(string format, object arg0, object arg1, object arg2, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Error, string.Format(format, arg0, arg1, arg2), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Error(string format, object arg0, object arg1, object arg2, object arg3, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Error, string.Format(format, arg0, arg1, arg2, arg3), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		#endregion // Error

		#region Fatal
		public static void Fatal(string message, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Fatal, message, e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Fatal(string format, object arg0, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Fatal, string.Format(format, arg0), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Fatal(string format, object arg0, object arg1, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Fatal, string.Format(format, arg0, arg1), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Fatal(string format, object arg0, object arg1, object arg2, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Fatal, string.Format(format, arg0, arg1, arg2), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Fatal(string format, object arg0, object arg1, object arg2, object arg3, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(LogLevel.Fatal, string.Format(format, arg0, arg1, arg2, arg3), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		#endregion // Fatal

		#region Common write
		public static void Write(LogLevel logLevel, string message, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "", 
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(logLevel, message, e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Write(LogLevel logLevel, string format, object arg0, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(logLevel, string.Format(format, arg0), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Write(LogLevel logLevel, string format, object arg0, object arg1, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(logLevel, string.Format(format, arg0, arg1), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Write(LogLevel logLevel, string format, object arg0, object arg1, object arg2, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(logLevel, string.Format(format, arg0, arg1, arg2), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		public static void Write(LogLevel logLevel, string format, object arg0, object arg1, object arg2, object arg3, Exception e = null,
			[CallerMemberName] string callerMemberName = "",
			[CallerFilePath] string callerFilePath = "",
			[CallerLineNumber] int callerLineNumber = 0)
		{
			DoLog(logLevel, string.Format(format, arg0, arg1, arg2, arg3), e, callerMemberName, callerFilePath, callerLineNumber);
		}

		#endregion // Common write

		private static void DoLog(LogLevel logLevel, string message, Exception e, string callerMemberName, string callerFilePath, int callerLineNumber)
		{
			if (logLevel < CurrentLogLevel)
				return;

			WriteMessage(
				FormatLogEntry(logLevel, message, e, callerMemberName, callerFilePath, callerLineNumber)
				);
		}

		private static string FormatLogEntry(LogLevel logLevel, string message, Exception e, string callerMemberName, string callerFilePath, int callerLineNumber)
		{
			string datetime = DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} |{1}| {2}     <{3}({4}) {5} >",
				datetime, LogLevelToString(logLevel), string.IsNullOrEmpty(message) ? "Dummy message." : message,
				new FileInfo(callerFilePath).Name, callerLineNumber, callerMemberName);
			
			if (e != null)
			{
				const string delimeter = "------------------------";

				sb.Append(Environment.NewLine);
				sb.AppendLine("Exception(s):");
				sb.AppendLine(delimeter);
				sb.AppendLine(e.ToString());

				Exception currEx = e;
				while (currEx.InnerException != null)
				{
					currEx = currEx.InnerException;
					sb.AppendLine(currEx.ToString());
				}

				sb.AppendLine(delimeter);
			}

			return sb.ToString();
		}

		private static object LogLevelToString(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.Verbose:
					return "VERBOSE";
				case LogLevel.Info:
					return "INFO";
				case LogLevel.Warning:
					return "WARNING";
				case LogLevel.Error:
					return "ERROR";
				case LogLevel.Fatal:
					return "FATAL";
			}

			Debug.Fail("Unknown LogLevel value");
			return string.Empty;
		}

		private static void WriteMessage(string message)
		{
			LogEntries.Add(message);
		}
	}
}
