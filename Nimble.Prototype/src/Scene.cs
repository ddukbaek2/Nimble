using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Prototype
{
	/// <summary>
	/// 씬.
	/// </summary>
	public class Scene
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public Scene()
		{
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected virtual void OnCreate()
		{
		}

		/// <summary>
		/// 종료됨.
		/// </summary>
		protected virtual void OnDestroy()
		{

		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		protected virtual void OnUpdate(double timeDelta)
		{
		}

		/// <summary>
		/// 출력됨.
		/// </summary>
		protected virtual void OnDraw(SKCanvas canvas)
		{
		}

		/// <summary>
		/// 생성.
		/// </summary>
		internal void Create()
		{
			OnCreate();
		}

		/// <summary>
		/// 파괴.
		/// </summary>
		internal void Destroy()
		{
			OnDestroy();
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
		internal void Draw(SKCanvas canvas)
		{
			OnDraw(canvas);
		}
	}
}