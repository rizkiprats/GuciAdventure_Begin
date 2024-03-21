using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respwan : MonoBehaviour
{
    public Transform Respawn_Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(GameManager.instance != null)
            {
                if(Respawn_Player != null)
                {
                    GameObject.Find("Player").transform.position = Respawn_Player.transform.position;
                }
                else 
                {
                    GameObject.Find("Player").transform.position = GameManager.instance.respawnPoint;
                }
            }
        }
    }
}