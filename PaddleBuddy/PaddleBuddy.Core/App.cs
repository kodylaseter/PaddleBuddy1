using MvvmCross.Platform.IoC;
using PaddleBuddy.Core.Services;

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
            bool isLoggedIn = false;
            if (!isLoggedIn) RegisterAppStart<ViewModels.PlanViewModel>();
            else RegisterAppStart<ViewModels.HomeViewModel>();

            if (!DatabaseService.GetInstance().UpdateAll().Result)
            {
                MessengerService.Toast(this, "Unable to fetch data", false);
            }

            MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }
    }
}