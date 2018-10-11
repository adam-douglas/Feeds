using System.Collections.Generic;
using Xamarin.Forms;
using Feeds.Views;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Feeds
{ 
    class DonationListViewModel : ExtendedBindableObject
    {
        private List<Donation> _donationList;
        public ICommand RefreshCommand { get; }
        public readonly IPageService _pageService;

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
            DonationList = await CosmosDBService.GetDonationsAsync();
        }

    }
}
