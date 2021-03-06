﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feeds.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public MainTabbedPage()
        {
            InitializeComponent();
            if (AppSettings.GetValueOrDefault("Type", "Unknown") == "Business")
            {
                var homePage = new DonationListView("business");
                homePage.Title = "Home";
                Children.Add(homePage);

                var donationFormNavPage = new DonationFormView();
                donationFormNavPage.Title = "Post Donation";
                Children.Add(donationFormNavPage);
            }
            else if(AppSettings.GetValueOrDefault("Type", "Unknown") == "Organisation")
            {
                var donationListNavPage = new DonationListView("org_available");
                donationListNavPage.Title = "Feed";
                Children.Add(donationListNavPage);

                var homePage = new DonationListView("org_accepted");
                homePage.Title = "Pickups";
                Children.Add(homePage);
            }
            else
            {
                Children.Add(new LoginView());
            }
            var settingsPage = new SettingsView();
            settingsPage.Title = "Settings";
            Children.Add(settingsPage);

        }
    }
}