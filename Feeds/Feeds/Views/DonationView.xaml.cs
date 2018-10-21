using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonationView : ContentPage
	{
        private DonationViewModel donationViewModel;

		public DonationView(Donation selectedDonation)
		{
			InitializeComponent();
            donationViewModel = new DonationViewModel(new PageService(), selectedDonation);
            this.BindingContext = donationViewModel;
		}
	}
}