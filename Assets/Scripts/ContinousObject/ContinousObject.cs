using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousObject : MonoBehaviour
{
    public Transform[] Objek;
    public float speed;
    public float turnPoint;

    [SerializeField] bool MoveVertical;
    [SerializeField] bool InvertMove;

    private Vector3 direction;

    private Transform camera;
    private Vector3 cameraStartPos;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(-1, 0, 0);
        camera = Camera.main.transform;
        cameraStartPos = camera.position;
        turnPoint = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        positionUpdate();
        checkPosition();
    }

    private void checkPosition()
    {
        if (MoveVertical)
        {
            if (InvertMove)
            {
                if (Objek[0].position.y >= turnPoint)
                {
                    moveTo(0);
                }
                if (Objek[1].position.y >= turnPoint)
                {
                    moveTo(1);
                }
            }
            else
            {
                if (Objek[0].position.y <= -turnPoint)
                {
                    moveTo(0);
                }
                if (Objek[1].position.y <= -turnPoint)
                {
                    moveTo(1);
                }
            }
        }
        else
        {
            if (InvertMove)
            {
                if (Objek[0].position.x >= turnPoint)
                {
                    moveTo(0);
                }
                if (Objek[1].position.x >= turnPoint)
                {
                    moveTo(1);
                }
            }
            else
            {
                if (Objek[0].position.x <= -turnPoint)
                {
                    moveTo(0);
                }
                if (Objek[1].position.x <= -turnPoint)
                {
                    moveTo(1);
                }
            }
        } 
    }

    private void moveTo(int index)
    {
        if (MoveVertical)
        {
            if (InvertMove)
            {
                if (index == 0)
                {
                    Objek[0].position = Objek[1].position + new Vector3(0, -turnPoint, 0);
                }
                else if (index == 1)
                {
                    Objek[1].position = Objek[0].position + new Vector3(0, -turnPoint, 0);
                }
            }
            else
            {
                if (index == 0)
                {
                    Objek[0].position = Objek[1].position + new Vector3(0, turnPoint, 0);
                }
                else if (index == 1)
                {
                    Objek[1].position = Objek[0].position + new Vector3(0, turnPoint, 0);
                }
            }
        }
        else
        {
            if (InvertMove)
            {
                if (index == 0)
                {
                    Objek[0].position = Objek[1].position + new Vector3(-turnPoint, 0, 0);
                }
                else if (index == 1)
                {
                    Objek[1].position = Objek[0].position + new Vector3(-turnPoint, 0, 0);
                }
            }
            else
            {
                if (index == 0)
                {
                    Objek[0].position = Objek[1].position + new Vector3(turnPoint, 0, 0);
                }
                else if (index == 1)
                {
                    Objek[1].position = Objek[0].position + new Vector3(turnPoint, 0, 0);
                }
            }
        } 
    }

    private void positionUpdate()
    {
        Objek[0].position += direction * Time.deltaTime * speed;
        Objek[1].position += direction * Time.deltaTime * speed;
    }

    private void LateUpdate()
    {
        distance = camera.position.x - cameraStartPos.x;
        transform.position = new Vector3(camera.position.x, transform.position.y, transform.position.z);
    }
}