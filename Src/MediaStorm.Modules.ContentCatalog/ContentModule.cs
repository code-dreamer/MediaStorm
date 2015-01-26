using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

using MediaStorm.Infrastructure;
using MediaStorm.Modules.ContentCatalog.ViewModels;
using MediaStorm.Modules.ContentCatalog.Views;

namespace MediaStorm.Modules.ContentCatalog
{
	[Module(ModuleName = "ContentModule")]
	[ModuleDependency("DownloaderModule")]
	public class ContentModule : IModule
	{
		private readonly IUnityContainer _container;
		private readonly IRegionManager _regionManager;

		public ContentModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

		public void Initialize()
		{
			//_regionManager.RegisterViewWithRegion(RegionNames.LeftSidebarRegion,
				//									   () => this.container.Resolve<EmployeeListView>());


			//_regionManager.RegisterViewWithRegion(RegionNames.LeftSidebarRegion, typeof(SidebarItemView));

			IRegion sidebarRegion = _regionManager.Regions[RegionNames.LeftSidebarRegion];
			IRegion contentRegion = _regionManager.Regions[RegionNames.ContentRegion];

			var items = GetItems();
			foreach (var item in items)
			{
				var contentSidebarItemModel = new ContentSidebarItemModel(item.Item1, item.Item2, item.Item3);
				var contentSidebarItemView = new ContentSidebarItemView(contentSidebarItemModel);
				sidebarRegion.Add(contentSidebarItemView, contentSidebarItemModel.Label);

				var contentViewModel = new ContentViewModel();
				var contentView = new ContentView(contentViewModel);
				contentRegion.Add(contentView, contentSidebarItemModel.Label + "Content");

				contentSidebarItemModel.ItemSelected += (sender, url) =>
				{
					Contract.Requires(!string.IsNullOrEmpty(url));

					contentViewModel.Url = url;
				};
			}
		}

		static List<Tuple<string, string, string>> GetItems()
		{
			Func<string, string> getImagePath = (imageFile) =>
				string.Format("pack://application:,,,/{0};component/Resources/Images/Sidebar/{1}", Assembly.GetExecutingAssembly().GetName().Name, imageFile);

			return new List<Tuple<string, string, string>>(new[]
			{
				new Tuple<string, string, string>(getImagePath("Films.png"), "Films", "http://google.com"),
				new Tuple<string, string, string>(getImagePath("Sport.png"), "Sport", "http://google.com"),
				new Tuple<string, string, string>(getImagePath("Books.png"), "Books", "http://google.com"),
				new Tuple<string, string, string>(getImagePath("Games.png"), "Games", "http://google.com"),
				new Tuple<string, string, string>(getImagePath("Music.png"), "Music", "http://google.com"),
				new Tuple<string, string, string>(getImagePath("Tv.png"), "Tv", "http://google.com"),
				new Tuple<string, string, string>(getImagePath("Radio.png"), "Radio", "http://google.com"),
			});
		}
	}
}
