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

    struct SymbolStruct
    {
        public List<GameObject> symbolObjs;
        public int position;
        public int stringNum;
    }

    List<NoteStruct> notes;
    List<SymbolStruct> symbols;

    public int division;
    // Start is called before the first frame update
    void Start()
    {
        notes = new List<NoteStruct>();
        symbols = new List<SymbolStruct>();
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

    public void AddSymbol(GameObject symbol, int position, int stringNum)
    {
        int index = -1;
        int otherIndex = -1;
        for (int i = 0; i < symbols.Count; i++)
        {
            if (position == symbols[i].position && stringNum == symbols[i].stringNum)
            {
                index = i;
                List<GameObject> symbolObjs = symbols[i].symbolObjs;
                for (int j = 0; j < symbolObjs.Count; j++)
                {
                    if (symbolObjs[j].GetComponent<Symbol>().symbolType == symbol.GetComponent<Symbol>().symbolType)
                    {
                        otherIndex = j;
                    }
                }
            }
        }

        if (index >= 0 && otherIndex >= 0)
        {
            GameObject symbolObject = symbols[index].symbolObjs[otherIndex];
            Destroy(symbolObject);
            symbols[index].symbolObjs[otherIndex] = symbol;
        }
        else if (index >= 0)
        {
            symbols[index].symbolObjs.Add(symbol);
        }
        else
        {
            SymbolStruct newSymbol = new SymbolStruct();
            newSymbol.symbolObjs = new List<GameObject>(){ symbol };
            newSymbol.position = position;
            newSymbol.stringNum = stringNum;

            symbols.Add(newSymbol);
        }
    }

    public void DeleteSymbol(int position, int stringNum)
    {
        int index = -1;
        for (int i = 0; i < symbols.Count; i++)
        {
            if (position == symbols[i].position && stringNum == symbols[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            List<GameObject> symbolObjs = symbols[index].symbolObjs;
            for (int j = 0; j < symbolObjs.Count; j++)
            {
                Destroy(symbolObjs[j]);
            }
            symbols[index].symbolObjs.Clear();
        }
    }
}
