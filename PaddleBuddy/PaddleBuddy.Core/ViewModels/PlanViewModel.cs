using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class PlanViewModel : BaseViewModel
    {
        private string _startId;
        private string _endId;
        private TripEstimate _trip;
        private bool _isLoading;

        public TripEstimate Trip
        {
            get { return _trip; }
            set { _trip = value; }
        }


        public PlanViewModel()
        {
            StartId = 48.ToString();
            EndId = 52.ToString();
        }

        public string StartId
        {
            get { return _startId; }
            set
            {
                _startId = value;
                Estimate();
            }
        }

        public string EndId
        {
            get { return _endId; }
            set
            {
                _endId = value;
                Estimate();
            }
        }

        public bool CanStart
        {
            get { return _trip != null; }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value; 
                RaisePropertyChanged(() => IsLoading);
            }
        }


        public void StartTrip()
        {
            if (!CanStart)
            {
                MessengerService.Toast(this, "Invalid trip data", true);
                return;
            }
            ShowViewModel<MapViewModel>(new {initMode = MapInitModes.Plan, start = int.Parse(_startId), end = int.Parse(_endId) });
        }

        public async void Estimate()
        {
            Trip = null;
            IsLoading = true;
            if (!string.IsNullOrWhiteSpace(EndId) && !string.IsNullOrWhiteSpace(StartId))
            {
                var resp = await PlanService.GetInstance().EstimateTime(int.Parse(_startId), int.Parse(_endId), 17);
                if (resp.Success)
                {
                    Trip = (TripEstimate) resp.Data;
                }
                else
                {
                    MessengerService.Toast(this, "Error: " + resp.Detail, true);
                }
            }
            IsLoading = false;
            RaisePropertyChanged(() => CanStart);
        }


        public ICommand StartCommand
        {
            get { return new MvxCommand(StartTrip); }
        }
    }
}
