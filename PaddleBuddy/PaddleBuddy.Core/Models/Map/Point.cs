using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaddleBuddy.Core.Models.Map
{
    public class Point
    {
        public int Id { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public int RiverId { get; set; }

        public override string ToString()
        {
            return "id: " + Id + ", lat: " + Lat;
        }
    }
}
