using UnityEngine;
using System;
using System.Runtime.InteropServices;
using NvVFx_Status = System.Int32;

namespace MaxineVFX
{
    public class VFxAPI
    {
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_CreateEffect(string code, out IntPtr effect);

        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_GetString(IntPtr effect, string paramName, out char* str);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_SetString(IntPtr effect, string paramName, string str);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_GetU32(IntPtr effect, string paramName, out uint val);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_SetU32(IntPtr effect, string paramName, uint val);

        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_CudaStreamCreate(out IntPtr stream);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_SetCudaStream(IntPtr effect, string paramName, IntPtr stream);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_AllocateState(IntPtr effect, out IntPtr handle);

        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_Load(IntPtr effect);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_Run(IntPtr effect, int async);

        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_SetImage(IntPtr effect, string paramName, ref NvCVImage im);
        [DllImport("NVVideoEffects")]
        public static extern unsafe NvVFx_Status NvVFX_SetStateObjectHandleArray(IntPtr effect, string paramName, IntPtr handle);

        /// <summary>
        /// Initialize an empty NvCVImage in RGB Format
        /// </summary>
        public static void EmptyNVWrapper(ref NvCVImage nvcvIm)
        {
            int l = (int)VFxConfig.Width * (int)VFxConfig.Height * 3;
            byte[] data = new byte[l];

            IntPtr ptr = Marshal.AllocHGlobal(l);
            Marshal.Copy(data, 0, ptr, l);

            nvcvIm.pixels = ptr;
            nvcvIm.width = VFxConfig.Width;
            nvcvIm.height = VFxConfig.Height;
            nvcvIm.pitch = (int)VFxConfig.Width * 3;
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
}