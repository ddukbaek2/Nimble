# 님블엔진

# 기능
- C# 코드로 크로스플랫폼 개발
- 크로스플랫폼 GUI 시스템

# 빌드 및 실행 지원 플랫폼
|플랫폼|기반|설명|
|:---:|:---|:---|
|Windows|SILK.NET|-|
|macOS|SILK.NET|-|
|Linux|SILK.NET|-|
|Android|.NET for Android|-|
|iOS|.NET for iOS|-|
|Web|.NET WebAssembly Runtime|-|

# 지원 애플리케이션 타입
- 
- 앱
	- 앱루프를 통해서 유지모드 UI 렌더링 가능. (변화시에만 갱신하고 성능 소모 낮춤.)
	- 모든 API가 비동기 실행을 전제로 개발.
	- 라이프사이클 핸들러를 통해 큰 작업 없이 개발.
	- 대부분 위젯은 개발 할 필요 없이 높은 수준으로 지원됨.
- 게임
	- 게임루프를 통해서 직접실행모드 UI 렌더링 가능. (실시간으로 갱신하고 성능 소모 높임.)
- 서버
	- 가능은 하지만 사실상 플랫폼을 지원할 의미가 크게 없어서 권장하지 않음.

# Nimble.Core
# Nimble.UI
- 유지모드 UI에 기반한 최적화. (Retain UI)
- 스키아 렌더러에 기반하여 네이티브 성능 보장.
- 다양한 위젯 컴포넌트가 지원되며, 자체 렌더링에 기준하므로 모든 플랫폼에서 동일한 형태.
- C# 기반의 자체 UI 레이아웃 디자인 포맷팅 및 작업 도구 지원. (Nimble.UI.Designer)
	- 디자이너도 님블엔진으로 개발



# 기반 클래스 목록
|항목|부모|상속 가능|설명|
|:---|:---|:---|:---|
|class Nimble.Object|System.Object|가능|클래스 기반 객체.|
|interface Nimble.IStructure|System.Object|가능|구조체 기반 인터페이스.|
|class Nimble.Disposable|Nimble.Object|가능|해제 가능한 클래스 기반 객체.|

|sealed class Nimble.Application|Nimble.Object|불가능|애플리케이션 객체.|
|sealed class Nimble.Scene|Nimble.Object|불가능|애플리케이션 핸들러에 등록하는 라이프 사이클 객체.|

|class Nimble.ApplicationHandler|Nimble.Object|가능|애플리케이션 핸들러 객체.|
|class Nimble.SceneHandler|Nimble.Object|가능|씬 핸들러 객체.|



# 지원하는 위젯 컴포넌트 목록
|항목|부모|설명|
|:---|:---|:---|
|Nimble.UI.Widget|Nimble.Object|위젯 기반 객체.|
|Nimble.UI.Drawable|Nimble.Disposable|위젯의 렌더링 노드 기반 객체.|
|Nimble.UI.Paintable|Nimble.Disposable|위젯의 렌더링 노드 기반 객체.|



- Object
	- Application
    	- ApplicationHandler
	- Scene
    	- SceneHandler
        - UIScene
            - 