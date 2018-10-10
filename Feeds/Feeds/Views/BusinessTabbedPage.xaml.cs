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
    public partial class BusinessTabbedPage : TabbedPage
    {
        public BusinessTabbedPage ()
        {
            InitializeComponent();
            var donationFormNavPage = new DonationFormView();
            donationFormNavPage.Title = "Post Donation";
            Children.Add(donationFormNavPage);

            var homePage = new ContentPage();
            homePage.Title = "Home";
            Children.Add(homePage);
        }
    }
}