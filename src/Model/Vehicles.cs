using System.Collections.Generic;
using Newtonsoft.Json;

namespace src.Model
{
    public class Vehicles
    {
        [JsonProperty("vehicleIds")]
        public List<int> VehicleIds {get; set;} = new List<int>();
    }
}