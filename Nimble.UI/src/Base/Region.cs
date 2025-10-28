using Nimble.Core;
using SkiaSharp;
using System.Collections.Generic;
using Math = System.Math;


namespace Nimble.UI
{
	/// <summary>
	/// 영역.
	/// </summary>
	public sealed class Region : Object
	{
		private List<SKRect> m_Rects;

		public Region()
		{
			m_Rects = new List<SKRect>();
		}


		//public bool TryMerge(ref SKRect rect, SKRect other, float tol)
		//{
		//	var scene = new Scene();
		//	var at = Inflate(rect, tol);
		//}

		static bool NearlyAligned(SKRect a, SKRect b, float tol) =>
		Math.Abs(a.Top - b.Top) <= tol || Math.Abs(a.Left - b.Left) <= tol;

		public static SKRect Inflate(SKRect rect, float padding)
		{
			return new SKRect(rect.Left - padding, rect.Top - padding, rect.Right + padding, rect.Bottom + padding);
		}

		public static SKRect Normalize(SKRect r)
		{
			var left = Math.Min(r.Left, r.Right);
			var top = Math.Min(r.Top, r.Bottom);
			var right = Math.Max(r.Left, r.Right);
			var bottom = Math.Max(r.Top, r.Bottom);
			return new SKRect(left, top, right, bottom);
		}
	}
}