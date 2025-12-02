# CleverTap
A weather SDK application.

# Unity WeatherSDK Project

This repository is a minimal, working WeatherSDK that demonstrates:
- A Unity package folder with a GameObject bridge that shows platform-native Toast message for Android or UIAlert for iOS.
- A Weather scene uses WeatherUI scripts to get device Location from the LocationService script and call the WeatherAPI to fetch temperature.
- Android and iOS native plugin templates to show native UI.
- Instructions to create and export the Unity package/prefab.

**Note:**
To use in Unity, import the folder as a copy inside `Assets/` as `WeatherSDK/` into an existing Unity project (Unity 2020.3+ recommended). [used Unity 6000.51f1 LTS]

## Project layout

```
Assets/WeatherSDK/
  >> Plugins/
    - Android/src/com/sdk/toast/ToastBridge.java
    - iOS/ToastBridge.mm
  >> Scripts/
    - ToastManager.cs
    - NativeBridge.cs
    - LocationService.cs
    - WeatherAPI.cs
    - WeatherUI.cs
  >> Prefabs/
	 - ToastGameObject.prefab.txt
```
	
## Scene Setup

- 1.
Check the Weather scene, how the Toast Button functionality has been used.

- 2.
>> Create a new scene
>> Add a Button in the canvas. Attach the NativeBridge script to the Button.
>> Create a GameObject, Attach - WeatherUI script.
 Reference the LocationService, WeatherAPI scripts, and Button component.
>> Create a GameObject Attach - LocationService script.
>> Create a GameObject Attach - WeatherAPI script.

- 3. 
For the Android plugin, the Java file is placed under `WeatherSDK/Plugins/Android/...` and will be compiled into the Android package when building.

For the iOS plugin, the Objective-C++ file is under `WeatherSDK/Plugins/iOS/...`. When building for iOS, 
	Xcode will compile this file, and you may need to add `-ObjC` linker flag depending on the Unity version.
  
- 4. 

To make Android Build -
>> Generate a Manifest file from Unity Project settings.
 Then add Internet and Location permission inside the Manifest file.
`<uses-permission android:name="android.permission.INTERNET" />`
`<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />`
`<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />`

To make iOS Build -
>> Export the iOS project.
>> Open in Xcode.
>> Give Internet and Location permission.

Run in the Editor: Toast falls back to `Debug.Log`. On the device, native UI will show.

- 5. [BUILD]

## API

We use the API as specified:
`https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&daily=temperature_2m_max&timezone=auto`

See `WeatherSDK/Scripts/WeatherAPI.cs` for parsing.

## App Architecture

The application is the final product that the end-user interacts with.
Its architecture focuses on:

`UX/UI`
- A UI Button.

`Game Flow`
- Button, scripts system, navigation, and transitions.

## Library Architecture

- Safe
- Stable
- Modular
- Easy to integrate
- Not intrusive
- Not dependent on any app structure
- Usable in ANY Unity project

## Testing

[1.] Make an Android APK -> Install the APK -> Once open it will ask for Location permission. [Granted] -> Toast message will show the temperature.
	>> Also, on Button click, it will show the temperature.
	
