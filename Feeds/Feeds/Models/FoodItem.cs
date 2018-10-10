using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feeds
{
    public class FoodItem : ExtendedBindableObject
    {
        string _description;
        [JsonProperty("description")]
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value)
                    return;

                _description = value;

                RaisePropertyChanged(() => Description);
            }
        }

        string _quantity;
        [JsonProperty("quantity")]
        public string Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value)
                    return;

                _quantity = value;

                RaisePropertyChanged(() => Quantity);
            }
        }

        string _measurement;
        [JsonProperty("measurement")]
        public string Measurement
        {
            get => _measurement;
            set
            {
                if (_measurement == value)
                    return;

                _measurement = value;

                RaisePropertyChanged(() => Measurement);
            }
        }
    }
}
