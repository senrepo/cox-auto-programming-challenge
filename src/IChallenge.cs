using System.Threading.Tasks;
using src.Model;

namespace src
{
    public interface IChallenge
    {
        Task<Status> Execute();
    }
}