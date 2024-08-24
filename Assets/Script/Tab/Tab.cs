using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    ExternalAudioPlayer player;

    List<Section> sections;

    public Section section;

    public GameObject mainSection;
    public GameObject previousSection;
    public GameObject nextSection;

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

    public void SetNoteAH(int position, int stringNum, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].SetNoteAH(position, stringNum);
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
    }

    public Section GetSection(int sectionIndex)
    {
        return sections[sectionIndex];
    }

    public int GetSectionTotal()
    {
        return sections.Count;
    }

    public void AddSection()
    {
        sections.Add(Instantiate(section));
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
