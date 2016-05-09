using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class PlanViewModel : BaseViewModel
    {
        private int _startID;
        private int _endID;

        public int StartID
        {
            get { return _startID; }
            set { _startID = value; }
        }

        public int EndID
        {
            get { return _endID; }
            set { _endID = value; }
        }

        public bool IsLoading { get; set; }

        public async void Estimate()
        {
            IsLoading = true;
            var resp = await PlanService.GetInstance().EstimateTime(_startID, _endID, 17);
            if (resp.Success)
            {
                Messenger.Publish(new ToastMessage(this, "Estimated time: " + ((TimeEstimate)resp.Data).Time, true));
            }
        }


        public ICommand EstimateCommand
        {
            get { return new MvxCommand(Estimate); }
        }
    }
}
