using Crockhead.Core;
using Silk.NET.Maths;
using System;


namespace Nimble.Core
{
	/// <summary>
	/// 애플리케이션 핸들러.
	/// </summary>
	public class ApplicationHandler : ManagedObject
	{
		/// <summary>
		/// 공유 인스턴스.
		/// </summary>
		public static ApplicationHandler Shared => SharedInstances.Get<ApplicationHandler>();

		/// <summary>
		/// 생성됨.
		/// </summary>
		public ApplicationHandler() : base()
		{		
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			Console.WriteLine("[ApplicationHandler] OnCreate()");
			base.OnCreate();
			SharedInstances.Set<ApplicationHandler>(this);
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			Console.WriteLine("[ApplicationHandler] OnDestroy()");
			base.OnDestroy();
		}

		/// <summary>
		/// 애플리케이션 로드됨.
		/// </summary>
		protected virtual void OnLoad()
		{
			Console.WriteLine("[ApplicationHandler] OnLoad()");
		}

		/// <summary>
		/// 애플리케이션 닫힘.
		/// </summary>
		protected virtual void OnClose()
		{
			Console.WriteLine("[ApplicationHandler] OnClose()");
		}

		/// <summary>
		/// 애플리케이션 크기 변경됨.
		/// </summary>
		protected virtual void OResize(Vector2D<int> size)
		{
			Console.WriteLine($"[ApplicationHandler] OResize({size})");
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		protected virtual void OnUpdate(double timeDelta)
		{
			Console.WriteLine($"[ApplicationHandler] OnUpdate({timeDelta})");
		}

		/// <summary>
		/// 출력.
		/// </summary>
		protected virtual void OnRender(double timeDelta)
		{
			Console.WriteLine($"[ApplicationHandler] OnRender({timeDelta})");
		}

		/// <summary>
		/// 열기.
		/// </summary>
		internal void InternalLoad()
		{
			OnLoad();
		}

		/// <summary>
		/// 닫기.
		/// </summary>
		internal void InternalClose()
		{
			OnClose();
		}

		/// <summary>
		/// 애플리케이션 크기 변경.
		/// </summary>
		internal void InternalResize(Vector2D<int> size)
		{
			OResize(size);
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		internal void InternalUpdate(double timeDelta)
		{
			OnUpdate(timeDelta);
		}

		/// <summary>
		/// 출력.
		/// </summary>
		internal void InternalRender(double timeDelta)
		{
			OnRender(timeDelta);
		}
	}
}