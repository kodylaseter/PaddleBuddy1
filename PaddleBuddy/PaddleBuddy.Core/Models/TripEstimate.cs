using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.Models
{
    public class TripEstimate
    {
        public float Time { get; set; }
        public string TimeUnit { get; set; }
        public float Distance { get; set; }
        public string DistanceUnit { get; set; }

        public int StartId { get; set; }
        public int EndId { get; set; }
        public int RiverId { get; set; }


    }
}
