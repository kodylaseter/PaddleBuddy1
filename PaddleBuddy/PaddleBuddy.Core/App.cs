using MvvmCross.Platform.IoC;
using PaddleBuddy.Core.Services;
using PaddleBuddy.Core.ViewModels;
using System.Threading.Tasks;

namespace PaddleBuddy.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //TODO enable login
            bool isLoggedIn = true;
            //isLoggedIn = false;
            if (!isLoggedIn) RegisterAppStart<IntroViewModel>();
            else RegisterAppStart<MapViewModel>();

            Task.Run(() => SetupData());
            Task.Run(() => SetupLocation());
            MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }

        private async void SetupData()
        {
            await DatabaseService.GetInstance().Setup();
        }

        private async void SetupLocation()
        {
            LocationService.GetInstance().StartListening();
            await LocationService.GetInstance().GetLocationAsync();
        }
    }
}