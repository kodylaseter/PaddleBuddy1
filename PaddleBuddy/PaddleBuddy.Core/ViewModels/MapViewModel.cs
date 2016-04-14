using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public IMapDrawer MapDrawer { get; set; }

        public void Setup()
        {
            MapDrawer = Mvx.Resolve<IMapDrawer>();
            MapDrawer.MoveCameraZoom(LocationService.GetInstance().GetCurrentLocation(), 9);
            SetCamera();
        }

        public async void SetCamera()
        {
            await MapService.GetInstance().GetClosestRiver();
        }
    }
}
