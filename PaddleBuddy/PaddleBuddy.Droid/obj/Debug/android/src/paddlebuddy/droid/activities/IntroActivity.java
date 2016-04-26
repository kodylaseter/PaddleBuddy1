package paddlebuddy.droid.activities;


public class IntroActivity
	extends md5204979768ea66d3a79201c4efd7c602a.MvxAppCompatActivity_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PaddleBuddy.Droid.Activities.IntroActivity, PaddleBuddy.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", IntroActivity.class, __md_methods);
	}


	public IntroActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == IntroActivity.class)
			mono.android.TypeManager.Activate ("PaddleBuddy.Droid.Activities.IntroActivity, PaddleBuddy.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
