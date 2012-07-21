// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Text;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Generates random buffer for use in buffer overflow tests.
	/// </summary>
	public sealed class BufferOverflowGenerator
	{
		/// <summary>
		/// Creates a new BufferOverflowGenerator.
		/// </summary>
		public BufferOverflowGenerator()
		{
		}

		/// <summary>
		/// Generates a random buffer with the specified length.
		/// </summary>
		/// <param name="chars"> The char lenght to generate.</param>
		/// <returns> A string with the generated value.</returns>
		public string GenerateStringBuffer(int chars)
		{
			//StringBuilder sb = new StringBuilder();
			return Convert.ToBase64String(this.GenerateByteBuffer(chars)).Substring(0,chars);
			
			//return c.ToString();
			//return sb.ToString();
		}
		/// <summary>
		/// Generates a random byte buffer with the specified length.
		/// </summary>
		/// <param name="bytes"> The byte length to generate.</param>
		/// <returns> A byte array.</returns>
		public byte[] GenerateByteBuffer(int bytes)
		{
			Random rnd = new Random();
			Byte[] byteBuffer = new Byte[bytes];
			rnd.NextBytes(byteBuffer);
			
			return byteBuffer;
		}
	}
}
