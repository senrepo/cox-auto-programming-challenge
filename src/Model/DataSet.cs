using Newtonsoft.Json;

namespace src.Model
{
    public class DataSet
    {
        [JsonProperty("datasetId")]
        public string DataSetId {get; set;}
    }
}