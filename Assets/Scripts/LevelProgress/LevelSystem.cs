using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSystem : MonoBehaviour,InterfaceDataSave
{
    [Header("Level Progress")]
    public LevelProgress levelProgress;

    [Header("Level Progress Events")]
    public ProgressEvent progressEvents;

    [Header("Define Reset Level Progress at Progress in Index 0")]
    public bool Index0AlsoReset;

    [Header("Define Level System Number")]
    [SerializeField] public int levelNumber;

    [Header("Check The Complete of Level Progress (Automatic)")]
    [SerializeField] private bool LevelComplete;

    [Header("Events When Level Progress Complete")]
    [SerializeField] private UnityEvent TriggerComplete;
    [SerializeField] private UnityEvent TriggerUncomplete;

    // Start is called before the first frame update
    void Start()
    {
        CheckProgress();
    }

    // Update is called once per frame
    void Update()
    {
        LevelComplete = CheckAllProgress();

        voidCheckComplete();

        CheckProgress();
    }

    private bool CheckAllProgress()
    {
        for (int i = 0; i < levelProgress.Level_Progress[levelNumber].PointProgress.Length; i++)
        {
            if (levelProgress.Level_Progress[levelNumber].PointProgress[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    public void CheckProgress()
    {
        for (int i = 0; i < levelProgress.Level_Progress[levelNumber].PointProgress.Length; i++)
        {
            if (levelProgress.Level_Progress[levelNumber].PointProgress[i] == true)
            {
                progressEvents.Progress_Events[levelNumber].ProgressEvent[i].Invoke();
            }
        }
    }

   public void ResetProgress()
    {
        for (int i = 0; i < levelProgress.Level_Progress[levelNumber].PointProgress.Length; i++)
        {
            if(i == 0)
            {
                if (Index0AlsoReset)
                {
                    levelProgress.Level_Progress[levelNumber].PointProgress[i] = false;
                }
                else
                {
                    levelProgress.Level_Progress[levelNumber].PointProgress[i] = true;
                }     
            }
            else
            {
                levelProgress.Level_Progress[levelNumber].PointProgress[i] = false;
            }
        }
    }

    public void voidCheckComplete()
    {
        if (LevelComplete)
        {
            TriggerComplete.Invoke();
        }
        else
        {
            TriggerUncomplete.Invoke();
        }
    }

    public void CheckLevelComplete(int delay)
    {
        Invoke("voidCheckComplete", delay);
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
            this.levelProgress = data.levelProgressSaved;
    }

    public void SaveData(ref GameDataSave data)
    {
        data.levelProgressSaved = this.levelProgress;
    }
}