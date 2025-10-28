using Crockhead.Core;


namespace Nimble.Core
{
	/// <summary>
	/// 관리되는 객체.
	/// </summary>
	public abstract class ManagedObject : Object
	{
		/// <summary>
		/// 생성됨 여부.
		/// </summary>
		private bool m_IsCreated;

		/// <summary>
		/// 파괴됨 여부.
		/// </summary>
		private bool m_IsDestroyed;

		/// <summary>
		/// 생성됨 여부 프로퍼티.
		/// </summary>
		public bool IsCreated => m_IsCreated;

		/// <summary>
		/// 파괴됨 여부 프로퍼티.
		/// </summary>
		public bool IsDestroyed => m_IsDestroyed;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public ManagedObject() : base()
		{
			m_IsCreated = false;
			m_IsDestroyed = false;
			ManagedObjectHelper.Register(this);
		}

		/// <summary>
		/// 소멸됨.
		/// </summary>
		~ManagedObject()
		{
			ManagedObjectHelper.Unregister(this);
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void OnCreate()
		{
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
		}

		/// <summary>
		/// 내부적인 생성.
		/// </summary>
		internal void InternalOnCreate()
		{
			if (m_IsCreated)
				return;

			OnCreate();
			m_IsCreated = true;
		}

		/// <summary>
		/// 내부적인 파괴.
		/// </summary>
		internal void InternalOnDestroy()
		{
			if (m_IsDestroyed)
				return;

			OnDestroy();
			m_IsDestroyed = true;
		}

		/// <summary>
		/// 생성.
		/// </summary>
		public static T Create<T>(params object[] arguments) where T : ManagedObject
		{
			var obj = Reflections.CreateInstance<T>(arguments);
			return obj;
		}

		/// <summary>
		/// 파괴. (예약)
		/// </summary>
		public static void Destroy(ManagedObject obj)
		{
			if (obj == null)
				return;

			if (obj.IsDestroyed)
				return;

			ManagedObjectHelper.ReserveUnregister(obj);
		}

		/// <summary>
		/// 즉시 파괴.
		/// </summary>
		public static void DestroyImmediate(ManagedObject obj)
		{
			if (obj == null)
				return;

			if (obj.IsDestroyed)
				return;

			ManagedObjectHelper.Unregister(obj);
		}
	}
}