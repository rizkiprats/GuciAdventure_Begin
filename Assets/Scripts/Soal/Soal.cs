using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Soal
{
    [System.Serializable]
    public struct JawabanSoal
    {
        //[TextArea(2, 10)]
        //public string PertanyaanSentence;

        //[System.Serializable]
        //public struct choiceBox
        //{
        //    public bool addJawabanisBenar;
        //    [TextArea(2, 10)]
        //    public string JawabanSentence;
        //}
        //public choiceBox[] choiceBoxes;

        public bool jawaban;
        public UnityEvent EventTrue;
        public UnityEvent EventFalse;
    }

    [Header("Soal (Result Config List)")]
    public JawabanSoal[] soal = new JawabanSoal[1];

    public JawabanSoal[] InitiateSoal(int soalcount)
    {
        return new JawabanSoal[soalcount];
    }
}