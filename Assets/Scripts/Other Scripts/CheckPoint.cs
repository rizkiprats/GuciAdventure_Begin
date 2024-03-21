using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(DataSaveManager.instance != null)
            {
                DataSaveManager.instance.SaveGame();
            }
        }
    }
}