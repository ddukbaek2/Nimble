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


	/// <summary>
	/// 프로그램.
	/// </summary>
	public static class Program
	{
		private const int SW_HIDE = 0;

#if DEBUG
		private static IntPtr GetConsoleWindow() { return IntPtr.Zero; }
		private static bool ShowWindow(IntPtr hWnd, int nCmdShow) { return false; }
#else
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
#endif

		/// <summary>
		/// 콘솔 닫기.
		/// </summary>
		private static void HideConsole()
		{
			if (!OperatingSystem.IsWindows())
				return;

			var handle = GetConsoleWindow();
			if (handle == IntPtr.Zero)
				return;

			ShowWindow(handle, SW_HIDE);
		}

		/// <summary>
		/// 코드 진입점.
		/// </summary>
		public static int Main(string[] arguments)
		{
#if !DEBUG
			HideConsole();
#endif
			var prototypeScene = new PrototypeScene();
			var applicationHandler = new ApplicationHandler();
			//return applicationHandler.RunAsRealtimeRendering(prototypeScene);
			return applicationHandler.RunAsMinimumRendering(prototypeScene);
		}
	}
}