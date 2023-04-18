using System;
using System.Runtime.InteropServices;
using NvAFX_Status = System.Int32;

namespace MaxineAFX
{
    public class AFxAPI
    {
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_GetEffectList(out int num_effects, out char** effects);
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_CreateEffect(string code, out IntPtr effect);

        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_SetString(IntPtr effect, string param_name, string val);
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_SetFloat(IntPtr effect, string param_name, float val);
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_SetU32(IntPtr effect, string param_name, uint val);

        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_GetString(IntPtr effect, string param_name, out string val);
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_GetFloat(IntPtr effect, string param_name, out float val);
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_GetU32(IntPtr effect, string param_name, out uint val);

        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_Load(IntPtr effect);
        [DllImport("NVAudioEffects")]
        public static extern unsafe NvAFX_Status NvAFX_Run(IntPtr effect, IntPtr[] input, IntPtr[] output, uint num_input_samples, uint num_input_channels);
    }
}