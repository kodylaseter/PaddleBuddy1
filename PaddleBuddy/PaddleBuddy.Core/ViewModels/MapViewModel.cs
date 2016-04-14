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
            SetCamera();
        }

        public async void SetCamera()
        {
            MapDrawer.MoveCameraZoom(LocationService.GetInstance().GetCurrentLocation(), 11);
            var river = await MapService.GetInstance().GetClosestRiver();
            MapDrawer.DrawLine(river.Points.ToArray());
        }
    }
}
