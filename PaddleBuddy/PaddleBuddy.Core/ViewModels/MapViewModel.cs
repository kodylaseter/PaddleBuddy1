using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public IMapDrawer MapDrawer { get; set; }

        //public ILocationProvider LocationProvider { get; set; }

        public void Setup()
        {
            MapDrawer = Mvx.Resolve<IMapDrawer>();
            var atl = new Point
            {
                Lat = 33.7490,
                Lng = -84.3880
            };
            //MapDrawer.MoveCameraZoom(LocationService.GetInstance().GetCurrentLocation(), 9);
            MapDrawer.MoveCameraZoom(atl, 9);

        }
    }
}
