using MvvmCross.Plugins.Messenger;

namespace PaddleBuddy.Core.Models.Messages
{
    public class ToastMessage : MvxMessage
    {
        public string Text { get; set; }
        public bool IsShort { get; set; }

        public ToastMessage(object sender, string text, bool isShort) : base(sender)
        {
            Text = text;
            IsShort = isShort;
        }
    }
}
