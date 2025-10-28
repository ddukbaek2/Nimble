using Nimble.Core;


namespace Nimble.Prototype
{
	/// <summary>
	/// 프로그램.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// 코드 진입점.
		/// </summary>
		public static int Main(string[] arguments)
		{
			//Consoles.HideMainConsole();

			var prototypeScene = new PrototypeScene();
			var applicationHandler = new ApplicationHandler();
			return Application.Run(applicationHandler);
		}
	}
}