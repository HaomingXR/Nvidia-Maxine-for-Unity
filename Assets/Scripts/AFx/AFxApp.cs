using UnityEngine;
using System;
using System.Runtime.InteropServices;
using NvAFX_Status = System.Int32;

namespace MaxineAFX
{
    public class AFxApp
    {
        public static bool READY { get; private set; }

        private static unsafe IntPtr handle;
        private static IntPtr[] input;
        private static IntPtr[] output;
        private static NvAFX_Status status;

        /// <summary>
        /// Initialize the Maxine AFx Systems
        /// </summary>
        public static unsafe void Init()
        {
            READY = false;
            input = new IntPtr[1];
            output = new IntPtr[1];
            input[0] = Marshal.AllocHGlobal((int)AFxConfig.NUM_INPUT_SAMPLE * sizeof(float));
            output[0] = Marshal.AllocHGlobal((int)AFxConfig.NUM_INPUT_SAMPLE * sizeof(float));

#if UNITY_EDITOR
            status = AFxAPI.NvAFX_GetEffectList(out int num_effects, out char** effects);
            AFxStatus.Catch(status);

            string log = $"Number of Effects: {num_effects}\n";
            log += "List of Effects: \n";

            for (int i = 0; i < num_effects; i++)
                log += $"  ({i + 1}) " + Utils.ConvertCharPtrToString(effects[i]) + "\n";

            Debug.Log(log);
#endif

            status = AFxAPI.NvAFX_CreateEffect(NvParameters.NVAFX_EFFECT_DENOISER, out handle);
            AFxStatus.Catch(status);

            status = AFxAPI.NvAFX_SetString(handle, NvParameters.NVAFX_PARAM_MODEL_PATH, AFxConfig.MODEL_FILE);
            AFxStatus.Catch(status);

            status = AFxAPI.NvAFX_SetFloat(handle, NvParameters.NVAFX_PARAM_INTENSITY_RATIO, AFxConfig.INTENSITY_RATIO);
            AFxStatus.Catch(status);

            status = AFxAPI.NvAFX_SetU32(handle, NvParameters.NVAFX_PARAM_ENABLE_VAD, AFxConfig.VAD);
            AFxStatus.Catch(status);

            status = AFxAPI.NvAFX_Load(handle);
            AFxStatus.Catch(status);

            if (status == AFxStatus.NVAFX_STATUS_SUCCESS)
            {
                READY = true;

#if UNITY_EDITOR
                Debug.Log("Effect Loaded!");

                uint sample_rate, input_channel, input_sample;
                status = AFxAPI.NvAFX_GetU32(handle, NvParameters.NVAFX_PARAM_INPUT_SAMPLE_RATE, out sample_rate);
                AFxStatus.Catch(status);
                status = AFxAPI.NvAFX_GetU32(handle, NvParameters.NVAFX_PARAM_NUM_INPUT_CHANNELS, out input_channel);
                AFxStatus.Catch(status);
                status = AFxAPI.NvAFX_GetU32(handle, NvParameters.NVAFX_PARAM_NUM_INPUT_SAMPLES_PER_FRAME, out input_sample);
                AFxStatus.Catch(status);

                Debug.Log($"Required Sample Rate: {sample_rate}");
                Debug.Log($"Required Input Channel: {input_channel}");
                Debug.Log($"Required Input Sample: {input_sample}");
#endif
            }
        }

        /// <summary>
        /// Process Data
        /// </summary>
        /// <param name="data">Input Audio Data</param>
        public static unsafe void Run(ref float[] data)
        {
            Marshal.Copy(data, 0, input[0], (int)AFxConfig.NUM_INPUT_SAMPLE);

            status = AFxAPI.NvAFX_Run(handle, input, output, AFxConfig.NUM_INPUT_SAMPLE, 1);
            AFxStatus.Catch(status);

            Marshal.Copy(output[0], data, 0, (int)AFxConfig.NUM_INPUT_SAMPLE);
        }

        /// <summary>
        /// Free Up Memory
        /// </summary>
        public static void Garbage()
        {
            Marshal.FreeHGlobal(input[0]);
            Marshal.FreeHGlobal(output[0]);
        }
    }
}