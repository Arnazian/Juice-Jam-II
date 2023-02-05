using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class UiButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip[] hovers;
    [SerializeField] private AudioClip[] clicks;

    private float _defaultY;

    private void Awake()
    {
        _defaultY = transform.position.y;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var rand = Random.Range(0, 1);
        AudioManager.Instance.PlaySfx("uiHover", hovers[rand], -1, 1, false, false);
        transform.DOMoveY(_defaultY + 10, 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Instance.Stop("uiHover");
        transform.DOMoveY(_defaultY - 10, 0.5f).SetUpdate(true);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var rand = Random.Range(0, 1);
        //AudioManager.Instance.PlaySfx("uiClick", clicks[rand], -1, false, false);
    }
}
