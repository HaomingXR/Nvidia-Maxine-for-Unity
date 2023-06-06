using UnityEngine;

public class HeadTracking
{
    /// <summary>
    /// Head Tracking Auto-Focus
    /// </summary>
    /// <param name="input">Input WebCam Texture</param>
    /// <param name="origin">The center coordinate of the Head</param>
    /// <param name="size">The width and height of desired tracking</param>
    /// <returns>Cropped part of the auto-focused head</returns>
    public static void AutoFocus(ref Texture2D output, WebCamTexture input, Vector2 origin, Vector2 size)
    {
        RenderTexture renderTexture = new RenderTexture(input.width, input.height, 0);
        RenderTexture.active = renderTexture;

        output.Reinitialize((int)size.x, (int)size.y);
        Graphics.Blit(input, renderTexture);

        output.ReadPixels(new Rect(origin.x, origin.y, size.x, size.y), 0, 0);
        output.Apply();

        RenderTexture.active = null;
        renderTexture.Release();
    }
}