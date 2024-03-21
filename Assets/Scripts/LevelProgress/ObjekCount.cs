using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ObjekCount : MonoBehaviour, InterfaceDataSave
{
    [Header("Text To Store Counted Object")]
    public TMP_Text TextObjekCount;
    public TMP_Text TextObjekObtained;

    [Header("The Objects To Counted")]
    public GameObject[] Objek;

    private int obtainedObjek;

    [SerializeField] private UnityEvent eventobjectobtain;
    [SerializeField] private UnityEvent eventobtainfinish;

    public GameObject Notification;
    public string TextObtainFinish;

    // Start is called before the first frame update
    void Start()
    {
        TextObjekCount.text = ObjekCountCheck().ToString();
        TextObjekObtained.text = obtainedObjek.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TextObjekCount.text = ObjekCountCheck().ToString();
        TextObjekObtained.text = obtainedObjek.ToString();
    }

    public int ObjekCountCheck()
    {
        return Objek.Length;
    }

    public void ResetobtainedCount()
    {
        obtainedObjek = 0;
    }

    public bool CheckObjectCount()
    {
        if(obtainedObjek == Objek.Length)
        {
            return true;
        }
        return false;
    }

    public void addObtainObject()
    {
        obtainedObjek += 1;

        if (CheckObjectCount() == true)
        {
            Notification.gameObject.SetActive(true);

            if (Notification != null)
            {
                if (Notification.gameObject.GetComponentInChildren<TMP_Text>() != null)
                {
                    Notification.gameObject.GetComponentInChildren<TMP_Text>().text = TextObtainFinish;
                }

                if (Notification.gameObject.GetComponent<Animator>() != null)
                {
                    Notification.gameObject.GetComponent<Animator>().SetTrigger("Tekan");
                }
            }
            eventobtainfinish.Invoke();
        }
        else
        {
            Notification.gameObject.SetActive(true);

            if (Notification != null)
            {
                if (Notification.gameObject.GetComponent<Animator>() != null)
                {
                    Notification.gameObject.GetComponent<Animator>().SetTrigger("Tekan");
                }
            }
            eventobjectobtain.Invoke();
        }

        if (DataSaveManager.instance != null)
        {
            DataSaveManager.instance.SaveGame();
        }   
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
        {
            this.obtainedObjek = data.Obtained_Object_Count_Saved;
        }      
    }

    public void SaveData(ref GameDataSave data)
    {
        data.Obtained_Object_Count_Saved = this.obtainedObjek;
    }
}