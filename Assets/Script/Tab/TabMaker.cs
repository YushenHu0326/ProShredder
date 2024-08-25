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
    public GameObject symbol1Object;
    public GameObject symbol2Object;
    public GameObject symbol3Object;
    public GameObject symbol4Object;
    public GameObject symbol5Object;
    public GameObject symbol6Object;
    public GameObject symbol7Object;

    Section section;

    int sectionIndex;
    int subsection;
    int position;
    int fret;
    int stringNum;

    int totalString;

    bool isEditing;

    InputField[] inputFields;

    public Text writeButton;
    string inputStr;
    GameObject lastSymbolAdded;

    float sectionLength;

    public Image pointer;
    RectTransform pointerRect;

    int defaultDivision;
    int currentDivision;

    void Start()
    {
        stringNum = 1;
        position = 1;

        totalString = 6;

        defaultDivision = 8;

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

        inputFields = FindObjectsOfType(typeof(InputField)) as InputField[];
    }

    void Update()
    {
        if (pointerRect && tab)
        {
            section = tab.GetSection(sectionIndex);
            Vector2 pos = new Vector2();
            pos.x = ((sectionLength - 20f) / (float)(section.division - 1)) * (float)(position - 1);
            pos.y = -10f * (stringNum - 1);

            pointerRect.anchoredPosition = pos;
        }

        if (inputFields.Length > 0)
        {
            bool focused = false;
            foreach (InputField inputField in inputFields)
            {
                if (inputField.isFocused)
                {
                    focused = true;
                }
            }

            if (isEditing && !focused)
            {
                if (Input.GetKeyDown("1"))
                {
                    inputStr += "1";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("2"))
                {
                    inputStr += "2";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("3"))
                {
                    inputStr += "3";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("4"))
                {
                    inputStr += "4";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("5"))
                {
                    inputStr += "5";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("6"))
                {
                    inputStr += "6";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("7"))
                {
                    inputStr += "7";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("8"))
                {
                    inputStr += "8";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("9"))
                {
                    inputStr += "9";
                    SetCurrentFret();
                }
                if (Input.GetKeyDown("0"))
                {
                    inputStr += "0";
                    SetCurrentFret();
                }

                if (Input.GetKeyDown("x"))
                {
                    inputStr = "x";
                    SetCurrentFret();
                }

                if (Input.GetKeyDown("backspace"))
                {
                    if (inputStr.Length > 1)
                    {
                        inputStr = inputStr.Substring(0, inputStr.Length - 1);
                        SetCurrentFret();
                    }
                    else
                    {
                        ClearNote();
                    }
                }
            }
        }

        if (tab != null)
        {
            currentDivision = tab.GetSection(sectionIndex).division;
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
        lastSymbolAdded = null;
        inputStr = "";
    }

    public void MoveRight()
    {
        position += 1;
        if (position > currentDivision)
        {
            position = 1;
            sectionIndex += 1;

            if (sectionIndex + 1 > tab.GetSectionTotal())
            {
                tab.AddSection();
            }
        }

        tab.CycleSection(sectionIndex);
        lastSymbolAdded = null;
        inputStr = "";
    }

    public void MoveUp()
    {
        if (stringNum > 1)
        {
            stringNum -= 1;
            lastSymbolAdded = null;
            inputStr = "";
        }
    }

    public void MoveDown()
    {
        if (stringNum < totalString)
        {
            stringNum += 1;
            lastSymbolAdded = null;
            inputStr = "";
        }
    }

    public void SetCurrentFret()
    {
        int result;
        if (int.TryParse(inputStr, out result))
        {
            if (tab != null && noteObject != null)
            {
                Section currentSection = tab.GetSection(sectionIndex);
                if (mainSectionTransform != null && section != null)
                {
                    GameObject newNote = Instantiate(noteObject, mainSectionTransform.transform);
                    newNote.GetComponent<Note>().SetNote(position, (sectionLength - 20f) / (float)(section.division - 1), result, stringNum);
                    tab.AddNote(newNote, position, stringNum, sectionIndex);
                }
            }
        }
        else
        {
            if (inputStr.ToLower().Equals("x"))
            {
                GameObject newNote = Instantiate(noteObject, mainSectionTransform.transform);
                newNote.GetComponent<Note>().SetNote(position, (sectionLength - 20f) / (float)(section.division - 1), -1, stringNum);
                tab.AddNote(newNote, position, stringNum, sectionIndex);
                inputStr = "";
            }
        }
    }

    public void SetSubdivision(string division)
    {
        int result;
        if (tab != null && int.TryParse(division, out result))
        {
            if (result > 0 && result <= 32)
            {
                Section currentSection = tab.GetSection(sectionIndex);
                currentSection.division = result;
                currentSection.ResetNotePosition((sectionLength - 20f) / (float)(section.division - 1), defaultDivision / (float)result);
            }
        }
    }

    public void WriteNote()
    {
        isEditing = !isEditing;
        if (writeButton != null)
        {
            if (isEditing)
            {
                writeButton.text = "Stop";
            }
            else
            {
                writeButton.text = "Write";
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

    public void WriteSymbol1()
    {
        if (tab != null && symbol1Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol1Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void WriteSymbol2()
    {
        if (tab != null && symbol2Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol2Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void WriteSymbol3()
    {
        if (tab != null && symbol3Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol3Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void WriteSymbol4()
    {
        if (tab != null && symbol4Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol4Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void WriteSymbol5()
    {
        if (tab != null && symbol5Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol5Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void WriteSymbol6()
    {
        if (tab != null && symbol6Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol6Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void WriteSymbol7()
    {
        if (tab != null && symbol7Object != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            if (mainSectionTransform != null && section != null)
            {
                GameObject newSymbol = Instantiate(symbol7Object, mainSectionTransform.transform);
                newSymbol.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, (float)defaultDivision / (float)currentDivision);
                tab.AddSymbol(newSymbol, position, stringNum, sectionIndex);
                lastSymbolAdded = newSymbol;
            }
        }
    }

    public void ReduceSymbolSpan()
    {
        if (lastSymbolAdded != null)
        {
            float span = lastSymbolAdded.GetComponent<Symbol>().currentSymbolSpan;
            span -= 1f;
            if (span < 1f)
                span = 1f;

            lastSymbolAdded.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, span);
        }
    }

    public void AddSymbolSpan()
    {
        if (lastSymbolAdded != null)
        {
            float span = lastSymbolAdded.GetComponent<Symbol>().currentSymbolSpan;
            span += 1f;
            lastSymbolAdded.GetComponent<Symbol>().SetSymbol(position, (sectionLength - 20f) / (float)(section.division - 1), stringNum, span);
        }
    }

    public void ClearSymbol()
    {
        if (tab != null)
        {
            tab.DeleteSymbol(position, stringNum, sectionIndex);
        }
    }
}
