using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TabMaker : MonoBehaviour
{
    [System.Serializable]
    public class TabData
    {
        public List<NoteData> notes;
        public List<SymbolData> symbols;
        public int position;
        public int stringNum;
        public int totalString;
        public int totalSections;

        public int currentSection;

        public List<int> bpms;
        public List<int> divisions;
    }

    [System.Serializable]
    public class NoteData 
    {
        public int section;
        public int position;
        public int fret;
        public int stringNum;
        public bool aH;
        public int pH;
    }

    [System.Serializable]
    public class SymbolData
    {
        public int section;
        public int symbolType;
        public int position;
        public int stringNum;
        public float span;
    }

    public Tab tab;

    string tabName;
    string loadTabName;

    public GameObject mainSection;
    public GameObject previousSection;
    public GameObject nextSection;

    public GameObject mainSectionTransform;
    public GameObject previousSectionTransform;
    public GameObject nextSectionTransform;

    public GameObject[] mainSectionStrings;
    public GameObject[] previousSectionStrings;
    public GameObject[] nextSectionStrings;

    public Text bpmIndicator;

    public GameObject noteObject;
    public GameObject symbol1Object;
    public GameObject symbol2Object;
    public GameObject symbol3Object;
    public GameObject symbol4Object;
    public GameObject symbol5Object;
    public GameObject symbol6Object;
    public GameObject symbol7Object;

    Section section;
    int bpm;

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

    int currentDivision;

    Note selectedNote;

    public bool interactiveMode;

    void Start()
    {
        tabName = "tab";
        loadTabName = "tab";

        stringNum = 1;
        position = 1;

        totalString = 5;
        SetStrings();

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
            bpm = tab.GetSectionBPM(sectionIndex);
        }

        inputFields = FindObjectsOfType(typeof(InputField)) as InputField[];

        interactiveMode = true;
    }

    void Update()
    {
        if (pointerRect && tab)
        {
            section = tab.GetSection(sectionIndex);
            Vector2 pos = new Vector2();
            pos.x = (sectionLength / (float)section.division) * ((float)position - 0.5f);
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
                    SetNote();
                }
                if (Input.GetKeyDown("2"))
                {
                    inputStr += "2";
                    SetNote();
                }
                if (Input.GetKeyDown("3"))
                {
                    inputStr += "3";
                    SetNote();
                }
                if (Input.GetKeyDown("4"))
                {
                    inputStr += "4";
                    SetNote();
                }
                if (Input.GetKeyDown("5"))
                {
                    inputStr += "5";
                    SetNote();
                }
                if (Input.GetKeyDown("6"))
                {
                    inputStr += "6";
                    SetNote();
                }
                if (Input.GetKeyDown("7"))
                {
                    inputStr += "7";
                    SetNote();
                }
                if (Input.GetKeyDown("8"))
                {
                    inputStr += "8";
                    SetNote();
                }
                if (Input.GetKeyDown("9"))
                {
                    inputStr += "9";
                    SetNote();
                }
                if (Input.GetKeyDown("0"))
                {
                    inputStr += "0";
                    SetNote();
                }

                if (Input.GetKeyDown("x"))
                {
                    inputStr = "x";
                    SetNote();
                }

                if (Input.GetKeyDown("backspace"))
                {
                    if (inputStr.Length > 1)
                    {
                        inputStr = inputStr.Substring(0, inputStr.Length - 1);
                        SetNote();
                    }
                    else
                    {
                        inputStr = "";
                        ClearNote();
                    }
                }
            }

            if (!focused)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveLeft();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveRight();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveUp();
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveDown();
                }
            }
        }

        if (tab != null)
        {
            Section currentSection = tab.GetSection(sectionIndex);
            currentDivision = currentSection.division;

            Note newSelectedNote = currentSection.GetNote(position, stringNum);

            if (selectedNote != null && selectedNote != newSelectedNote)
            {
                selectedNote.OnNoteDeselected();
            }

            if (newSelectedNote != null && interactiveMode)
            {
                selectedNote = newSelectedNote;
                selectedNote.OnNoteSelected();
            }

            if (bpmIndicator != null)
            {
                bpmIndicator.text = "BPM " + tab.GetSectionBPM(sectionIndex);
            }
        }
    }

    public void SaveTab()
    {
        TabData tabData = new TabData();

        if (tab != null)
        {
            List<NoteData> noteDataList = new List<NoteData>();
            List<SymbolData> symbolDataList = new List<SymbolData>();

            List<int> bpms = new List<int>();
            List<int> divisions = new List<int>();

            for (int i = 0; i < tab.GetSectionTotal(); i++)
            {
                List<Note> notes = tab.GetAllNotes(i);
                List<Symbol> symbols = tab.GetAllSymbols(i);

                foreach (Note note in notes)
                {
                    NoteData noteData = new NoteData();
                    noteData.section = i;
                    noteData.position = note.localPosition;
                    noteData.fret = note.fret;
                    noteData.stringNum = note.stringNum;
                    noteData.aH = note.aH;
                    noteData.pH = note.pHFret;

                    noteDataList.Add(noteData);
                }

                foreach (Symbol symbol in symbols)
                {
                    SymbolData symbolData = new SymbolData();
                    symbolData.section = i;
                    symbolData.symbolType = symbol.symbolID;
                    symbolData.position = symbol.localPosition;
                    symbolData.stringNum = symbol.stringNum;
                    symbolData.span = symbol.currentSymbolSpan;

                    symbolDataList.Add(symbolData);
                }

                bpms.Add(tab.GetSectionBPM(i));
                divisions.Add(tab.GetSection(i).division);
            }

            tabData.notes = noteDataList;
            tabData.symbols = symbolDataList;
            tabData.position = position;
            tabData.stringNum = stringNum;
            tabData.totalString = totalString;
            tabData.totalSections = tab.GetSectionTotal();
            tabData.bpms = bpms;
            tabData.divisions = divisions;

            tabData.currentSection = sectionIndex;

            string tabStr = JsonUtility.ToJson(tabData);
            System.IO.Directory.CreateDirectory(Application.dataPath + "/Saved/Tab");
            System.IO.File.WriteAllText(Application.dataPath + "/Saved/Tab/" + tabName + ".json", tabStr);
        }
    }

    public void LoadTab()
    {
        if (tab != null)
        {
            tab.ResetTab();
            string tabDataStr = File.ReadAllText(Application.dataPath + "/Saved/Tab/" + loadTabName + ".json");
            TabData tabData = JsonUtility.FromJson<TabData>(tabDataStr);

            for (int i = 1; i < tabData.totalSections; i++)
            {
                tab.AddSection(tabData.bpms[i]);
            }

            foreach (NoteData noteData in tabData.notes)
            {
                Section currentSection = tab.GetSection(noteData.section);
                if (mainSectionTransform != null && noteObject != null)
                {
                    GameObject newNote = Instantiate(noteObject, mainSectionTransform.transform);
                    newNote.GetComponent<Note>().SetNote(noteData.position, sectionLength / (float)tabData.divisions[noteData.section], noteData.fret, noteData.stringNum);
                    tab.AddNote(newNote, noteData.position, noteData.stringNum, noteData.section);
                }
            }

            foreach (SymbolData symbolData in tabData.symbols)
            {
                Section currentSection = tab.GetSection(symbolData.section);

                if (mainSectionTransform != null)
                {
                    sectionIndex = symbolData.section;
                    position = symbolData.position;
                    stringNum = symbolData.stringNum;

                    if (symbolData.symbolType == 1) WriteSymbol1();
                    if (symbolData.symbolType == 2) WriteSymbol2();
                    if (symbolData.symbolType == 3) WriteSymbol3();
                    if (symbolData.symbolType == 4) WriteSymbol4();
                    if (symbolData.symbolType == 5) WriteSymbol5();
                    if (symbolData.symbolType == 6) WriteSymbol6();
                    if (symbolData.symbolType == 7) WriteSymbol7();

                    if (symbolData.span > 1f)
                    {
                        for (int i = 1; i < (int)symbolData.span; i++)
                        {
                            AddSymbolSpan();
                        }
                    }
                }
            }

            totalString = tabData.totalString;
            SetStrings();

            tab.CycleSection(tabData.currentSection);
            tab.UpdateSectionDisplay(tabData.currentSection, mainSectionTransform.transform, previousSectionTransform.transform, nextSectionTransform.transform);
            bpm = tab.GetSectionBPM(tabData.currentSection);

            stringNum = tabData.stringNum;
            position = tabData.position;

            sectionIndex = tabData.currentSection;
        }
    }

    public void SetSaveTabName(string name)
    {
        tabName = name;
    }

    void SetStrings()
    {
        for (int i = 0; i < totalString; i++)
        {
            if (i < mainSectionStrings.Length) mainSectionStrings[i].SetActive(true);
            if (i < previousSectionStrings.Length) previousSectionStrings[i].SetActive(true);
            if (i < nextSectionStrings.Length) nextSectionStrings[i].SetActive(true);
        }

        for (int i = totalString; i < mainSectionStrings.Length; i++)
        {
            mainSectionStrings[i].SetActive(false);
            previousSectionStrings[i].SetActive(false);
            nextSectionStrings[i].SetActive(false);

            if (tab != null) tab.UnloadString(i);
        }
    }

    public void AddStringNum()
    {
        if (totalString < mainSectionStrings.Length)
            totalString += 1;

        SetStrings();
    }

    public void DeleteStringNum()
    {
        if (totalString - 1 > 0)
            totalString -= 1;

        SetStrings();
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
                position = 1;
            }
        }

        tab.CycleSection(sectionIndex);
        tab.UpdateSectionDisplay(sectionIndex, mainSectionTransform.transform, previousSectionTransform.transform, nextSectionTransform.transform);
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
                tab.AddSection(bpm);
            }
        }

        tab.CycleSection(sectionIndex);
        tab.UpdateSectionDisplay(sectionIndex, mainSectionTransform.transform, previousSectionTransform.transform, nextSectionTransform.transform);
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

    public void SetBPM(string bpm)
    {
        int result;
        if (int.TryParse(bpm, out result) && tab != null)
        {
            this.bpm = result;
            tab.SetSectionBPM(sectionIndex, result);
        }
    }

    public void SetNote()
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
                    newNote.GetComponent<Note>().SetNote(position, sectionLength / (float)section.division, result, stringNum);
                    tab.AddNote(newNote, position, stringNum, sectionIndex);
                }
            }
        }
        else
        {
            if (inputStr.ToLower().Equals("x"))
            {
                if (mainSectionTransform != null && section != null)
                {
                    GameObject newNote = Instantiate(noteObject, mainSectionTransform.transform);
                    newNote.GetComponent<Note>().SetNote(position, sectionLength / (float)section.division, -1, stringNum);
                    tab.AddNote(newNote, position, stringNum, sectionIndex);
                    inputStr = "";
                }
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
                int previousDivision = currentDivision;
                currentSection.division = result;
                currentSection.ResetNotePosition(sectionLength / (float)section.division, previousDivision, result);
                position = Mathf.RoundToInt((float)(position - 1) * (float)result / (float)currentDivision) + 1;
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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
                newSymbol.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, 1f);
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

            lastSymbolAdded.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, span);
        }
    }

    public void AddSymbolSpan()
    {
        if (lastSymbolAdded != null)
        {
            float span = lastSymbolAdded.GetComponent<Symbol>().currentSymbolSpan;
            span += 1f;
            lastSymbolAdded.GetComponent<Symbol>().SetSymbol(position, sectionLength / (float)section.division, stringNum, span);
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
