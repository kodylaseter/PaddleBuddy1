using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.DependencyServices
{
    public interface ILocationProvider
    {
        Task<Point> GetCurrentLocation();
    }
}
