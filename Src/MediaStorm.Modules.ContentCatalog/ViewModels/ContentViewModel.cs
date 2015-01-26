using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace MediaStorm.Modules.ContentCatalog.ViewModels
{
	class ContentViewModel : BindableBase
	{
		private string _url;

		public string Url
		{
			get { return _url; }
			set { SetProperty(ref _url, value); }
		}

		public ContentViewModel()
		{
			Url = "about:blank";
		}
	}
}
