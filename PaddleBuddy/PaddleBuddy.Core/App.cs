using MvvmCross.Platform.IoC;
using PaddleBuddy.Core.Services;
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
            TaskEx.Run(() => SearchService.GetInstance());
            if (!isLoggedIn) RegisterAppStart<ViewModels.IntroViewModel>();
            else RegisterAppStart<ViewModels.HomeViewModel>();

            MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }
    }
}