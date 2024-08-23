using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    public Image symbol;
    public int symbolSpan;
    public float width, height;
    public float offsetX, offsetY;

    public void SetSymbol(int position, float interval, int fret, int stringNum, int span)
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.x = -115f + ((float)position - 1) * interval;
        pos.y = (float)((stringNum - 1) * 10);
        pos.x += offsetX;
        pos.y += offsetY;

        if (symbolSpan > 0 && span > 1)
        {
            float newWidth = width * (float)span;
            Vector2 size = rect.sizeDelta;
            size.x = newWidth;
            rect.sizeDelta = size;
            pos.x += (newWidth - newWidth) / 2;
        }
        rect.anchoredPosition = pos;
    }
}
