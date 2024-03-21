using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Add_SoalEntry : MonoBehaviour
{
    private bool addJawabanIsBenar;

    private int SoalNumber;

    [Header("Text Panel To Store The Answer")]
    public TMP_Text TextAnswerTrue;
    public TMP_Text TextAnswerFalse;

    public void AddJawaban()
    {
        if(this.gameObject.GetComponentInChildren<TMP_Text>() != null)
        {
            if(TextAnswerTrue != null)
                TextAnswerTrue.text = this.gameObject.GetComponentInChildren<TMP_Text>().text;
            if(TextAnswerFalse != null)
                TextAnswerFalse.text = this.gameObject.GetComponentInChildren<TMP_Text>().text;
        }

        if (addJawabanIsBenar)
        {
            GameObject.FindObjectOfType<Action_Soal>().SetJawaban(SoalNumber, true);
        }
        else
        {
            GameObject.FindObjectOfType<Action_Soal>().SetJawaban(SoalNumber, false);
        }

        GameObject.FindObjectOfType<Action_Soal>().CheckJawaban(SoalNumber);
    }

    public void setJawaban(bool isBenar, int soalNumber)
    {
        addJawabanIsBenar = isBenar;
        SoalNumber = soalNumber;
    } 
}