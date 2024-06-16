using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public Button toggleButton;
    public AudioClip[] songs;
    public AudioClip[] comments;

    private AudioSource audioSource;
    private bool isRadioOn = false;
    private int consecutiveSongs = 0;
    private AudioClip lastSong;
    private AudioClip lastComment;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        toggleButton.onClick.AddListener(ToggleRadio);
    }

    void ToggleRadio()
    {
        isRadioOn = !isRadioOn;

        if (isRadioOn)
        {
            PlayRandomAudio();
        }
        else
        {
            audioSource.Stop();
            StopAllCoroutines();
        }
    }

    void PlayRandomAudio()
    {
        if (!isRadioOn) return;

        AudioClip clipToPlay = null;
        if (consecutiveSongs >= 3 || (consecutiveSongs < 3 && UnityEngine.Random.Range(0, 4) == 0))
        {
            clipToPlay = GetRandomComment();
            consecutiveSongs = 0;
        }
        else
        {
            clipToPlay = GetRandomSong();
            consecutiveSongs++;
        }

        audioSource.clip = clipToPlay;
        audioSource.Play();
        StartCoroutine(WaitForClipToEnd(audioSource.clip.length));
    }

    IEnumerator WaitForClipToEnd(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        PlayRandomAudio();
    }

    AudioClip GetRandomSong()
    {
        AudioClip selectedSong;
        do
        {
            selectedSong = songs[UnityEngine.Random.Range(0, songs.Length)];
        } while (selectedSong == lastSong);

        lastSong = selectedSong;
        return selectedSong;
    }

    AudioClip GetRandomComment()
    {
        AudioClip selectedComment;
        do
        {
            selectedComment = comments[UnityEngine.Random.Range(0, comments.Length)];
        } while (selectedComment == lastComment);

        lastComment = selectedComment;
        return selectedComment;
    }
}
