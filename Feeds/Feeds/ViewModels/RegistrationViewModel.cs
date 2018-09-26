using Feeds.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        private UserAccounts _userAccounts = new UserAccounts();
        public string Email2 { get; set; }
        public string Password2 { get; set; }
        public ICommand SubmitCommand { get; set; }
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

        public RegistrationViewModel(IPageService pageService)
        {
            _pageService = pageService;
            SubmitCommand = new Command(OnSubmit);
        }

        private async void OnSubmit()
        {
            await _pageService.DisplayAlert("Registered", "Successfully registered.", "OK", "Cancel");
        }
    }
}
