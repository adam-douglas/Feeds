using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds.Models
{
    public class OrgRegistrationModel
    {
        public string Username { get; set; }
        public string OrgName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public ICommand SubmitCommand { get; set; }

        public OrgRegistrationModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public void OnSubmit()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "OrgSubmitAlert", Username);
            }
        }
    }
}
