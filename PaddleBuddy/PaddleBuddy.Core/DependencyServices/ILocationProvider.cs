using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.DependencyServices
{
    public interface ILocationProvider
    {
        Point GetCurrentLocation();
    }
}
