using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private float fadeTime;
    [SerializeField] private Image fadeScreen;

    public void FadeScene(string sceneName)
    {
        fadeScreen.DOFade(1f, fadeTime).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
            FadeOut();
        });
    }

    private void FadeOut()
    {
        fadeScreen.DOFade(0f, fadeTime);
    }
}
