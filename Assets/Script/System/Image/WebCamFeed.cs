using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamFeed : MonoBehaviour
{
    WebCamTexture tex;

    // Start is called before the first frame update
    public bool StartVideo(string webCamName)
    {
        if (webCamName.Length == 0) return false;

        RawImage image = gameObject.GetComponent<RawImage>();
        if (image != null)
        {
            tex = new WebCamTexture(webCamName, 320, 240, 30);
            image.texture = tex;
            tex.Play();
            return true;
        }

        return false;
    }

    public bool StopVideo(string webCamName)
    {
        if (webCamName.Length == 0) return false;

        RawImage image = gameObject.GetComponent<RawImage>();
        if (image != null && tex != null)
        {
            tex.Stop();
            return true;
        }

        return false;
    }
}
