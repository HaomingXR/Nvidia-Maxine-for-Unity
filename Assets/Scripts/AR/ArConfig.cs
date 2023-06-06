namespace MaxineAR
{
    public class ArConfig
    {
        public static string MODEL_DIR { get { return UnityEngine.Application.streamingAssetsPath; } }

        public const uint Width = 1280u;
        public const uint Height = 720u;
        public const int FPS = 30;
    }
}