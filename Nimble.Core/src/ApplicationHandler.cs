using Crockhead.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkiaSharp;
using System;
using System.Collections.Generic;


namespace Nimble.Core
{
	/// <summary>
	/// 애플리케이션 핸들러.
	/// </summary>
	public class ApplicationHandler : Object
	{
		//public static ApplicationHandler Shared => SharedInstances.Get<ApplicationHandler>();

		private IWindow m_Window;
		private GL m_GL;
		private GRGlInterface m_GRGLInterface;
		private GRContext m_GRContext;
		private GRBackendRenderTarget m_GRBackendRenderTarget;
		private SKSurface m_SKSurface;
		private bool m_IsRepainting;

		/// <summary>
		/// 씬 목록.
		/// </summary>
		private List<Scene> m_Scenes;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public ApplicationHandler() : base()
		{
			m_Window = null;
			m_GL = null;
			m_GRGLInterface = null;
			m_GRContext = null;
			m_GRBackendRenderTarget = null;
			m_SKSurface = null;
			m_IsRepainting = false;
			m_Scenes = new List<Scene>();

			SharedInstances.Clear();
			SharedInstances.Set(this);
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			Console.WriteLine("[ApplicationHandler] OnCreate()");
			base.OnCreate();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			Console.WriteLine("[ApplicationHandler] OnDestroy()");
			base.OnDestroy();
		}

		/// <summary>
		/// 애플리케이션 로드됨.
		/// </summary>
		protected virtual void OnLoad()
		{
			Console.WriteLine("[ApplicationHandler] OnLoad()");

			m_GL = GL.GetApi(m_Window);
			m_GRGLInterface = GRGlInterface.Create();
			m_GRContext = GRContext.CreateGl(m_GRGLInterface);
			m_IsRepainting = true;

			OnSize(Vector2D<int>.Zero);
		}

		/// <summary>
		/// 애플리케이션 닫힘.
		/// </summary>
		protected virtual void OnClose()
		{
			Console.WriteLine("[ApplicationHandler] OnClose()");

			m_GRBackendRenderTarget.Dispose();
			m_SKSurface.Dispose();
			m_GRContext.Dispose();
			m_GRGLInterface.Dispose();
		}

		/// <summary>
		/// 애플리케이션 크기 변경됨.
		/// </summary>
		protected virtual void OnSize(Vector2D<int> size)
		{
			Console.WriteLine($"[ApplicationHandler] OnSize({size})");

			const int GL_RGBA8 = 0x8058;

			var width = Math.Max(1, m_Window.FramebufferSize.X);
			var height = Math.Max(1, m_Window.FramebufferSize.Y);
			var frameBufferObjectId = 0u;
			var frameBufferInfo = new GRGlFramebufferInfo(frameBufferObjectId, GL_RGBA8);

			m_GRBackendRenderTarget?.Dispose();
			m_GRBackendRenderTarget = new GRBackendRenderTarget(width, height, 0, 8, frameBufferInfo);

			m_SKSurface?.Dispose();
			m_SKSurface = SKSurface.Create(m_GRContext, m_GRBackendRenderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Bgra8888);
			m_IsRepainting = true;
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		protected virtual void OnUpdate(double timeDelta)
		{
			Console.WriteLine($"[ApplicationHandler] OnUpdate({timeDelta})");

			Objects.BeginFrame();

			foreach (var scene in m_Scenes.ToArray())
			{
				scene.Update(timeDelta);
			}

			Objects.EndFrame();
		}

		/// <summary>
		/// 출력.
		/// </summary>
		protected virtual void OnRender(double timeDelta)
		{
			if (m_SKSurface == null)
				return;

			Console.WriteLine($"[ApplicationHandler] OnRender({timeDelta})");

			//var canvas = m_SKSurface.Canvas;
			//canvas.Clear(SKColors.White);
			//using var paint = new SKPaint { IsAntialias = true, TextSize = 48 };
			//canvas.DrawCircle(140, 140, 90, paint);
			//canvas.DrawText($"Hello Skia: {timeDelta}", 260, 160, paint);
			//canvas.Flush();

			var canvas = m_SKSurface.Canvas;
			canvas.Clear(SKColors.White);
			foreach (var scene in m_Scenes.ToArray())
			{
				scene.Render(canvas);
			}
			canvas.Flush();

			//m_GRContext.Flush();
		}

		/// <summary>
		/// 윈도우 생성.
		/// </summary>
		private IWindow CreateWindow()
		{
			var windowOptions = WindowOptions.Default;
			windowOptions.Title = "Nimble.Core";
			windowOptions.Size = new Vector2D<int>(800, 600);
			windowOptions.PreferredBitDepth = new Vector4D<int>(8, 8, 8, 8);
			windowOptions.API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, ContextFlags.Default, new APIVersion(3, 3));
			var window = Window.Create(windowOptions);
			return window;
		}

		/// <summary>
		/// 수동 이벤트 루프.
		/// </summary>
		private void RunEventLoop()
		{	
			m_Window.Initialize();
			m_Window.Run
			(
				() =>
				{
					m_Window.DoEvents();

					//if (!m_Window.IsClosing)
					//{
					//	m_Window.DoUpdate();
					//}
					//if (!m_Window.IsClosing)
					//{
					//	m_Window.DoRender();
					//}

					if (!m_Window.IsClosing)
					{
						Objects.BeginFrame();
						{
							// 렌더링이 필요할 때만 처리.
							if (m_IsRepainting)
							{
								m_Window.DoRender();
								m_IsRepainting = false;
							}
						}
						Objects.EndFrame();
					}
				}
			);

			m_Window.DoEvents();
			m_Window.Reset();
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public int RunAsRealtimeRendering(Scene scene)
		{
			if (scene != null)
			{
				m_Scenes.Add(scene);
			}

			m_Window = CreateWindow();
			m_Window.Load += OnLoad;
			m_Window.Closing += OnClose;
			m_Window.FramebufferResize += OnSize;
			m_Window.Update += OnUpdate;
			m_Window.Render += OnRender;
			//m_Window.FileDrop += (_) => m_IsRepainting = true;
			//m_Window.MouseMove += (_, __) => m_IsRepainting = true;
			//m_Window.MouseDown += (_, __) => m_IsRepainting = true;
			//m_Window.KeyDown += (_, __) => m_IsRepainting = true;

			// 자동 이벤트 루프.
			m_Window.Run();

			if (scene != null)
			{
				m_Scenes.Remove(scene);
				Object.Destroy(scene);
			}

			return 0;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public int RunAsMinimumRendering(Scene scene)
		{
			if (scene != null)
			{
				m_Scenes.Add(scene);
			}

			m_Window = CreateWindow();
			//m_Window.IsEventDriven = false;
			m_Window.Load += OnLoad;
			m_Window.Closing += OnClose;
			m_Window.FramebufferResize += OnSize;
			m_Window.Update += OnUpdate;
			m_Window.Render += OnRender;
			m_Window.FileDrop += (_) => m_IsRepainting = true;
			//m_Window.MouseMove += (_, __) => m_IsRepainting = true;
			//m_Window.MouseDown += (_, __) => m_IsRepainting = true;
			//m_Window.KeyDown += (_, __) => m_IsRepainting = true;

			// 수동 이벤트 루프.
			RunEventLoop();

			if (scene != null)
			{
				m_Scenes.Remove(scene);
				Object.Destroy(scene);
			}

			return 0;
		}
	}
}