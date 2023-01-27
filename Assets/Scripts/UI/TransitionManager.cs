using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private float fadeTime;
    [SerializeField] private Image fadeScreen;

    private bool _isFading;

    public void FadeScene(string sceneName)
    {
        if(_isFading)
            return;
        _isFading = true;
        fadeScreen.DOFade(1f, fadeTime).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
            FadeOut();
        });
    }

    private void FadeOut()
    {
        fadeScreen.DOFade(0f, fadeTime).OnComplete(() => _isFading = false);
    }
}
