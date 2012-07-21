using System;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Messaging;
using Ecyware.GreenBlue.LicenseServices;

namespace ServerTester
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Class1
	{
		private static LicenseServices services;
		//private static EndpointReference requestEndpoint;

		public Class1()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");			
			EndpointReference requestEndpoint = new EndpointReference(new Uri("soap.tcp://localhost:2005/LicenseServices"));
			services = new LicenseServices();
			SoapReceivers.Add(requestEndpoint, services);
			Console.WriteLine("Service started.");
			Console.WriteLine("Press X to exit");			

			Console.Read();
			Console.Read();
			Console.WriteLine("Service stopped.");
		}
	}
}
