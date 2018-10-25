using Feeds.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class SettingsViewModel : ExtendedBindableObject
    {
        private readonly IPageService _pageService;
        public ICommand LogoutCommand { get; private set; }

        public SettingsViewModel(IPageService pageService)
        {
            _pageService = pageService;
            LogoutCommand = new Command(OnLogout);
        }

        private async void OnLogout()
        {
            ClearSettings();
            await _pageService.PushAsync(new LoginView());
        }

        private void ClearSettings()
        {
            return;
        }
    }
}
