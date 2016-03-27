namespace PaddleBuddy.Core.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public MapViewModel()
        {
            Test = "map view model";
        }

        private string _test;

        public string Test
        {
            get { return _test; }
            set
            {
                _test = value; 
                RaisePropertyChanged(() => Test);
            }
        }

    }
}
