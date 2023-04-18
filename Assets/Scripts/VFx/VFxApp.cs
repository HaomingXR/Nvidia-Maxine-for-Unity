using UnityEngine;
using System;
using System.Runtime.InteropServices;
using NvVFx_Status = System.Int32;

namespace MaxineVFX
{
    public class VFxApp
    {
        private static unsafe IntPtr handle;
        private static unsafe IntPtr state;
        private static unsafe IntPtr stream;

        public static bool EFFECT_READY { get; private set; }
        private static bool BUFFER_INIT;

        private static NvCVImage _srcGpu;
        private static NvCVImage _dstGpu;
        private static NvCVImage _srcVFX;
        private static NvCVImage _dstVFX;
        private static NvCVImage _tmpVFX;

        private static IntPtr[] stateArray;
        private static Texture2D webcamTexture;
        private static Texture2D outputTexture;

        private static NvVFx_Status status;

        /// <summary>
        /// Initialize the Maxine VFx Systems
        /// </summary>
        public static unsafe void Init()
        {
            EFFECT_READY = false;
            BUFFER_INIT = false;

            status = VFxAPI.NvVFX_CreateEffect(NvParameters.NVVFX_FX_GREEN_SCREEN, out handle);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetString(handle, NvParameters.NVVFX_MODEL_DIRECTORY, VFxConfig.MODEL_DIR);
            NvCVStatus.Catch(status);

#if UNITY_EDITOR
            status = VFxAPI.NvVFX_GetString(handle, NvParameters.NVVFX_INFO, out char* info);
            NvCVStatus.Catch(status);
            Debug.Log(Utils.ConvertCharPtrToString(info));
#endif

            status = VFxAPI.NvVFX_SetU32(handle, NvParameters.NVVFX_MODE, VFxConfig.MODE);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetU32(handle, NvParameters.NVVFX_CUDA_GRAPH, VFxConfig.False);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_CudaStreamCreate(out stream);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetCudaStream(handle, NvParameters.NVVFX_CUDA_STREAM, stream);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetU32(handle, NvParameters.NVVFX_MAX_INPUT_WIDTH, VFxConfig.Width);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetU32(handle, NvParameters.NVVFX_MAX_INPUT_HEIGHT, VFxConfig.Height);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetU32(handle, NvParameters.NVVFX_MAX_NUMBER_STREAMS, VFxConfig.StreamNumber);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_Load(handle);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_AllocateState(handle, out state);
            NvCVStatus.Catch(status);

            if (status == NvCVStatus.NVCV_SUCCESS)
                EFFECT_READY = true;
        }

        private static void AllocateBuffers()
        {
            if (!BUFFER_INIT)
            {
                stateArray = new IntPtr[1];
                outputTexture = new Texture2D((int)VFxConfig.Width, (int)VFxConfig.Height, TextureFormat.RGB24, false);
                webcamTexture = new Texture2D((int)VFxConfig.Width, (int)VFxConfig.Height, TextureFormat.RGB24, false);

                // src GPU
                _srcGpu = new NvCVImage();
                status = NvCVAPI.NvCVImage_Alloc(ref _srcGpu, VFxConfig.Width, VFxConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 1);
                NvCVStatus.Catch(status);

                // dst GPU
                _dstGpu = new NvCVImage();
                status = NvCVAPI.NvCVImage_Alloc(ref _dstGpu, VFxConfig.Width, VFxConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_A, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 1);
                NvCVStatus.Catch(status);

                // tmp GPU
                _tmpVFX = new NvCVImage();
                status = NvCVAPI.NvCVImage_Alloc(ref _tmpVFX, VFxConfig.Width, VFxConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 0);
                NvCVStatus.Catch(status);

                BUFFER_INIT = true;
            }
            else
            {
                status = NvCVAPI.NvCVImage_Realloc(ref _srcGpu, VFxConfig.Width, VFxConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 1);
                NvCVStatus.Catch(status);

                status = NvCVAPI.NvCVImage_Realloc(ref _dstGpu, VFxConfig.Width, VFxConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_A, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 1);
                NvCVStatus.Catch(status);

                status = NvCVAPI.NvCVImage_Realloc(ref _tmpVFX, VFxConfig.Width, VFxConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 0);
                NvCVStatus.Catch(status);
            }
        }

        public static unsafe Texture2D Run(WebCamTexture input)
        {
            AllocateBuffers();

            webcamTexture.SetPixels(input.GetPixels());
            webcamTexture.Apply();

            VFxAPI.NVWrapperFromTexture2D(ref webcamTexture, ref _srcVFX);
            VFxAPI.EmptyNVWrapper(ref _dstVFX);

            status = VFxAPI.NvVFX_SetImage(handle, NvParameters.NVVFX_INPUT_IMAGE, ref _srcGpu);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_SetImage(handle, NvParameters.NVVFX_OUTPUT_IMAGE, ref _dstGpu);
            NvCVStatus.Catch(status);

            status = NvCVAPI.NvCVImage_Transfer(_srcVFX, _srcGpu, 1.0f, stream, _tmpVFX);
            NvCVStatus.Catch(status);

            stateArray[0] = state;
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(IntPtr)) * stateArray.Length);
            Marshal.Copy(stateArray, 0, buffer, stateArray.Length);

            status = VFxAPI.NvVFX_SetStateObjectHandleArray(handle, NvParameters.NVVFX_STATE, buffer);
            NvCVStatus.Catch(status);

            status = VFxAPI.NvVFX_Run(handle, 0);
            NvCVStatus.Catch(status);

            status = NvCVAPI.NvCVImage_Transfer(_dstGpu, _dstVFX, 1.0f, stream, _tmpVFX);
            NvCVStatus.Catch(status);

            VFxAPI.NVWrapperToTexture2D(ref _dstVFX, ref outputTexture);

            Marshal.FreeHGlobal(buffer);
            DeallocateBuffers();

            return outputTexture;
        }

        private static void DeallocateBuffers()
        {
            status = NvCVAPI.NvCVImage_Dealloc(ref _srcGpu);
            NvCVStatus.Catch(status);

            status = NvCVAPI.NvCVImage_Dealloc(ref _dstGpu);
            NvCVStatus.Catch(status);

            status = NvCVAPI.NvCVImage_Dealloc(ref _tmpVFX);
            NvCVStatus.Catch(status);

            Marshal.FreeHGlobal(_srcVFX.pixels);
            Marshal.FreeHGlobal(_dstVFX.pixels);
        }
    }
}