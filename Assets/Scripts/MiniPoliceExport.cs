using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Quicksave : MonoBehaviour
{
    private bool _called;
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _called = false;
        _time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_called)
        {
            if (_time == 0)
            {
                _time = Time.fixedTime + 1;
            }
            else if (Time.fixedTime > _time)
            {
                _called = true;
                var renderTexture = (RenderTexture) GetComponent<MeshRenderer>().material.mainTexture;
                File.WriteAllBytes("Assets/Textures/police.png", toTexture2D(renderTexture).EncodeToPNG());
                Debug.Log("Saved");
            }
        }
    }
    
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}