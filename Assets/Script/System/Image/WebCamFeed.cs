using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamFeed : MonoBehaviour
{
    WebCamTexture tex;
    RawImage image;
    Color color;

    void Start()
    {
        image = gameObject.GetComponent<RawImage>();
        if (image != null)
        {
            color = image.color;
        }
    }

    // Start is called before the first frame update
    public bool StartVideo(string webCamName)
    {
        if (webCamName.Length == 0) return false;

        if (image != null)
        {
            tex = new WebCamTexture(webCamName, 700, 400, 30);
            image.texture = tex;
            image.color = Color.white;
            tex.Play();
            return true;
        }

        return false;
    }

    public bool StopVideo(string webCamName)
    {
        if (webCamName.Length == 0) return false;

        if (image != null && tex != null)
        {
            tex.Stop();
            image.texture = null;
            image.color = color;
            return true;
        }

        return false;
    }
}
