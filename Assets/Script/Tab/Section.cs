using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    List<Note> notes;
    List<Symbol> symbols;

    public int division;
    // Start is called before the first frame update
    void Start()
    {
        notes = new List<Note>();
        symbols = new List<Symbol>();
        division = 8;
    }

    public void AddNote(GameObject note, int position, int stringNum)
    {
        int index = -1;
        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].localPosition && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            Note oldNote = notes[index];
            notes.Remove(oldNote);
            Destroy(oldNote.gameObject);
        }

        note.GetComponent<Note>().localPosition = position;
        notes.Add(note.GetComponent<Note>());
    }

    public void DeleteNote(int position, int stringNum)
    {
        int index = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].localPosition && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            Note oldNote = notes[index];
            notes.Remove(oldNote);
            Destroy(oldNote.gameObject);
        }
    }

    public void SetNoteAH(int position, int stringNum)
    {
        int index = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].localPosition && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            notes[index].SetNoteAH();
        }
    }

    public void SetNotePH(int position, int stringNum, int fretNum)
    {
        int index = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].localPosition && stringNum == notes[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            if (index >= 0)
            {
                notes[index].SetNotePH(fretNum);
            }
        }
    }

    public void AddSymbol(GameObject symbol, int position, int stringNum)
    {
        int index = -1;

        for (int i = 0; i < symbols.Count; i++)
        {
            if (position == symbols[i].localPosition && stringNum == symbols[i].stringNum)
            {
                if (symbols[i].symbolType == symbol.GetComponent<Symbol>().symbolType)
                {
                    index = i;
                }
            }
        }

        if (index >= 0)
        {
            Symbol oldSymbol = symbols[index];
            symbols.Remove(oldSymbol);
            Destroy(oldSymbol.gameObject);
        }
        
        symbol.GetComponent<Symbol>().localPosition = position;
        symbols.Add(symbol.GetComponent<Symbol>());
    }

    public void DeleteSymbol(int position, int stringNum)
    {
        int index = -1;
        for (int i = 0; i < symbols.Count; i++)
        {
            if (position == symbols[i].localPosition && stringNum == symbols[i].stringNum)
            {
                index = i;
            }
        }

        if (index >= 0)
        {
            Symbol oldSymbol = symbols[index];
            symbols.Remove(oldSymbol);
            Destroy(oldSymbol.gameObject);
        }
    }

    public void ResetNotePosition(float interval, float spanRation)
    {
        foreach (Note note in notes)
        {
            int position = note.localPosition;
            int fret = note.fret;
            int stringNum = note.stringNum;
            note.SetNote(position, interval, fret, stringNum);
        }

        foreach (Symbol symbol in symbols)
        {
            int position = symbol.localPosition;
            int stringNum = symbol.stringNum;
            float span = symbol.symbolSpan;
            span *= spanRation;
            symbol.SetSymbol(position, interval, stringNum, span);
        }
    }
}
