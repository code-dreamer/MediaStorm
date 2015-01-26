using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MediaStorm.Controls
{
	public class SidebarControl : Selector //ItemsControl
	{
		protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);
		}
	}
}
