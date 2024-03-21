using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoActive : MonoBehaviour
{
    public GameObject Object;
    private void Awake()
    {
        if (Object != null)
        {
            Object.SetActive(true);
        }
    }
    void Start()
    {
        if(Object != null)
        {
            Object.SetActive(true);
        }
    }
}