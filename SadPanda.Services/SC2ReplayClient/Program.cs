using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SC2ReplayClient
{
	class Program
	{
		static void Main(string[] args)
		{
			//EndpointAddress address = new EndpointAddress("http://localhost:8999/SC2ReplayHost");
			using (SC2ReplayHostClient host = new SC2ReplayHostClient())
			{
				List<string> test = host.GetReplayNames().ToList();
				test.ForEach(s =>
				{
					Console.Write("{0}{1}", s, Environment.NewLine);
				});
				host.Close();
				Console.ReadLine();
			}
		}
	}
}
