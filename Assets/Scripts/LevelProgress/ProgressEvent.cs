using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ProgressEvent
{
    [System.Serializable]
    public struct Progress
    {
        public UnityEvent[] ProgressEvent;
    }

    public Progress[] Progress_Events = new Progress[1];

    public int GetLength()
    {
        return Progress_Events.Length;
    }
}
