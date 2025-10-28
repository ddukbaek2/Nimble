using Crockhead.Core;


namespace Nimble.Core
{
	/// <summary>
	/// 공유 인스턴스 핸들러.
	/// </summary>
	public struct SharedInstanceHandler<TManagedObject> : IStructure where TManagedObject : ManagedObject, new()
	{
		/// <summary>
		/// 공유 인스턴스.
		/// </summary>
		public static TManagedObject SharedInstance
		{
			get
			{
				if (SharedInstances.TryGet<TManagedObject>(out var sharedInstance))
					return sharedInstance;

				sharedInstance = new TManagedObject();
				SharedInstances.Set<TManagedObject>(sharedInstance);
				return sharedInstance;
			}
		}
	}
}