# Nvidia Maxine for Unity
This is a project that implements the SDKs from [Nvidia Maxine](https://developer.nvidia.com/maxine) into Unity C#.

Nvidia Maxine includes Audio Effects, Video Effects and Augmented Reality.

## Requirements
- You need to [download](https://catalog.ngc.nvidia.com/resources?filters=&orderBy=scoreDESC&query=Maxine) the necessary `.dll` and models (`.trtpkg`) from the official site yourself first. *\*Requires Nvidia NGC Account.*
- **RTX** series GPU is needed for using the Maxine effects.

## Features
- Include scripts that implement the functions from those `.dll` to call.
- Include 3 Demos Scenes:
  - AFx Demo: Record a short clip from Microphone, then process it through the **Noise Removal** effect.
  - VFx Demo: Take the input from your Webcam, then process it through the **Background Removal** effect.
  - AR Demo: Take the input from your Webcam, then process it through the **Face Tracking** effect.
- You can modity the scripts and configs to achieve other effects.
- Check the official SDKs to see what each component does.

## Reference
- https://github.com/NVIDIA/MAXINE-VFX-SDK
- https://github.com/NVIDIA/MAXINE-AFX-SDK
- https://github.com/NVIDIA/MAXINE-AR-SDK

## Note
The VFx Demo and AR Demo seem to have performance issues when running in the Editor. 
Building and running in the standalone program is fine however.
