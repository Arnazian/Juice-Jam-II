using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
        musicSlider.onValueChanged.AddListener(UpdateVolume);
        sfxSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void Start()
    {
        UpdateVolume(0f);
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }
    
    private void UpdateVolume(float newVolume)
    {
        AudioListener.volume = volumeSlider.value;
        musicMixerGroup.audioMixer.SetFloat("Volume", musicSlider.value);
        sfxMixerGroup.audioMixer.SetFloat("Volume", sfxSlider.value);
    }
}
