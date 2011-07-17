using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SadPanda.Services
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri baseAddress = new Uri("http://localhost:8999/SC2ReplayHost");
			ServiceHost localHost = new ServiceHost(typeof(SC2ReplayHost), baseAddress);

			try
			{
				localHost.AddServiceEndpoint(typeof(ISC2ReplayHost), new WSHttpBinding(), "SC2ReplayHost");

				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				localHost.Description.Behaviors.Add(smb);
				localHost.Open();
				Console.WriteLine("Service initialized.");
				Console.WriteLine("Press the ENTER key to terminate service.");
				Console.WriteLine();
				Console.ReadLine();
				localHost.Close();
			}
			catch (CommunicationException ex)
			{
				Console.WriteLine("Oops! Exception: {0}", ex.Message);
				localHost.Abort();
			}

		}
	}

	[ServiceContract(Namespace = "")]
	public interface ISC2ReplayHost
	{
		[OperationContract]
		List<string> GetReplayNames();
	}

	public class SC2ReplayHost : ISC2ReplayHost
	{
		public List<string> GetReplayNames()
		{
			List<string> test = new List<string>();
			test.Add("abc");
			return test;
		}
	}
}
