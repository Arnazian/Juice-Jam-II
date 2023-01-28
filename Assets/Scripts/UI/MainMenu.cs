#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(UpdateVolume);
        musicSlider.onValueChanged.AddListener(UpdateVolume);
        sfxSlider.onValueChanged.AddListener(UpdateVolume);
        UpdateVolume(0f);
        LoadSettings();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    public void Play()
    {
        TransitionManager.Instance.FadeScene("Imated Scene");
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    private void UpdateVolume(float newVolume)
    {
        AudioListener.volume = masterSlider.value;
        musicMixerGroup.audioMixer.SetFloat("Volume", musicSlider.value);
        sfxMixerGroup.audioMixer.SetFloat("Volume", sfxSlider.value);
    }
    
    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("Master Volume", masterSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
        PlayerPrefs.SetFloat("SFX Volume", sfxSlider.value);
    }
    
    private void LoadSettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master Volume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", -15);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume", -15);
    }
}
