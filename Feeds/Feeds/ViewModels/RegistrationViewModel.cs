using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class RegistrationViewModel : ExtendedBindableObject
    {
        private ValidatableObject<string> _username;
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _street;
        private ValidatableObject<string> _city;
        private ValidatableObject<string> _postcode;
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _phoneNo;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _type;
        private User newUser;
        public ICommand SubmitCommand { get; set; }
        private readonly IPageService _pageService;

        public RegistrationViewModel(IPageService pageService)
        {
            _pageService = pageService;
            _username = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();
            _street = new ValidatableObject<string>();
            _city = new ValidatableObject<string>();
            _postcode = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();
            _phoneNo = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _type = new ValidatableObject<string>();
            newUser = new User();
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

        public ValidatableObject<string> Street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
                RaisePropertyChanged(() => Street);
            }
        }

        public ValidatableObject<string> City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
            }
        }

        public ValidatableObject<string> Postcode
        {
            get
            {
                return _postcode;
            }
            set
            {
                _postcode = value;
                RaisePropertyChanged(() => Postcode);
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

        public ValidatableObject<string> Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        private bool Validate()
        {
            bool isValidUser = _username.Validate();
            bool isValidName = _name.Validate();
            bool isValidStreet = _street.Validate();
            bool isValidCity = _city.Validate();
            bool isValidPostcode = _postcode.Validate();
            bool isValidEmail = _email.Validate();
            bool isValidPhoneNo = _phoneNo.Validate();
            bool isValidPassword = _password.Validate();
            bool isValidType = _type.Validate();

            return isValidUser
                && isValidName
                && isValidStreet
                && isValidCity
                && isValidPostcode
                && isValidEmail
                && isValidPhoneNo
                && isValidPassword
                && isValidType;
        }

        private void AddValidations()
        {
            _username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });
            _street.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An address is required." });
            _city.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A city is required." });
            _postcode.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A post code is required." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email." });
            _phoneNo.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A phone number is required." });
            _password.Validations.Add(new PasswordRule<string> { ValidationMessage = "Should contain at least 8 characters, 1 numeric, 1 lowercase, 1 uppercase, 1 special character." });
            _type.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "User type is required." });
        }

        private async void OnSubmit()
        {
            if (Validate())
            {
                newUser.Username = Username.Value;
                newUser.Name = Name.Value;
                newUser.Address = new Address
                {
                    Street = Street.Value,
                    City = City.Value,
                    Postcode =Postcode.Value
                };
                newUser.Email = Email.Value;
                newUser.PhoneNo = PhoneNo.Value;
                newUser.Password = Hasher.Hash(Password.Value);
                newUser.Type = Type.Value;
                await CosmosDBService.CreateUser(newUser);
                await _pageService.DisplayAlert("Registered", "Successfully registered.", "OK", "Cancel");
            }
        }
    }
}
