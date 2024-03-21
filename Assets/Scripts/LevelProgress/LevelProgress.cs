using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LevelProgress
{
    [System.Serializable]
    public struct Progress
    {
        public bool[] PointProgress;
    }

    public Progress[] Level_Progress = new Progress[1];

    public int GetLength()
    {
        return Level_Progress.Length;
    }
}