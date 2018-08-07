package md539027c6c0fd9063ac5911f6dc75db946;


public class GeolocationServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("fencing.Droid.Services.GeolocationServiceBinder, fencing.Android", GeolocationServiceBinder.class, __md_methods);
	}


	public GeolocationServiceBinder ()
	{
		super ();
		if (getClass () == GeolocationServiceBinder.class)
			mono.android.TypeManager.Activate ("fencing.Droid.Services.GeolocationServiceBinder, fencing.Android", "", this, new java.lang.Object[] {  });
	}

	public GeolocationServiceBinder (md539027c6c0fd9063ac5911f6dc75db946.GeolocationService p0)
	{
		super ();
		if (getClass () == GeolocationServiceBinder.class)
			mono.android.TypeManager.Activate ("fencing.Droid.Services.GeolocationServiceBinder, fencing.Android", "fencing.Droid.Services.GeolocationService, fencing.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
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
