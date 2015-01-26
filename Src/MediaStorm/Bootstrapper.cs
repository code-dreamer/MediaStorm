using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

using MediaStorm.ViewModels;
using MediaStorm.Views;
using MediaStorm.Core.ErrorHandling;
using MediaStorm.Infrastructure.Services.ErrorHandling;
using MediaStorm.Modules.ContentCatalog;
using MediaStorm.Modules.Downloader;

namespace MediaStorm
{
	class Bootstrapper : UnityBootstrapper
	{
		protected override void ConfigureModuleCatalog()
		{
			base.ConfigureModuleCatalog();

			ModuleCatalog moduleCatalog = (ModuleCatalog)ModuleCatalog;
			moduleCatalog.AddModule(typeof(DownloaderModule));
			moduleCatalog.AddModule(typeof(ContentModule));
		}

		/*
		protected override IModuleCatalog CreateModuleCatalog()
		{
			//var moduleCatalog = new DirectoryModuleCatalog { ModulePath = @"d:\workNew\C#\MediaStorm\Build\Debug\Modules" };
			var moduleCatalog = new DirectoryModuleCatalog { ModulePath = @".\" };
			return moduleCatalog;
		}*/

		protected override DependencyObject CreateShell()
		{
			ShellViewModel shellViewModel = Container.Resolve<ShellViewModel>();
			return new ShellView(shellViewModel);
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();

			//AppInfo a = new AppInfo();

			Application.Current.MainWindow = (Window)Shell;
			Application.Current.MainWindow.Show();
		}
		
		protected override ILoggerFacade CreateLogger()
		{
			return new LoggerFacade();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

			// register dependencies
			Container.RegisterInstance<IErrorHandler>(new ErrorHandler());

			//this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);
			//this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);
		}
	}
}
