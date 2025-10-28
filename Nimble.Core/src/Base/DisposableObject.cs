using Crockhead.Core;


namespace Nimble.Core
{
	/// <summary>
	/// 해제 가능한 오브젝트.
	/// </summary>
	public abstract class DisposableObject : Disposable, IObject
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public DisposableObject() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}
	}
}
