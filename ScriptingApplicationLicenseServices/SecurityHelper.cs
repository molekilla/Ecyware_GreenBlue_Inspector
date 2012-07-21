using System;
using System.Web.Services.Protocols;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Messaging;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Contains the helper methods for security tokens.
	/// </summary>
	public class SecurityHelper
	{

		/// <summary>
		/// Gets the license token.
		/// </summary>
		/// <param name="context"> The SoapContext type.</param>
		/// <returns> A UsernameToken type.</returns>
		public static UsernameToken GetLicenseToken(SoapContext context)
		{
			if (context == null)
				throw new Exception(
					"Only SOAP requests are permitted.");

			// Make sure there's a token
			if (context.Security.Tokens.Count == 0)
			{
				throw new SoapException(
					"Missing security token", 
					SoapException.ClientFaultCode);
			}
			else
			{
				if ( context.Security.Tokens["LicenseToken"] != null )
				{
					UsernameToken tok = (UsernameToken)context.Security.Tokens["LicenseToken"];

					return tok;
//
//					if ( tok.Password.Length > 16 )
//					{
//						throw new SoapException(
//							"Missing security token", 
//							SoapException.ClientFaultCode);
//					} 
//					else 
//					{
//						return tok;
//					}
				} 
				else 
				{
					throw new Exception("LicenseToken not supplied");
				}
			}
		}

	}
}
