using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProgress : MonoBehaviour
{
    //Automatic Get From Level System
    private int LevelNumber;

    [Header("Define The Progress Number in Index")]
    [SerializeField] private int ProgressNumber;

    [Header("Define The Commit To Progress")]
    public bool addProgress;

    public void AddToProgress()
    {
        if (addProgress)
        {
            if( FindObjectOfType<LevelSystem>() != null)
            {
                LevelSystem LevelObject = FindObjectOfType<LevelSystem>();

                LevelNumber = LevelObject.levelNumber;

                LevelObject.levelProgress.Level_Progress[LevelNumber].PointProgress[ProgressNumber] = true;

            }

            DataSaveManager.instance.SaveGame();
        }
    }
}