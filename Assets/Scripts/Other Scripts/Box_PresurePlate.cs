using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box_PresurePlate : MonoBehaviour, InterfaceDataSave
{
    public static Box_PresurePlate instance;
    
    [Header("Box PressurePlate")]
    public bool Pressed;
    public bool byBox;
    public Animator anim;

    [Header("Stuff to Control")]
    public GameObject Nextlevel;
    public Animator doorAnim;
    public GameObject RockUpDown;
    [SerializeField] private UnityEvent PressedAction;
    [SerializeField] private UnityEvent NotPressedAction;

    [Header("The Box GameObject")]
    public GameObject theboxes;
    public Vector3 theboxesPosition;
    public void Awake()
    {
        instance = this;
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Pressed)
        {
            if(theboxes != null)
            {
                theboxes.transform.position = theboxesPosition;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Nextlevel != null)
        {
            Nextlevel.SetActive(Pressed);
        }

        if (doorAnim != null)
        {
            doorAnim.SetBool("open", Pressed);
        }
           
        if (anim != null)
        {
            anim.SetBool("pressed", Pressed);
        }

        if(RockUpDown != null)
        {
            if(RockUpDown.GetComponent<Animator>() != null)
            {
                RockUpDown.GetComponent<Animator>().SetBool("isRockUp", Pressed);
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Box"))
            {
                theboxes = collision.gameObject;
                byBox = true;
                Pressed = true;
            }
            else
            {
                Pressed = true;
                byBox = false;
            }

            PressedAction.Invoke();

            if (theboxes != null)
            {
                theboxesPosition = theboxes.transform.position;
            }

            if (DataSaveManager.instance != null)
            {
                DataSaveManager.instance.SaveGame();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Box"))
            {
                theboxes = collision.gameObject;
                byBox = true;
                Pressed = true;
            }
            else
            {
                Pressed = true;
                byBox = false;
            }

            PressedAction.Invoke();

            if (theboxes != null)
            {
                theboxesPosition = theboxes.transform.position;
            }

            if (DataSaveManager.instance != null)
            {
                DataSaveManager.instance.SaveGame();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Box"))
            {
                theboxes = null;
                byBox = false;
                Pressed = false;
            }
            else
            {
                Pressed = false;
                byBox = false;
            }

            NotPressedAction.Invoke();

            if (theboxes != null)
            {
                theboxesPosition = theboxes.transform.position;
            }

            if (DataSaveManager.instance != null)
            {
                DataSaveManager.instance.SaveGame();
            }
        }
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
        {
            this.Pressed = data.boxPressurePlate_PressedSaved;
            this.theboxesPosition = data.boxPressurePlate_PosSaved;
        }  
    }

    public void SaveData(ref GameDataSave data)
    {
        data.boxPressurePlate_PressedSaved = this.Pressed;
        data.boxPressurePlate_PosSaved = this.theboxesPosition;
    }
}