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
    }
}