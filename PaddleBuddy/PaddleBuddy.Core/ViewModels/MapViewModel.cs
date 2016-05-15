using System;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Messages;
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
            try
            {
                var river = await MapService.GetInstance().GetClosestRiver();
                if (river.Points != null)
                {
                    MapDrawer.DrawLine(river.Points.ToArray());
                }
                else
                {
                    Messenger.Publish(new ToastMessage(this, "Failed to get nearest river", true));
                }
            }
            catch (Exception)
            {
                Messenger.Publish(new ToastMessage(this, "Failed to get nearest fiver", true));
            }
        }
    }
}
