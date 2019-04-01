using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Model;

namespace src
{
    public class AutoChallenge
    {
        private readonly RestHttpClient client = null;

        public AutoChallenge()
        {
            client = new RestHttpClient();
        }

        public async Task<Status> Execute()
        {
            var dataset = await GetDataSet();
            var dealers = await GetAllDealersWithVehicles(dataset);
            var status = await SubmitAutoChallenge(dataset, dealers);
            return status;
        }

        private async Task<DataSet> GetDataSet()
        {
            var dataset = await client.Get<DataSet>(Configuration.GetDataSetUrl());
            return dataset;
        }

        private async Task<List<Dealer>> GetAllDealersWithVehicles(DataSet dataset)
        {
            var dealers = new ConcurrentDictionary<int, Dealer>();
            var dealerList = new List<Dealer>();

            var taskList = new List<Task>();
            var vehicles = await client.Get<Vehicles>(Configuration.GetVehiclesUrl(dataset.DataSetId));

            //iterate the vehicle ids
            foreach (var vehId in vehicles.VehicleIds)
            {
                Task task = Task.Factory.StartNew((id) =>
                {
                    //get the vehicle with vehid
                    var vehicle = client.Get<Vehicle>(Configuration.GetVehicleUrl(dataset.DataSetId, (int)id)).Result;

                    // //check if the dealer exists
                    // if (!dealers.TryGetValue(vehicle.DealerId, out Dealer dealer))
                    // {
                    //       dealer = client.Get<Dealer>(Configuration.GetDealerUrl(dataset.DataSetId, vehicle.DealerId)).Result;  
                    // }

                    var dealer = client.Get<Dealer>(Configuration.GetDealerUrl(dataset.DataSetId, vehicle.DealerId)).Result; 
                    //update the vehicle to the dealer
                    dealer.Vehicles.Add(vehicle);

                    //add the dealer back to the dealers collection
                    dealers.AddOrUpdate(dealer.DealerId, dealer, (key, existing) =>
                    {
                        existing.Vehicles.Add(vehicle);
                        return existing;
                    });

                }, vehId);

                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());

            foreach (var item in dealers.Values) dealerList.Add(item);
            return dealerList;

        }

        private async Task<Status> SubmitAutoChallenge(DataSet dataset, List<Dealer> dealers)
        {
            var status = await client.Post<RequestPayLoad, Status>(Configuration.PostAutoChallenge(dataset.DataSetId), new RequestPayLoad(dealers));
            return status;
        }
    }
}