using System;
using Ecyware.GreenBlue.LicenseServices.Client;
using System.Data;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Messaging;
using System.Windows.Forms;

namespace LicenserServiceClientTest
{
	/// <summary>
	/// Summary description for LicenseServicesClientProxy.
	/// </summary>
	public class LicenseServicesClientProxy
	{		
		static LicenseServiceClient client = null;

		/// <summary>
		/// Creates a new client proxy.
		/// </summary>
		private LicenseServicesClientProxy()
		{	
		}

		/// <summary>
		/// Gets the proxy client.
		/// </summary>
		/// <returns> A LicenseServiceClient.</returns>
		public static LicenseServiceClient GetClientProxy()
		{
			if ( client == null )
			{
				if ( System.Configuration.ConfigurationSettings.AppSettings["LicenseServicesUrl"] == null )
				{
					throw new ArgumentException("There is no License Services URL set in the configuration file.");
				} 
				else 
				{
					string url = System.Configuration.ConfigurationSettings.AppSettings["LicenseServicesUrl"];
					EndpointReference requestEndpoint = new EndpointReference(new Uri(url));

					// Client
					client = new LicenseServiceClient(requestEndpoint);
					Security security = new Security();
					client.Security = security;
				}
			}

			return client;
		}

//		public static void RegisterControlForExceptionHandling(Form1 control)
//		{			
//			client.ExceptionEventHandler += control.ExceptionEventHandler;
//		}
	}
}
