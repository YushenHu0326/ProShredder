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
    public float offsetY;

    public int symbolType;
    public int symbolID;

    public int localPosition;
    public int stringNum;

    public void SetSymbol(int position, float interval, int stringNum, float span)
    {
        localPosition = position;

        this.stringNum = stringNum;
        
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.x = ((float)position - 0.5f) * interval;
        pos.y = (float)((stringNum - 1) * -10);
        
        pos.y += offsetY;

        if (symbolSpan > 0)
        {
            float newWidth = interval * span;
            Vector2 size = rect.sizeDelta;
            size.x = newWidth;
            rect.sizeDelta = size;
            pos.x += newWidth / 2f;
            currentSymbolSpan = span;
        }
        else
        {
            pos.x += width / 2f;
        }
        rect.anchoredPosition = pos;
    }
}
