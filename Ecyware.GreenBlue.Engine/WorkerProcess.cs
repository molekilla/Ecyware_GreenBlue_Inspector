// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Threading;
using System.Collections;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains logic that handles BaseHttpForm asynchronous calls results.
	/// This allows the asynchronous class to end gracefully, while a background process
	/// handles any additional processing.
	/// </summary>
	internal class WorkerProcess
	{
		//internal static event ReturnPacket ReturnPacketEvent;
		internal static event PipelineCommandResultEventHandler PipelineCommandEvent;

		internal static ArrayList ReceiveList;
		internal static ManualResetEvent ReceiveEvent = new ManualResetEvent(false);

		internal static Thread ReceiverThread = null;

		#region Worker thread method
		/// <summary>
		/// Starts worker process. If already started, does nothing.
		/// </summary>
		/// <returns> Returns true if started, else false.</returns>
		internal static bool StartWorkerProcess()
		{
			// if is null, start
			if ( ReceiverThread == null )
			{	
				ReceiveList = new ArrayList();
				ReceiverThread = new Thread(new ThreadStart(ProcessResponseBufferList));
				ReceiverThread.IsBackground = true;
				ReceiverThread.Start();
				return true;
			}
			// if not alive, restart
			if (!ReceiverThread.IsAlive )
			{				
				ReceiveList = new ArrayList();
				ReceiverThread = new Thread(new ThreadStart(ProcessResponseBufferList));
				ReceiverThread.IsBackground = true;
				ReceiverThread.Start();
				return true;
			}
			return false;
		}

//		/// <summary>
//		/// Process the incoming buffer from the BaseHttpForm asynchronous calls results.
//		/// </summary>
//		internal static void ProcessResponseBufferList2()
//		{
//			bool flag;
//			while (true)
//			{
//				flag = ReceiveEvent.WaitOne();
//
//				if ( flag )
//				{
//					ReceiveEvent.Reset();
//
//					Monitor.Enter(ReceiveList);
//					
//					while ( ReceiveList.Count > 0 )
//					{
//						//1: get data
//						PacketStateData packet = (PacketStateData)ReceiveList[0];
//						
//						//2: remove item here
//						ReceiveList.RemoveAt(0);
//						ResponseBuffer response = null;
//
//						// --------- start pipeline process -------
//						// process here
//						// TODO: we could even use some type of delegation, pipeline this thing
//						if ( packet.ErrorMessage.Length == 0 )
//						{
//							// Exception catching necessary.
//							try
//							{
//								// -- Fill Response Buffer --
//								response = HttpPipeline.FillResponseBuffer(
//									packet.WebResponse,
//									packet.HttpStateData.HttpRequest,
//									packet.ClientSettings, 
//									packet.HttpStateData);
//
//								// -- Parse Scripts --
//								response = HttpPipeline.ParseScriptTags(response);
//
//								// -- Load Scripts from source --
//								response = HttpPipeline.LoadScriptsFromSrc((Uri)response.RequestHeaderCollection["Request Uri"],
//									response,
//									packet.ClientSettings);
//							}
//							catch (System.Net.WebException ex)
//							{
//								response = HttpPipeline.FillErrorBuffer(ex.Message,
//									packet.HttpStateData);
//							}	
//							catch ( Exception ex )
//							{
//								Utils.ExceptionHandler.RegisterException(ex);
//							}
//						} 
//						else 
//						{							
//							response = HttpPipeline.FillErrorBuffer(packet.ErrorMessage,
//								packet.HttpStateData);
//						}
//
//						// Save Response
//						packet.ResponseData = response;
//						
//						// --------- end pipeline process -------
//						// Call event
//						ReturnPacketEvent(packet);
//					}
//
//					Monitor.Exit(ReceiveList);
//				}
//			}
//		}


		/// <summary>
		/// Process the incoming buffer from the BaseHttpForm asynchronous calls results.
		/// </summary>
		internal static void ProcessResponseBufferList()
		{
			bool flag;
			while (true)
			{
				flag = ReceiveEvent.WaitOne();
				System.Diagnostics.Debug.Write("WP WaitOne.\r\n");

				if ( flag )
				{
					ReceiveEvent.Reset();
					System.Diagnostics.Debug.Write("WP Reset.\r\n");

					System.Diagnostics.Debug.Write("Packet Start.\r\n");
					Monitor.Enter(ReceiveList);
					
					while ( ReceiveList.Count > 0 )
					{
						//1: get data
						IPipelineCommand pipelineCommand = (IPipelineCommand)ReceiveList[0];
						
						//2: remove item here
						ReceiveList.RemoveAt(0);

						pipelineCommand.ExecuteCommand();

						// Call event
						PipelineCommandEvent(pipelineCommand);
						
					}

					Monitor.Exit(ReceiveList);

					System.Diagnostics.Debug.Write("Packet Done.\r\n");
				}
			}
		}
		#endregion

	}
}
