using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FreezePosition_Y: MonoBehaviour
{
    private RectTransform rectTransform;
    private float posY;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posY = rectTransform.transform.position.y;
    }
    void Update()
    {
        rectTransform.transform.position = new Vector3(transform.position.x, posY, transform.position.y);
    }
}
