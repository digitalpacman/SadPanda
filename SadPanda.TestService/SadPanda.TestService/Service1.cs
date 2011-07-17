using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SadPanda.TestService
{
	public partial class Service1 : ServiceBase
	{
		public Service1()
		{
			InitializeComponent();
			if (!System.Diagnostics.EventLog.SourceExists("DoDyLogSourse"))
				System.Diagnostics.EventLog.CreateEventSource("DoDyLogSourse",
																	  "DoDyLog");

			eventLog1.Source = "DoDyLogSourse";
			// the event log source by which 


			//the application is registered on the computer


			eventLog1.Log = "DoDyLog";
		}

		protected override void OnStart(string[] args)
		{
			eventLog1.WriteEntry("my service started"); 
		}

		protected override void OnStop()
		{
		}
	}
}
