using DotNetObject = System.Object;


namespace Nimble.Core
{
	/// <summary>
	/// 기본 객체.
	/// </summary>
	public abstract class Object : DotNetObject, IObject
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public Object() : base()
		{
		}

		/// <summary>
		/// 소멸됨.
		/// </summary>
		~Object()
		{
		}
	}
}