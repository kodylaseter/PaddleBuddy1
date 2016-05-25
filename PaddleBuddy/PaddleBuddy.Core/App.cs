using MvvmCross.Platform.IoC;

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

            MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }
    }
}