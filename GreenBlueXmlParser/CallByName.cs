using System;
using System.Reflection;


namespace Ecyware.GreenBlue.HtmlProcessor
{
		public class ClassA
		{        
			//NOTE: Must be public function to be called by name.
			public void Function1()
			{
				Console.WriteLine("Function1 is called.");
			}
        
			//NOTE: Must be public function to be called by name.
			public void Function2()
			{
				Console.WriteLine("Function2 is called.");
			}

			private void Caller() 
			{
				//The string variable holding the function name.
				string FunctionName="Function1";
            
				Type t=this.GetType();
				MethodInfo mi=t.GetMethod(FunctionName);
				if (mi!=null) 
					mi.Invoke(this, null);
				else
					Console.WriteLine("Wrong Function Name");

				Console.ReadLine();
			}

			static void Main()
			{
				ClassA ca=new ClassA();
				ca.Caller();
			}
		}    
}
