using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class NextLevel : MonoBehaviour
{
    [Header("Next Level Events")]
    [SerializeField] private UnityEvent TriggerNextLevel;

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    public void OnSceneUnLoaded(Scene scene)
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.activePlayer.transform.position = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TriggerNextLevel.Invoke();

            if (GameObject.Find("FadeOut") != null)
            {
                GameObject.Find("FadeOut").SetActive(true);
            }

            if (FindObjectOfType<MenuSetting>() != null)
            {
                FindObjectOfType<MenuSetting>().FadeMusicOff();
            }
                
            if (FindObjectOfType<ObjekCount>() != null)
            {
                FindObjectOfType<ObjekCount>().ResetobtainedCount();
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.setTimer();
                GameManager.instance.ResetLevel();
            }
           
            Invoke("LoadNextLevel", 2);  
        }
    }

    private void LoadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int scene_number = scene.buildIndex;

        SceneManager.LoadScene(scene_number + 1);
    }
}