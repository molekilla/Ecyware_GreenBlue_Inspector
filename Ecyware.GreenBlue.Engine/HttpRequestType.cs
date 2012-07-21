using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// The HTTP request types allowed for the class.
	/// </summary>
	[Serializable]
	public enum HttpRequestType
	{
		GET,
		POST,
		/// <summary>
		/// Retrieves only the metadata of a URI Resource
		/// </summary>
		HEAD,
		/// <summary>
		/// Uploads a file
		/// </summary>
		PUT,
		/// <summary>
		/// Deletes a URI Resource
		/// </summary>
		DELETE,
		/// <summary>
		/// Traces proxy chains
		/// </summary>
		TRACE,
		/// <summary>
		/// Queries HTTP Server options
		/// </summary>
		OPTIONS,
		/// <summary>
		/// Soap over HTTP
		/// </summary>
		SOAPHTTP
	}	
}
