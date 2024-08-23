using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMaker : MonoBehaviour
{
    public Tab tab;

    public GameObject mainSection;
    public GameObject previousSection;
    public GameObject nextSection;

    int sectionIndex;
    int subsection;
    int position;
    int fret;
    int stringNum;

    int totalString;

    float sectionLength;

    void Start()
    {
        stringNum = 1;
        position = 1;

        totalString = 6;

        if (mainSection != null)
        {
            RectTransform rect = mainSection.GetComponent<RectTransform>();
            sectionLength = rect.sizeDelta.x;
        }
    }

    public void MoveLeft()
    {
        position -= 1;

        if (position == 0)
        {
            if (sectionIndex > 0)
            {
                sectionIndex -= 1;
                position = tab.GetSectionDivision(sectionIndex);
            }
            else
            {
                position += 1;
            }
        }
    }

    public void MoveRight()
    {
        int division = tab.GetSectionDivision(sectionIndex);

        position += 1;
        if (position > division)
        {
            position = 0;
            sectionIndex += 1;

            if (sectionIndex + 1 > tab.GetSectionTotal())
            {
                tab.AddSection();
            }
        }
    }

    public void MoveUp()
    {
        if (stringNum > 1)
        {
            stringNum -= 1;
        }
    }

    public void MoveDown()
    {
        if (stringNum < totalString)
        {
            stringNum += 1;
        }
    }
}
