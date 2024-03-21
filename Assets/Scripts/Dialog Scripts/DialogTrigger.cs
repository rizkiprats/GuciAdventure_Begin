using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [Header("Dialog")]
    public Dialog dialog;

    [Header("Jurnal Entry")]
    public bool addJournalEntry;
    public int jurnalPoint;

    public void TriggerDialog()
    {
        dialog.startDialogEvent.Invoke();

        if (FindObjectOfType<PlayerControllers>() != null)
            FindObjectOfType<PlayerControllers>().OnDisable();

        dialog.setJurnalEntry(addJournalEntry);
        dialog.setJurnalPoint(jurnalPoint);

        FindObjectOfType<DialogManager>().StartDialog(dialog);
        
    }
}