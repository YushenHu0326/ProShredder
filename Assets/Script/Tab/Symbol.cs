using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    public Image symbol;
    public float symbolSpan;
    public float currentSymbolSpan;
    public float width, height;
    public float offsetX, offsetY;

    public int symbolType;

    public int localPosition;
    public int stringNum;

    public void SetSymbol(int position, float interval, int stringNum, float span)
    {
        localPosition = position;

        this.stringNum = stringNum;
        
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.x = ((float)position - 1) * interval;
        pos.y = (float)((stringNum - 1) * -10);
        pos.x += offsetX;
        pos.y += offsetY;

        if (symbolSpan > 0)
        {
            float newWidth = width * span;
            Vector2 size = rect.sizeDelta;
            size.x = newWidth;
            rect.sizeDelta = size;
            pos.x += (newWidth - width) / 2;
            currentSymbolSpan = span;
        }
        rect.anchoredPosition = pos;
    }
}
