using System;
using System.Runtime.InteropServices;
using UnityEngine;
using NvAR_Status = System.Int32;

namespace MaxineAR
{
    public class ArApp
    {
        private static unsafe IntPtr handle;
        private static unsafe IntPtr stream;

        public static bool EFFECT_READY { get; private set; }

        private static NvAR_Status status;
        private static NvCVImage inputImageBuffer;
        private static NvCVImage fxSrcChunkyCPU;
        private static NvCVImage _tmpVFX;

        private static Texture2D webcamTexture;

        private static ArDefs.NvAR_Rect[] BBox_Data;
        private static float[] BBOx_Confidence;
        private static ArDefs.NvAR_BBoxes Output_BBox;

        private const int BoxCount = 1;

        /// <summary>
        /// Initialize the Maxine AR Systems
        /// </summary>
        public static unsafe void Init()
        {
            EFFECT_READY = false;

#if UNITY_EDITOR
            status = ArAPI.NvAR_GetVersion(out uint version);
            Debug.Log(GetVersion(version));
#endif
            status = ArAPI.NvAR_CudaStreamCreate(out stream);
            NvCVStatus.Catch(status);

            status = ArAPI.NvAR_Create(NvParameters.NvAR_Feature_FaceDetection, out handle);
            NvCVStatus.Catch(status);

            status = ArAPI.NvAR_SetString(handle, NvParameters.NvAR_Parameter_Config("ModelDir"), ArConfig.MODEL_DIR);
            NvCVStatus.Catch(status);

            status = ArAPI.NvAR_SetCudaStream(handle, NvParameters.NvAR_Parameter_Config("CUDAStream"), stream);
            NvCVStatus.Catch(status);

            status = ArAPI.NvAR_SetU32(handle, NvParameters.NvAR_Parameter_Config("Temporal"), 0);
            NvCVStatus.Catch(status);

            status = ArAPI.NvAR_Load(handle);
            NvCVStatus.Catch(status);

            InitBuffer();

            IntPtr imagePtr = Marshal.AllocHGlobal(Marshal.SizeOf<NvCVImage>());
            Marshal.StructureToPtr(inputImageBuffer, imagePtr, false);
            status = ArAPI.NvAR_SetObject(handle, NvParameters.NvAR_Parameter_Input("Image"), imagePtr, (uint)Marshal.SizeOf<NvCVImage>());
            NvCVStatus.Catch(status);

            InitBoundingBox();

            IntPtr boxPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ArDefs.NvAR_BBoxes>());
            Marshal.StructureToPtr(Output_BBox, boxPtr, false);
            status = ArAPI.NvAR_SetObject(handle, NvParameters.NvAR_Parameter_Output("BoundingBoxes"), boxPtr, (uint)Marshal.SizeOf<ArDefs.NvAR_BBoxes>());
            NvCVStatus.Catch(status);

            status = ArAPI.NvAR_SetF32Array(handle, NvParameters.NvAR_Parameter_Output("BoundingBoxesConfidence"), BBOx_Confidence, BoxCount);
            NvCVStatus.Catch(status);

            EFFECT_READY = true;
        }

        public static unsafe (float, float, float, float) Run(WebCamTexture input)
        {
            webcamTexture.SetPixels(input.GetPixels());
            webcamTexture.Apply();

            status = NvCVAPI.NvCVImage_Realloc(ref inputImageBuffer, ArConfig.Width, ArConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 1);
            NvCVStatus.Catch(status);
            status = NvCVAPI.NvCVImage_Realloc(ref _tmpVFX, ArConfig.Width, ArConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 0);
            NvCVStatus.Catch(status);

            NvCVAPI.NVWrapperFromTexture2D(ref webcamTexture, ref fxSrcChunkyCPU);
            status = NvCVAPI.NvCVImage_Transfer(fxSrcChunkyCPU, inputImageBuffer, 1.0f, stream, _tmpVFX);
            NvCVStatus.Catch(status);

            try
            {
                ArAPI.NvAR_Run(handle);

                Marshal.FreeHGlobal(fxSrcChunkyCPU.pixels);
                status = NvCVAPI.NvCVImage_Dealloc(ref inputImageBuffer);
                NvCVStatus.Catch(status);
                status = NvCVAPI.NvCVImage_Dealloc(ref _tmpVFX);
                NvCVStatus.Catch(status);

                return (BBox_Data[0].x, BBox_Data[0].y, BBox_Data[0].width, BBox_Data[0].height);
            }
            catch
            {
                Marshal.FreeHGlobal(fxSrcChunkyCPU.pixels);
                status = NvCVAPI.NvCVImage_Dealloc(ref inputImageBuffer);
                NvCVStatus.Catch(status);
                status = NvCVAPI.NvCVImage_Dealloc(ref _tmpVFX);
                NvCVStatus.Catch(status);

                return (-1.0f, -1.0f, -1.0f, -1.0f);
            }
        }

        private static void InitBuffer()
        {
            webcamTexture = new Texture2D((int)ArConfig.Width, (int)ArConfig.Height, TextureFormat.RGB24, false);

            inputImageBuffer = new NvCVImage();
            status = NvCVAPI.NvCVImage_Alloc(ref inputImageBuffer, ArConfig.Width, ArConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 1);
            NvCVStatus.Catch(status);

            _tmpVFX = new NvCVImage();
            status = NvCVAPI.NvCVImage_Alloc(ref _tmpVFX, ArConfig.Width, ArConfig.Height, NvParameters.NvCVImage_PixelFormat.NVCV_BGR, NvParameters.NvCVImage_ComponentType.NVCV_U8, NvParameters.NVCV_CHUNKY, NvParameters.NVCV_GPU, 0);
            NvCVStatus.Catch(status);
        }

        private static unsafe void InitBoundingBox()
        {
            BBox_Data = new ArDefs.NvAR_Rect[BoxCount];
            BBOx_Confidence = new float[BoxCount];

            for (int i = 0; i < BoxCount; i++)
            {
                BBox_Data[i] = new ArDefs.NvAR_Rect { x = 0.0f, y = 0.0f, width = 0.0f, height = 0.0f };
                BBOx_Confidence[i] = 0.0f;
            }

            Output_BBox = new ArDefs.NvAR_BBoxes();

            fixed (ArDefs.NvAR_Rect* ptr = BBox_Data)
            {
                Output_BBox.boxes = ptr;
            }

            Output_BBox.num_boxes = 0;
            Output_BBox.max_boxes = BoxCount;
        }

        private static string GetVersion(uint version) => $"Version: {(version >> 24) & 0xFF}.{(version >> 16) & 0xFF}.{(version >> 8) & 0xFF}";
    }
}