using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feeds
{
    public class Donation : ExtendedBindableObject
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
        [JsonProperty("createdAt")]
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

        TimeSpan _pickupFrom;
        [JsonProperty("pickupFrom")]
        public TimeSpan PickupFrom
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

        TimeSpan _pickupTo;
        [JsonProperty("pickupTo")]
        public TimeSpan PickupTo
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

        List<FoodItem> _foodItems;
        [JsonProperty("foodItems")]
        public List<FoodItem> FoodItems
        {
            get => _foodItems;
            set
            {
                if (_foodItems == value)
                    return;

                _foodItems = value;

                RaisePropertyChanged(() => FoodItems);
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
    }
}
