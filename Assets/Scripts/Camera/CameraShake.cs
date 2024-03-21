using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera MainCam;

    float shakeAmount = 0;

    public bool doShake;

    private void Awake()
    {
        if (MainCam == null)
            MainCam = Camera.main;
    }

    void Update()
    {
        if (doShake)
        {
            Shake(0.2f, 0.2f);
        }
        else
        {
            Shake(0.0f, 0.0f);
        }
    }

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake",0,0.01f);
        Invoke("StopShake", length);
    }

    void BeginShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = MainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            MainCam.transform.position = camPos;
        }
    }

    public void StopShake()
    {
        CancelInvoke("BeginShake");
        MainCam.transform.localPosition = Vector3.zero;
    }
}