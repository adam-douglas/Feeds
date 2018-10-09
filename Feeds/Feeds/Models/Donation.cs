using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feeds.Models
{
    public class Donation : ExtendedBindableObject
    {
        string _createdBy;
        [JsonProperty("createdBy")]
        public string CreatedBy
        {
            get => _createdBy;
            set
            {
                if (_createdBy == value)
                    return;

                _createdBy = value;

                RaisePropertyChanged(() => CreatedBy);
            }
        }

        DateTime _createdAt;
        [JsonProperty("createdBy")]
        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                if (_createdAt == value)
                    return;

                _createdAt = value;

                RaisePropertyChanged(() => CreatedAt);
            }
        }

        DateTime _pickupDate;
        [JsonProperty("pickupDate")]
        public DateTime PickupDate
        {
            get => _pickupDate;
            set
            {
                if (_pickupDate == value)
                    return;

                _pickupDate = value;

                RaisePropertyChanged(() => PickupDate);
            }
        }

        string _pickupFrom;
        [JsonProperty("pickupFrom")]
        public string PickupFrom
        {
            get => _pickupFrom;
            set
            {
                if (_pickupFrom == value)
                    return;

                _pickupFrom = value;

                RaisePropertyChanged(() => PickupFrom);
            }
        }

        string _pickupTo;
        [JsonProperty("pickupFrom")]
        public string PickupTo
        {
            get => _pickupTo;
            set
            {
                if (_pickupTo == value)
                    return;

                _pickupTo = value;

                RaisePropertyChanged(() => PickupTo);
            }
        }
    }
}
