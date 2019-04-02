using System.Threading.Tasks;

namespace src.Client
{
    public interface IRestHttpClient
    {
        Task<T> Get<T>(string url);
        Task<T2> Post<T1, T2>(string url, T1 request);
    }
}