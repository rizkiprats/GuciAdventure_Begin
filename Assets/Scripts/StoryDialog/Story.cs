using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Story
{
    [Header("Dialog Properties")]
    public string name;

    [TextArea(2, 10)]
    public string[] sentences;

    public Sprite[] ObjectImage;

    public AudioClip[] StoryAudioClipsentences;

    [Header("Story Events")]
    public UnityEvent startStoryEvent;
    public UnityEvent endStoryEvent;
}