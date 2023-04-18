using System;
using System.Runtime.InteropServices;
using NvAR_Status = System.Int32;

namespace MaxineAR
{
    public class ArAPI
    {
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_GetVersion(out uint version);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_Create(string featureID, out IntPtr handle);

        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_Load(IntPtr handle);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_Run(IntPtr handle);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_Destroy(IntPtr handle);

        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_GetString(IntPtr handle, string paramName, out char* str);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_SetString(IntPtr handle, string paramName, string str);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_GetU32(IntPtr handle, string paramName, out uint val);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_SetU32(IntPtr handle, string paramName, uint val);
        [DllImport("nvARPose", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe NvAR_Status NvAR_SetObject(IntPtr handle, string name, IntPtr ptr, ulong typeSize);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_SetF32Array(IntPtr handle, string name, float[] vals, int count);

        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_CudaStreamCreate(out IntPtr stream);
        [DllImport("nvARPose")]
        public static extern unsafe NvAR_Status NvAR_SetCudaStream(IntPtr handle, string paramName, IntPtr stream);
    }
}