using SkiaSharp;


namespace Nimble.UI
{
	/// <summary>
	/// 실제 렌더링 객체.
	/// </summary>
	public class Paintable
	{
		/// <summary>
		/// 렌더링 캐시.
		/// </summary>
		private SKImage m_Cache;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Paintable()
		{
		}

		/// <summary>
		/// 출력.
		/// </summary>
		public void Paint(SKCanvas canvas, SKRect rect)
		{
		}

		/// <summary>
		/// 마지막 캐시 기반 출력.
		/// </summary>
		public void Repaint(SKCanvas canvas, SKRect rect)
		{
			using var autoCanvasRestore = new SKAutoCanvasRestore(canvas, true);
			canvas.ClipRect(rect);
			DrawBackToFront(this, canvas, rect);
		}

		private void DrawBackToFront(Paintable drawable, SKCanvas canvas, SKRect rect)
		{
			drawable.Paint(canvas, rect);
		}
}
}