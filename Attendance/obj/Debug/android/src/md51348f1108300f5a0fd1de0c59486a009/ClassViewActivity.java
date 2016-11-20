package md51348f1108300f5a0fd1de0c59486a009;


public class ClassViewActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Attendance.ClassViewActivity, Attendance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ClassViewActivity.class, __md_methods);
	}


	public ClassViewActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ClassViewActivity.class)
			mono.android.TypeManager.Activate ("Attendance.ClassViewActivity, Attendance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
