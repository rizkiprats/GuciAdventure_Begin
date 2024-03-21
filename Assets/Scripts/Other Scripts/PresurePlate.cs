using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour, InterfaceDataSave
{
    public static PresurePlate instance;

    [Header("Pressure Plate")]
    public bool isOn;
    public Animator anim;

    [Header("Stuff to Control")]
    public GameObject NextLevelObjek;
    public GameObject NotifObjek;
    public Animator doorAnim;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NextLevelObjek != null)
            NextLevelObjek.gameObject.SetActive(isOn);
 
        anim.SetBool("pressed",isOn);
        doorAnim.SetBool("open", isOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isOn = true;
            
            if (isOn == true)
            {
                GetComponent<Collider2D>().enabled = false;
                
                DataSaveManager.instance.SaveGame();
                
                doorAnim.SetTrigger("openDoor");
                
                NotifObjek.gameObject.SetActive(false);
                NotifObjek.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
            this.isOn = data.isOn_PressurePlateSaved;
    }

    public void SaveData(ref GameDataSave data)
    {
        
    }
}