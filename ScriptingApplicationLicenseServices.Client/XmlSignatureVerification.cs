// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;
using System.Security;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for XmlSignatureVerification.
	/// </summary>
	public class XmlSignatureVerification
	{
		public XmlSignatureVerification()
		{
		}

		internal bool VerifyLicense(XmlDocument document, string publicKeyString)
		{
			bool valid = false;
			try
			{										
//				// Load license file into XmlDocument
//				XmlDocument doc = new XmlDocument();
//				doc.Load(signedLicense);

				//				// Get the public key from instance
				//				Stream publicKey 
				//					= type.Assembly.GetManifestResourceStream(type.Assembly.GetName().Name + ".LicensePK.gpub");

				// Read in the public key
				RSA signingKey = new RSACryptoServiceProvider();
				signingKey.FromXmlString(publicKeyString);
				//keyPair.Close();

				// Load Signature Element
				SignedXml verifier = new SignedXml(document);
				verifier.LoadXml(document.GetElementsByTagName("Signature")[0] as XmlElement);

				// Validate license.
				if ( verifier.CheckSignature(signingKey) )
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
