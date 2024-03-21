using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Uang : MonoBehaviour
{
    [SerializeField]private int jumlah_uang;
    [SerializeField]private TMP_Text UangText;

    // Start is called before the first frame update
    void Start()
    {
        UangText.text = jumlah_uang.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UangText.text = jumlah_uang.ToString();
    }

    public int Check_Uang()
    {
        return jumlah_uang;
    }

    public void tambahUang(int uangtambah)
    {
        jumlah_uang += uangtambah;
    }

    public void kurangiUang(int uangkurang)
    {
        jumlah_uang -= uangkurang;
    }
}