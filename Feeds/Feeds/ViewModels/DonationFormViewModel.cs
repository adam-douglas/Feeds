using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class DonationFormViewModel : ExtendedBindableObject
    {
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
                FoodItems = new List<FoodItem>()
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
            await CosmosDBService.CreateDonation(NewDonation);
            await _pageService.DisplayAlert("Test", NewDonation.PickupFrom.ToString("c"), "OK", "Cancel");
        }
    }
}
