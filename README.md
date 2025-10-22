# Nimble


## 개요
- C# 기반의 크로스플랫폼 레이어.   
- 다수의 외부 라이브러리와 프레임워크를 기반하여 닷넷 기반의 애플리케이션 레이어 제공.
- 스키아엔진 기반의 애플리케이션 GUI 렌더링.   
- SILK.NET 기반의 데스크탑 애플리케이션 빌드. (Widnows/Linux/macOS)    
- .NET for Android 기반의 Android 모바일 애플리케이션 빌드. (Android)
- .NET for iOS 기반의 iOS 모바일 애플리케이션 빌드. (iOS)
- 기반 렌더러와 각 플랫폼 렌더러 애플리케이션 사이의 UI 계층 제공. (선택적)   

## 기반 라이브러리
- SkiaSharp
- SkiaSharp.NativeAssets.Linux.NoDependencies
- SkiaSharp.NativeAssets.MacOS
- SkiaSharp.NativeAssets.Win32
- SILK.NET.Windowing
- SILK.NET.OpenGL
- .NET for Android
- .NET for iOS

## 프로젝트 구성
|프로젝트|설명|
|:---|:---|
|Nimble.Core|기반 프레임워크.|
|Nimble.UI|UI 프레임워크.|
|Nimble.Application|-|
|Nimble.Application.Windows|-|
|Nimble.Application.macOS|-|
|Nimble.Application.Linux|-|
|Nimble.Application.Android|-|
|Nimble.Application.iOS|-|
|Nimble.Prototype|Nimble.Core을 사용한 최소한의 플랫폼 이벤트 및 렌더링 루프. (개발을 위한 데스크탑 앱 빌드)|