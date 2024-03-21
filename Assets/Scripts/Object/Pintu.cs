using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pintu : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void setPintu(bool action)
    {
        if(anim != null)
        {
            anim.SetBool("Opened", action);
        }
    }
}