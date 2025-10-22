using Nimble.Core;
using SkiaSharp;
using System;


namespace Nimble.UI
{
	/// <summary>
	/// UI 씬.
	/// </summary>
	public class UIScene : Scene
	{
		/// <summary>
		/// 출력됨.
		/// </summary>
		protected override void OnDraw(SKCanvas canvas)
		{
			Console.WriteLine($"[UIScene] OnDraw()");
			using var paint = new SKPaint { IsAntialias = true, TextSize = 48 };
			canvas.DrawCircle(140, 140, 90, paint);
			canvas.DrawText($"UIScene", 260, 160, paint);
		}
	}
}