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
	public partial class OrgRegistrationView : ContentPage
	{
        public OrgRegistrationModel orgRegistrationModel;

		public OrgRegistrationView ()
		{
			InitializeComponent ();
            orgRegistrationModel = new OrgRegistrationModel();

            MessagingCenter.Subscribe<OrgRegistrationModel, string>(this, "OrgSubmitAlert", (sender, username) =>
            {
                DisplayAlert("Title", username, "Okay");
            });

            this.BindingContext = orgRegistrationModel;

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                orgNameEntry.Focus();
            };

            orgNameEntry.Completed += (object sender, EventArgs e) =>
            {
                addressEntry.Focus();
            };

            addressEntry.Completed += (object sender, EventArgs e) =>
            {
                emailEntry.Focus();
            };

            emailEntry.Completed += (object sender, EventArgs e) =>
            {
                email2Entry.Focus();
            };

            email2Entry.Completed += (object sender, EventArgs e) =>
            {
                phoneEntry.Focus();
            };

            phoneEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };

            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                password2Entry.Focus();
            };

            password2Entry.Completed += (object sender, EventArgs e) =>
            {
                orgRegistrationModel.SubmitCommand.Execute(null);
            };
        }
	}
}