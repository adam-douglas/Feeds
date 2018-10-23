using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonationView : ContentPage
	{
        private DonationViewModel donationViewModel;
        private Donation _selectedDonation;

        public DonationView(Donation selectedDonation, string type)
        {
            InitializeComponent();
            donationViewModel = new DonationViewModel(new PageService(), selectedDonation, type);
            this.BindingContext = donationViewModel;
            _selectedDonation = selectedDonation;
            Button donationButton = new Button
            {
                CornerRadius = 10,
                BorderWidth = 1,
                BorderColor = Color.Black,
                WidthRequest = 100,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 8,
                BindingContext = donationViewModel
            };
            if (type == "org_available")
            {
                donationButton.Text = "Accept";
                donationButton.Command = donationViewModel.AcceptCommand;
                donationLayout.Children.Add(donationButton);
            }
            if (type == "org_accepted")
            {
                donationButton.Text = "Cancel";
                donationButton.Command = donationViewModel.CancelCommand;
                donationLayout.Children.Add(donationButton);
            }
            if (type == "business")
            {
                if (selectedDonation.AcceptedBy == null)
                {
                    donationButton.Text = "Delete";
                    donationButton.Command = donationViewModel.DeleteCommand;
                    donationLayout.Children.Add(donationButton);
                }
                else
                {
                    donationButton.Text = "Picked Up";
                    donationButton.Command = donationViewModel.DeleteCommand;
                    donationLayout.Children.Add(donationButton);
                }
                
            }            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_selectedDonation.AcceptedBy != null)
            {
                donationViewModel.GetAcceptedByCommand.Execute(null);
            }
        }
    }
}