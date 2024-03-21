using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject PanelEnding;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PanelEnding.SetActive(true);

            if (GameManager.instance != null)
            {
                GameManager.instance.timerDisable();
            }

            if (FindObjectOfType<PlayerControllers>() != null)
            {
                FindObjectOfType<PlayerControllers>().RunAudio.Stop();
                FindObjectOfType<PlayerControllers>().JumpAudio.Stop();
            }
            
            Invoke("playerDisable", 1);
        }
    }

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

    public void nextLevel()
    {
        if (GameObject.Find("FadeOut") != null)
        {
            GameObject.Find("FadeOut").SetActive(true);
        }

        if (FindObjectOfType<MenuSetting>() != null)
            FindObjectOfType<MenuSetting>().FadeMusicOff();

        Invoke("StartContinueLevelScene", 2);

        if (GameManager.instance != null)
        {
            GameManager.instance.setTimer();

            GameManager.instance.ResetLevel();
        }
    }

    public void playerDisable()
    {
        GameManager.instance.PlayerDisabled();
    }

    public void StartContinueLevelScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int scene_number = scene.buildIndex;
        Time.timeScale = 1;

        SceneManager.LoadScene(scene_number + 1);
    }
}