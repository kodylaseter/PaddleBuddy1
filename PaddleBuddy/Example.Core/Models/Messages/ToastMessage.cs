using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace PaddleBuddy.Models.Messages
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
