using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace Feeds
{ 
    class DonationListViewModel : ExtendedBindableObject
    {
        private List<Donation> _donationList;
        private string _type;
        private bool _isRefreshing = false;
        public ICommand RefreshCommand { get; }
        public readonly IPageService _pageService;
        private static ISettings AppSettings => CrossSettings.Current;

        public DonationListViewModel(IPageService pageService, string type)
        {
            _type = type;
            _pageService = pageService;
            _donationList = new List<Donation>();
            RefreshCommand = new Command(async () => await GetDonations());
        }

        public List<Donation> DonationList
        {
            get
            {
                return _donationList;
            }
            set
            {
                _donationList = value;
                RaisePropertyChanged(() => DonationList);
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        async Task GetDonations()
        {
            IsRefreshing = true;
            if (_type == "org_available")
            {
                DonationList = await CosmosDBService.GetDonationsAsync(AppSettings.GetValueOrDefault("City", ""));
            }
            if (_type == "org_accepted")
            {
                DonationList = await CosmosDBService.GetAcceptedDonations(AppSettings.GetValueOrDefault("UserId", ""));
            }
            if (_type == "business")
            {
                DonationList = await CosmosDBService.GetUsersDonations(AppSettings.GetValueOrDefault("UserId", ""));
            }
            IsRefreshing = false;
        }

    }
}
