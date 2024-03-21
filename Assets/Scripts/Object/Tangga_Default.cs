using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangga_Default : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerControllers>().setUpandDownDefault();
        }
        if (collision.gameObject.tag == "Companion_NPC")
        {
            collision.GetComponent<NPC_Controllers>().setUpandDownDefault();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerControllers>().setUpandDownDefault();
        }
        
        if (collision.gameObject.tag == "Companion_NPC")
        {
            collision.GetComponent<NPC_Controllers>().setUpandDownDefault();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerControllers>().setUpandDownDefault();
        }

        if (collision.gameObject.tag == "Companion_NPC")
        {
            collision.GetComponent<NPC_Controllers>().setUpandDownDefault();
        }
    }
}