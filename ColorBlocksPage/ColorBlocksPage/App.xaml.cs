using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorBlocksPage
{
	//Chris Hubler & Hannah Wiles
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new ColorBlockDemo();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
