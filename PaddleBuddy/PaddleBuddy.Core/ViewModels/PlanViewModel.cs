using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Services;
using System.Linq;
using System.Collections.Generic;
using PaddleBuddy.Core.Models.Map;

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
            var beginList = (from point in DatabaseService.GetInstance().Points where point.RiverId == riverId select new { BeginId = point.Id, BeginLat = point.Lat, BeginLng = point.Lng }).ToList() ;
            var endList = (from point in DatabaseService.GetInstance().Points where point.RiverId == riverId select new { EndId = point.Id, EndLat = point.Lat, EndLng = point.Lng }).ToList();
            var result = (from link in DatabaseService.GetInstance().Links join p1 in beginList on link.Begin equals p1.BeginId join p2 in endList on link.End equals p2.EndId select new { link.Id, link.Begin, link.End, link.Speed, link.River, p1.BeginLat, p1.BeginLng, p2.EndLat, p2.EndLng}).ToList();

            var temp = (from first in result where first.Begin == startId select first).First();
            if (temp == null)
            {
                MessengerService.Toast(this, "Could not find first point", true);
                return;
            }
            int newId;
            var list = new[] { temp}.ToList();
            list.Remove(temp);
            while (temp != null && temp.End != endId)
            {
                list.Add(temp);
                newId = temp.End;
                result.Remove(temp);
                temp = (from f in result where f.Begin == newId select f).First();
            }
            if (temp == null)
            {
                MessengerService.Toast(this, "Did not reach end point", true);
                return;
            }
            if (temp.End == endId)
            {
                list.Add(temp);
            }
            if (list.ElementAt(0).Begin == startId && list.Last().End == endId)
            {
                var trip = new TripEstimate();
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
