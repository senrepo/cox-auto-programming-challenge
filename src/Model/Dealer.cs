using System.Collections.Generic;
using Newtonsoft.Json;

namespace src.Model
{
    [JsonObject("dealer")]
    public class Dealer
    {
        [JsonProperty("dealerId")]
        public int DealerId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("vehicles")]
        public List<Vehicle> Vehicles {get; set;} = new List<Vehicle>();
    }
}