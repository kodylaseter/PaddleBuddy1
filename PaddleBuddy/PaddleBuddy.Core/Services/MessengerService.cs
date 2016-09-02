using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Core.Services
{
    public class MessengerService
    {
        private static IMvxMessenger _messenger;

        public static IMvxMessenger Messenger
        {
            get { return _messenger ?? (_messenger = Mvx.Resolve<IMvxMessenger>()); }
        }

        public static void Toast(object sender, string message, bool isShort)
        {
            Messenger.Publish(new ToastMessage(sender, message, isShort));
        }

        public static void Permission(object sender, string permission, bool hasPermission)
        {
            Messenger.Publish(new PermissionMessage(sender, permission, hasPermission));
        }
    }
}
