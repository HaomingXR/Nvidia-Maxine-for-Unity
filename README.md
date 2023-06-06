# Nvidia Maxine for Unity
This is a project that implements the SDKs from [Nvidia Maxine](https://developer.nvidia.com/maxine) into Unity C#.

Nvidia Maxine includes Audio Effects, Video Effects and Augmented Reality.

## Requirements
- A **RTX** series GPU is needed.
- You need to download the necessary libraries (`.dll`) and models (`.trtpkg`) from the [official NGC website](https://catalog.ngc.nvidia.com/resources?filters=&orderBy=scoreDESC&query=Maxine) first.
    - A Nvidia NGC Account is needed
    - Put libraries into `Plugins` folder ; Put models into `StreamingAssets` folder

## Features
- Include scripts that implement some of the functions from those `.dll` to call.
- Include 3 Demos Scenes:
    - **AFx Demo**: Record a short audio clip from the microphone, then process it through the **Noise Removal** effect.
    - **VFx Demo**: Take the input from your Webcam, then process it through the **Background Removal** effect.
    - **AR Demo**: Take the input from your Webcam, then process it through the **Face Tracking** effect.
- You can modify the scripts and configs to achieve other effects.
- Refer to the official SDKs to see what each component does.

## Performance
The **VFx** and **AR** demo were able to run at around `1920x1080@24FPS` or `1280x720@30FPS` on a `RTX 3060`

## Reference
- https://github.com/NVIDIA/MAXINE-VFX-SDK
- https://github.com/NVIDIA/MAXINE-AFX-SDK
- https://github.com/NVIDIA/MAXINE-AR-SDK