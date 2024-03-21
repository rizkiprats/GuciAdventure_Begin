using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [Header("Dialog Panel")]
    public TMP_Text nameTextTMP;
    
    public TMP_Text dialogTextTMP;

    [Header("Dialog Dimmer Image")]
    public GameObject dimmer;

    [Header("Dialog Object Image")]
    public Image karakterImage;
    public Image ObjectImage;

    [Header("Dialog Animator")]
    public Animator animator;

    [Header("Dialog Audio")]
    public AudioSource dialogTypingAudioSource;
    public AudioSource dialogClipAudioSource;
    private Queue<string> sentences;
    private Queue<AudioClip> dialogAudioClips;
    private bool stopDialog;

    [Header("Dialog Panel Skip Button")]
    public GameObject Skipbutton;
    private bool skipDialog;

    [Header("Define That Dialog Has Ended (Automatic)")]
    public bool endDialog;

    private UnityEvent endDialogEvent;
    private bool addJournalEntry;
    private int dialogJurnalPoint;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogAudioClips = new Queue<AudioClip>();
        skipDialog = false;
    }

    public void StartDialog(Dialog dialog)
    { 
        dimmer.SetActive(true);

        skipDialog = false;

        animator.SetBool("IsOpen", true);

        Debug.Log("Starting Conversation from " + dialog.name);

        nameTextTMP.text = dialog.name;

        karakterImage.sprite = dialog.karakterImage;

        endDialogEvent = dialog.endDialogEvent;
        addJournalEntry = dialog.getJurnalEntry();
        dialogJurnalPoint = dialog.getJurnalPoint();

        if(dialog.ObjectImage != null)
        {
            ObjectImage.gameObject.SetActive(true);
            ObjectImage.sprite = dialog.ObjectImage;
        }
        else
        {
            ObjectImage.gameObject.SetActive(false);
        }

        sentences.Clear();
        dialogAudioClips.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (AudioClip AudioClipsentense in dialog.AudioClipSentences)
        {
            dialogAudioClips.Enqueue(AudioClipsentense);
        }

        DisplayNextSentence();

        endDialog = false;
    }

    public void DisplayNextSentence()
    {
        Skipbutton.SetActive(true);
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        AudioClip AudioClipsentence = dialogAudioClips.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, AudioClipsentence));

        endDialog = false;
    }

    IEnumerator TypeSentence(string sentence, AudioClip AudioClipsentence)
    {
        skipDialog = false;
        stopDialog = false;
        dialogTextTMP.text = "";

        if (AudioClipsentence != null)
        {
            dialogClipAudioSource.clip = AudioClipsentence;
        }

        foreach (char letter in sentence.ToCharArray())
        {
            if (skipDialog)
            {
                dialogTextTMP.text += "";
                dialogClipAudioSource.Stop();
                dialogTypingAudioSource.Stop();
                StopAllCoroutines();
                dialogTextTMP.text = sentence;
            }
            else
            {
                dialogTextTMP.text += letter;
                if (stopDialog)
                    dialogClipAudioSource.Stop();
                else
                    dialogClipAudioSource.Play();
                dialogTypingAudioSource.Play();
                yield return new WaitForSeconds(0.07f);
                //Debug.Log(dialogClipAudioSource.time);
                if (dialogClipAudioSource.time == 0f)
                {
                    stopDialog = true;
                }
                dialogClipAudioSource.Pause();
                dialogTypingAudioSource.Pause();
            }
        }

        if (dialogTextTMP.text == sentence)
        {
            stopDialog = true;
            dialogClipAudioSource.Stop();
            dialogTypingAudioSource.Stop();
            StopAllCoroutines();
        }

        Skipbutton.SetActive(false);
    }

    void EndDialog()
    {
       
        Skipbutton.SetActive(false); 

        dimmer.SetActive(false);

        animator.SetBool("IsOpen", false);

        endDialog = true;

        if (addJournalEntry)
        {
            FindObjectOfType<GameManager>().setJurnalEntry(dialogJurnalPoint);
        }

        endDialogEvent.Invoke();

        Debug.Log("End Of Conversation.");

        if (FindObjectOfType<PlayerControllers>() != null)
            FindObjectOfType<PlayerControllers>().OnEnable();

        Invoke("DisablendDialog", 1);
    }

    public void DisablendDialog()
    {
        endDialog = false;
    }

    public void SkipDialog()
    {
        skipDialog = true;
        Skipbutton.SetActive(false);
    }

    public void OnGamePause()
    {
        dialogClipAudioSource.Pause();
        dialogTypingAudioSource.Pause();
    }
}