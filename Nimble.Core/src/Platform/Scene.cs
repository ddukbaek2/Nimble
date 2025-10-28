namespace Nimble.Core
{
	/// <summary>
	/// 씬.
	/// </summary>
	public abstract class Scene : Object
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		protected Scene()
		{
			OnCreate();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		~Scene()
		{
			OnDestroy();
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
		private void OnDestroy()
		{
		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		private void OnUpdate(double timeDelta)
		{
		}

		/// <summary>
		/// 출력 시작됨.
		/// </summary>
		private void OnBeginRender()
		{
		}

		/// <summary>
		/// 출력됨.
		/// </summary>
		private void OnRender(Renderer renderer)
		{
		}

		/// <summary>
		/// 출력 완료됨.
		/// </summary>
		private void OnEndRender()
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