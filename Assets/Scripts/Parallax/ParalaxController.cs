using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    Transform camera;
    Vector3 cameraStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] materials;
    float[] backspeeds;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    private void Start()
    {
        camera = Camera.main.transform;
        cameraStartPos = camera.position;

        int backCount = transform.childCount;
        materials = new Material[backCount];
        backspeeds = new float[backCount];
        backgrounds = new GameObject[backCount];


        for(int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for(int i = 0; i < backCount; i++)
        {
            if((backgrounds[i].transform.position.z - camera.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - camera.position.z;
            }
        }

        for(int i = 0; i < backCount; i++)
        {
            backspeeds[i] = 1 - (backgrounds[i].transform.position.z - camera.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = camera.position.x - cameraStartPos.x;
        transform.position = new Vector3(camera.position.x, 0, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backspeeds[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}