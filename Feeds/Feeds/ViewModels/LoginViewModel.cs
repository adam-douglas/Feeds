using Feeds.Models;
using Feeds.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private UserAccounts _userAccounts = new UserAccounts();
        public ICommand LoginCommand { get; private set; }
        public ICommand BusinessTapCommand { get; set; }
        public ICommand OrgTapCommand { get; set; }
        private readonly IPageService _pageService;

        public UserAccounts UserAccounts
        {
            get { return _userAccounts; }
            set
            {
                _userAccounts = value;
                OnPropertyChanged();
            }
        }
        
        public LoginViewModel(IPageService pageService)
        {
            _pageService = pageService;
            LoginCommand = new Command(OnLogin);
            BusinessTapCommand = new Command(OnBusinessTap);
            OrgTapCommand = new Command(OnOrgTap);
        }

        private async void OnLogin()
        {
            await _pageService.DisplayAlert("Login", "Successfully logged in.", "OK", "Cancel");
        }

        private async void OnBusinessTap()
        {
            await _pageService.PushAsync(new BusinessRegistrationView());
        }

        private async void OnOrgTap()
        {
            await _pageService.PushAsync(new OrgRegistrationView());
        }
    }
}
