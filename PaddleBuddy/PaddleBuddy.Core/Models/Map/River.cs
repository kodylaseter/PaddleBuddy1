using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaddleBuddy.Core.Models.Map
{
    public class River
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Point> Points{ get; set; }
    }
}
