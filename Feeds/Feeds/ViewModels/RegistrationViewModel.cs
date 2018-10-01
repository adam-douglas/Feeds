using Feeds.Models;
using Feeds.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds.ViewModels
{
    class RegistrationViewModel : ExtendedBindableObject
    {
        private ValidatableObject<string> _username;
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _address;
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _phoneNo;
        private ValidatableObject<string> _password;
        public ICommand SubmitCommand { get; set; }
        private readonly IPageService _pageService;

        public RegistrationViewModel(IPageService pageService)
        {
            _pageService = pageService;
            _username = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();
            _address = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();
            _phoneNo = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            SubmitCommand = new Command(OnSubmit);
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

        public ValidatableObject<string> Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public ValidatableObject<string> Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public ValidatableObject<string> PhoneNo
        {
            get
            {
                return _phoneNo;
            }
            set
            {
                _phoneNo = value;
                RaisePropertyChanged(() => PhoneNo);
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

        private bool Validate()
        {
            bool isValidUser = _username.Validate();
            bool isValidName = _name.Validate();
            bool isValidAddress = _address.Validate();
            bool isValidEmail = _email.Validate();
            bool isValidPhoneNo = _phoneNo.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser
                && isValidName
                && isValidAddress
                && isValidEmail
                && isValidPhoneNo
                && isValidPassword;
        }

        private void AddValidations()
        {
            _username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });
            _address.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An address is required." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email." });
            _phoneNo.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A phone number is required." });
            _password.Validations.Add(new PasswordRule<string> { ValidationMessage = "should contain at least 8 characters, 1 numeric, 1 lowercase, 1 uppercase, 1 special character." });
        }

        private async void OnSubmit()
        {
            if (Validate())
            {
                await _pageService.DisplayAlert("Registered", "Successfully registered.", "OK", "Cancel");
            }
        }
    }
}
