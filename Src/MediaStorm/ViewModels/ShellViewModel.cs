using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;

using MediaStorm.Resources.Lang;
using MediaStorm.Infrastructure.Services.ErrorHandling;

namespace MediaStorm.ViewModels
{
	public class ShellViewModel : BindableBase
	{
		private readonly IErrorHandler _errorHandler;

		public ShellViewModel(IErrorHandler errorHandler)
		{
			Contract.Requires(errorHandler != null);
			_errorHandler = errorHandler;
		}

		public string Title
		{
			get { return Strings.MainWndTitle; }
		}

		public int Width
		{
			get { return 1225; }
		}

		public int Height
		{
			get { return 750; }
		}
		
		public DelegateCommand<object> VkClickCommand
		{
			get
			{
				return new DelegateCommand<object>((obj) =>
				{
					const string url = "http://vk.com";

					try
					{
						Process.Start(url);
					}
					catch (Exception e)
					{
						_errorHandler.ReportError(e, "Failed to launch '{0}' in default browser.", url);
					}
				});
			}
		}
	}
}
