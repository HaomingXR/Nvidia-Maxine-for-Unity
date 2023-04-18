using UnityEngine;

public class FramerateLimit : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 24;
    }

    void Start()
    {
        Application.targetFrameRate = 24;
    }
}