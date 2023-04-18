using MaxineVFX;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
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

    /// <summary>
    /// Create a NvCVImage from a Texture2D in RGB Format
    /// </summary>
    public static void NVWrapperFromTexture2D(ref Texture2D texture, ref NvCVImage nvcvIm)
    {
        FlipTextureVertically(ref texture);
        byte[] imageBytes = texture.GetRawTextureData();

        IntPtr ptr = Marshal.AllocHGlobal(imageBytes.Length);
        Marshal.Copy(imageBytes, 0, ptr, imageBytes.Length);

        nvcvIm.pixels = ptr;
        nvcvIm.width = (uint)texture.width;
        nvcvIm.height = (uint)texture.height;
        nvcvIm.pitch = texture.width * 3;
        nvcvIm.pixelFormat = NvParameters.NvCVImage_PixelFormat.NVCV_RGB;
        nvcvIm.componentType = NvParameters.NvCVImage_ComponentType.NVCV_U8;
        nvcvIm.bufferBytes = 0;
        nvcvIm.pixelBytes = (byte)3;
        nvcvIm.componentBytes = (byte)1;
        nvcvIm.numComponents = (byte)3;
        nvcvIm.planar = NvParameters.NVCV_CHUNKY;
        nvcvIm.gpuMem = NvParameters.NVCV_CPU;
        nvcvIm.reserved1 = 0;
        nvcvIm.reserved2 = 0;
    }

    /// <summary>
    /// Create a Texture2D from a NvCVImage in RGB Format
    /// </summary>
    public static void NVWrapperToTexture2D(ref NvCVImage nvcvIm, ref Texture2D texture)
    {
        int length = (int)nvcvIm.width * (int)nvcvIm.height * (int)nvcvIm.pixelBytes;
        byte[] data = new byte[length];

        Marshal.Copy(nvcvIm.pixels, data, 0, length);

        texture.LoadRawTextureData(data);
        FlipTextureVertically(ref texture);
        texture.Apply();
    }

    // The image format is up-side-down between Unity and Nvidia
    private static unsafe void FlipTextureVertically(ref Texture2D original)
    {
        byte[] data = original.GetRawTextureData();

        IntPtr ptr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, ptr, data.Length);

        Utils.FlipImageVertically(ptr, original.width, original.height, 3);

        Marshal.Copy(ptr, data, 0, data.Length);

        original.LoadRawTextureData(data);
        original.Apply();
        Marshal.FreeHGlobal(ptr);
    }
}