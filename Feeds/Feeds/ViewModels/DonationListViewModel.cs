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
        public ICommand RefreshCommand { get; }
        public readonly IPageService _pageService;
        private static ISettings AppSettings => CrossSettings.Current;

        public DonationListViewModel(IPageService pageService)
        {
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

        async Task GetDonations()
        {

            DonationList = await CosmosDBService.GetDonationsAsync(AppSettings.GetValueOrDefault("City",""));
        }

    }
}
