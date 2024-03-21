using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inspector_LoadSceneDelay : MonoBehaviour
{
    [Header("Main Settings")]
    public string TargetScene;
    public float Delay;

    void LoadScene()
    {
        SceneManager.LoadScene(TargetScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", Delay);
    }
}