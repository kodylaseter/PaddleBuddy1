using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Droid.Activities;
using PaddleBuddy.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Platform;

namespace PaddleBuddy.Droid.Fragments
{
    public abstract class BaseFragment : MvxFragment, ITextWatcher
    {
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;
        private bool _searchOpen;
        private InputMethodManager _imm;
        private Android.Support.V7.App.ActionBar _actionBar;
        private AppCompatEditText _edtSearch;
        

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _searchOpen = false;
            HasOptionsMenu = true;
            _imm = (InputMethodManager)Context.ApplicationContext.GetSystemService(Android.Content.Context.InputMethodService);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var ignore = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(FragmentId, null);

			_toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
			if (_toolbar != null)
			{
				((MainActivity)Activity).SetSupportActionBar(_toolbar);
				((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);

				_drawerToggle = new MvxActionBarDrawerToggle(
					Activity,                               // host Activity
					((MainActivity)Activity).DrawerLayout,  // DrawerLayout object
					_toolbar,                               // nav drawer icon to replace 'Up' caret
					Resource.String.drawer_open,            // "open drawer" description
					Resource.String.drawer_close            // "close drawer" description
				);
				_drawerToggle.DrawerOpened += (sender, e) => ((MainActivity)Activity).HideSoftKeyboard ();
				((MainActivity)Activity).DrawerLayout.SetDrawerListener(_drawerToggle);
            }
            _actionBar = ((MainActivity)Activity).SupportActionBar;
            _actionBar.SetCustomView(Resource.Layout.toolbar_search);
            _edtSearch = (AppCompatEditText)_actionBar.CustomView.FindViewById(Resource.Id.edtSearch);
            _edtSearch.AddTextChangedListener(this);

            Messenger = Mvx.Resolve<IMvxMessenger>();
            Messenger.Subscribe<ToastMessage>(DisplayToast);

            return view;
		}

        protected IMvxMessenger Messenger { get; private set; }

        public void DisplayToast(ToastMessage message)
        {
            Toast.MakeText(Activity.ApplicationContext, message.Text, message.IsShort ? ToastLength.Short : ToastLength.Long).Show();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.main_menu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_search)
            {
                HandleSearch(item);
            }
            return base.OnOptionsItemSelected(item);
        }


        protected abstract int FragmentId { get; }

        public void HandleSearch(IMenuItem item)
        {
            if (_searchOpen)
            {
                ((BaseViewModel) ViewModel).ShowSpacer = false;
                SetSearchString();
                _actionBar.SetDisplayShowTitleEnabled(true);
                _actionBar.SetDisplayShowCustomEnabled(false);
                item.SetIcon(Resource.Drawable.ic_search_white);
                _imm.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, 0);
            }
            else
            {
                ((BaseViewModel) ViewModel).ShowSpacer = true;
                if (!string.IsNullOrEmpty(_edtSearch.Text))
                {
                    SetSearchString(_edtSearch.Text);
                }
                _actionBar.SetDisplayShowTitleEnabled(false);
                _actionBar.SetDisplayShowCustomEnabled(true);
                _edtSearch.RequestFocus();
                _imm.ShowSoftInput(_edtSearch, ShowFlags.Implicit);
                item.SetIcon(Resource.Drawable.ic_clear_white);
            }
            _searchOpen = !_searchOpen;
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (_toolbar != null)
                _drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            if (_toolbar != null)
                _drawerToggle.SyncState();
        }

        public void AfterTextChanged(IEditable s)
        {
            //throw new NotImplementedException();
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            //throw new NotImplementedException();
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            SetSearchString(s.ToString());
        }

        public void SetSearchString(string s = null)
        {
            ((BaseViewModel) ViewModel).SearchString = s;
        }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}

