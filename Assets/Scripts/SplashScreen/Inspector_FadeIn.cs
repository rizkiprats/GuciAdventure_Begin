using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inspector_FadeIn : MonoBehaviour
{
    [Header("Main Settings")]
    public Image TargetImage;
    public float FadeSpeed;

    void Awake()
    {
        if(TargetImage != null)
            TargetImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void FadeIn()
    {
        if(TargetImage != null)
            TargetImage.color = Color.Lerp(TargetImage.color, Color.clear, FadeSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
    }
}