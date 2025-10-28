namespace Nimble.Core
{
	/// <summary>
	/// 씬 핸들러.
	/// </summary>
	public class SceneHandler : ManagedObject
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		protected virtual void OnUpdate(double timeDelta)
		{
		}

		/// <summary>
		/// 출력 시작됨.
		/// </summary>
		protected virtual void OnBeginRender()
		{
		}

		/// <summary>
		/// 출력됨.
		/// </summary>
		protected virtual void OnRender(Renderer renderer)
		{
		}

		/// <summary>
		/// 출력 완료됨.
		/// </summary>
		protected virtual void OnEndRender()
		{
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
		internal void InternalRender(Renderer renderer)
		{
			OnBeginRender();
			OnRender(renderer);
			OnEndRender();
		}
	}
}