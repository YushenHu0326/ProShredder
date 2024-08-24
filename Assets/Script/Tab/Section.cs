using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    List<GameObject> notes;
    public int division;
    // Start is called before the first frame update
    void Start()
    {
        notes = new List<GameObject>();
        division = 8;
    }

    public void AddNote(GameObject note)
    {
        notes.Add(note);
    }

    public void DeleteNote(GameObject note)
    {
        if (notes.Contains(note))
        {
            notes.Remove(note);
        }
    }
}
