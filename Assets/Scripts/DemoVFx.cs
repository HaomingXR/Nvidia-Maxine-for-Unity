using MaxineVFX;
using UnityEngine;

public class DemoVFx : MonoBehaviour
{
    private WebCamTexture webcam;
    private bool useEffect;

    void Awake()
    {
        Application.targetFrameRate = VFxConfig.FPS;

        webcam = new WebCamTexture((int)VFxConfig.Width, (int)VFxConfig.Height, VFxConfig.FPS);
        useEffect = false;
        VFxApp.Init();
    }

    void Start()
    {
        webcam.Play();
        GetComponent<Renderer>().material.mainTexture = webcam;
    }

    void Update()
    {
        if (!useEffect)
        {
            if (Input.GetKeyDown(KeyCode.Space) && VFxApp.EFFECT_READY)
                useEffect = true;
            return;
        }

        GetComponent<Renderer>().material.SetTexture("_Alpha", VFxApp.Run(webcam));
    }
}