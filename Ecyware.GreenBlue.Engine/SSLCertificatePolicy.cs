// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// SSLCertificatePolicy is used to apply the SSL settings.
	/// This class contains a CheckValidationResult that always returns true.
	/// </summary>
	public class SSLCertificatePolicy : ICertificatePolicy
	{
		public SSLCertificatePolicy()
		{
		}

		/// <summary>
		/// Validates the result for a policy.
		/// </summary>
		/// <param name="sp"> The current connection service point.</param>
		/// <param name="cert"> The certificate associated with the connection.</param>
		/// <param name="req"> The WebRequest type.</param>
		/// <param name="problem"> The problem value.</param>
		/// <returns> Returns true is validation succesful, else false.</returns>
		public bool CheckValidationResult(ServicePoint sp,
		X509Certificate cert,WebRequest req, int problem)
		{

			return true;
		}
	}
}
