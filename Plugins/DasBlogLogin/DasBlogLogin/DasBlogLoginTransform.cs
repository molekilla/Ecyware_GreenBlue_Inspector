using System;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Protocols.Http.Transforms;
using Ecyware.GreenBlue.Protocols.Http.Transforms.Designers;
using System.Security.Cryptography;
using System.Text;

namespace DasBlogLogin
{
	/// <summary>
	/// Contains logic for a DasBlog login authentication.
	/// </summary>
	[WebTransformAttribute("DasBlog Login Transform","input","Handles the client side authentication for dasBlog logins.")]
	[UITransformEditor(typeof(FillFormTransformDesigner))]
	public class DasBlogLoginTransform : FillFormTransform
	{
		/// <summary>
		/// Creates a new DasBlogLoginTransform.
		/// </summary>
		public DasBlogLoginTransform()
		{
		}

		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			/* PseudoCode
			 *  password = MD5($password) (in BitConverter format)
				str = $challenge + $password + $username
				str = MD5(str) (in BitConverter format)
				$challenge = str
				$password = String.Empty
			 * */
			// Get Password and Username field
			// Check if they exist first.

			string username = string.Empty;
			string password = string.Empty;
			FormField passwordField = null;
			string challenge = string.Empty;
			FormField challengeField = null;
			object temp;

			#region Get Form Field and Transform Values.
			foreach ( FormField field in this.FormFields )
			{
				// Get username
				if ( field.FieldName == "SignIn:username" )
				{
					temp = field.TransformValue.GetValue(response);

					if ( temp != null )
						username = Convert.ToString(temp);
				}

				// Get password
				if ( field.FieldName == "SignIn:password" )
				{
					passwordField = field;
					temp = field.TransformValue.GetValue(response);

					if ( temp != null )
						password = Convert.ToString(temp);
				}

				// Get challenge
				if ( field.FieldName == "SignIn:challenge" )
				{
					challengeField = field;
					temp = field.TransformValue.GetValue(response);

					if ( temp != null )
						challenge = Convert.ToString(temp);
				}
			}
			#endregion

			// If fields are null, we skip the custom processing.
			if ( challengeField != null  && passwordField != null )
			{																																
				// Apply logic
				System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
				string hash = BitConverter.ToString(md5.ComputeHash(Encoding.Unicode.GetBytes(password)));

				string str = challenge + hash + username;
				str = BitConverter.ToString(md5.ComputeHash(Encoding.Unicode.GetBytes(str)));
				challenge = str;
				password = string.Empty;

				// update fields
				DefaultTransformValue challengeValue = new DefaultTransformValue();
				challengeValue.Value = challenge;

				DefaultTransformValue passwordValue = new DefaultTransformValue();
				passwordValue.Value = password;

				challengeField.TransformValue = challengeValue;
				passwordField.TransformValue = passwordValue;
			}

			// Apply Fill Form Transform
			base.ApplyTransform (request, response);
		}

	}
}
