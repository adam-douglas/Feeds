using Feeds.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class DonationFormViewModel : ExtendedBindableObject
    {
        private static ISettings AppSettings => CrossSettings.Current;
        private Donation _newDonation;
        private int foodItemCount;
        public ICommand AddFoodItemCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        private readonly IPageService _pageService;

        public DonationFormViewModel(IPageService pageService)
        {
            _pageService = pageService;
            _newDonation = new Donation
            {
                FoodItems = new List<FoodItem>(),
                Address = new Address()
            };
            AddFoodItemCommand = new Command(AddFoodItem);
            SubmitCommand = new Command(OnSubmit);
            foodItemCount = 0;
        }

        public Donation NewDonation
        {
            get
            {
                return _newDonation;
            }
            set
            {
                _newDonation = value;
                RaisePropertyChanged(() => NewDonation);
            }
        }

        private void AddFoodItem()
        {
            NewDonation.FoodItems.Add(new FoodItem());
            FoodItemEntryForm newFoodItemEntryForm = new FoodItemEntryForm(NewDonation.FoodItems, foodItemCount);
            MessagingCenter.Send(this, "AddFoodItemClicked", newFoodItemEntryForm);
            foodItemCount++;
        }

        private async void OnSubmit()
        {
            NewDonation.CreatedAt = DateTime.Now;
            NewDonation.BusinessName = AppSettings.GetValueOrDefault("Name", "");
            NewDonation.CreatedBy = AppSettings.GetValueOrDefault("UserId", "");
            NewDonation.Address.Street = AppSettings.GetValueOrDefault("Street", "");
            NewDonation.Address.City = AppSettings.GetValueOrDefault("City", "");
            NewDonation.Address.Postcode = AppSettings.GetValueOrDefault("Postcode", "");
            await CosmosDBService.CreateDonation(NewDonation);
            if (await _pageService.DisplayAlert("Success", "Donation created.", "OK", "Cancel"))
            {
                await _pageService.PushAsync(new DonationFormView());
            }
        }
    }
}
