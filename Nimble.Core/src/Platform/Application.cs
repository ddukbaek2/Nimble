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
	/// 애플리케이션.
	/// </summary>
	public sealed class Application : Object
	{
		/// <summary>
		/// 공유 인스턴스.
		/// </summary>
		public static Application Shared => SharedInstances.Get<Application>();

		/// <summary>
		/// 윈도우.
		/// </summary>
		private IWindow m_Window;

		/// <summary>
		/// 그래픽 컨텍스트.
		/// </summary>
		private GL m_GL;

		/// <summary>
		/// 렌더링 컨텍스트 생성 처리기.
		/// </summary>
		private GRGlInterface m_GRGLInterface;

		/// <summary>
		/// 렌더링 컨텍스트.
		/// </summary>
		private GRContext m_GRContext;
		
		/// <summary>
		/// 렌더링 대상.
		/// </summary>
		private GRBackendRenderTarget m_GRBackendRenderTarget;

		/// <summary>
		/// 스키아 표면.
		/// </summary>
		private SKSurface m_SKSurface;

		/// <summary>
		/// 렌더링 갱신 여부.
		/// </summary>
		private bool m_IsRepainting;
		
		/// <summary>
		/// 애플리케이션 핸들러.
		/// </summary>
		private ApplicationHandler m_ApplicationHandler;

		/// <summary>
		/// 렌더러.
		/// </summary>
		private Renderer m_Renderer;

		/// <summary>
		/// 씬 목록.
		/// </summary>
		private List<Scene> m_Scenes;

		/// <summary>
		/// 생성됨.
		/// </summary>
		private Application() : base()
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
			SharedInstances.Set<Application>(this);

			OnCreate();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		~Application()
		{
			OnDestroy();
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		private void OnCreate()
		{
			Console.WriteLine("[Application] OnCreate()");
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		private void OnDestroy()
		{
			Console.WriteLine("[Application] OnDestroy()");
		}

		/// <summary>
		/// 애플리케이션 로드됨.
		/// </summary>
		private void OnLoad()
		{
			Console.WriteLine("[Application] OnLoad()");

			m_GL = GL.GetApi(m_Window);
			m_GRGLInterface = GRGlInterface.Create();
			m_GRContext = GRContext.CreateGl(m_GRGLInterface);
			m_IsRepainting = true;
			m_ApplicationHandler.InternalLoad();

			OnSize(Vector2D<int>.Zero);
		}

		/// <summary>
		/// 애플리케이션 닫힘.
		/// </summary>
		private void OnClose()
		{
			Console.WriteLine("[Application] OnClose()");

			m_ApplicationHandler.InternalClose();

			m_GRBackendRenderTarget.Dispose();
			m_SKSurface.Dispose();
			m_GRContext.Dispose();
			m_GRGLInterface.Dispose();
		}

		/// <summary>
		/// 애플리케이션 크기 변경됨.
		/// </summary>
		private void OnSize(Vector2D<int> size)
		{
			Console.WriteLine($"[Application] OResize({size})");

			const int GL_RGBA8 = 0x8058;

			var width = Math.Max(1, m_Window.FramebufferSize.X);
			var height = Math.Max(1, m_Window.FramebufferSize.Y);
			var frameBufferObjectId = 0u;
			var frameBufferInfo = new GRGlFramebufferInfo(frameBufferObjectId, GL_RGBA8);

			m_GRBackendRenderTarget?.Dispose();
			m_GRBackendRenderTarget = new GRBackendRenderTarget(width, height, 0, 8, frameBufferInfo);

			m_SKSurface?.Dispose();
			m_SKSurface = SKSurface.Create(m_GRContext, m_GRBackendRenderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Bgra8888);
			m_Renderer = new Renderer(m_SKSurface.Canvas);
			m_IsRepainting = true;

			m_ApplicationHandler.InternalResize(size);
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		private void OnUpdate(double timeDelta)
		{
			Console.WriteLine($"[Application] OnUpdate({timeDelta})");

			ManagedObjectHelper.BeginFrame();
			foreach (var scene in m_Scenes.ToArray())
			{
				scene.InternalUpdate(timeDelta);
			}
			m_ApplicationHandler.InternalUpdate(timeDelta);
			ManagedObjectHelper.EndFrame();
		}

		/// <summary>
		/// 출력.
		/// </summary>
		private void OnRender(double timeDelta)
		{
			if (m_SKSurface == null)
				return;

			Console.WriteLine($"[Application] OnRender({timeDelta})");

			//var canvas = m_SKSurface.Canvas;
			//canvas.Clear(SKColors.White);
			//using var paint = new SKPaint { IsAntialias = true, TextSize = 48 };
			//canvas.DrawCircle(140, 140, 90, paint);
			//canvas.DrawText($"Hello Skia: {timeDelta}", 260, 160, paint);
			//canvas.Flush();

			//var canvas = m_SKSurface.Canvas;
			var canvas = m_Renderer.Canvas;
			canvas.Clear(SKColors.White);
			foreach (var scene in m_Scenes.ToArray())
			{
				scene.InternalRender(m_Renderer);
			}
			m_ApplicationHandler.InternalRender(timeDelta);
			canvas.Flush();

			//m_GRContext.Flush();
		}

		/// <summary>
		/// 씬 추가.
		/// </summary>
		public void AddScene(Scene scene)
		{
			m_Scenes.Add(scene);
		}

		/// <summary>
		/// 씬 제거.
		/// </summary>
		public void RemoveScene(Scene scene)
		{
			m_Scenes.Remove(scene);
		}

		/// <summary>
		/// 모든 씬 제거.
		/// </summary>
		public void RemoveAllScenes()
		{
			m_Scenes.Clear();
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
						ManagedObjectHelper.BeginFrame();
						{
							// 렌더링이 필요할 때만 처리.
							if (m_IsRepainting)
							{
								m_Window.DoRender();
								m_IsRepainting = false;
							}
						}
						ManagedObjectHelper.EndFrame();
					}
				}
			);

			m_Window.DoEvents();
			m_Window.Reset();
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public int RunAsRealtimeRendering()
		{
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

			// 모든 씬 제거.
			RemoveAllScenes();

			return 0;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public int RunAsMinimalRendering()
		{
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

			// 모든 씬 제거.
			RemoveAllScenes();

			return 0;
		}

		/// <summary>
		/// 실행.
		/// </summary>
		public static int Run(ApplicationHandler applicationHandler)
		{
			var application = new Application();
			application.m_ApplicationHandler = applicationHandler;
			
			//application.RunAsRealtimeRendering();
			application.RunAsMinimalRendering();
			return 0;
		}
	}
}