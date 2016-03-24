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

            bool isLoggedIn = true;
            if (!isLoggedIn) RegisterAppStart<ViewModels.IntroViewModel>();
            else RegisterAppStart<ViewModels.HomeViewModel>();
           
            MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }
    }
}