namespace MaxineAFX
{
    public class AFxConfig
    {
        public const int SAMPLE_RATE = 16000;
        public const float INTENSITY_RATIO = 1.0f;
        public const uint VAD = 0;
        public const uint NUM_INPUT_SAMPLE = 160;
        public static string MODEL_FILE { get { return UnityEngine.Application.streamingAssetsPath + "/denoiser_16k.trtpkg"; } }
    }
}