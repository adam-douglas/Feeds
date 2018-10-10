using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonationFormView : ContentPage
	{
        private DonationFormViewModel donationFormViewModel;

        public DonationFormView()
        {
            InitializeComponent();
            donationFormViewModel = new DonationFormViewModel(new PageService());
            this.BindingContext = donationFormViewModel;
            pickupDatePicker.MinimumDate = DateTime.Now;
            MessagingCenter.Subscribe<DonationFormViewModel, FoodItemEntryForm>
                (this, "AddFoodItemClicked", AddFoodItemClicked);
        }

        private void AddFoodItemClicked(DonationFormViewModel sender, FoodItemEntryForm foodItemEntryForm)
        {
            foodLayout.Children.Add(foodItemEntryForm.DescriptionEntry);
            foodLayout.Children.Add(foodItemEntryForm.QuantityEntry);
            foodLayout.Children.Add(foodItemEntryForm.MeasurementEntry);
            foodLayout.Children.Add(foodItemEntryForm.Seperator);
        }
    }
}