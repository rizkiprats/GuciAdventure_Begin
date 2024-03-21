using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger_Random : MonoBehaviour
{
    [Header("Dialog")]
    public List<Dialog> dialog;

    [Header("Jurnal Entry")]
    public bool addJournalEntry;
    public int jurnalPoint;

    public void TriggerDialog()
    {
        if (FindObjectOfType<PlayerControllers>() != null)
            FindObjectOfType<PlayerControllers>().OnDisable();

        int Randomindex = Random.Range(0, dialog.Count);
        dialog[Randomindex].setJurnalEntry(addJournalEntry);
        dialog[Randomindex].setJurnalPoint(jurnalPoint);

        FindObjectOfType<DialogManager>().StartDialog(dialog[Randomindex]);

        dialog[Randomindex].startDialogEvent.Invoke();
    }
}