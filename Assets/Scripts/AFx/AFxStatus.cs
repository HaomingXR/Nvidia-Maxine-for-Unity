namespace MaxineAFX
{
    public class AFxStatus
    {
        public static void Catch(int code)
        {
            if (code != NVAFX_STATUS_SUCCESS)
                UnityEngine.Debug.LogError(GetErrorCode(code));
        }

        public static string GetErrorCode(int i)
        {
            switch (i)
            {
                case 1:
                    return "Failure";
                case 2:
                    return "Handle invalid";
                case 3:
                    return "Parameter value invalid";
                case 4:
                    return "Parameter value immutable";
                case 5:
                    return "Insufficient data to process";
                case 6:
                    return "Effect not supported";
                case 7:
                    return "Given buffer length too small to hold requested data";
                case 8:
                    return "Model file could not be loaded";
                case 9:
                    return "(32 bit SDK only) COM server was not registered; please see user manual for details";
                case 10:
                    return "(32 bit SDK only) COM operation failed";
                case 11:
                    return "The selected GPU is not supported. The SDK requires Turing and above GPU with Tensor cores";
                case 12:
                    return "Cuda Context Failure Error";
                default:
                    return "Success";
            }
        }

        public const int NVAFX_STATUS_SUCCESS = 0;
        public const int NVAFX_STATUS_FAILED = 1;
        public const int NVAFX_STATUS_INVALID_HANDLE = 2;
        public const int NVAFX_STATUS_INVALID_PARAM = 3;
        public const int NVAFX_STATUS_IMMUTABLE_PARAM = 4;
        public const int NVAFX_STATUS_INSUFFICIENT_DATA = 5;
        public const int NVAFX_STATUS_EFFECT_NOT_AVAILABLE = 6;
        public const int NVAFX_STATUS_OUTPUT_BUFFER_TOO_SMALL = 7;
        public const int NVAFX_STATUS_MODEL_LOAD_FAILED = 8;

        public const int NVAFX_STATUS_32_SERVER_NOT_REGISTERED = 9;
        public const int NVAFX_STATUS_32_COM_ERROR = 10;
        public const int NVAFX_STATUS_GPU_UNSUPPORTED = 11;
        public const int NVAFX_STATUS_CUDA_CONTEXT_CREATION_FAILED = 12;
    }
}