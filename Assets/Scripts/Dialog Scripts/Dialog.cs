using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialog
{
    [Header("Dialog Properties")]
    public Sprite karakterImage;
    public Sprite ObjectImage;
    public string name;

    [Header("Dialog Sentences")]
    [TextArea(2, 10)]
    public string[] sentences;
    public AudioClip[] AudioClipSentences;

    [Header("Dialog Events")]
    public UnityEvent startDialogEvent;
    public UnityEvent endDialogEvent;

    [Header("Dialog Jurnal Entry (Get From Dialog Trigger)")]
    private bool addJournalEntry;
    private int dialogJurnalPoint;

    public void setJurnalEntry(bool Entry) 
    {
        addJournalEntry = Entry;
    }

    public bool getJurnalEntry()
    {
        return addJournalEntry;
    }

    public void setJurnalPoint(int Point)
    {
        dialogJurnalPoint = Point;
    }

    public int getJurnalPoint()
    {
        return dialogJurnalPoint;
    }
}