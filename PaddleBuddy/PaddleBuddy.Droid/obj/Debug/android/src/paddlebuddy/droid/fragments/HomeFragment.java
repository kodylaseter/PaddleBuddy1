package paddlebuddy.droid.fragments;


public class HomeFragment
	extends md53ae486e6d3c4d2aa35c958338b255dce.BaseFragment_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PaddleBuddy.Droid.Fragments.HomeFragment, PaddleBuddy.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HomeFragment.class, __md_methods);
	}


	public HomeFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HomeFragment.class)
			mono.android.TypeManager.Activate ("PaddleBuddy.Droid.Fragments.HomeFragment, PaddleBuddy.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
