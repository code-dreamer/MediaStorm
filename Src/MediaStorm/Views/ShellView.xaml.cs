using System.Windows;
using MediaStorm.ViewModels;

namespace MediaStorm.Views
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : Window
	{
		public ShellView(ShellViewModel shellViewModel)
		{
			InitializeComponent();

			DataContext = shellViewModel;
		}
	}
}
