using System;
using System.Web;
using System.Threading;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// API to create custom cache dependencies 
	/// </summary>
	public class CacheHelper
	{
		internal static string GetHelperKeyName(string key)
		{
			return key + ":Ecyware:Sals";
		}

		
		public static void Insert(string key, object keyValue, CacheDependency dep)
		{
			// Create the array of cache keys for the CacheDependency ctor
			string storageKey = GetHelperKeyName(key);
			string[] rgDepKeys = new string[1];
			rgDepKeys[0] = storageKey;

			// Create a helper cache key
			HttpContext.Current.Cache.Insert(storageKey, dep);

			// Create a standard CacheDependency object
			System.Web.Caching.CacheDependency keyDep;
			keyDep = new System.Web.Caching.CacheDependency(null, rgDepKeys);

			// Create a new entry in the cache and make it dependent on a helper key
			HttpContext.Current.Cache.Insert(key, keyValue, keyDep);
		}
	}

	
	
	/// <summary>
	/// Base ASP.NET 1.1 class for custom cache dependencies
	/// </summary>
	public abstract class CacheDependency
	{
		// ************************************************************************
		// The internal timer used to poll the data source
		protected Timer InternalTimer;
		// Seconds to wait between two successive polls 
		protected int Polling = 10;
		// Name of the dependent cache key
		protected string DependentStorageKey;


		// ************************************************************************
		// Last update
		public DateTime UtcLastModified;


		// ************************************************************************
		// Class constructor
		public CacheDependency(string cacheKey, int polling)
		{
			// Store the name of the cache key to evict in case of changes
			DependentStorageKey = cacheKey;

			// Poll
			Polling = polling;

			// Set the current time
			UtcLastModified = DateTime.Now;

			// Set up the timer
			if (InternalTimer == null)
			{
				TimerCallback func = new TimerCallback(InternalTimerCallback);
				InternalTimer = new Timer(func, this, Polling*1000, Polling*1000);
			}
		}



		// ************************************************************************
		// Built-in timer callback that fires an event to the caller
		private void InternalTimerCallback(object sender) 
		{
			CacheDependency dep = (CacheDependency) sender;
			if (dep.HasChanged())
				NotifyDependencyChanged(dep);
		}

		
		
		// ************************************************************************
		// Must-override member that determines if the monitored source has changed
		protected abstract bool HasChanged();


		// ************************************************************************
		// Modify the helper key thus breaking the dependency in the Cache
		protected virtual void NotifyDependencyChanged(CacheDependency dep)
		{
			string key = CacheHelper.GetHelperKeyName(DependentStorageKey);
			dep.UtcLastModified = DateTime.Now;
			HttpRuntime.Cache.Insert(key, dep);
		}


	}
}
