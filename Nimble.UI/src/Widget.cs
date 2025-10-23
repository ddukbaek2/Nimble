using Nimble.Core;
using SkiaSharp;
using System.Collections.Generic;


namespace Nimble.UI
{
	/// <summary>
	/// 위젯.
	/// </summary>
	public class Widget : Object
	{
		private Widget m_Parent;
		private List<Widget> m_Children;
		private SKRect m_Bound;

		private bool m_IsRendering;
		private Paintable m_Paintable;

		public SKRect Bound => m_Bound;
		public SKRect LocalBound => m_Bound;

		protected override void OnCreate()
		{
			m_Parent = null;
			m_Children = new List<Widget>();
			m_Paintable = new Paintable();
			m_IsRendering = false;
		}

		/// <summary>
		/// 출력.
		/// </summary>
		public void Paint()
		{
			m_IsRendering = true;
		}

		/// <summary>
		/// 자식 위젯 추가.
		/// </summary>
		public void AddChild(Widget widget)
		{
			m_Children.Add(widget);
		}

		/// <summary>
		/// 실제 렌더링.
		/// </summary>
		private void PaintAsCache(SKCanvas canvas)
		{
			m_Paintable.Paint(canvas, Bound);
		}

		/// <summary>
		/// 실제 렌더링.
		/// </summary>
		private void PaintAsRealtime(SKCanvas canvas)
		{
			m_Paintable.Paint(canvas, Bound);
		}
	}
}
