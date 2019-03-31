using System;
using src;
using src.Model;
using Xunit;

namespace auto_challenge_tests
{
    public class RestHttpClientTests
    {
        [Fact]
        public async void Test_DataSet_Get()
        {
            var url = "http://vautointerview.azurewebsites.net/api/datasetId";
            var client = new RestHttpClient();
            var dataset = await client.Get<DataSet>(url);
            Assert.NotNull(dataset);
            Assert.False(string.IsNullOrEmpty(dataset.DataSetId));
        }

        [Fact]
        public async void Test_Vehicles_Get()
        {
            var dataSetUrl = "http://vautointerview.azurewebsites.net/api/datasetId";
            var vehiclesUrl = "http://vautointerview.azurewebsites.net/api/{0}/vehicles";
            var client = new RestHttpClient();
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
            
            var client = new RestHttpClient();
            var dataset = await client.Get<DataSet>(dataSetUrl);

            vehiclesUrl = string.Format(vehiclesUrl, dataset.DataSetId);
            var vehicles = await client.Get<Vehicles>(vehiclesUrl);



            Assert.NotNull(vehicles);
            Assert.True(vehicles.VehicleIds.Count > 0);
        }
    }
}
