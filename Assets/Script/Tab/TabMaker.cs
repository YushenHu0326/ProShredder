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

    public GameObject mainSectionTransform;
    public GameObject previousSectionTransform;
    public GameObject nextSectionTransform;

    public GameObject noteObject;

    Section section;

    int sectionIndex;
    int subsection;
    int position;
    int fret;
    int stringNum;

    int totalString;

    float sectionLength;

    public Image pointer;
    RectTransform pointerRect;

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

        if (pointer && mainSectionTransform)
        {
            pointer.transform.SetParent(mainSectionTransform.transform);
            pointerRect = pointer.GetComponent<RectTransform>();
            if (pointerRect)
            {
                pointerRect.anchoredPosition = new Vector2();
            }
        }

        if (tab != null)
        {
            tab.CycleSection(sectionIndex);
        }
    }

    void Update()
    {
        if (pointerRect && tab)
        {
            section = tab.GetSection(sectionIndex);
            Vector2 pos = new Vector2();
            pos.x = ((sectionLength - 10f) / (float)section.division) * (float)(position - 1);
            pos.y = -10f * (stringNum - 1);

            pointerRect.anchoredPosition = pos;
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

        tab.CycleSection(sectionIndex);
    }

    public void MoveRight()
    {
        int division = tab.GetSectionDivision(sectionIndex);

        position += 1;
        if (position > division)
        {
            position = 1;
            sectionIndex += 1;

            if (sectionIndex + 1 > tab.GetSectionTotal())
            {
                tab.AddSection();
            }
        }

        tab.CycleSection(sectionIndex);
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

    public void SetCurrentFret(string note)
    {
        int result;
        if (int.TryParse(note, out result))
        {
            fret = result;
        }
    }

    public void WriteNote()
    {
        if (tab != null && noteObject != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newNote = Instantiate(noteObject, mainSectionTransform.transform);
                newNote.GetComponent<Note>().SetNote(position, (sectionLength - 10f) / (float)section.division, fret, stringNum);
                tab.AddNote(newNote, position, stringNum, sectionIndex);
            }
        }
    }

    public void ClearNote()
    {
        if (tab != null)
        {
            tab.DeleteNote(position, stringNum, sectionIndex);
        }
    }

    public void SetNoteAH()
    {
        if (tab != null)
        {
            tab.SetNoteAH(position, stringNum, sectionIndex);
        }
    }

    public void SetNotePH(string fret)
    {
        if (fret.Length == 0 && tab != null)
        {
            tab.SetNotePH(position, stringNum, -1, sectionIndex);
            return;
        }

        int fretNum;

        if (tab != null && int.TryParse(fret, out fretNum))
        {
            tab.SetNotePH(position, stringNum, fretNum, sectionIndex);
        }
    }
}
