using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Feeds.ViewModels;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        private LoginViewModel loginViewModel;

        public LoginView()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel(new PageService());
            this.BindingContext = loginViewModel;

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };

            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                loginViewModel.LoginCommand.Execute(null);
            };
        }
    }
}