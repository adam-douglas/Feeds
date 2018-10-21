using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class DonationViewModel : ExtendedBindableObject
    {
        private readonly Donation _selectedDonation;
        public ICommand AcceptCommand { get; set; }
        private readonly IPageService _pageService;

        public DonationViewModel(IPageService pageService, Donation selectedDonation)
        {
            _pageService = pageService;
            _selectedDonation = selectedDonation;
            AcceptCommand = new Command(OnAccept);
        }

        public Donation SelectedDonation
        {
            get
            {
                return _selectedDonation;
            }
        }

        private void OnAccept()
        {

        }
    }
}
