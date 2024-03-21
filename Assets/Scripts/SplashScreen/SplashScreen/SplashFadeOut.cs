using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashFadeOut : MonoBehaviour
{
    [Header("Main Settings")]
    public Image TargetImage;
    public float FadeSpeed;
    public float Delay;
    bool StartFadeOut = false;

    void Awake()
    {
        TargetImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        
        TargetImage.enabled = false;
        TargetImage.color = Color.clear;
    }

    void FadeOut()
    {
        TargetImage.color = Color.Lerp(TargetImage.color, Color.black, FadeSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetTrue_StartFadeOut", Delay);
    }

    void SetTrue_StartFadeOut()
    {
        StartFadeOut = true;
        
        TargetImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartFadeOut)
        {
            FadeOut();
        }
    }
}