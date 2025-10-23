using Crockhead.Core;
using System;


namespace Nimble.Core
{
	/// <summary>
	/// 기본 님블 오브젝트.
	/// </summary>
	public abstract class Object
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
		public Object() : base()
		{
			m_IsCreated = false;
			m_IsDestroyed = false;
			Objects.Register(this);
		}

		/// <summary>
		/// 소멸됨.
		/// </summary>
		~Object()
		{
			Objects.Unregister(this);
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
		public static T Create<T>(params object[] arguments) where T : Object
		{
			var obj = Reflections.CreateInstance<T>(arguments);
			return obj;
		}

		/// <summary>
		/// 파괴. (예약)
		/// </summary>
		public static void Destroy(Object obj)
		{
			if (obj == null)
				return;

			if (obj.IsDestroyed)
				return;

			Objects.ReserveUnregister(obj);
		}

		/// <summary>
		/// 즉시 파괴.
		/// </summary>
		public static void DestroyImmediate(Object obj)
		{
			if (obj == null)
				return;

			if (obj.IsDestroyed)
				return;

			Objects.Unregister(obj);
		}
	}
}