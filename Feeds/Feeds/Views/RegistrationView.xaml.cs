using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationView : ContentPage
	{
        private RegistrationViewModel registrationViewModel;

        public RegistrationView ()
		{
			InitializeComponent ();
            registrationViewModel = new RegistrationViewModel(new PageService());
            this.BindingContext = registrationViewModel;

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                nameEntry.Focus();
            };

            nameEntry.Completed += (object sender, EventArgs e) =>
            {
                streetEntry.Focus();
            };

            streetEntry.Completed += (object sender, EventArgs e) =>
            {
                cityEntry.Focus();
            };

            cityEntry.Completed += (object sender, EventArgs e) =>
            {
                postcodeEntry.Focus();
            };

            postcodeEntry.Completed += (object sender, EventArgs e) =>
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