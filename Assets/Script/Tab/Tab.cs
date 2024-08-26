using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    ExternalAudioPlayer player;

    List<Section> sections;
    List<int> bpms;

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

        sections = new List<Section>();

        sections.Add(Instantiate(section));

        bpms = new List<int>(){ 120 };
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
            if (previousSection) previousSection.SetActive(false);
        }
        else
        {
            if (previousSection) previousSection.SetActive(true);
        }

        if (sectionIndex + 1 == sections.Count)
        {
            if (nextSection) nextSection.SetActive(false);
        }
        else
        {
            if (nextSection) nextSection.SetActive(true);
        }

        if (sectionIndex == 0)
        {
            if (bpmIndicator != null) bpmIndicator.SetActive(true);
        }
        else if (bpms[sectionIndex - 1] != bpms[sectionIndex])
        {
            if (bpmIndicator != null) bpmIndicator.SetActive(true);
        }
        else
        {
            if (bpmIndicator != null) bpmIndicator.SetActive(false);
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

    public int GetSectionTotal()
    {
        return sections.Count;
    }

    public void AddSection(int bpm)
    {
        sections.Add(Instantiate(section));
        bpms.Add(bpm);
    }

    public int GetSectionDivision(int index)
    {
        if (sections.Count > index)
        {
            return sections[index].division;
        }

        return 0;
    }
}
