using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct NvCVImage
{
    public uint width;                  // The number of pixels horizontally in the image.
    public uint height;                 // The number of pixels  vertically  in the image.
    public int pitch;                   // The byte stride between pixels vertically.
    public NvParameters.NvCVImage_PixelFormat pixelFormat;         // The format of the pixels in the image.
    public NvParameters.NvCVImage_ComponentType componentType;     // The data type used to represent each component of the image.
    public byte pixelBytes;             // The number of bytes in a chunky pixel.
    public byte componentBytes;         // The number of bytes in each pixel component.
    public byte numComponents;          // The number of components in each pixel.
    public byte planar;                 // NVCV_CHUNKY, NVCV_PLANAR, NVCV_UYVY, ....
    public byte gpuMem;                 // NVCV_CPU, NVCV_CPU_PINNED, NVCV_CUDA, NVCV_GPU
    public byte colorspace;             // An OR of colorspace, range and chroma phase.
    public byte reserved1;              // For structure padding and future expansion. Set to 0.
    public byte reserved2;              // For structure padding and future expansion. Set to 0.
    public IntPtr pixels;               // Pointer to pixel(0,0) in the image.
    public IntPtr deletePtr;            // Buffer memory to be deleted (can be NULL).
    public IntPtr p;                    // Delete procedure to call rather than free().
    public ulong bufferBytes;           // The maximum amount of memory available through pixels.

    public NvCVImage(uint width, uint height, NvParameters.NvCVImage_PixelFormat format,
        NvParameters.NvCVImage_ComponentType type, byte layout, byte memSpace, uint alignment)
    {
        this = new NvCVImage();
        NvCVAPI.NvCVImage_Alloc(ref this, width, height, format, type, layout, memSpace, alignment);
    }

    public NvCVImage(NvCVImage fullImg, int x, int y, uint width, uint height)
    {
        this = new NvCVImage();
        NvCVAPI.NvCVImage_InitView(this, fullImg, x, y, width, height);
    }

    public void Destroy()
    {
        NvCVAPI.NvCVImage_Dealloc(ref this);
    }
}