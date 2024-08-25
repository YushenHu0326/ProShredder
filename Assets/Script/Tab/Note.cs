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
    public Text pH;

    public int localPosition;

    public void SetNote(int position, float interval, int fret, int stringNum)
    {
        localPosition = position;

        if (noteText != null)
        {
            string text = "";
            text = fret.ToString();
            if (fret == -1) text = "x";
            noteText.text = text;
            this.fret = fret;
            this.stringNum = stringNum;

            RectTransform rect = gameObject.GetComponent<RectTransform>();
            Vector2 pos = rect.anchoredPosition;
            pos.x = ((float)position - 1) * interval;
            pos.y = (float)((stringNum - 1) * -10);
            rect.anchoredPosition = pos;
        }
    }

    public void SetNoteAH()
    {
        aH = !aH;

        string text = "";

        if (aH)
        {
            text += "<";
        }

        text += fret.ToString();

        if (aH)
        {
            text += ">";
        }

        noteText.text = text;
    }

    public void SetNotePH(int fret)
    {
        if (fret < 0)
        {
            pH.text = "";
        }
        else{
            string text = "<";
            text += fret.ToString();
            text += ">";
            pH.text = text;
        }
    }
}
