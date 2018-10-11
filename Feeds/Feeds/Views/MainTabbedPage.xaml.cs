using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public MainTabbedPage()
        {
            InitializeComponent();
            if (AppSettings.GetValueOrDefault("Type", "Unknown") == "Business")
            {
                var donationFormNavPage = new DonationFormView();
                donationFormNavPage.Title = "Post Donation";
                Children.Add(donationFormNavPage);

                var homePage = new ContentPage();
                homePage.Title = "Home";
                Children.Add(homePage);
            }
            else if(AppSettings.GetValueOrDefault("Type", "Unknown") == "Organisation")
            {
                var donationListNavPage = new DonationListView();
                donationListNavPage.Title = "Donations";
                Children.Add(donationListNavPage);

                var homePage = new ContentPage();
                homePage.Title = "Home";
                Children.Add(homePage);
            }
            else
            {
                Children.Add(new LoginView());
            }
        }
    }
}