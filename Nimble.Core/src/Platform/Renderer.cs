using SkiaSharp;


namespace Nimble.Core
{
	/// <summary>
	/// 렌더러.
	/// </summary>
	public sealed class Renderer : Object
	{
		/// <summary>
		/// 스키아 렌더링 캔버스.
		/// </summary>
		private SKCanvas m_Canvas;

		/// <summary>
		/// 스키아 렌더링 캔버스 프로퍼티.
		/// </summary>
		public SKCanvas Canvas => m_Canvas;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public Renderer(SKCanvas canvas)
		{
			m_Canvas = canvas;
		}
	}
}