using System;
using Newtonsoft.Json;
using src;
using src.Model;
using Xunit;

namespace auto_challenge_tests
{
    public class RestHttpClientTests
    {
         RestHttpClient client = new RestHttpClient();

        [Fact]
        public async void Test_DataSet_Get()
        {
            var url = "http://vautointerview.azurewebsites.net/api/datasetId";
            var dataset = await client.Get<DataSet>(url);
            Assert.NotNull(dataset);
            Assert.False(string.IsNullOrEmpty(dataset.DataSetId));
        }

        [Fact]
        public async void Test_Vehicles_Get()
        {
            var dataSetUrl = "http://vautointerview.azurewebsites.net/api/datasetId";
            var vehiclesUrl = "http://vautointerview.azurewebsites.net/api/{0}/vehicles";
            var dataset = await client.Get<DataSet>(dataSetUrl);

            vehiclesUrl = string.Format(vehiclesUrl, dataset.DataSetId);
            var vehicles = await client.Get<Vehicles>(vehiclesUrl);

            Assert.NotNull(vehicles);
            Assert.True(vehicles.VehicleIds.Count > 0);
        }

        [Fact]
        public async void Test_Vehicle_Get()
        {
            var dataSetUrl = "http://vautointerview.azurewebsites.net/api/datasetId";
            var vehiclesUrl = "http://vautointerview.azurewebsites.net/api/{0}/vehicles";
            var vehicleUrl = "http://vautointerview.azurewebsites.net/api/{0}/vehicles/{1}";
            
            var dataset = await client.Get<DataSet>(dataSetUrl);

            vehiclesUrl = string.Format(vehiclesUrl, dataset.DataSetId);
            var vehicles = await client.Get<Vehicles>(vehiclesUrl);

            vehicleUrl = string.Format(vehicleUrl,  dataset.DataSetId, vehicles.VehicleIds[0]);
            var vehicle = await client.Get<Vehicle>(vehicleUrl);

            Assert.NotNull(vehicle);
        }

        [Fact]
        public async void Test_Dealer_Get()
        {
            var dataSetUrl = "http://vautointerview.azurewebsites.net/api/datasetId";
            var vehiclesUrl = "http://vautointerview.azurewebsites.net/api/{0}/vehicles";
            var vehicleUrl = "http://vautointerview.azurewebsites.net/api/{0}/vehicles/{1}";
            var dealerUrl = "http://vautointerview.azurewebsites.net/api/{0}/dealers/{1}";
            
            var dataset = await client.Get<DataSet>(dataSetUrl);

            vehiclesUrl = string.Format(vehiclesUrl, dataset.DataSetId);
            var vehicles = await client.Get<Vehicles>(vehiclesUrl);

            vehicleUrl = string.Format(vehicleUrl,  dataset.DataSetId, vehicles.VehicleIds[0]);
            var vehicle = await client.Get<Vehicle>(vehicleUrl);

            dealerUrl = string.Format(dealerUrl, dataset.DataSetId, vehicle.DealerId);
            var dealer = await client.Get<Dealer>(dealerUrl);            

            Assert.NotNull(dealer);
        }    

        [Fact]
        public async void Test_RequestPayLoad_Post()
        {
            var url = "http://vautointerview.azurewebsites.net/api/{0}/answer";
            var dataSetUrl = "http://vautointerview.azurewebsites.net/api/datasetId";
            var payloadString = "{'dealers':[{'dealerId':0,'name':'string','vehicles':[{'vehicleId':0,'year':0,'make':'string','model':'string'}]}]}";
            
            var dataset = await client.Get<DataSet>(dataSetUrl);
            url = string.Format(url, dataset.DataSetId);

            var request = JsonConvert.DeserializeObject<RequestPayLoad>(payloadString);
            var response = await client.Post<RequestPayLoad, Status>(url, request);

            Assert.NotNull(response as Status);
        }    
    }
}
