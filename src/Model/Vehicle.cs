using Newtonsoft.Json;

namespace src.Model
{
    [JsonObject("vehicle")]
    public class Vehicle
    {
        [JsonProperty("vehicleId")]
        public int VehicleId { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("make")]
        public string Make { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("dealerId")]
        public string DealerId { get; set; }
    }
}