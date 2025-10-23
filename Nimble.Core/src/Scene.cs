using Crockhead.Core;
using SkiaSharp;


namespace Nimble.Core
{
	/// <summary>
	/// 씬.
	/// </summary>
	public class Scene : Object
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
		protected virtual void OnRender(SKCanvas canvas)
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
		internal void Update(double timeDelta)
		{
			OnUpdate(timeDelta);
		}

		/// <summary>
		/// 출력.
		/// </summary>
		internal void Render(SKCanvas canvas)
		{
			OnBeginRender();
			OnRender(canvas);
			OnEndRender();
		}
	}
}