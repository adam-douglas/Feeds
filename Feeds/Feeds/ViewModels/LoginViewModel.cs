using Feeds.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class LoginViewModel : ExtendedBindableObject
    {
        private ValidatableObject<string> _username;
        private ValidatableObject<string> _password;
        public ICommand ValidateUsernameCommand { get; private set; }
        public ICommand ValidatePasswordCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand BusinessTapCommand { get; private set; }
        public ICommand OrgTapCommand { get; private set; }
        private readonly IPageService _pageService;
        
        public LoginViewModel(IPageService pageService)
        {
            _pageService = pageService;
            _username = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            ValidateUsernameCommand = new Command(() => ValidateUsername());
            ValidatePasswordCommand = new Command(() => ValidatePassword());
            LoginCommand = new Command(OnLogin);
            BusinessTapCommand = new Command(OnBusinessTap);
            OrgTapCommand = new Command(OnOrgTap);
            AddValidations();
        }

        public ValidatableObject<string> Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);

            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private bool ValidateUsername()
        {
            return _username.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUsername();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private void AddValidations()
        {
            _username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private async void OnLogin()
        {
            if (Validate())
            {
                User testuser = await CosmosDBService.getAsync("1");

                await _pageService.DisplayAlert("Login", testuser.Username, "OK", "Cancel");
            }
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
