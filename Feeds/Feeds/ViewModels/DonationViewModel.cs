using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using Feeds.Views;
using System.Threading.Tasks;

namespace Feeds
{
    class DonationViewModel : ExtendedBindableObject
    {
        private static ISettings AppSettings => CrossSettings.Current;
        private string _type;
        private User _acceptedBy;
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand GetAcceptedByCommand { get; set; }
        public Donation SelectedDonation { get; }
        private readonly IPageService _pageService;

        public DonationViewModel(IPageService pageService, Donation selectedDonation, string type)
        {
            _type = type;
            _pageService = pageService;
            SelectedDonation = selectedDonation;
            AcceptCommand = new Command(OnAcceptAsync);
            CancelCommand = new Command(OnCancelAsync);
            DeleteCommand = new Command(OnDeleteAsync);
            GetAcceptedByCommand = new Command(async () => await GetUserAsync());
            
        }

        public User AcceptedBy
        {
            get
            {
                return _acceptedBy;
            }
            set
            {
                _acceptedBy = value;
                RaisePropertyChanged(() => AcceptedBy);
            }
        }

        async Task GetUserAsync()
        {
            AcceptedBy = await CosmosDBService.GetByIdAsync(SelectedDonation.AcceptedBy);
        }

        private async void OnAcceptAsync()
        {
            SelectedDonation.AcceptedBy = AppSettings.GetValueOrDefault("UserId", "");
            await CosmosDBService.UpdateDonation(SelectedDonation);
            if (await _pageService.DisplayAlert("Donation", "Accepted donation.", "Ok", "Cancel"))
            {
                await _pageService.PushAsync(new MainTabbedPage());
            }
        }

        private async void OnCancelAsync()
        {
            SelectedDonation.AcceptedBy = null;
            await CosmosDBService.UpdateDonation(SelectedDonation);
            if (await _pageService.DisplayAlert("Donation", "Cancelled.", "Ok", "Cancel"))
            {
                await _pageService.PushAsync(new MainTabbedPage());
            }
        }

        private async void OnDeleteAsync()
        {
            await CosmosDBService.DeleteDonation(SelectedDonation.Id);
            if (await _pageService.DisplayAlert("Donation", "Donation deleted.", "Ok", "Cancel"))
            {
                await _pageService.PushAsync(new MainTabbedPage());
            }
        }
    }
}
