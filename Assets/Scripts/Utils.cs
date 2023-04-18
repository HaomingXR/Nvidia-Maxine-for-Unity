using System;
using System.Runtime.InteropServices;

public class Utils
{
    [DllImport("ImageFlip")]
    public static extern void FlipImageVertically(IntPtr imageData, int width, int height, int bytesPerPixel);

    public static unsafe string ConvertCharPtrToString(char* charPtr)
    {
        return Marshal.PtrToStringAnsi((IntPtr)charPtr);
    }
}