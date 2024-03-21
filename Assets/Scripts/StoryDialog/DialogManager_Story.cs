using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogManager_Story : MonoBehaviour
{
    [Header("Story Panel")]
    public GameObject StoryPanel;
    public TextMeshProUGUI StoryDialog;

    [Header("Story Panel Animator")]
    public Animator StoryPanelAnimator;

    [Header("Story Object Image")]
    public Image ObjectImage;

    [Header("Story Audio")]
    public AudioSource storyTypingAudioSource;
    public AudioSource storyClipAudioSource;
    private Queue<string> storysentences;
    private Queue<Sprite> storyimages;
    private Queue<AudioClip> storyAudioClip;
    private bool stopDialog;

    [Header("Skip Story Button")]
    public GameObject SkipStoryButton;

    private bool skipstoryDialog;

    public bool isStoryEnd;

    private UnityEvent endStoryEvent;

    // Start is called before the first frame update
    void Start()
    {
        storysentences = new Queue<string>();
        storyAudioClip = new Queue<AudioClip>();
        storyimages = new Queue<Sprite>();
        skipstoryDialog = false;
        isStoryEnd = false;
    }

    public void StartStoryDialog(Story story)
    {
        StoryPanel.gameObject.SetActive(true);

        skipstoryDialog = false;
        isStoryEnd = false;

        StoryPanelAnimator.SetBool("IsOpen", true);

        Debug.Log("Start Conversation from " + story.name);

        endStoryEvent = story.endStoryEvent;

        storysentences.Clear();
        storyAudioClip.Clear();
        storyimages.Clear();

        foreach (string sentence in story.sentences)
        {
            storysentences.Enqueue(sentence);
        }

        foreach (Sprite image in story.ObjectImage)
        {
            storyimages.Enqueue(image);
        }

        foreach (AudioClip AudioClipsentence in story.StoryAudioClipsentences)
        {
            storyAudioClip.Enqueue(AudioClipsentence);
        }

        DisplayNextStory();

    }

    public void DisplayNextStory()
    {
        SkipStoryButton.SetActive(true);

        if(storysentences.Count == 0)
        {
            EndStory();
            return;
        }

        string sentence = storysentences.Dequeue();
        Sprite image = storyimages.Dequeue();
        AudioClip AudioClipsentence = storyAudioClip.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeStory(sentence, AudioClipsentence, image));
    }

    IEnumerator TypeStory(string sentence, AudioClip AudioClipsentence, Sprite image)
    {
        stopDialog = false;
        skipstoryDialog = false;
        isStoryEnd = false;
        StoryDialog.text = "";

        if (image != null)
        {
            ObjectImage.gameObject.SetActive(true);
            ObjectImage.sprite = image;
        }
        else
        {
            ObjectImage.gameObject.SetActive(false);
        }

        if (AudioClipsentence != null)
        {
            storyClipAudioSource.clip = AudioClipsentence;
        }

        foreach (char letter in sentence.ToCharArray())
        {
            if (skipstoryDialog)
            {
                StoryDialog.text += "";
                StopAllCoroutines();
                StoryDialog.text = sentence;
                storyClipAudioSource.Stop();
            }
            else
            {
                StoryDialog.text += letter;
                if (stopDialog)
                    storyClipAudioSource.Stop();
                else
                    storyClipAudioSource.Play();
                storyTypingAudioSource.Play();
                yield return new WaitForSeconds(0.075f);
                //Debug.Log(storyClipAudioSource.time);
                if (storyClipAudioSource.time == 0f)
                {
                    stopDialog = true;
                }
                storyClipAudioSource.Pause();
                storyTypingAudioSource.Pause();
            }
        }

        SkipStoryButton.SetActive(false);

        if(StoryDialog.text == sentence)
        {
            yield return new WaitForSeconds(1.5f);
            DisplayNextStory();
        }
    }

    void EndStory()
    {
        storyClipAudioSource.Stop();
        storyTypingAudioSource.Stop();

        StopAllCoroutines();

        SkipStoryButton.SetActive(false);

        isStoryEnd = true;

        StoryPanelAnimator.SetBool("IsOpen", false);

        endStoryEvent.Invoke();

        Debug.Log("End Of Conversation.");

        if (FindObjectOfType<PlayerControllers>() != null)
            FindObjectOfType<PlayerControllers>().OnEnable();
    }

    public void SkipStory()
    {
        storyTypingAudioSource.Pause();
        skipstoryDialog = true;
        SkipStoryButton.SetActive(false);
    }

    public void DisableStoryPanel()
    {
        StoryPanel.gameObject.SetActive(false);
    }

    public void SkipStoryDialog()
    {
        EndStory();
    }
}