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
            foreach (FoodItem food in selectedDonation.FoodItems)
            {
                StackLayout newFoodLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
                Label descLabel = new Label
                {
                    FontSize = 8,
                    Text = food.Description
                };
                Label quantityLabel = new Label
                {
                    FontSize = 8,
                    Text = food.Quantity
                };
                Label measurementLabel = new Label
                {
                    FontSize = 8,
                    Text = food.Measurement
                };
                newFoodLayout.Children.Add(descLabel);
                newFoodLayout.Children.Add(quantityLabel);
                newFoodLayout.Children.Add(measurementLabel);
                foodLayout.Children.Add(newFoodLayout);
            }
            Button donationButton = new Button
            {
                CornerRadius = 10,
                BorderWidth = 1,
                WidthRequest = 100,
                HeightRequest = 35,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 8,
                BindingContext = donationViewModel
            };
            if (type == "org_available")
            {
                donationButton.Text = "Accept";
                donationButton.BorderColor = Color.FromHex("6EB275");
                donationButton.BackgroundColor = Color.FromHex("6EB275");
                donationButton.Command = donationViewModel.AcceptCommand;
                donationLayout.Children.Add(donationButton);
            }
            if (type == "org_accepted")
            {
                donationButton.Text = "Cancel";
                donationButton.BorderColor = Color.FromHex("835AA6");
                donationButton.BackgroundColor = Color.FromHex("835AA6");
                donationButton.Command = donationViewModel.CancelCommand;
                donationLayout.Children.Add(donationButton);
            }
            if (type == "business")
            {
                if (selectedDonation.AcceptedBy == null)
                {
                    donationButton.Text = "Delete";
                    donationButton.BorderColor = Color.FromHex("835AA6");
                    donationButton.BackgroundColor = Color.FromHex("835AA6");
                    donationButton.Command = donationViewModel.DeleteCommand;
                    donationLayout.Children.Add(donationButton);
                }
                else
                {
                    donationButton.BorderColor = Color.FromHex("6EB275");
                    donationButton.BackgroundColor = Color.FromHex("6EB275");
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