using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Story : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<StoryTrigger>().TriggerStory();
        }
    }
}