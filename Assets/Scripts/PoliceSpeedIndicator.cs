using System;
using UnityEngine;
using UnityEngine.UI;

public class PoliceSpeedIndicator : MonoBehaviour
{
    public Texture2DArray arrowTexArray;
    public int imageCount;

    private RawImage _rawImage;
    private int _currentImage;
    private bool _initialized;

    // Start is called before the first frame update
    void Start()
    {
        _rawImage = GetComponent<RawImage>();
        _rawImage.texture = new Texture2D(64, 64, arrowTexArray.format, arrowTexArray.mipmapCount, true);
        _currentImage = -1;
        _initialized = true;
        SetValue(0.0f);
    }

    /**
     * New value must be something between -1.f and 1.f
     */
    public void SetValue(float newValue)
    {
        if (_initialized)
        {
            int nextImage = Math.Min(imageCount - 1, Math.Max(0, (int) ((newValue + 1) * imageCount / 2)));
            if (nextImage != _currentImage)
            {
                _currentImage = nextImage;
                Graphics.CopyTexture(arrowTexArray, nextImage + 1, _rawImage.texture, 0);
            }
        }
    }
}
