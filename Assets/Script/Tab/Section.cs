using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    struct NoteStruct
    {
        public GameObject note;
        public int position;
        public int stringNum;
    }

    List<NoteStruct> notes;
    public int division;
    // Start is called before the first frame update
    void Start()
    {
        notes = new List<NoteStruct>();
        division = 8;
    }

    public void AddNote(GameObject note, int position, int stringNum)
    {
        int index = -1;
        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].position && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            NoteStruct noteStruct = notes[index];
            GameObject noteObject = noteStruct.note;
            Destroy(noteObject);
            noteStruct.note = note;
        }
        else
        {
            NoteStruct newNote = new NoteStruct();
            newNote.note = note;
            newNote.position = position;
            newNote.stringNum = stringNum;

            notes.Add(newNote);
        }
    }

    public void DeleteNote(int position, int stringNum)
    {
        int index = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].position && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            NoteStruct noteStruct = notes[index];
            GameObject noteObject = noteStruct.note;
            Destroy(noteObject);
            notes.Remove(notes[index]);
        }
    }

    public void SetNoteAH(int position, int stringNum)
    {
        int index = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].position && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            NoteStruct noteStruct = notes[index];
            GameObject noteObject = noteStruct.note;
            Note noteComp = noteObject.GetComponent<Note>();
            if (noteComp != null)
            {
                noteComp.SetNoteAH();
            }
        }
    }

    public void SetNotePH(int position, int stringNum, int fretNum)
    {
        int index = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].position && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            NoteStruct noteStruct = notes[index];
            GameObject noteObject = noteStruct.note;
            Note noteComp = noteObject.GetComponent<Note>();
            if (noteComp != null)
            {
                noteComp.SetNotePH(fretNum);
            }
        }
    }
}
