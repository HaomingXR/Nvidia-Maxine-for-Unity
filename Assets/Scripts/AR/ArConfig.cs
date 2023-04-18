namespace MaxineAR
{
    public class ArConfig
    {
        public static string MODEL_DIR { get { return UnityEngine.Application.streamingAssetsPath; } }

        public const uint Width = 1920u;
        public const uint Height = 1080u;
        public const int FPS = 24;
    }
}