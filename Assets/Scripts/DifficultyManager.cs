using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
    [SerializeField] private List<Transform> difficultyButtons;

    public Difficulty GetCurrentDifficulty => difficulty;
    [SerializeField] private Difficulty difficulty;

    protected override void Awake()
    {
        base.Awake();
        SelectDifficultyButton(0);
    }

    public void SetDifficulty(int d)
    {
        difficulty = (Difficulty) d;
    }

    public void SelectDifficultyButton(int btn)
    {
        foreach (var difficultyButton in difficultyButtons)
            difficultyButton.DOScale(Vector3.one, 0.5f);
        difficultyButtons[btn].DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.5f);
        SetDifficulty(btn);
    }
}
