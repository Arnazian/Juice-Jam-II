#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] private GameObject container;
    
    public bool IsPaused { get; private set; }

    private void Start()
    {
        Play();
    }

    public void Pause()
    {
        IsPaused = true;
        container.SetActive(true);
        while(Time.timeScale != 0f)
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, 1f);
    }

    public void Play()
    {
        IsPaused = false;
        container.SetActive(false);
        while(Time.timeScale != 1f)
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 1f);
    }

    public void Menu()
    {
        Play();
        TransitionManager.Instance.FadeScene("Main Menu");
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(IsPaused)
                Play();
            else
                Pause();
        }
    }
}
