using System;
using System.Runtime.InteropServices;
using NvCV_Status = System.Int32;

public class NvCVAPI
{
    [DllImport("NVCVImage")]
    public static extern NvCV_Status NvCVImage_Alloc(ref NvCVImage im, uint width, uint height, NvParameters.NvCVImage_PixelFormat format,
        NvParameters.NvCVImage_ComponentType type, byte layout, byte memSpace, uint alignment);
    [DllImport("NVCVImage")]
    public static extern NvCV_Status NvCVImage_Realloc(ref NvCVImage im, uint width, uint height, NvParameters.NvCVImage_PixelFormat format,
  NvParameters.NvCVImage_ComponentType type, byte layout, byte memSpace, byte alignment);
    [DllImport("NVCVImage")]
    public static extern NvCV_Status NvCVImage_Dealloc(ref NvCVImage im);

    [DllImport("NVCVImage")]
    public static extern NvCV_Status NvCVImage_Init(NvCVImage im, uint width, uint height, int pitch,
            IntPtr pixels, NvParameters.NvCVImage_PixelFormat format, NvParameters.NvCVImage_ComponentType type, byte layout, byte memSpace);
    [DllImport("NVCVImage")]
    public static extern NvCV_Status NvCVImage_InitView(NvCVImage subImg, NvCVImage fullImg,
            int x, int y, uint width, uint height);

    [DllImport("NVCVImage")]
    public static extern unsafe NvCV_Status NvCVImage_Transfer(NvCVImage src, NvCVImage dst, float scale,
     IntPtr stream, NvCVImage tmp);
}