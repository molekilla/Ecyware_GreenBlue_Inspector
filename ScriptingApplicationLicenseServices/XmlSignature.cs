using System;
using System.Security;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for XmlSignature.
	/// </summary>
	public class XmlSignature
	{
		public XmlSignature()
		{
		}

//		/// <summary>
//		/// Signs a license.
//		/// </summary>
//		/// <param name="unsignedLicense"> The unsigned license stream.</param>
//		/// <param name="keyPair"> The stream containing the private key file.</param>
//		/// <param name="output"> The output stream containing the new signed license.</param>
//		internal void SignLicense(XmlTextReader unsignedLicense, Stream keyPair, Stream output)
//		{
//			try
//			{
//				// setup the document to sign
//				XmlDocument licenseDocument = new XmlDocument();
//				licenseDocument.Load(unsignedLicense);
//
//				// read in the public key
//				RSA signingKey = new RSACryptoServiceProvider();
//				using(StreamReader reader = new StreamReader(keyPair))
//				{
//					signingKey.FromXmlString(reader.ReadLine());
//				}
//
//				// now sign the document
//				SignedXml signer = new SignedXml(licenseDocument);
//				signer.SigningKey = signingKey;
//
//				// create a reference to the root of the document
//				Reference orderRef = new Reference("");
//				orderRef.AddTransform(new XmlDsigEnvelopedSignatureTransform());
//				signer.AddReference(orderRef);
//
//				// add transforms that only select the order items, type, and
//				// compute the signature, and add it to the document
//				signer.ComputeSignature();
//				licenseDocument.DocumentElement.AppendChild(signer.GetXml());
//
//				licenseDocument.Save(output);
//			}
//			catch
//			{
//				throw;
//			}
//		}

		/// <summary>
		/// Signs the XmlDocument.
		/// </summary>
		/// <param name="document"> The XmlDocument to sign.</param>
		/// <param name="signingKey"> The signing key.</param>
		/// <returns> A signed XmlDocument.</returns>
		internal XmlDocument SignXmlDocument(XmlDocument document, RSA signingKey)
		{
			try
			{
//				// setup the document to sign
//				XmlDocument licenseDocument = new XmlDocument();
//				licenseDocument.Load(unsignedLicense);

				// now sign the document
				SignedXml signer = new SignedXml(document);
				signer.SigningKey = signingKey;

				// create a reference to the root of the document
				Reference reference = new Reference("");
				reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
				signer.AddReference(reference);

				// compute the signature, and add it to the document
				signer.ComputeSignature();
				document.DocumentElement.AppendChild(signer.GetXml());

				return document;
			}
			catch
			{
				throw;
			}
		}

	}
}
