using System.Collections.Concurrent;
using System.Collections.Generic;
using PlaceHolder = System.Byte;


namespace Nimble.Core
{
	/// <summary>
	/// 오브젝트 유틸리티.
	/// </summary>
	public static class Objects
	{
		/// <summary>
		/// 보관 중인 오브젝트 목록.
		/// </summary>
		private static ConcurrentDictionary<Object, PlaceHolder> s_StoredObjects;

		/// <summary>
		/// 보관중인 파괴 예약 오브젝트 목록.
		/// </summary>
		private static ConcurrentDictionary<Object, PlaceHolder> s_ReservedUnregisterObjects;

		/// <summary>
		/// 보관 중인 오브젝트 목록 프로퍼티.
		/// </summary>
		public static IEnumerable<Object> StoredObjects => s_StoredObjects.Keys;

		/// <summary>
		/// 생성됨.
		/// </summary>
		static Objects()
		{
			s_StoredObjects = new ConcurrentDictionary<Object, PlaceHolder>();
			s_ReservedUnregisterObjects = new ConcurrentDictionary<Object, PlaceHolder>();
		}

		/// <summary>
		/// 등록.
		/// </summary>
		public static bool Register(Object obj)
		{
			if (obj == null)
				return false;

			if (IsRegistered(obj))
				return false;

			var added = s_StoredObjects.TryAdd(obj, PlaceHolder.MinValue);
			if (added)
			{
				obj.InternalOnCreate();
			}

			return added;
		}

		/// <summary>
		/// 등록 해제.
		/// </summary>
		public static bool Unregister(Object obj)
		{
			if (obj == null)
				return false;

			if (!IsRegistered(obj))
				return false;

			// 등록 해제 예약보다 등록 해제가 우선됨.
			s_ReservedUnregisterObjects.TryRemove(obj, out var placeHolder);

			// 등록 해제.
			var removed = s_StoredObjects.TryRemove(obj, out placeHolder);
			if (removed)
			{
				obj.InternalOnDestroy();
			}

			return removed;
		}

		/// <summary>
		/// 프레임 시작.
		/// </summary>
		public static void BeginFrame()
		{
		}

		/// <summary>
		/// 프레임 종료.
		/// </summary>
		public static void EndFrame()
		{
		}

		/// <summary>
		/// 등록 여부 반환.
		/// </summary>
		public static bool IsRegistered(Object obj)
		{
			if (obj == null)
				return false;

			var contains = s_StoredObjects.ContainsKey(obj);
			if (!contains)
				return false;

			return true;
		}

		/// <summary>
		/// 등록 해제 예약.
		/// </summary>
		public static bool ReserveUnregister(Object obj)
		{
			if (!IsRegistered(obj))
				return false;

			// 등록 해제가 예약 되어있다면.
			if (IsReservedUnregister(obj))
				return true;

			var added = s_ReservedUnregisterObjects.TryAdd(obj, PlaceHolder.MinValue);
			return added;
		}

		/// <summary>
		/// 등록 해제 예약 여부 반환.
		/// </summary>
		public static bool IsReservedUnregister(Object obj)
		{
			var reservedUnregister = s_ReservedUnregisterObjects.ContainsKey(obj);
			return reservedUnregister;
		}
	}
}