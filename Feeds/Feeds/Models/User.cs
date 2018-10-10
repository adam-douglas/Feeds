using Newtonsoft.Json;

namespace Feeds
{
    public class User : ExtendedBindableObject
    {
        string _id;
        [JsonProperty("id")]
        public string Id
        {
            get => _id;
            set
            {
                if (_id == value)
                    return;

                _id = value;

                RaisePropertyChanged(() => Id);
            }
        }

        string _username;
        [JsonProperty("username")]
        public string Username
        {
            get => _username;
            set
            {
                if (_username == value)
                    return;

                _username = value;

                RaisePropertyChanged(() => Username);
            }
        }

        string _name;
        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;

                RaisePropertyChanged(() => Name);
            }
        }

        Address _address;
        [JsonProperty("address")]
        public Address Address
        {
            get => _address;
            set
            {
                if (_address == value)
                    return;

                _address = value;

                RaisePropertyChanged(() => Address);
            }
        }

        string _email;
        [JsonProperty("email")]
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;

                _email = value;

                RaisePropertyChanged(() => Email);
            }
        }

        string _phoneNo;
        [JsonProperty("phoneNo")]
        public string PhoneNo
        {
            get => _phoneNo;
            set
            {
                if (_phoneNo == value)
                    return;

                _phoneNo = value;

                RaisePropertyChanged(() => PhoneNo);
            }
        }

        string _password;
        [JsonProperty("password")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;

                _password = value;

                RaisePropertyChanged(() => Password);
            }
        }

        string _type;
        [JsonProperty("type")]
        public string Type
        {
            get => _type;
            set
            {
                if (_type == value)
                    return;

                _type = value;

                RaisePropertyChanged(() => Type);
            }
        }
    }
}
