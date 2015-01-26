using System.Diagnostics.Contracts;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace MediaStorm.Modules.ContentCatalog.Views
{
	/// <summary>
	/// Interaction logic for ContentSidebarItemView.xaml
	/// </summary>
	public partial class ContentSidebarItemView : UserControl
	{
		public ContentSidebarItemView(BindableBase model)
		{
			Contract.Requires(model != null);

			InitializeComponent();

			DataContext = model;
		}
	}
}
