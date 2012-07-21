using System;
using System.Security;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Ecyware.GreenBlue.Configuration.DigitalSignature
{
	/// <summary>
	/// Summary description for DigitalSignatureVerifier.
	/// </summary>
	public class DigitalSignatureVerifier
	{
		/// <summary>
		/// Creates a new DigitalSignatureVerifier.
		/// </summary>
		public DigitalSignatureVerifier()
		{
		}

		/// <summary>
		/// Verifies the digital signature.
		/// </summary>
		/// <param name="digitalSignature"> The XML Digital Signature.</param>
		/// <param name="publicKey"> The RSA public key.</param>
		/// <returns> Returns true if valid, else false.</returns>
		public static bool VerifyDigitalSignature(XmlTextReader digitalSignature, RSA publicKey)
		{
			bool valid = false;
			try
			{										
				// Load license file into XmlDocument
				XmlDocument doc = new XmlDocument();
				doc.Load(digitalSignature);

				// Load Signature Element
				SignedXml verifier = new SignedXml(doc);
				verifier.LoadXml(doc.GetElementsByTagName("Signature")[0] as XmlElement);

				// Validate license.
				if ( verifier.CheckSignature(publicKey) )
				{
					valid = true;
				}
				else
				{
					valid = false;
				}
			}
			catch
			{
				valid = false;
			}

			return valid;
		}
	}
}
