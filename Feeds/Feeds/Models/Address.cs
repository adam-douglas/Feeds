using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feeds
{
    public class Address
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }
    }
}
