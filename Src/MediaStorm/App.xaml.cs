using System;
using System.Windows;
using Microsoft.Practices.Unity;
using MediaStorm.Infrastructure.Logging;
using MediaStorm.Infrastructure.Services.ErrorHandling;

namespace MediaStorm
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly Bootstrapper _bootstrapper;

		public App()
		{
			_bootstrapper = new Bootstrapper();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			Logger.Info("Start app");

#if (DEBUG)
			_bootstrapper.Run();
#else
             AppDomain.CurrentDomain.UnhandledException += (sender, eventargs) =>
             {
				IErrorHandler errorHandler = _bootstrapper.Container.Resolve<IErrorHandler>();
				errorHandler.ReportError(eventargs.ExceptionObject as Exception);
				
				Environment.Exit(1);
			};

			try
			{
				_bootstrapper.Run();
			}
			catch (Exception ex)
			{
				IErrorHandler errorHandler = _bootstrapper.Container.Resolve<IErrorHandler>();
				errorHandler.ReportError(ex);
				
				Environment.Exit(1);
			}
#endif
		}

		protected override void OnExit(ExitEventArgs e)
		{
			Logger.Info("Shutdown app");
			Logger.Flush();

			base.OnExit(e);
		}
	}
}
