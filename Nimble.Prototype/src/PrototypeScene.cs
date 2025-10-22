using Nimble.Core;
using SkiaSharp;
using System;


namespace Nimble.Prototype
{
	/// <summary>
	/// 프로토타입 씬.
	/// </summary>
	public class PrototypeScene : Scene
	{
		/// <summary>
		/// 출력됨.
		/// </summary>
		protected override void OnDraw(SKCanvas canvas)
		{
			Console.WriteLine($"[PrototypeScene] OnDraw()");

			//canvas.Clear(SKColors.White);
			using var paint = new SKPaint { IsAntialias = true, TextSize = 48 };
			canvas.DrawCircle(140, 140, 90, paint);
			canvas.DrawText($"PrototypeScene", 260, 160, paint);
			//canvas.Flush();
		}
	}
}