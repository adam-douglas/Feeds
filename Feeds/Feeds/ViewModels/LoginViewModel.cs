using Feeds.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Threading.Tasks;
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
        //public ICommand AutoLoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        private readonly IPageService _pageService;
        private static ISettings AppSettings => CrossSettings.Current;
        
        public LoginViewModel(IPageService pageService)
        {
            _pageService = pageService;
            _username = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            ValidateUsernameCommand = new Command(() => ValidateUsername());
            ValidatePasswordCommand = new Command(() => ValidatePassword());
            //AutoLoginCommand = new Command(async () => await AutoLoginAsync());
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
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
                await Login(Username.Value, Password.Value);
            }
        }

        private async Task Login(string uname, string pw)
        {
            User loginUser = await CosmosDBService.GetByUsernameAsync(uname);

            if (loginUser == null)
            {
                await _pageService.DisplayAlert("Error", "Username not found", "OK", "Cancel");
            }
            else if (!Hasher.Verify(pw, loginUser.Password))
            {
                await _pageService.DisplayAlert("Error", "Wrong password", "OK", "Cancel");
            }
            else
            {
                UpdateAppSettings(loginUser);
                await _pageService.PushAsync(new MainTabbedPage());
            }
        }

        private void UpdateAppSettings(User loginUser)
        {
            AppSettings.AddOrUpdateValue("UserId", loginUser.Id);
            AppSettings.AddOrUpdateValue("Username", loginUser.Username);
            AppSettings.AddOrUpdateValue("Password", Password.Value);
            AppSettings.AddOrUpdateValue("Name", loginUser.Name);
            AppSettings.AddOrUpdateValue("Street", loginUser.Address.Street);
            AppSettings.AddOrUpdateValue("City", loginUser.Address.City);
            AppSettings.AddOrUpdateValue("Postcode", loginUser.Address.Postcode);
            AppSettings.AddOrUpdateValue("Email", loginUser.Email);
            AppSettings.AddOrUpdateValue("PhoneNo", loginUser.PhoneNo);
            AppSettings.AddOrUpdateValue("Type", loginUser.Type);
        }

        //private async Task AutoLoginAsync()
        //{
        //    string uname = AppSettings.GetValueOrDefault("Username", "failed");
        //    string pw = AppSettings.GetValueOrDefault("Password", "failed");
        //    if (uname != "failed" && pw != "failed")
        //    {
        //        await Login(uname, pw);
        //    }
        //}

        private async void OnRegister()
        {
            await _pageService.PushAsync(new RegistrationView());
        }
    }
}
