public class NvParameters
{
    // ----------
    // AFx
    // ----------

    // Effect Selectors
    /** Denoiser Effect */
    public const string NVAFX_EFFECT_DENOISER = "denoiser";
    /** Dereverb Effect */
    public const string NVAFX_EFFECT_DEREVERB = "dereverb";
    /** Dereverb Denoiser Effect */
    public const string NVAFX_EFFECT_DEREVERB_DENOISER = "dereverb_denoiser";
    /** Acoustic Echo Cancellation Effect */
    public const string NVAFX_EFFECT_AEC = "aec";
    /** Super-resolution Effect */
    public const string NVAFX_EFFECT_SUPERRES = "superres";

    // Common Effect Parameters
    /** Number of audio streams in I/O (unsigned int). */
    public const string NVAFX_PARAM_NUM_STREAMS = "num_streams";
    /** To set if SDK should select the default GPU to run the effects in a Multi-GPU setup(unsigned int).
        Default value is 0. Please see user manual for details.*/
    public const string NVAFX_PARAM_USE_DEFAULT_GPU = "use_default_gpu";
    /** To be set to '1' if SDK user wants to create and manage own CUDA context. Other users can simply
        ignore this parameter. Once set to '1' this cannot be unset for that session (unsigned int) rw param 
        Note: NVAFX_PARAM_USE_DEFAULT_GPU and NVAFX_PARAM_USER_CUDA_CONTEXT cannot be used at the same time */
    public const string NVAFX_PARAM_USER_CUDA_CONTEXT = "user_cuda_context";
    /** To be set to '1' if SDK user wants to disable cuda graphs. Other users can simply ignore this parameter.
Using Cuda Graphs helps to reduce the inference between GPU and CPU which makes operations quicker.*/
    public const string NVAFX_PARAM_DISABLE_CUDA_GRAPH = "disable_cuda_graph";
    /** To be set to '1' if SDK user wants to enable VAD */
    public const string NVAFX_PARAM_ENABLE_VAD = "enable_vad";

    // Effect Parameters
    /** Model path (char*) */
    public const string NVAFX_PARAM_MODEL_PATH = "model_path";
    /** Input Sample rate (unsigned int). Currently supported sample rate(s): 48000, 16000, 8000 */
    public const string NVAFX_PARAM_INPUT_SAMPLE_RATE = "input_sample_rate";
    /** Output Sample rate (unsigned int). Currently supported sample rate(s): 48000, 16000 */
    public const string NVAFX_PARAM_OUTPUT_SAMPLE_RATE = "output_sample_rate";
    /** Number of input samples per frame (unsigned int). This is immutable parameter */
    public const string NVAFX_PARAM_NUM_INPUT_SAMPLES_PER_FRAME = "num_input_samples_per_frame";
    /** Number of output samples per frame (unsigned int). This is immutable parameter */
    public const string NVAFX_PARAM_NUM_OUTPUT_SAMPLES_PER_FRAME = "num_output_samples_per_frame";
    /** Number of input audio channels */
    public const string NVAFX_PARAM_NUM_INPUT_CHANNELS = "num_input_channels";
    /** Number of output audio channels */
    public const string NVAFX_PARAM_NUM_OUTPUT_CHANNELS = "num_output_channels";
    /** Effect intensity factor (float) */
    public const string NVAFX_PARAM_INTENSITY_RATIO = "intensity_ratio";

    // ----------
    // VFx
    // ----------

    // Filter Selectors
    public const string NVVFX_FX_TRANSFER = "Transfer";
    public const string NVVFX_FX_GREEN_SCREEN = "GreenScreen";                  // Green Screen 
    public const string NVVFX_FX_BGBLUR = "BackgroundBlur";                     // Background blur
    public const string NVVFX_FX_ARTIFACT_REDUCTION = "ArtifactReduction";      // Artifact Reduction  
    public const string NVVFX_FX_SUPER_RES = "SuperRes";                        // Super Res 
    public const string NVVFX_FX_SR_UPSCALE = "Upscale";                        // Super Res Upscale 
    public const string NVVFX_FX_DENOISING = "Denoising";                       // Denoising 

    // Parameter Selectors
    public const string NVVFX_INPUT_IMAGE_0 = "SrcImage0";                      // There may be multiple input images
    public const string NVVFX_INPUT_IMAGE = NVVFX_INPUT_IMAGE_0;                // but there is usually only one input image
    public const string NVVFX_INPUT_IMAGE_1 = "SrcImage1";                      // Source Image 1
    public const string NVVFX_OUTPUT_IMAGE_0 = "DstImage0";                     // There may be multiple output images
    public const string NVVFX_OUTPUT_IMAGE = NVVFX_OUTPUT_IMAGE_0;              // but there is usually only one output image
    public const string NVVFX_MODEL_DIRECTORY = "ModelDir";                     // The directory where the model may be found
    public const string NVVFX_CUDA_STREAM = "CudaStream";                       // The CUDA stream to use
    public const string NVVFX_CUDA_GRAPH = "CudaGraph";                         // Enable CUDA graph to use
    public const string NVVFX_INFO = "Info";                                    // Get info about the effects
    public const string NVVFX_MAX_INPUT_WIDTH = "MaxInputWidth";                // Maximum width of the input supported
    public const string NVVFX_MAX_INPUT_HEIGHT = "MaxInputHeight";              // Maximum height of the input supported
    public const string NVVFX_MAX_NUMBER_STREAMS = "MaxNumberStreams";          // Maximum number of concurrent input streams
    public const string NVVFX_SCALE = "Scale";                                  // Scale factor
    public const string NVVFX_STRENGTH = "Strength";                            // Strength for different filters
    public const string NVVFX_STRENGTH_LEVELS = "StrengthLevels";               // Number of strength levels
    public const string NVVFX_MODE = "Mode";                                    // Mode for different filters
    public const string NVVFX_TEMPORAL = "Temporal";                            // Temporal mode: 0=image, 1=video
    public const string NVVFX_GPU = "GPU";                                      // Preferred GPU (optional)
    public const string NVVFX_BATCH_SIZE = "BatchSize";                         // Batch Size (default 1)
    public const string NVVFX_MODEL_BATCH = "ModelBatch";                       // The preferred batching model to use (default 1)
    public const string NVVFX_STATE = "State";                                  // State variable  
    public const string NVVFX_STATE_SIZE = "StateSize";                         // Number of bytes needed to store state  
    public const string NVVFX_STATE_COUNT = "NumStateObjects";                  // Number of active state object handles

    // ----------
    // NvCV
    // ----------

    public enum NvCVImage_PixelFormat
    {
        NVCV_FORMAT_UNKNOWN = 0,    // Unknown pixel format.
        NVCV_Y = 1,                 // Luminance (gray).
        NVCV_A = 2,                 // Alpha (opacity)
        NVCV_YA = 3,                // { Luminance, Alpha }
        NVCV_RGB = 4,               // { Red, Green, Blue }
        NVCV_BGR = 5,               // { Red, Green, Blue }
        NVCV_RGBA = 6,              // { Red, Green, Blue, Alpha }
        NVCV_BGRA = 7               // { Red, Green, Blue, Alpha }
    }

    public enum NvCVImage_ComponentType
    {
        NVCV_TYPE_UNKNOWN = 0,  // Unknown type of component.
        NVCV_U8 = 1,            // Unsigned 8-bit integer.
        NVCV_U16 = 2,           // Unsigned 16-bit integer.
        NVCV_S16 = 3,           // Signed 16-bit integer.
        NVCV_F16 = 4,           // 16-bit floating-point.
        NVCV_U32 = 5,           // Unsigned 32-bit integer.
        NVCV_S32 = 6,           // Signed 32-bit integer.
        NVCV_F32 = 7,           // 32-bit floating-point (float).
        NVCV_U64 = 8,           // Unsigned 64-bit integer.
        NVCV_S64 = 9,           // Signed 64-bit integer.
        NVCV_F64 = 10           // 64-bit floating-point (double).
    }

    public const int NVCV_INTERLEAVED = 0;  // All components of pixel(x,y) are adjacent (same as chunky) (default for non-YUV).
    public const int NVCV_CHUNKY = 0;       // All components of pixel(x,y) are adjacent (same as interleaved).
    public const int NVCV_PLANAR = 1;       // The same component of all pixels are adjacent.
    public const int NVCV_UYVY = 2;         // [UYVY]    Chunky 4:2:2 (default for 4:2:2)
    public const int NVCV_VYUY = 4;         // [VYUY]    Chunky 4:2:2
    public const int NVCV_YUYV = 6;         // [YUYV]    Chunky 4:2:2
    public const int NVCV_YVYU = 8;         // [YVYU]    Chunky 4:2:2
    public const int NVCV_CYUV = 10;        // [YUV]     Chunky 4:4:4
    public const int NVCV_CYVU = 12;        // [YVU]     Chunky 4:4:4
    public const int NVCV_YUV = 3;          // [Y][U][V] Planar 4:2:2 or 4:2:0 or 4:4:4
    public const int NVCV_YVU = 5;          // [Y][V][U] Planar 4:2:2 or 4:2:0 or 4:4:4
    public const int NVCV_YCUV = 7;         // [Y][UV]   Semi-planar 4:2:2 or 4:2:0 (default for 4:2:0)
    public const int NVCV_YCVU = 9;         // [Y][VU]   Semi-planar 4:2:2 or 4:2:0

    public const int NVCV_CPU = 0;          // The buffer is stored in CPU memory.
    public const int NVCV_GPU = 1;          // The buffer is stored in CUDA memory.
    public const int NVCV_CUDA = 1;         // The buffer is stored in CUDA memory.
    public const int NVCV_CPU_PINNED = 2;   // The buffer is stored in pinned CPU memory.
    public const int NVCV_CUDA_ARRAY = 3;   // A CUDA array is used for storage.

    // ----------
    // AR
    // ----------

    public const string NvAR_Feature_FaceBoxDetection = "FaceBoxDetection";
    public const string NvAR_Feature_FaceDetection = "FaceDetection";
    public const string NvAR_Feature_LandmarkDetection = "LandmarkDetection";
    public const string NvAR_Feature_Face3DReconstruction = "Face3DReconstruction";
    public const string NvAR_Feature_BodyDetection = "BodyDetection";
    public const string NvAR_Feature_BodyPoseEstimation = "BodyPoseEstimation";
    public const string NvAR_Feature_GazeRedirection = "GazeRedirection";
    public const string NvAR_Feature_FaceExpressions = "FaceExpressions";

    public static string NvAR_Parameter_Input(string name) => $"NvAR_Parameter_Input_{name}";
    public static string NvAR_Parameter_Output(string name) => $"NvAR_Parameter_Output_{name}";
    public static string NvAR_Parameter_Config(string name) => $"NvAR_Parameter_Config_{name}";
    public static string NvAR_Parameter_InOut(string name) => $"NvAR_Parameter_InOut_{name}";

    public const uint NVAR_TEMPORAL_FILTER_FACE_BOX = (1U << 0);                // 0x001
    public const uint NVAR_TEMPORAL_FILTER_FACIAL_LANDMARKS = (1U << 1);        // 0x002
    public const uint NVAR_TEMPORAL_FILTER_FACE_ROTATIONAL_POSE = (1U << 2);    // 0x004
    public const uint NVAR_TEMPORAL_FILTER_FACIAL_EXPRESSIONS = (1U << 4);      // 0x010
    public const uint NVAR_TEMPORAL_FILTER_FACIAL_GAZE = (1U << 5);             // 0x020
    public const uint NVAR_TEMPORAL_FILTER_ENHANCE_EXPRESSIONS = (1U << 8);     // 0x100
}