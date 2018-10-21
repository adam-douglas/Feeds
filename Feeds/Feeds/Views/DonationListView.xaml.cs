using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonationListView : ContentPage
    {
        private DonationListViewModel donationListViewModel;

        public DonationListView()
        {
            InitializeComponent();
            donationListViewModel = new DonationListViewModel(new PageService());
            this.BindingContext = donationListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            donationListViewModel.RefreshCommand.Execute(null);
        }

        private void ViewList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DonationView(e.Item as Donation));
        }
    }
}
