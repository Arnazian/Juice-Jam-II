using UnityEngine;
using UnityEngine.EventSystems;

public class UiButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip[] hovers;
    [SerializeField] private AudioClip[] clicks;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var rand = Random.Range(0, 1);
        AudioManager.Instance.PlaySfx("uiHover", hovers[rand], -1, false, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Instance.Stop("uiHover");
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var rand = Random.Range(0, 1);
        AudioManager.Instance.PlaySfx("uiClick", clicks[rand], -1, false, false);
    }
}
