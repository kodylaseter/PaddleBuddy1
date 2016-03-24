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

            bool isLoggedIn = false;
            //if (!isLoggedIn) RegisterAppStart<ViewModels.LoginViewModel>();
            //else RegisterAppStart<ViewModels.TestViewModel>();
            RegisterAppStart<ViewModels.HomeViewModel>();

            //TODO ensure this
            //Cirrious.MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }
    }
}