using MaxineAR;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class DemoAR : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private Texture2D boxTexture;
    private bool useEffect;

    [SerializeField]
    private RawImage webcamDisplay;
    [SerializeField]
    private RawImage boxDisplay;

    private float x, y, w, h;
    private NativeArray<Color> colors;

    void Awake()
    {
        webcamTexture = new WebCamTexture((int)ArConfig.Width, (int)ArConfig.Height, ArConfig.FPS);
        boxTexture = new Texture2D((int)ArConfig.Width, (int)ArConfig.Height, TextureFormat.RGBA32, false);

        colors = new NativeArray<Color>(boxTexture.GetPixels(), Allocator.Persistent);

        useEffect = false;
        ArApp.Init();
    }

    void Start()
    {
        webcamTexture.Play();
        webcamDisplay.texture = webcamTexture;
    }

    void OnDisable()
    {
        colors.Dispose();
    }

    void Update()
    {
        if (!useEffect)
        {
            if (Input.GetKeyDown(KeyCode.Space) && ArApp.EFFECT_READY)
                useEffect = true;
            return;
        }

        (x, y, w, h) = ArApp.Run(webcamTexture);

        if (x < 0.0f)
        {
            boxDisplay.enabled = false;
            return;
        }

        MarkTextureJob job = new MarkTextureJob
        {
            x = x,
            y = y,
            w = w,
            h = h,
            colors = colors
        };

        JobHandle jobHandle = job.Schedule(colors.Length, 32);
        jobHandle.Complete();

        boxTexture.SetPixels(colors.ToArray());
        boxTexture.Apply();

        boxDisplay.texture = boxTexture;
        boxDisplay.enabled = true;
    }

    [BurstCompile]
    public struct MarkTextureJob : IJobParallelFor
    {
        [ReadOnly] public float x;
        [ReadOnly] public float y;
        [ReadOnly] public float w;
        [ReadOnly] public float h;

        public NativeArray<Color> colors;

        public void Execute(int index)
        {
            int col = index % (int)ArConfig.Width;
            int row = (int)ArConfig.Height - index / (int)ArConfig.Width;

            if (col >= x && col <= x + w && row >= y && row <= y + h)
                colors[index] = new Color(1.0f, 0.0f, 0.0f, 0.5f);
            else
                colors[index] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
    }
}