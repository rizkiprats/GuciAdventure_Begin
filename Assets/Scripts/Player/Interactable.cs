using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject InteractPanel;
    public GameObject InteractButton;

    void Awake()
    {
        if(InteractPanel != null)
            InteractPanel.SetActive(false);
        if(InteractButton != null)
            InteractButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractPanel != null)
                InteractPanel.SetActive(true);
            if (InteractButton != null)
                InteractButton.SetActive(true);
            other.gameObject.GetComponent<PlayerControllers>().setInteract(true);
            other.gameObject.GetComponent<PlayerControllers>().interactable = this.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractPanel != null)
                InteractPanel.SetActive(true);
            if (InteractButton != null)
                InteractButton.SetActive(true);
            other.gameObject.GetComponent<PlayerControllers>().setInteract(true);
            other.gameObject.GetComponent<PlayerControllers>().interactable = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractPanel != null)
                InteractPanel.SetActive(false);
            if (InteractButton != null)
                InteractButton.SetActive(false);
            other.gameObject.GetComponent<PlayerControllers>().setInteract(false);
            other.gameObject.GetComponent<PlayerControllers>().interactable = null;
        }
    }
}