using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class CheckDoorOpen : MonoBehaviour
{
    public Animator anim;
    private AudioSource DoorOpenAudio;

    // Start is called before the first frame update
    void Start()
    {
        DoorOpenAudio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("open") == true)
        {
            DoorOpenAudio.Play();
            Debug.Log("door_anim : " + anim.GetBool("open"));
        }
    }
}