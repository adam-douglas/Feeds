using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonationListView : ContentPage
    {
        private DonationListViewModel donationListViewModel;
        private string _type;

        public DonationListView(string type)
        {
            InitializeComponent();
            _type = type;
            donationListViewModel = new DonationListViewModel(new PageService(), _type);
            this.BindingContext = donationListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            donationListViewModel.RefreshCommand.Execute(null);
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DonationView(e.Item as Donation, _type));
        }
    }
}
