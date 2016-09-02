using MvvmCross.Plugins.Messenger;

namespace PaddleBuddy.Core.Models.Messages
{
    public class PermissionMessage : MvxMessage
    {
        public string Permission { get; set; }
        public bool HasPermission { get; set; }

        public PermissionMessage(object sender, string permission, bool hasPermission) : base(sender)
        {
            Permission = permission;
            HasPermission = hasPermission;
        }
    }
}
