using MaxineAFX;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DemoAFx : MonoBehaviour
{
    private float[] clipBuffer;
    private float[] fxBuffer;
    private bool effectAvailable;
    private bool isRecording;

    [Header("Demo")]
    [SerializeField, Range(0.5f, 10.0f)]
    private float volumeGain;
    [SerializeField]
    private bool useEffect;

    private AudioSource source;
    private AudioClip micClip;
    private AudioClip processedClip;
    private float recTime;

    private const int ClipDuration = 5;
    private const int Mono = 1;

    [Header("UI")]
    [SerializeField]
    private Image indicator;

    void Awake()
    {
        if (Microphone.devices.Length < 1)
        {
            Debug.LogWarning("No Microphone Detected!");
            effectAvailable = false;
            return;
        }

        AFxApp.Init();

        clipBuffer = new float[ClipDuration * AFxConfig.SAMPLE_RATE];
        fxBuffer = new float[AFxConfig.NUM_INPUT_SAMPLE];
        effectAvailable = true;
        isRecording = false;
        source = GetComponent<AudioSource>();
        processedClip = AudioClip.Create("output", ClipDuration * AFxConfig.SAMPLE_RATE, Mono, AFxConfig.SAMPLE_RATE, false);
        recTime = -1.0f;
    }

    private void StartRecording()
    {
        micClip = Microphone.Start(null, false, ClipDuration, AFxConfig.SAMPLE_RATE);
        recTime = Time.time;
        isRecording = true;
    }

    private void StopRecording()
    {
        Microphone.End(null);

        micClip.GetData(clipBuffer, 0);
        Denoise(ref clipBuffer);
        processedClip.SetData(clipBuffer, 0);

        source.PlayOneShot(processedClip);
        isRecording = false;
    }

    void Update()
    {
        if (!effectAvailable || !AFxApp.READY)
            return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isRecording)
                StartRecording();
            else
                StopRecording();
        }

        if (isRecording && Time.time - ClipDuration > recTime)
            StopRecording();

        indicator.color = isRecording ? Color.green : Color.red;
    }

    private int num_of_samples;
    private int num_of_frames;
    private float[] sample_channel;

    private void Init(ref float[] data)
    {
        num_of_samples = data.Length;
        num_of_frames = Mathf.CeilToInt((float)num_of_samples / AFxConfig.NUM_INPUT_SAMPLE);
        sample_channel = new float[num_of_frames * AFxConfig.NUM_INPUT_SAMPLE];
    }

    private void Denoise(ref float[] data)
    {
        if (!useEffect)
            return;

        Init(ref data);

        for (int i = 0; i < num_of_frames * AFxConfig.NUM_INPUT_SAMPLE; i++)
            sample_channel[i] = (i < num_of_samples) ? Mathf.Clamp(data[i] * volumeGain, -1.0f, 1.0f) : 0.0f;

        for (int i = 0; i < num_of_frames; i++)
        {
            int startIndex = (int)(i * AFxConfig.NUM_INPUT_SAMPLE);

            Array.Copy(sample_channel, startIndex, fxBuffer, 0, AFxConfig.NUM_INPUT_SAMPLE);
            AFxApp.Run(ref fxBuffer);
            Array.Copy(fxBuffer, 0, sample_channel, startIndex, AFxConfig.NUM_INPUT_SAMPLE);
        }

        for (int i = 0; i < data.Length; i++)
            data[i] = sample_channel[i];
    }

    void OnDisable()
    {
        Microphone.End(null);
        AFxApp.Garbage();
    }
}