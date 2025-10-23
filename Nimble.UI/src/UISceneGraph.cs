using Nimble.Core;
using SkiaSharp;
using System;


namespace Nimble.UI
{
	/// <summary>
	/// UI 씬.
	/// </summary>
	public class UISceneGraph : Scene
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
		/// 출력됨.
		/// </summary>
		protected override void OnRender(SKCanvas canvas)
		{
			Console.WriteLine($"[UISceneGraph] OnRender()");
			using var paint = new SKPaint { IsAntialias = true, TextSize = 48 };
			canvas.DrawCircle(140, 140, 90, paint);
			canvas.DrawText($"UISceneGraph", 260, 160, paint);
		}
	}
}