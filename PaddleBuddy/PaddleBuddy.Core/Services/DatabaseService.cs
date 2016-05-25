using System.Collections.Generic;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.Services
{
    public class DatabaseService
    {
        private static DatabaseService _databaseService;
        private List<River> _rivers;
        private List<Point> _points;
        private List<Link> _links;

        public static DatabaseService GetInstance()
        {
            return _databaseService ?? (_databaseService = new DatabaseService());
        }

        public List<Point> Points
        {
            get { return _points; }
            set { _points = value; }
        }


        public List<River> Rivers
        {
            get { return _rivers; }
            set { _rivers = value; }
        }


        public List<Link> Links
        {
            get { return _links; }
            set { _links = value; }
        }


    }
}
