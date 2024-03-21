using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryTrigger : MonoBehaviour
{
    [Header("Story")]
    public Story story;

    public void TriggerStory()
    {
        story.startStoryEvent.Invoke();

        if (FindObjectOfType<PlayerControllers>() != null)
            FindObjectOfType<PlayerControllers>().OnDisable();

        FindObjectOfType<DialogManager_Story>().StartStoryDialog(story);
    }
}