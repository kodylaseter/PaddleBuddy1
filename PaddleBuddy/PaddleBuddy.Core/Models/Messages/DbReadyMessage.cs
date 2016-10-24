using MvvmCross.Plugins.Messenger;

namespace PaddleBuddy.Core.Models.Messages
{
    public class DbReadyMessage : MvxMessage
    {
        public DbReadyMessage(object sender) : base(sender)
        {
        }
    }
}
