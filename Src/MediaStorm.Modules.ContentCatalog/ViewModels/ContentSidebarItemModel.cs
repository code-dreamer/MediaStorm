using System;
using System.Diagnostics.Contracts;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace MediaStorm.Modules.ContentCatalog.ViewModels
{
	public class ContentSidebarItemModel : BindableBase
	{
		private readonly string _contentUrl;
		public ContentSidebarItemModel(string imageFilepath, string label, string contentUrl)
		{
			Contract.Requires(!string.IsNullOrEmpty(imageFilepath));
			Contract.Requires(!string.IsNullOrEmpty(label));
			Contract.Requires(!string.IsNullOrEmpty(contentUrl));

			ImagePath = imageFilepath;
			Label = label;
			_contentUrl = contentUrl;
		}

		public string ImagePath { get; private set; }
		
		public string Label { get; private set; }

		public ICommand SelectCommand
		{
			get
			{
				return new DelegateCommand(OnItemSelected);
			}
		}

		public event EventHandler<string> ItemSelected;

		private void OnItemSelected()
		{
			if (ItemSelected != null)
				ItemSelected(this, _contentUrl);
		}

	}
}
