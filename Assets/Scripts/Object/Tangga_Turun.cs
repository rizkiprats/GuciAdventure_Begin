using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangga_Turun : MonoBehaviour
{
    public GameObject Tangga;
    public GameObject Lantai;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerControllers>().ismoveDown())
            {
                Tangga.GetComponent<BoxCollider2D>().isTrigger = false;
                Lantai.GetComponent<Collider2D>().isTrigger = true;
                collision.GetComponent<PlayerControllers>().setRBConstrains((RigidbodyConstraints2D.FreezePositionX |
                    RigidbodyConstraints2D.FreezeRotation));
            }
            else
            {
                Tangga.GetComponent<BoxCollider2D>().isTrigger = true;
                Lantai.GetComponent<Collider2D>().isTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Companion_NPC")
        {
            //Tangga.GetComponent<BoxCollider2D>().isTrigger = false;
            //Lantai.GetComponent<Collider2D>().isTrigger = true;
            collision.GetComponent<NPC_Controllers>().setRBConstrains((RigidbodyConstraints2D.FreezePositionX |
                RigidbodyConstraints2D.FreezeRotation));
        }
    }
}