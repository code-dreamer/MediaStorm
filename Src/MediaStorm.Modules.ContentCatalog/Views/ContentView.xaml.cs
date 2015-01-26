using System.Diagnostics.Contracts;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace MediaStorm.Modules.ContentCatalog.Views
{
	/// <summary>
	/// Interaction logic for ContentView.xaml
	/// </summary>
	public partial class ContentView : UserControl
	{
		public ContentView(BindableBase model)
		{
			Contract.Requires(model != null);

			InitializeComponent();

			DataContext = model;
		}
	}
}
