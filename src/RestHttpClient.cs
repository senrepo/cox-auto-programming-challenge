using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace src
{
    public class RestHttpClient
    {
        public static HttpClient httpClient = null;

        public async Task<T> Get<T>(string url)
        {
            try
            {
                var client = GetHttpClient();
                var response = await httpClient.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private HttpClient GetHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }

            return httpClient;
        }
    }
}