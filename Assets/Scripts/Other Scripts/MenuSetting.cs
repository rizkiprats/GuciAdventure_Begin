using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuSetting : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer MusicAudioMixer;
    public AudioMixer SfxAudioMixer;
    public AudioMixer UIAudioMixer;

    [Header("Toggle")]
    public GameObject toogleMusicOff;
    public GameObject toogleMusicOn;
    public GameObject toogleAudioOff;
    public GameObject toogleAudioOn;

    [Header("Audio Source")]
    public AudioSource MusicAudio;

    private void Start()
    {
        float musicVolume;

        if(MusicAudioMixer.GetFloat("MusicVolume", out musicVolume ) == true)
        {
            if(musicVolume == -18)
            {
                if(toogleMusicOff != null)
                    toogleMusicOff.SetActive(true);
                if(toogleMusicOff != null)
                    toogleMusicOn.SetActive(false);
            }
            else
            {
                if (toogleMusicOff != null)
                    toogleMusicOff.SetActive(false);
                if(toogleMusicOn != null)
                    toogleMusicOn.SetActive(true);
            }
        }

        float audioVolume;
        float uiVolume;

        if (SfxAudioMixer.GetFloat("SfxVolume", out audioVolume) == true && UIAudioMixer.GetFloat("UIVolume", out uiVolume) == true)
        {
            if (audioVolume == 5 && uiVolume == -20)
            {
                if (toogleAudioOff != null)
                    toogleAudioOff.SetActive(true);
                if(toogleAudioOn != null)
                    toogleAudioOn.SetActive(false);
            }
            else
            {
                if (toogleAudioOff != null)
                    toogleAudioOff.SetActive(false);
                if(toogleAudioOn != null)
                    toogleAudioOn.SetActive(true);
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        MusicAudioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        SfxAudioMixer.SetFloat("SfxVolume", volume);
    }

    public void setSoundOff(bool choice)
    {
        if (choice == true)
        {
            SfxAudioMixer.SetFloat("SfxVolume", -80);
            UIAudioMixer.SetFloat("UIVolume", -80);
        }
        else
        {
            SfxAudioMixer.SetFloat("SfxVolume", 5);
            UIAudioMixer.SetFloat("UIVolume", -20);
        }
    }
    public void setMusicOff(bool choice)
    {
        if(choice == true)
        {
            MusicAudioMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            MusicAudioMixer.SetFloat("MusicVolume", -18);
        }
    }

    public void FadeMusicOff(float duration)
    {
        StartCoroutine(MenuSetting.StartFade(MusicAudio, duration, -80));
    }

    public void FadeMusicOff()
    {
        StartCoroutine(MenuSetting.StartFade(MusicAudio, 100, -80));
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}