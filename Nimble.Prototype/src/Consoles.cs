using System;
using System.Runtime.InteropServices;


namespace Nimble.Prototype
{
	/// <summary>
	/// 콘솔 유틸리티.
	/// </summary>
	public static class Consoles
	{
		/// <summary>
		/// 콘솔 닫기.
		/// </summary>
		public static bool HideMainConsole()
		{
			if (!OperatingSystem.IsWindows())
				return false;

			const int SW_HIDE = 0;

			[DllImport("kernel32.dll")]
			static extern IntPtr GetConsoleWindow();

			[DllImport("user32.dll")]
			static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			var windowHandle = GetConsoleWindow();
			if (windowHandle == IntPtr.Zero)
				return false;

			ShowWindow(windowHandle, SW_HIDE);
			return true;
		}
	}
}