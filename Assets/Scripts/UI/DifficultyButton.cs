using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private Sprite easy;
    [SerializeField] private Sprite medium;
    [SerializeField] private Sprite hard;
    
    private Image _image;
    private int _currentDifficulty = 1;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnClick()
    {
        _currentDifficulty++;
        if (_currentDifficulty > 3)
            _currentDifficulty = 1;
        DifficultyManager.Instance.SetDifficulty(_currentDifficulty);
        if (_currentDifficulty == 1)
            _image.sprite = easy;
        else if (_currentDifficulty == 2)
            _image.sprite = medium;
        else
            _image.sprite = hard;
    }
}
