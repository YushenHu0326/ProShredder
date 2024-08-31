using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    ExternalAudioPlayer player;

    List<Section> sections;
    List<int> bpms;
    List<string> annotations;

    public Section section;

    public GameObject mainSection;
    public GameObject previousSection;
    public GameObject nextSection;

    public GameObject bpmIndicator;

    // Start is called before the first frame update
    void Awake()
    {
        ExternalAudioPlayer[] players = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];

        if (players.Length > 0)
        {
            player = players[0];
        }

        ResetTab();
    }

    public void ResetTab()
    {
        Note[] notes = FindObjectsOfType(typeof(Note)) as Note[];
        Symbol[] symbols = FindObjectsOfType(typeof(Symbol)) as Symbol[];
        Section[] currentSections = FindObjectsOfType(typeof(Section)) as Section[];

        foreach (Section section in currentSections)
        {
            Destroy(section.gameObject);
        }

        foreach (Note note in notes)
        {
            Destroy(note.gameObject);
        }

        foreach (Symbol symbol in symbols)
        {
            Destroy(symbol.gameObject);
        }

        sections = new List<Section>();

        Section sectionComp = Instantiate(section).GetComponent<Section>();
        sectionComp.division = 8;
        sectionComp.timeSignatureLower = 4;
        sectionComp.timeSignatureUpper = 4;
        sections.Add(sectionComp);

        bpms = new List<int>() { 120 };
        annotations = new List<string>() { "" };
    }

    public void AddNote(GameObject note, int position, int stringNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].AddNote(note, position, stringNum);
        }
    }

    public void DeleteNote(int position, int stringNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].DeleteNote(position, stringNum);
        }
    }

    public void UnloadString(int stringIndex)
    {
        foreach (Section section in sections)
        {
            section.UnloadString(stringIndex);
        }
    }

    public void SetNoteAH(int position, int stringNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].SetNoteAH(position, stringNum);
        }
    }

    public void SetNotePH(int position, int stringNum, int fretNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].SetNotePH(position, stringNum, fretNum);
        }
    }

    public void AddSymbol(GameObject symbol, int position, int stringNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].AddSymbol(symbol, position, stringNum);
        }
    }

    public void DeleteSymbol(int position, int stringNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].DeleteSymbol(position, stringNum);
        }
    }

    public void CycleSection(int sectionIndex)
    {
        if (sectionIndex == 0)
        {
            previousSection.SetActive(false);
        }
        else
        {
            previousSection.SetActive(true);
        }

        if (sectionIndex + 1 == sections.Count)
        {
            nextSection.SetActive(false);
        }
        else
        {
            nextSection.SetActive(true);
        }

        if (sectionIndex == 0)
        {
            bpmIndicator.SetActive(true);
        }
        else if (bpms[sectionIndex - 1] != bpms[sectionIndex])
        {
            bpmIndicator.SetActive(true);
        }
        else
        {
            bpmIndicator.SetActive(false);
        }
    }

    public void UpdateSectionDisplay(int sectionIndex, Transform mainTransform, Transform previousTransform, Transform nextTransform)
    {
        List<Section> displayedSections = new List<Section>() { sections[sectionIndex] };
        sections[sectionIndex].LoadSection(mainTransform);

        if (sectionIndex > 0)
        {
            displayedSections.Add(sections[sectionIndex - 1]);
            sections[sectionIndex - 1].LoadSection(previousTransform);
        }
        if (sectionIndex + 1 < sections.Count)
        {
            displayedSections.Add(sections[sectionIndex + 1]);
            sections[sectionIndex + 1].LoadSection(nextTransform);
        }

        foreach (Section section in sections)
        {
            if (!displayedSections.Contains(section)) section.UnloadSection();
        }
    }

    public Section GetSection(int sectionIndex)
    {
        if (sectionIndex < 0) return null;
        if (sectionIndex >= sections.Count) return null;

        return sections[sectionIndex];
    }

    public int GetSectionBPM(int sectionIndex)
    {
        return bpms[sectionIndex];
    }

    public void SetSectionBPM(int sectionIndex, int bpm)
    {
        bpms[sectionIndex] = bpm;
    }

    public string GetSectionAnnotation(int sectionIndex)
    {
        if (sectionIndex < 0) return "";
        if (sectionIndex >= annotations.Count) return "";
        return annotations[sectionIndex];
    }

    public void SetSectionAnnotation(int sectionIndex, string annotation)
    {
        annotations[sectionIndex] = annotation;
    }

    public int GetSectionTotal()
    {
        return sections.Count;
    }

    public void AddSection(int bpm, int division, int timeSignatureLower, int timeSignatureUpper)
    {
        Section sectionComp = Instantiate(section).GetComponent<Section>();
        sectionComp.division = division;
        sectionComp.timeSignatureLower = timeSignatureLower;
        sectionComp.timeSignatureUpper = timeSignatureUpper;
        sections.Add(sectionComp);
        bpms.Add(bpm);
        annotations.Add("");
    }

    public int GetSectionDivision(int index)
    {
        if (sections.Count > index)
        {
            return sections[index].division;
        }

        return 0;
    }

    public List<Note> GetAllNotes(int sectionIndex)
    {
        List<Note> notes = new List<Note>();
        List<Note> sectionNotes = sections[sectionIndex].GetAllNotes();
        foreach (Note note in sectionNotes)
            notes.Add(note);

        return notes;
    }

    public List<Symbol> GetAllSymbols(int sectionIndex)
    {
        List<Symbol> symbols = new List<Symbol>();
        List<Symbol> sectionSymbols = sections[sectionIndex].GetAllSymbols();
        foreach (Symbol symbol in sectionSymbols)
            symbols.Add(symbol);

        return symbols;
    }
}
