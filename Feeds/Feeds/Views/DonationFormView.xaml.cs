using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonationFormView : ContentPage
	{
        private DonationFormViewModel donationFormViewModel;
        private int foodItemCount = 0;

        public DonationFormView()
        {
            InitializeComponent();
            donationFormViewModel = new DonationFormViewModel(new PageService());
            this.BindingContext = donationFormViewModel;

            addFoodButton.Clicked += (object sender, System.EventArgs e) =>
            {
                foodItemCount++;
                Entry descriptionEntry = new Entry
                {
                    FontSize = 8,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 200,
                    Placeholder = "Food Description"
                };
                Entry quantityEntry = new Entry
                {
                    FontSize = 8,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 200,
                    Placeholder = "Quantity"
                };
                Entry measurementEntry = new Entry
                {
                    FontSize = 8,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 200,
                    Placeholder = "Measurement (eg grams, cans, crates)"
                };
                BoxView seperator = new BoxView
                {
                    Color = Color.Gray,
                    HeightRequest = 1,
                    WidthRequest = 100,
                    HorizontalOptions = LayoutOptions.Center,
                    Opacity = 0.5
                };
                foodLayout.Children.Add(descriptionEntry);
                foodLayout.Children.Add(quantityEntry);
                foodLayout.Children.Add(measurementEntry);
                foodLayout.Children.Add(seperator);
            };
        }
    }
}