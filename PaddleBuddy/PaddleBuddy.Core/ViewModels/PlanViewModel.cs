using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.Services;
using System.Linq;

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
            
            //if (!string.IsNullOrWhiteSpace(EndId) && !string.IsNullOrWhiteSpace(StartId))
            //{
            //    var resp = await PlanService.GetInstance().EstimateTime(int.Parse(_startId), int.Parse(_endId), 17);
            //    if (resp.Success)
            //    {
            //        Trip = (TripEstimate) resp.Data;
            //    }
            //    else
            //    {
            //        MessengerService.Toast(this, "Error: " + resp.Detail, true);
            //    }
            //}
            int startId = 48;//int.Parse(_startId);
            int endId = 52;//int.Parse(_endId);
            int riverId = 17;
            //from river in DatabaseService.GetInstance().Rivers where river.Id == id select river
            var beginList = (from point in DatabaseService.GetInstance().Points where point.RiverId == riverId select new { begin_id = point.Id, begin_lat = point.Lat, begin_lng = point.Lng }).ToList() ;
            var endList = (from point in DatabaseService.GetInstance().Points where point.RiverId == riverId select new { end_id = point.Id, end_lat = point.Lat, end_lng = point.Lng }).ToList();
            var result = (from link in DatabaseService.GetInstance().Links join p1 in beginList on link.Begin equals p1.begin_id join p2 in endList on link.End equals p2.end_id select new { link.Id, link.Begin, link.End, link.Speed, link.River, p1.begin_id, p1.begin_lat, p1.begin_lng, p2.end_id, p2.end_lat, p2.end_lng}).ToList();

            var first = (from temp in result where temp.Begin == startId select temp).First();
            IsLoading = false;
            RaisePropertyChanged(() => CanStart);
        }


        public ICommand StartCommand
        {
            get { return new MvxCommand(StartTrip); }
        }
    }
}
