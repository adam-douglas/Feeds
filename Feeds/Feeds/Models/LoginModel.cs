using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Feeds.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand BusinessTapCommand { get; set; }
        public ICommand OrgTapCommand { get; set; }

        public LoginModel()
        {
            SubmitCommand = new Command(OnSubmit);
            BusinessTapCommand = new Command(OnBusinessRegister);
            OrgTapCommand = new Command(OnOrgRegister);
        }

        public void OnSubmit()
        {
            if(!string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "LoginAlert", Username);
            }
        }

        public void OnBusinessRegister()
        {
            MessagingCenter.Send(this, "BusinessAlert");
        }

        public void OnOrgRegister()
        {
            MessagingCenter.Send(this, "OrgAlert");
        }
    }
}
