using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds.Models
{
    public class BusinessRegistrationModel
    {
        public string Username { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public ICommand SubmitCommand { get; set; }

        public BusinessRegistrationModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public void OnSubmit()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "BusinessSubmitAlert", Username);
            }
        }
    }
}
