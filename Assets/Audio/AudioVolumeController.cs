using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {

        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        audioSource.volume = value;
    }
}

