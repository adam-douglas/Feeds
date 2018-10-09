using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds
{
    class DonationFormViewModel : ExtendedBindableObject
    {
        public ICommand AddFoodItemCommand { get; set; }
        private readonly IPageService _pageService;

        public DonationFormViewModel(IPageService pageService)
        {
            _pageService = pageService;
            AddFoodItemCommand = new Command(AddFoodItem);
        }

        private void AddFoodItem()
        {
            
        }
    }
}
