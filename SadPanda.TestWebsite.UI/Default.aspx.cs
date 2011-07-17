using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page
{
	protected delegate string AsyncMethodCaller(int callDuration, out int threadId);

	protected delegate string TheDelegate(string s);

    protected void Page_Load(object sender, EventArgs e)
    {
		Debug.WriteLine("Starting");

		DateTime? a = new DateTime(2000, 12, 5);
		a = null;


		TheDelegate delegateDefinition = s => s;
		string theReturnValue = delegateDefinition("test");

		AsyncDemo ad = new AsyncDemo();

		AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);
		int dummy = 0;
		IAsyncResult result = caller.BeginInvoke(3000,
			out dummy,
			new AsyncCallback(CallbackMethod),
			"The call executed on thread {0}, with return value \"{1}\".");

		Debug.WriteLine(string.Format("The main thread {0} continues to execute...",
			Thread.CurrentThread.ManagedThreadId));
		
		//Thread.Sleep(4000);

		Debug.WriteLine("The main thread ends.");


	}

	// The callback method must have the same signature as the
	// AsyncCallback delegate.
	static void CallbackMethod(IAsyncResult ar)
	{
		// Retrieve the delegate.
		AsyncResult result = (AsyncResult)ar;
		AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;

		// Retrieve the format string that was passed as state 
		// information.
		string formatString = (string)ar.AsyncState;

		// Define a variable to receive the value of the out parameter.
		// If the parameter were ref rather than out then it would have to
		// be a class-level field so it could also be passed to BeginInvoke.
		int threadId = 0;

		// Call EndInvoke to retrieve the results.
		string returnValue = caller.EndInvoke(out threadId, ar);

		// Use the format string to format the output message.
		Debug.WriteLine(string.Format(formatString, threadId, returnValue));
	}

	public class AsyncDemo
	{
		// The method to be executed asynchronously.
		public string TestMethod(int callDuration, out int threadId)
		{
			Debug.WriteLine("Test method begins.");
			Thread.Sleep(callDuration);
			threadId = Thread.CurrentThread.ManagedThreadId;
			return String.Format("My call time was {0}.", callDuration.ToString());
		}
	}

	protected override void Render(HtmlTextWriter writer)
	{
		base.Render(writer);
	}
}
