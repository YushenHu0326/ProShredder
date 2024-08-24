using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaController : MonoBehaviour
{
    public float desireAlpha;

    void Start()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            Color c = image.color;
            c.a = desireAlpha;
            image.color = c;
        }
    }
}
