using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public Text noteText;
    public int fret;
    public int stringNum;
    public bool aH;

    public GameObject pH;

    public void SetNote(int position, float interval, int fret, int stringNum, bool aH, int pH)
    {
        if (noteText != null)
        {
            this.aH = aH;

            string text = "";
            if (aH)
            {
                text += "<";
            }
            text = fret.ToString();
            if (aH)
            {
                text += ">";
            }

            noteText.text = text;
            this.stringNum = stringNum;

            RectTransform rect = gameObject.GetComponent<RectTransform>();
            Vector2 pos = rect.anchoredPosition;
            pos.x = -115f + ((float)position - 1) * interval;
            pos.y = (float)((stringNum - 1) * 10);
            rect.anchoredPosition = pos;
        }
    }
}
