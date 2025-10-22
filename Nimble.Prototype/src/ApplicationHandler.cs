using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkiaSharp;
using System;
using System.Threading;


namespace Nimble.Prototype
{
	/// <summary>
	/// 애플리케이션 핸들러.
	/// </summary>
	public class ApplicationHandler
	{
		private IWindow m_Window;
		private GL m_GL;
		private GRGlInterface m_GRGLInterface;
		private GRContext m_GRContext;
		private GRBackendRenderTarget m_GRBackendRenderTarget;
		private SKSurface m_SKSurface;
		private bool m_IsRendering;

		/// <summary>
		/// 생성.
		/// </summary>
		public ApplicationHandler()
		{
			m_Window = null;
			m_GL = null;
			m_GRGLInterface = null;
			m_GRContext = null;
			m_GRBackendRenderTarget = null;
			m_SKSurface = null;
			m_IsRendering = false;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected virtual void OnInitialize()
		{
			Console.WriteLine($"[ApplicationHandler] OnInitialize()");

			m_GL = GL.GetApi(m_Window);
			m_GRGLInterface = GRGlInterface.Create();
			m_GRContext = GRContext.CreateGl(m_GRGLInterface);
			m_IsRendering = true;

			OnResize(Vector2D<int>.Zero);
		}

		/// <summary>
		/// 종료됨.
		/// </summary>
		protected virtual void OnFinalize()
		{
			Console.WriteLine($"[ApplicationHandler] OnFinalize()");

			m_GRBackendRenderTarget.Dispose();
			m_SKSurface.Dispose();
			m_GRContext.Dispose();
			m_GRGLInterface.Dispose();
		}

		/// <summary>
		/// 화면 크기 변경됨.
		/// </summary>
		protected virtual void OnResize(Vector2D<int> size)
		{
			Console.WriteLine($"[ApplicationHandler] OnResize({size})");

			const int GL_RGBA8 = 0x8058;

			var width = Math.Max(1, m_Window.FramebufferSize.X);
			var height = Math.Max(1, m_Window.FramebufferSize.Y);
			var frameBufferObjectId = 0u;
			var frameBufferInfo = new GRGlFramebufferInfo(frameBufferObjectId, GL_RGBA8);

			m_GRBackendRenderTarget?.Dispose();
			m_GRBackendRenderTarget = new GRBackendRenderTarget(width, height, 0, 8, frameBufferInfo);

			m_SKSurface?.Dispose();
			m_SKSurface = SKSurface.Create(m_GRContext, m_GRBackendRenderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Bgra8888);
			m_IsRendering = true;
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		protected virtual void OnUpdate(double timeDelta)
		{
			Console.WriteLine($"[ApplicationHandler] OnUpdate({timeDelta})");
		}

		/// <summary>
		/// 출력.
		/// </summary>
		protected virtual void OnDraw(double timeDelta)
		{
			if (m_SKSurface == null)
				return;

			Console.WriteLine($"[ApplicationHandler] OnDraw({timeDelta})");

			var canvas = m_SKSurface.Canvas;
			canvas.Clear(SKColors.White);
			using var paint = new SKPaint { IsAntialias = true, TextSize = 48 };
			canvas.DrawCircle(140, 140, 90, paint);
			canvas.DrawText($"Hello Skia: {timeDelta}", 260, 160, paint);
			canvas.Flush();
			m_GRContext.Flush();
		}

		/// <summary>
		/// 윈도우 생성.
		/// </summary>
		private IWindow CreateWindow()
		{
			var windowOptions = WindowOptions.Default;
			windowOptions.Title = "Nimble.Prototype";
			windowOptions.Size = new Vector2D<int>(800, 600);
			windowOptions.PreferredBitDepth = new Vector4D<int>(8, 8, 8, 8);
			windowOptions.API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, ContextFlags.Default, new APIVersion(3, 3));
			var window = Window.Create(windowOptions);
			return window;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public int RunAsRealtimeRendering()
		{
			m_Window = CreateWindow();
			m_Window.Load += OnInitialize;
			m_Window.FramebufferResize += OnResize;
			m_Window.Update += OnUpdate;
			m_Window.Render += OnDraw;
			m_Window.Closing += OnFinalize;

			// 자동 이벤트 루프.
			m_Window.Run();
			return 0;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public int RunAsMinimumRendering()
		{
			m_Window = CreateWindow();
			m_Window.IsEventDriven = false;
			m_Window.Load += OnInitialize;
			m_Window.FramebufferResize += OnResize;
			m_Window.Update += OnUpdate;
			m_Window.Render += OnDraw;
			m_Window.Closing += OnFinalize;
			//m_Window.FileDrop += (_) => m_IsRendering = true;
			//m_Window.MouseMove += (_, __) => m_IsRendering = true;
			//m_Window.MouseDown += (_, __) => m_IsRendering = true;
			//m_Window.KeyDown += (_, __) => m_IsRendering = true;

			// 수동 이벤트 루프.
			m_Window.Initialize();
			m_Window.Run
			(
				() =>
				{
					m_Window.DoEvents();
					if (!m_Window.IsClosing)
					{
						// 기존 업데이트 처리 사용 안함.
						//m_Window.DoUpdate();
					}
					if (!m_Window.IsClosing)
					{
						// 기존 렌더 처리 사용 안함.
						//m_Window.DoRender();

						// 렌더링이 필요할 때만 처리.
						if (!m_IsRendering)
							return;

						m_Window.DoRender();
						m_IsRendering = false;
					}
				}
			);

			m_Window.DoEvents();
			m_Window.Reset();
			return 0;
		}
	}
}