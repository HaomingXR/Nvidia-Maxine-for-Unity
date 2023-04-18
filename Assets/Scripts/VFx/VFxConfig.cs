namespace MaxineVFX
{
    public class VFxConfig
    {
        public static string MODEL_DIR { get { return UnityEngine.Application.streamingAssetsPath; } }
        public const uint MODE = 0u;
        /// 0 - Quality
        /// 1 - Performance

        public const uint True = 1u;
        public const uint False = 0u;

        public const uint StreamNumber = 1u;
        public const uint Width = 1920u;
        public const uint Height = 1080u;
        public const int FPS = 24;
    }
}