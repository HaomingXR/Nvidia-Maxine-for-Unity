public class NvCVStatus
{
    public static void Catch(int code)
    {
        if (code != NVCV_SUCCESS)
            UnityEngine.Debug.LogError(GetErrorCode(code));
    }

    public static string GetErrorCode(int i)
    {
        switch (i)
        {
            case NVCV_ERR_GENERAL: return "An otherwise unspecified error has occurred.";
            case NVCV_ERR_UNIMPLEMENTED: return "The requested feature is not yet implemented.";
            case NVCV_ERR_MEMORY: return "There is not enough memory for the requested operation.";
            case NVCV_ERR_EFFECT: return "An invalid effect handle has been supplied.";
            case NVCV_ERR_SELECTOR: return "The given parameter selector is not valid in this effect filter.";
            case NVCV_ERR_BUFFER: return "An image buffer has not been specified.";
            case NVCV_ERR_PARAMETER: return "An invalid parameter value has been supplied for this effect+selector.";
            case NVCV_ERR_MISMATCH: return "Some parameters are not appropriately matched.";
            case NVCV_ERR_PIXELFORMAT: return "The specified pixel format is not accommodated.";
            case NVCV_ERR_MODEL: return "Error while loading the TRT model.";
            case NVCV_ERR_LIBRARY: return "Error loading the dynamic library.";
            case NVCV_ERR_INITIALIZATION: return "The effect has not been properly initialized.";
            case NVCV_ERR_FILE: return "The file could not be found.";
            case NVCV_ERR_FEATURENOTFOUND: return "The requested feature was not found.";
            case NVCV_ERR_MISSINGINPUT: return "A required parameter was not set.";
            case NVCV_ERR_RESOLUTION: return "The specified image resolution is not supported.";
            case NVCV_ERR_UNSUPPORTEDGPU: return "The GPU is not supported.";
            case NVCV_ERR_WRONGGPU: return "The current GPU is not the one selected.";
            case NVCV_ERR_UNSUPPORTEDDRIVER: return "The currently installed graphics driver is not supported.";
            case NVCV_ERR_MODELDEPENDENCIES: return "There is no model with dependencies that match this system.";
            case NVCV_ERR_PARSE: return "There has been a parsing or syntax error while reading a file.";
            case NVCV_ERR_MODELSUBSTITUTION: return "The specified model does not exist and has been substituted.";
            case NVCV_ERR_READ: return "An error occurred while reading a file.";
            case NVCV_ERR_WRITE: return "An error occurred while writing a file.";
            case NVCV_ERR_PARAMREADONLY: return "The selected parameter is read-only.";
            case NVCV_ERR_TRT_ENQUEUE: return "TensorRT enqueue failed.";
            case NVCV_ERR_TRT_BINDINGS: return "Unexpected TensorRT bindings.";
            case NVCV_ERR_TRT_CONTEXT: return "An error occurred while creating a TensorRT context.";
            case NVCV_ERR_TRT_INFER: return "The was a problem creating the inference engine.";
            case NVCV_ERR_TRT_ENGINE: return "There was a problem deserializing the inference runtime engine.";
            case NVCV_ERR_NPP: return "An error has occurred in the NPP library.";
            case NVCV_ERR_CONFIG: return "No suitable model exists for the specified parameter configuration.";
            case NVCV_ERR_TOOSMALL: return "A supplied parameter or buffer is not large enough.";
            case NVCV_ERR_TOOBIG: return "A supplied parameter is too big.";
            case NVCV_ERR_WRONGSIZE: return "A supplied parameter is not the expected size.";
            case NVCV_ERR_OBJECTNOTFOUND: return "The specified object was not found.";
            case NVCV_ERR_SINGULAR: return "A mathematical singularity has been encountered.";
            case NVCV_ERR_NOTHINGRENDERED: return "Nothing was rendered in the specified region.";
            case NVCV_ERR_CONVERGENCE: return "An iteration did not converge satisfactorily.";
            case NVCV_ERR_OPENGL: return "An OpenGL error has occurred.";
            case NVCV_ERR_DIRECT3D: return "A Direct3D error has occurred.";
            case NVCV_ERR_CUDA_BASE: return "CUDA errors are offset from this value.";
            case NVCV_ERR_CUDA_VALUE: return "A CUDA parameter is not within the acceptable range.";
            case NVCV_ERR_CUDA_MEMORY: return "There is not enough CUDA memory for the requested operation.";
            case NVCV_ERR_CUDA_PITCH: return "A CUDA pitch is not within the acceptable range.";
            case NVCV_ERR_CUDA_INIT: return "The CUDA driver and runtime could not be initialized.";
            case NVCV_ERR_CUDA_LAUNCH: return "The CUDA kernel launch has failed.";
            case NVCV_ERR_CUDA_KERNEL: return "No suitable kernel image is available for the device.";
            case NVCV_ERR_CUDA_DRIVER: return "The installed NVIDIA CUDA driver is older than the CUDA runtime library.";
            case NVCV_ERR_CUDA_UNSUPPORTED: return "The CUDA operation is not supported on the current system or device.";
            case NVCV_ERR_CUDA_ILLEGAL_ADDRESS: return "CUDA tried to load or store on an invalid memory address.";
            case NVCV_ERR_CUDA: return "An otherwise unspecified CUDA error has been reported.";
            default:
                return "Success";
        }
    }

    public const int NVCV_SUCCESS = 0;
    public const int NVCV_ERR_GENERAL = -1;
    public const int NVCV_ERR_UNIMPLEMENTED = -2;
    public const int NVCV_ERR_MEMORY = -3;
    public const int NVCV_ERR_EFFECT = -4;
    public const int NVCV_ERR_SELECTOR = -5;
    public const int NVCV_ERR_BUFFER = -6;
    public const int NVCV_ERR_PARAMETER = -7;
    public const int NVCV_ERR_MISMATCH = -8;
    public const int NVCV_ERR_PIXELFORMAT = -9;
    public const int NVCV_ERR_MODEL = -10;
    public const int NVCV_ERR_LIBRARY = -11;
    public const int NVCV_ERR_INITIALIZATION = -12;
    public const int NVCV_ERR_FILE = -13;
    public const int NVCV_ERR_FEATURENOTFOUND = -14;
    public const int NVCV_ERR_MISSINGINPUT = -15;
    public const int NVCV_ERR_RESOLUTION = -16;
    public const int NVCV_ERR_UNSUPPORTEDGPU = -17;
    public const int NVCV_ERR_WRONGGPU = -18;
    public const int NVCV_ERR_UNSUPPORTEDDRIVER = -19;
    public const int NVCV_ERR_MODELDEPENDENCIES = -20;
    public const int NVCV_ERR_PARSE = -21;
    public const int NVCV_ERR_MODELSUBSTITUTION = -22;
    public const int NVCV_ERR_READ = -23;
    public const int NVCV_ERR_WRITE = -24;
    public const int NVCV_ERR_PARAMREADONLY = -25;
    public const int NVCV_ERR_TRT_ENQUEUE = -26;
    public const int NVCV_ERR_TRT_BINDINGS = -27;
    public const int NVCV_ERR_TRT_CONTEXT = -28;
    public const int NVCV_ERR_TRT_INFER = -29;
    public const int NVCV_ERR_TRT_ENGINE = -30;
    public const int NVCV_ERR_NPP = -31;
    public const int NVCV_ERR_CONFIG = -32;
    public const int NVCV_ERR_TOOSMALL = -33;
    public const int NVCV_ERR_TOOBIG = -34;
    public const int NVCV_ERR_WRONGSIZE = -35;
    public const int NVCV_ERR_OBJECTNOTFOUND = -36;
    public const int NVCV_ERR_SINGULAR = -37;
    public const int NVCV_ERR_NOTHINGRENDERED = -38;
    public const int NVCV_ERR_CONVERGENCE = -39;

    public const int NVCV_ERR_OPENGL = -98;
    public const int NVCV_ERR_DIRECT3D = -99;

    public const int NVCV_ERR_CUDA_BASE = -100;
    public const int NVCV_ERR_CUDA_VALUE = -101;
    public const int NVCV_ERR_CUDA_MEMORY = -102;
    public const int NVCV_ERR_CUDA_PITCH = -112;
    public const int NVCV_ERR_CUDA_INIT = -127;
    public const int NVCV_ERR_CUDA_LAUNCH = -819;
    public const int NVCV_ERR_CUDA_KERNEL = -309;
    public const int NVCV_ERR_CUDA_DRIVER = -135;
    public const int NVCV_ERR_CUDA_UNSUPPORTED = -901;
    public const int NVCV_ERR_CUDA_ILLEGAL_ADDRESS = -800;
    public const int NVCV_ERR_CUDA = -1099;
}