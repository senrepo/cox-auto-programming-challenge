using System.Collections.Generic;
using Newtonsoft.Json;

namespace src.Model
{
    public class RequestPayLoad
    {
        [JsonProperty("dealers")]
        public List<Dealer> Dealers { get; set; } = new List<Dealer>();

        public RequestPayLoad(List<Dealer> dealers)
        {
            this.Dealers = dealers;
        }
    }
}