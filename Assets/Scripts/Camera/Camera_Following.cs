using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Following : MonoBehaviour, InterfaceDataSave
{
    public float m_DampTime;
    public Transform m_Target;
    public float m_XOffset = 0;
    public float m_YOffset = 0;

    private float margin = 0.1f;

    public static Camera_Following instance;

    private void Awake()
    {
        instance = this;
        m_XOffset = 1.2f;
    }

    void Start()
    {
        m_XOffset = 1.2f;

        if (m_Target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (m_Target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (m_Target)
        {
            float targetX = m_Target.position.x + m_XOffset;
            float targetY = m_Target.position.y + m_YOffset;

            if (Mathf.Abs(transform.position.x - targetX) > margin)
                targetX = Mathf.Lerp(transform.position.x, targetX, 1 / m_DampTime * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - targetY) > margin)
                targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }

    public Vector3 getPosCam()
    {
        return gameObject.transform.position;
    }

    public void LoadData(GameDataSave data)
    {
        if (data != null)
        {
            if(data.cameraPos == Vector3.zero)
            {
                if(m_Target != null)
                {
                    this.gameObject.transform.position = m_Target.position;
                }
            }
            else
            {
                this.gameObject.transform.position = data.cameraPos;
            } 
        }   
    }

    public void SaveData(ref GameDataSave data)
    {

    }
}