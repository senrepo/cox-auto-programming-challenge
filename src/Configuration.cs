namespace src
{
    public class Configuration
    {
        public static string GetDataSetUrl()
        {
            return "http://vautointerview.azurewebsites.net/api/datasetId";
        }
        public static string GetVehiclesUrl(string datasetId)
        {
            return $"http://vautointerview.azurewebsites.net/api/{datasetId}/vehicles";
        }
        public static string GetVehicleUrl(string datasetId, int vehicleId)
        {
            return $"http://vautointerview.azurewebsites.net/api/{datasetId}/vehicles/{vehicleId}";
        }
        public static string GetDealerUrl(string datasetId, int dealerId)
        {
            return $"http://vautointerview.azurewebsites.net/api/{datasetId}/dealers/{dealerId}";
        }
        public static string PostAutoChallenge(string datasetId)
        {
            return $"http://vautointerview.azurewebsites.net/api/{datasetId}/answer";
        }        
    }
}