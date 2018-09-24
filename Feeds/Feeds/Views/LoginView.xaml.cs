using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Feeds.Models;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        public LoginModel loginModel;
		public LoginView ()
		{
			InitializeComponent ();
            loginModel = new LoginModel();
            MessagingCenter.Subscribe<LoginModel,string>(this, "LoginAlert",(sender,username) =>
            {
                DisplayAlert("Title", username, "Okay");
            });
            this.BindingContext = loginModel;

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };

            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                loginModel.SubmitCommand.Execute(null);
            };
        }
    }
}