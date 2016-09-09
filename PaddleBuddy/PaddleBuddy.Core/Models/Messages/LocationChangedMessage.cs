using MvvmCross.Plugins.Messenger;

namespace PaddleBuddy.Core.Models.Messages
{
    public class LocationChangedMessage : MvxMessage
    {
        public LocationChangedMessage(object sender) : base(sender)
        {
        }
    }
}
