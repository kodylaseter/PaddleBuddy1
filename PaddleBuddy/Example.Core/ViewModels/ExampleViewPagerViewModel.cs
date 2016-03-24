using MvvmCross.Core.ViewModels;

namespace PaddleBuddy.Core.ViewModels
{
    public class ExampleViewPagerViewModel
        : MvxViewModel
    {
        public RecyclerViewModel Recycler { get; private set; }

        public ExampleViewPagerViewModel()
        {
            Recycler = new RecyclerViewModel();
        }
    }
}