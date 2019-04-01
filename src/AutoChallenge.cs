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
            var vehicles = await GetVehicles(dataset);
            var dealers = await GetDealers(dataset, vehicles);
            var status = await SubmitAutoChallenge(dataset, dealers);
            return status;
        }

        private async Task<DataSet> GetDataSet()
        {
            var dataset = await client.Get<DataSet>(Configuration.GetDataSetUrl());
            return dataset;
        }

        private async Task<List<Vehicle>> GetVehicles(DataSet dataset)
        {
            var vehicleList = new List<Vehicle>();
            var taskList = new List<Task<Vehicle>>();
            var vehicles = await client.Get<Vehicles>(Configuration.GetVehiclesUrl(dataset.DataSetId));

            foreach (var id in vehicles.VehicleIds)
            {
                Task<Vehicle> task = Task.Factory.StartNew((vehId) =>
                {
                    var vehicle = client.Get<Vehicle>(Configuration.GetVehicleUrl(dataset.DataSetId, id));
                    return vehicle.Result;
                }, id);

                taskList.Add(task);
            }

            while (taskList.Count > 0)
            {
                int i = Task.WaitAny(taskList.ToArray());
                try
                {
                    var vehicle = taskList[i].Result;
                    vehicleList.Add(vehicle);
                }
                catch (AggregateException ae)
                {
                    ae = ae.Flatten();
                    Console.WriteLine(ae.ToString());
                }
                taskList.RemoveAt(i);
            }

            return vehicleList;
        }

        private async Task<List<Dealer>> GetDealers(DataSet dataset, List<Vehicle> vehicles)
        {
            var dealers = new Dictionary<int, Dealer>();
            var dealerList = new List<Dealer>();

            foreach (var vehicle in vehicles)
            {
                if (dealers.ContainsKey(vehicle.DealerId))
                {
                    dealers[vehicle.DealerId].Vehicles.Add(vehicle);
                }
                else
                {
                    var dealer = client.Get<Dealer>(Configuration.GetDealerUrl(dataset.DataSetId, vehicle.DealerId)).Result;
                    dealer.Vehicles.Add(vehicle);
                    dealers.TryAdd(dealer.DealerId, dealer);
                }
            }

            foreach (var item in dealers.Values) dealerList.Add(item);
            return dealerList;
        }

           private async Task<Status> SubmitAutoChallenge(DataSet dataset, List<Dealer> dealers)
           {
               var status = await client.Post<RequestPayLoad,Status>(Configuration.PostAutoChallenge(dataset.DataSetId), new RequestPayLoad(dealers));
               return status;
           }
    }
}