using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    ExternalAudioPlayer player;

    List<Section> sections;

    public Section section;

    // Start is called before the first frame update
    void Start()
    {
        ExternalAudioPlayer[] players = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];

        if (players.Length > 0)
        {
            player = players[0];
        }

        sections = new List<Section>();

        sections.Add(Instantiate(section));
    }

    public void AddNote(GameObject note, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].AddNote(note);
        }
    }

    public void DeleteNote(GameObject note, int sectionIndex)
    {
        if (sections.Count > sectionIndex)
        {
            sections[sectionIndex].DeleteNote(note);
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
