using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    List<Note> notes;
    List<Symbol> symbols;

    public int division;
    public int timeSignatureLower;
    public int timeSignatureUpper;

    public void AddNote(GameObject note, int position, int stringNum)
    {
        if (notes == null) notes = new List<Note>();

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

    public Note GetNote(int position, int stringNum)
    {
        if (notes == null) notes = new List<Note>();

        for (int i = 0; i < notes.Count; i++)
        {
            if (position == notes[i].localPosition && stringNum == notes[i].stringNum)
            {
                return notes[i];
            }
        }

        return null;
    }

    public void DeleteNote(int position, int stringNum)
    {
        Note note = GetNote(position, stringNum);

        if (note != null)
        {
            notes.Remove(note);
            Destroy(note.gameObject);
        }
    }

    public void SetNoteAH(int position, int stringNum)
    {
        Note note = GetNote(position, stringNum);

        if (note != null)
        {
            note.SetNoteAH();
        }
    }

    public void SetNotePH(int position, int stringNum, int fretNum)
    {
        Note note = GetNote(position, stringNum);

        if (note != null)
        {
            note.SetNotePH(fretNum);
        }
    }

    public void AddSymbol(GameObject symbol, int position, int stringNum)
    {
        if (symbols == null) symbols = new List<Symbol>();

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
        if (symbols == null) symbols = new List<Symbol>();

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

    public void ResetNotePosition(float interval, int previousDivision, int newDivision)
    {
        if (notes == null) notes = new List<Note>();
        if (symbols == null) symbols = new List<Symbol>();

        List<Note> deleteNote = new List<Note>();
        List<Symbol> deleteSymbol = new List<Symbol>();

        foreach (Note note in notes)
        {
            int position = note.localPosition;
            if (position > newDivision)
            {
                deleteNote.Add(note);
            }
            int fret = note.fret;
            int stringNum = note.stringNum;
            note.SetNote(position, interval, fret, stringNum);
        }

        foreach (Symbol symbol in symbols)
        {
            int position = symbol.localPosition;
            if (position > newDivision)
            {
                deleteSymbol.Add(symbol);
            }
            int stringNum = symbol.stringNum;
            float span = symbol.symbolSpan;
            span *= (float) previousDivision / (float) previousDivision;
            symbol.SetSymbol(position, interval, stringNum, span);
        }

        foreach (Note note in deleteNote)
        {
            notes.Remove(note);
            Destroy(note.gameObject);
        }

        foreach (Symbol symbol in deleteSymbol)
        {
            symbols.Remove(symbol);
            Destroy(symbol.gameObject);
        }
    }

    public void LoadSection(Transform parent)
    {
        if (notes == null) notes = new List<Note>();
        if (symbols == null) symbols = new List<Symbol>();

        foreach (Note note in notes)
        {
            note.gameObject.SetActive(true);
            note.gameObject.transform.SetParent(parent, false);
        }

        foreach (Symbol symbol in symbols)
        {
            symbol.gameObject.SetActive(true);
            symbol.gameObject.transform.SetParent(parent, false);
        }
    }

    public void UnloadSection()
    {
        if (notes == null) notes = new List<Note>();
        if (symbols == null) symbols = new List<Symbol>();

        foreach (Note note in notes)
        {
            note.gameObject.SetActive(false);
        }

        foreach (Symbol symbol in symbols)
        {
            symbol.gameObject.SetActive(false);
        }
    }

    public void UnloadString(int stringIndex)
    {
        if (notes == null) notes = new List<Note>();
        if (symbols == null) symbols = new List<Symbol>();

        List<Note> deleteNotes = new List<Note>();
        List<Symbol> deleteSymbols = new List<Symbol>();

        foreach (Note note in notes)
        {
            if (note.stringNum == stringIndex)
                deleteNotes.Add(note);
        }

        foreach (Symbol symbol in symbols)
        {
            if (symbol.stringNum == stringIndex)
                deleteSymbols.Add(symbol);
        }

        foreach (Note note in deleteNotes)
        {
            notes.Remove(note);
            Destroy(note.gameObject);
        }

        foreach (Symbol symbol in deleteSymbols)
        {
            symbols.Remove(symbol);
            Destroy(symbol.gameObject);
        }
    }

    public List<Note> GetAllNotes()
    {
        return notes;
    }

    public List<Symbol> GetAllSymbols()
    {
        return symbols;
    }
}
