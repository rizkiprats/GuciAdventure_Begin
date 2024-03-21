using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLoadScene : MonoBehaviour
{
    [Header("Main Settings")]
    public int TargetScene;
    public float Delay;

    void LoadTheScene()
    {
        SceneManager.LoadScene(TargetScene);
    }

    public void CountinueGame()
    {
        Time.timeScale = 1;
        Invoke("LoadTheScene", Delay);
    }
}