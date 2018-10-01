using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Feeds.ViewModels;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusinessRegistrationView : ContentPage
	{
        private RegistrationViewModel registrationViewModel;

        public BusinessRegistrationView ()
		{
			InitializeComponent ();
            registrationViewModel = new RegistrationViewModel(new PageService());
            this.BindingContext = registrationViewModel;

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                businessNameEntry.Focus();
            };

            businessNameEntry.Completed += (object sender, EventArgs e) =>
            {
                addressEntry.Focus();
            };

            addressEntry.Completed += (object sender, EventArgs e) =>
            {
                emailEntry.Focus();
            };

            emailEntry.Completed += (object sender, EventArgs e) =>
            {
                phoneEntry.Focus();
            };

            phoneEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };

            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                registrationViewModel.SubmitCommand.Execute(null);
            };
        }
	}
}