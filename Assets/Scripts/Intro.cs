using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Image[] images;
    public AudioClip[] audios;
    public string nextSceneName;

    private AudioSource audioSource;
    private int currentIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        for (int i = 1; i < images.Length; i++)
        {
            SetImageAlpha(images[i], 0);
        }

        PlayNextAudio();
    }

    void PlayNextAudio()
    {
        if (currentIndex < audios.Length)
        {
            audioSource.clip = audios[currentIndex];
            audioSource.Play();
            StartCoroutine(FadeInImage(images[currentIndex]));
            StartCoroutine(WaitForClipToEnd(audioSource.clip.length));
        }
        else
        {
            LoadNextScene();
        }
    }

    IEnumerator WaitForClipToEnd(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        currentIndex++;
        PlayNextAudio();
    }

    IEnumerator FadeInImage(Image image)
    {
        float duration = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            SetImageAlpha(image, alpha);
            yield return null;
        }
        SetImageAlpha(image, 1);
    }

    void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
