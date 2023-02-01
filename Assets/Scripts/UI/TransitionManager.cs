using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private float fadeTime;
    private Animator _anim;

    private bool _isFading;

    private string _sceneName;
    
    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponent<Animator>();
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if(_anim)
                _anim.StopPlayback();
        };
    }

    public void FadeScene(string sceneName)
    {
        if(_isFading)
            return;
        _isFading = true;
        _sceneName = sceneName;
        
        _anim.Play("Fade In");
        Invoke(nameof(FadeOut), _anim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void FadeOut()
    {
        _anim.StopPlayback();
        SceneManager.LoadScene(_sceneName);
        _anim.Play("Fade Out");
        _isFading = false;
    }
    
    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
