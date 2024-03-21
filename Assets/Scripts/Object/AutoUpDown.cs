using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUpDown : MonoBehaviour
{
    [Header("Up & Down Move Points")]
    [SerializeField] private Transform UpEdge;
    [SerializeField] private Transform DownEgde;

    [Header("Up & Down Object")]
    [SerializeField] private Transform Object;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingDown;

    [SerializeField] private float idleDuration;
    private float idleTimer;

    private void Awake()
    {
        initScale = Object.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDown)
        {
            if (Object.position.y >= DownEgde.position.y)
            {
                MoveInDirection(-1);
            }
            else
            {
                directionChange();
            }
        }
        else
        {
            if (Object.position.y <= UpEdge.position.y)
            {
                MoveInDirection(1);
            }
            else
            {
                directionChange();
            }
        }
    }

    private void directionChange()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            movingDown = !movingDown;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        Object.position = new Vector3(Object.position.x,
            Object.position.y + Time.deltaTime * _direction * speed, Object.position.z);
    }
}