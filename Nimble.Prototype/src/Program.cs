using System;
using System.Runtime.InteropServices;


namespace Nimble.Prototype
{
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
			var applicationHandler = new ApplicationHandler();
			//return applicationHandler.RunAsRealtimeRendering();
			return applicationHandler.RunAsMinimumRendering();
		}
	}
}