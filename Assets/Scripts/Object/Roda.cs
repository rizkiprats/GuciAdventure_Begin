using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roda : MonoBehaviour
{
    [SerializeField] private Transform titikMulaiObjek;

    public float activeTime; 

    // Update is called once per frame
    void Update()
    {
        Invoke("DisableObjek", activeTime);
    }

    public void DisableObjek()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = titikMulaiObjek.transform.position;
    }
}