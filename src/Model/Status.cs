using Newtonsoft.Json;

namespace src.Model
{
    [JsonObject("status")]
    public class Status
    {
        [JsonProperty("success")]
        public string Success { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("totalMilliseconds")]
        public int MilliSeconds { get; set; }
    }
}