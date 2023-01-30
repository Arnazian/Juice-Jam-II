using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider bossHealthBar;
    public Slider GetBossHealthBar => bossHealthBar;

    [SerializeField] private Image rageMeterFill;
    public Image GetRageMeterFill => rageMeterFill;
}
