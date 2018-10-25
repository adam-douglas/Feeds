using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsView : ContentPage
	{
        private SettingsViewModel settingsViewModel;

		public SettingsView ()
		{
			InitializeComponent ();
            settingsViewModel = new SettingsViewModel(new PageService());
            this.BindingContext = settingsViewModel;
		}
	}
}