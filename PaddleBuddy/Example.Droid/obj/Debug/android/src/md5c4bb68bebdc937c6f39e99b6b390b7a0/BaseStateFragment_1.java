package md5c4bb68bebdc937c6f39e99b6b390b7a0;


public abstract class BaseStateFragment_1
	extends md5c4bb68bebdc937c6f39e99b6b390b7a0.BaseStateFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Example.Droid.Fragments.BaseStateFragment`1, Example.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseStateFragment_1.class, __md_methods);
	}


	public BaseStateFragment_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseStateFragment_1.class)
			mono.android.TypeManager.Activate ("Example.Droid.Fragments.BaseStateFragment`1, Example.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
